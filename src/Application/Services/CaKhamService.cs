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

		var danhSachLich = await _lichLamViecRepo.GetByNgayAsync(dto.NgayKham);

		if (!danhSachLich.Any())
			throw new Exception("Không có lịch làm việc trong ngày này");

		int tongCaDaTao = 0;

		foreach (var lich in danhSachLich)
		{
			var chucVuId = await _lichLamViecRepo
				.GetChucVuIdByLichLamViecIdAsync(lich.LichLamViecID);

            if (chucVuId != 1 && chucVuId != 2) continue;
            var LoaiCaKham = "";
			int? ID = await _nhanVienRepo.GetPhongChucNangIdByNhanVienIdAsync(lich.NhanVienID);
			if (ID == null)
				throw new Exception("Nhân viên không tồn tại!");
			int PhongChucNangID = ID.Value;

			if (PhongChucNangID == 1)
                LoaiCaKham = "Khám";
			else if (PhongChucNangID == 2)
                LoaiCaKham = "Điều trị";
			else
				continue;
			int MaxCa = LoaiCaKham == "Điều trị" ? 1 : 5;
            var khungGioIds = await _khungGioKhamRepo.GetKhungGioIdsByCaLamViecAsync(lich.CaLamViec);

            foreach (var khungGioId in khungGioIds)
            {
				if (await _caKhamRepo.ExistsAsync(dto.NgayKham, khungGioId, LoaiCaKham))
					continue;

				var soCaHienTai = await _caKhamRepo
					.CountByNgayAndKhungGioAsync(dto.NgayKham, khungGioId, LoaiCaKham);

				if (soCaHienTai >= MaxCa) continue;

				int soCanTao = MaxCa - soCaHienTai;

				for (int i = 0; i < soCanTao; i++)
				{
					var ca = new CaKham(
						loaiCaKham: LoaiCaKham,
						lichLamViecID: lich.LichLamViecID,
						phongChucNangID: PhongChucNangID,
						ngayKham: dto.NgayKham,
						khungGioID: khungGioId,
						trangThai: "Trống"
					);

					await _caKhamRepo.AddAsync(ca);
					tongCaDaTao++;
				}
			}
		}

		return tongCaDaTao;
	}
    public async Task<bool> DangKyKhamAsync(
    int caKhamID,
    int benhNhanID,
    string lyDoKham,
    DateTime ngayDat,
    string? ghiChu)
    {
        var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
        if (caKham == null)
            throw new Exception("Ca khám không tồn tại");

        if (caKham.BenhNhanID != null || caKham.TrangThai != "Trống")
            throw new Exception("Ca khám không khả dụng để đăng ký");

        // ===== KHÁM / TÁI KHÁM =====
        var taiKham = await _taiKhamRepo.GetByBenhNhanIdAsync(benhNhanID);

        if (caKham.LoaiCaKham == "Khám")
        {
            if (taiKham != null && taiKham.TrangThai == "Chờ xử lý")
            {
                taiKham.CapNhat("Đang xử lý", caKhamID);
                await _taiKhamRepo.UpdateAsync(taiKham);
            }
        }

        // ===== ĐIỀU TRỊ THEO LIỆU TRÌNH =====
        if (caKham.LoaiCaKham == "Điều trị")
        {
            var lieuTrinh = await _lieuTrinhRepo.GetByBenhNhanIdAsync(benhNhanID);
            if (lieuTrinh == null)
                throw new Exception("Bệnh nhân không có liệu trình điều trị");

            if (lieuTrinh.TrangThai != "Đang điều trị")
                throw new Exception("Liệu trình không ở trạng thái điều trị");

            int soBuoi =
                await _lieuTrinh_BuoiDieuTriRepo
                    .CountBySoBuoiAsync(lieuTrinh.LieuTrinhID) + 1;

            if (soBuoi > lieuTrinh.TongSoBuoi)
                throw new Exception("Liệu trình đã đủ số buổi");

            DateTime ngayDuKien =
                lieuTrinh.NgayBatDau.AddDays((soBuoi - 1) * 7);

            // 1️ Gán bệnh nhân cho ca khám
            caKham.DangKyKham(benhNhanID, lyDoKham, ngayDat, ghiChu);
            await _caKhamRepo.UpdateAsync(caKham);

            // 2️ Tạo buổi điều trị
            var buoi = new LieuTrinh_BuoiDieuTri(
                lieuTrinh.LieuTrinhID,
                caKhamID,
                soBuoi,
                ngayDuKien,
                caKham.NgayKham
            );

            await _lieuTrinh_BuoiDieuTriRepo.AddAsync(buoi);
            return true;
        }

        // ===== MẶC ĐỊNH =====
        caKham.DangKyKham(benhNhanID, lyDoKham, ngayDat, ghiChu);
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
	public async Task<CaKham?> LayCaKhamTheoIdAsync(int caKhamId)
	{
		return await _caKhamRepo.GetByIdAsync(caKhamId);
	}
	public async Task<List<CaKham>> DanhSachCaKhamTheoNgayAsync(DateTime ngay, string trangThai)
	{
		return await _caKhamRepo.LocAsync(ngay, trangThai);
	}
	public async Task<List<CaKham>> GetByBenhNhanAsync(int benhNhanID)
	{
		return await _caKhamRepo.GetByBenhNhanAsync(benhNhanID);
	}
	public async Task<List<CaKham>> GetAllAsync()
	{
		return await _caKhamRepo.GetAllAsync();
	}

}