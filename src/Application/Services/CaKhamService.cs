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
    private readonly IBenhNhanRepository _benhNhanRepo;
    public CaKhamService(
		ICaKhamRepository caKhamRepo,
		ILichLamViecRepository lichLamViecRepo, 
		IKhungGioKhamRepository khungGioKhamRepo,
		INhanVienRepository nhanVienRepo,
        ITaiKhamRepository taiKhamRepo,
        ILieuTrinh_BuoiDieuTriRepository lieuTrinh_BuoiDieuTriRepo,
        ILieuTrinhDieuTriRepository lieuTrinhRepo,
        IBenhNhanRepository benhNhanRepo)
	{
		_caKhamRepo = caKhamRepo;
		_lichLamViecRepo = lichLamViecRepo;
        _khungGioKhamRepo = khungGioKhamRepo;
        _nhanVienRepo = nhanVienRepo;
        _taiKhamRepo = taiKhamRepo;
		_lieuTrinh_BuoiDieuTriRepo = lieuTrinh_BuoiDieuTriRepo;
		_lieuTrinhRepo = lieuTrinhRepo;
        _benhNhanRepo = benhNhanRepo;
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
            var nv = await _nhanVienRepo.GetByIdAsync(lich.NhanVien.Id);
            if (nv == null)
                throw new Exception("Nhân viên không tồn tại!");
			if (nv.PhongChucNangID == null)
				throw new Exception("Nhân viên chưa được gán phòng chức năng");
			int phongChucNangID = nv.PhongChucNangID;
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
        if (caKham.ThongTinID != null || caKham.TrangThai != "Trống")
            throw new Exception("Ca khám không khả dụng để đăng ký");
        var lich = await _lichLamViecRepo.GetByIdAsync(caKham.LichLamViecID);
        if (lich == null)
            throw new Exception("Không tìm thấy lịch làm việc");
        var bn = await _benhNhanRepo.GetDetailAsync(dto.ThongTinID);
        if (bn == null)
            throw new Exception("Không tìm thấy bệnh nhân");
        var taiKham = await _taiKhamRepo.GetByBenhNhanIdAsync(bn.BenhNhanID);
        if (caKham.LoaiCaKham == "Khám")
        {
            if (taiKham != null && taiKham.TrangThai == "Chờ xử lý")
            {
                taiKham.CapNhat("Đang xử lý", caKhamID);
                await _taiKhamRepo.UpdateAsync(taiKham);
            }
        }
        caKham.DangKyKham(dto.ThongTinID, dto.LyDoKham, dto.NgayDat, dto.GhiChu);
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
	public async Task<CaKhamReadModel?> LayCaKhamTheoIdAsync(int caKhamId)
       => await _caKhamRepo.GetCaKhamDetailAsync(caKhamId);
    public async Task<List<int>> GetKhungGioConTrongAsync(DateTime ngayKham, string loaiCaKham)
    {
        return await _caKhamRepo.GetKhungGioConTrongAsync(ngayKham, loaiCaKham);
    }
    public async Task<int> GetCaKhamAsync(DateTime ngayKham, int khungGioId, string loaiCaKham)
    {
        return await _caKhamRepo.GetCaKhamAsync(ngayKham, khungGioId, loaiCaKham);
    }
    public async Task<bool> CheckBenhNhanDaDangKyAsync(DateTime ngay, int khungGioId, string loaiCaKham, int benhNhanId)
    {
        var daDangKy = await _caKhamRepo.CheckThongTinDaDangKyAsync(ngay, khungGioId, loaiCaKham, benhNhanId);
        return daDangKy;
    }
	public async Task<PagedResult<CaKhamListReadModel>> GetByBenhNhanAsync(int thongTinID, int pageNumber, int pageSize)
	{
		var (data, total) = await _caKhamRepo.GetByThongTinAsync(thongTinID, pageNumber, pageSize);
		return new PagedResult<CaKhamListReadModel>
		{
			Items = data,
			TotalCount = total,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
	public async Task<PagedResult<CaKhamListReadModel>> GetCaKhamPagedAsync(
        DateTime ngayKham, string trangThai, string loaiCaKham, int pageNumber, int pageSize)
	{
		var (data, totalCount) = await _caKhamRepo.GetCaKhamsAsync(
			ngayKham, trangThai, loaiCaKham, pageNumber, pageSize);
		return new PagedResult<CaKhamListReadModel>
		{
			Items = data,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
	public async Task<List<NameResponseDTO>>GetComboboxAsync(string trangThai, DateTime ngayKham)
	{
		var data = await _caKhamRepo.GetIdAndNameByStatusAsync(trangThai, ngayKham);
		return data.Select(x => new NameResponseDTO
		{
			Id = x.Id,
			Name = x.Ten
		}).ToList();
	}
}