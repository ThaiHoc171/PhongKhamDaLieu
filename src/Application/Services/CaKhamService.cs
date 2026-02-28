using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class CaKhamService
{
	private readonly ICaKhamRepository _caKhamRepo;
	private readonly ILichLamViecRepository _lichLamViecRepo;
	private readonly IKhungGioKhamRepository _khungGioKhamRepo;
	private readonly INhanVienRepository _nhanVienRepo;
	private readonly ITaiKhamRepository _taiKhamRepo;
	private readonly ILieuTrinh_BuoiDieuTriRepository _lieuTrinh_BuoiDieuTriRepo;
    private readonly ILieuTrinhDieuTriRepository _lieuTrinhRepo;

    public CaKhamService(
		ICaKhamRepository caKhamRepo,
		ILichLamViecRepository lichLamViecRepo, 
		IKhungGioKhamRepository khungGioKhamRepo,
		INhanVienRepository nhanVienRepo,
        ITaiKhamRepository taiKhamRepo,
        ILieuTrinh_BuoiDieuTriRepository lieuTrinh_BuoiDieuTriRepo,
        ILieuTrinhDieuTriRepository lieuTrinhRepo)
	{
		_caKhamRepo = caKhamRepo;
		_lichLamViecRepo = lichLamViecRepo;
        _khungGioKhamRepo = khungGioKhamRepo;
        _nhanVienRepo = nhanVienRepo;
        _taiKhamRepo = taiKhamRepo;
		_lieuTrinh_BuoiDieuTriRepo = lieuTrinh_BuoiDieuTriRepo;
		_lieuTrinhRepo = lieuTrinhRepo;
    }
    public async Task<int> TaoCaKhamAsync(TaoCaKhamDTO dto)
    {
        if (dto.NgayKham.Date < DateTime.Today)
            throw new Exception("Ngày khám không hợp lệ");

        var danhSachLich = await _lichLamViecRepo.GetByWeekAsync(dto.NgayKham, dto.NgayKetThuc);

        if (!danhSachLich.Any())
            throw new Exception("Không có lịch làm việc trong ngày này");

        int tongCaDaTao = 0;

        foreach (var lich in danhSachLich)
        {
            var ngayHienTai = lich.Ngay.Date;

            var chucVuId = await _lichLamViecRepo
                .GetChucVuIdByLichLamViecIdAsync(lich.LichLamViecID);

            if (chucVuId != 1 && chucVuId != 2) continue;

            int? ID = await _nhanVienRepo
                .GetPhongChucNangIdByNhanVienIdAsync(lich.NhanVienID);

            if (ID == null)
                throw new Exception("Nhân viên không tồn tại!");

            int phongChucNangID = ID.Value;

            string loaiCaKham =
                phongChucNangID == 1 ? "Khám" :
                phongChucNangID == 2 ? "Điều trị" : null;

            if (loaiCaKham == null) continue;

            int maxCa = loaiCaKham == "Điều trị" ? 1 : 5;

            var khungGioIds =
                await _khungGioKhamRepo.GetKhungGioIdsByCaLamViecAsync(lich.CaLamViec);

            foreach (var khungGioId in khungGioIds)
            {
                if (await _caKhamRepo.ExistsAsync(ngayHienTai, khungGioId, loaiCaKham))
                    continue;

                var soCaHienTai =
                    await _caKhamRepo.CountByNgayAndKhungGioAsync(
                        ngayHienTai, khungGioId, loaiCaKham);

                if (soCaHienTai >= maxCa) continue;

                int soCanTao = maxCa - soCaHienTai;

                for (int i = 0; i < soCanTao; i++)
                {
                    await _caKhamRepo.AddAsync(new CaKham(
                        loaiCaKham: loaiCaKham,
                        lichLamViecID: lich.LichLamViecID,
                        phongChucNangID: phongChucNangID,
                        ngayKham: ngayHienTai,
                        khungGioID: khungGioId,
                        trangThai: "Trống"
                    ));

                    tongCaDaTao++;
                }
            }
        }


        return tongCaDaTao;
    }
    public async Task<bool> DangKyKhamAsync(int caKhamID, DangKyCaKhamDTO dto)
    {
        var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
        if (caKham == null)
            throw new Exception("Ca khám không tồn tại");
       
        if (caKham.BenhNhanID != null || caKham.TrangThai != "Trống")
            throw new Exception("Ca khám không khả dụng để đăng ký");

        var lich = await _lichLamViecRepo.GetByIdAsync(caKham.LichLamViecID);
        var taiKham = await _taiKhamRepo.GetByBenhNhanIdAsync(dto.BenhNhanID);

        if (caKham.LoaiCaKham == "Khám")
        {
            if (taiKham != null && taiKham.TrangThai == "Chờ xử lý")
            {
                taiKham.CapNhat("Đang xử lý", caKhamID);
                await _taiKhamRepo.UpdateAsync(taiKham);
            }
        }

        if (caKham.LoaiCaKham == "Điều trị")
        {
            var lieuTrinh = await _lieuTrinhRepo.GetByBenhNhanIdAsync(dto.BenhNhanID);
            if (lieuTrinh == null)
                throw new Exception("Bệnh nhân không có liệu trình điều trị");

            if (lieuTrinh.TrangThai != "Đang điều trị")
                throw new Exception("Liệu trình không ở trạng thái điều trị");
            var buoiGanNhat = await _lieuTrinh_BuoiDieuTriRepo.GetBuoiGanNhatAsync(lieuTrinh.LieuTrinhID);

            if (buoiGanNhat != null)
            {
                if (!buoiGanNhat.NgayThucHien.HasValue)
                    throw new Exception("Buổi điều trị trước chưa hoàn thành");

                if (caKham.NgayKham < buoiGanNhat.NgayThucHien.Value.AddDays(7))
                    throw new Exception(
                        "Khoảng cách giữa các buổi điều trị phải tối thiểu 7 ngày"
                    );
            }

            var buoidieutri = await _lieuTrinh_BuoiDieuTriRepo.GetByLieuTrinhAsync(lieuTrinh.LieuTrinhID);
            foreach (var dt in buoidieutri)
            {
                if(dt.TrangThai=="Chờ xử lý")
                    throw new Exception("Bệnh nhân còn ca điều trị chưa xử lý xong, không thể đăng ký!");
            }
            int soBuoi = await _lieuTrinh_BuoiDieuTriRepo.CountBySoBuoiAsync(lieuTrinh.LieuTrinhID) + 1;

            if (soBuoi > lieuTrinh.TongSoBuoi)
                throw new Exception("Liệu trình đã đủ số buổi");

            DateTime ngayDuKien =
                lieuTrinh.NgayBatDau.AddDays((soBuoi - 1) * 7);

            caKham.DangKyKham(dto.BenhNhanID, dto.LyDoKham, dto.NgayDat, dto.GhiChu);
            await _caKhamRepo.UpdateAsync(caKham);

            var buoi = new LieuTrinh_BuoiDieuTri(
                lieuTrinhID: lieuTrinh.LieuTrinhID,
                caKhamID: caKhamID,
                soBuoi: soBuoi,
                ngayDuKien: ngayDuKien,
                ngayThucHien: caKham.NgayKham,
                nhanVienID: lich.NhanVienID
            );

            await _lieuTrinh_BuoiDieuTriRepo.AddAsync(buoi);
            return true;
        }

        caKham.DangKyKham(dto.BenhNhanID, dto.LyDoKham, dto.NgayDat, dto.GhiChu);
        await _caKhamRepo.UpdateAsync(caKham);
        return true;
    }

    public async Task<bool> UpdateTrangThaiAsync(int caKhamID, string trangThai)
	{
		var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
		if (caKham == null) return false;
        if (caKham.TrangThai == "Hoàn thành")
            throw new Exception("Không thể thay đổi ca đã hoàn thành");
        caKham.CapNhatTrangThai(trangThai);
		await _caKhamRepo.UpdateAsync(caKham);
		return true;
	}
	public async Task<CaKhamResponseDTO?> LayCaKhamTheoIdAsync(int caKhamId)
	{
        var ca = await _caKhamRepo.GetByIdAsync(caKhamId);
        if (ca == null) return null;

        return MapToDto(ca);
	}
	public async Task<List<CaKhamResponseDTO>> DanhSachCaKhamTheoNgayAsync(DateTime ngay, string trangThai)
	{
		var list = await _caKhamRepo.LocAsync(ngay, trangThai);

        return list.Select(MapToDto).ToList();
	}
    public async Task<List<int>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham)
    {
        return await _caKhamRepo.GetKhungGioConTrongAsync(ngayKham, loaiCaKham);
    }
    public async Task<int> GetCaKhamAsync(DateTime ngayKham, int khungGioId, string loaiCaKham)
    {
        return await _caKhamRepo.GetCaKhamAsync(ngayKham, khungGioId, loaiCaKham);
    }
    public async Task<string?> CheckBenhNhanDaDangKyAsync(DateTime ngay, int khungGioId, string loaiCaKham, int benhNhanId)
    {
        var daDangKy = await _caKhamRepo.CheckBenhNhanDaDangKyAsync(
        ngay, khungGioId, loaiCaKham, benhNhanId);

        if (daDangKy)
        {
            return "Bệnh nhân đã đăng ký ca khám vào khung giờ này";
        }

        return null;
    }
    public async Task<List<CaKhamResponseDTO>> GetByBenhNhanAsync(int benhNhanID)
	{
        var list = await _caKhamRepo.GetByBenhNhanAsync(benhNhanID);

        return list.Select(MapToDto).ToList();
    }
	public async Task<List<CaKhamResponseDTO>> GetAllAsync()
	{
        var list = await _caKhamRepo.GetAllAsync();

        return list.Select(MapToDto).ToList();
    }
    private static CaKhamResponseDTO MapToDto(CaKham ca)
    {
        return new CaKhamResponseDTO
        {
            CaKhamID = ca.CaKhamID,
            LoaiCaKham = ca.LoaiCaKham,
            LichLamViecID = ca.LichLamViecID,
            KhungGioID = ca.KhungGioID,
            PhongChucNangID = ca.PhongChucNangID,
            BenhNhanID = ca.BenhNhanID,
            LyDoKham = ca.LyDoKham,
            TrangThai = ca.TrangThai,
            NgayDat = ca.NgayDat,
            NgayKham = ca.NgayKham,
            GhiChu = ca.GhiChu
        };
    }
}