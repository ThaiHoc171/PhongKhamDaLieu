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

	public CaKhamService(
		ICaKhamRepository caKhamRepo,
		ILichLamViecRepository lichLamViecRepo, 
		IKhungGioKhamRepository khungGioKhamRepo,
		INhanVienRepository nhanVienRepo,
        ITaiKhamRepository taiKhamRepo)
	{
		_caKhamRepo = caKhamRepo;
		_lichLamViecRepo = lichLamViecRepo;
        _khungGioKhamRepo = khungGioKhamRepo;
        _nhanVienRepo = nhanVienRepo;
        _taiKhamRepo = taiKhamRepo;
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
        if (caKham == null) return false;

        var taiKham = await _taiKhamRepo.GetByBenhNhanIdAsync(benhNhanID);

        if (caKham.LoaiCaKham == "Khám"
            && taiKham != null
            && taiKham.TrangThai == "Chờ xử lý")
        {
            taiKham.CapNhat("Đang xử lý", caKhamID);
            await _taiKhamRepo.UpdateAsync(taiKham);
        }

        caKham.DangKyKham(benhNhanID, lyDoKham, ngayDat, ghiChu);
        await _caKhamRepo.UpdateAsync(caKham);

        return true;
    }

    public async Task<bool> UpdateTrangThaiAsync(int caKhamID, string trangThai)
	{
		var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
		if (caKham == null) return false;

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