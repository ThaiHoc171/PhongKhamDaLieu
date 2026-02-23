using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class NhanVienService
{
	private readonly INhanVienRepository _repo;
	private readonly ThongTinCaNhanService _thongTinService;

	public NhanVienService(
		INhanVienRepository repo,
		ThongTinCaNhanService thongTinService)
	{
		_repo = repo;
		_thongTinService = thongTinService;
	}

	public async Task TaoNhanVienAsync(TaoNhanVienDTO dto)
	{
		// 1. Tạo thông tin cá nhân
		var thongTinID = await _thongTinService.TaoNhanVienAsync(dto.ThongTin);

		// 2. Tạo entity NhanVien
		var nv = new NhanVien(
			thongTinID: thongTinID,
			chucVuID: dto.ChucVuID,
			phongChucNangID: dto.PhongChucNangID,
			ngayVaoLam: dto.NgayVaoLam,
			bangCap: dto.BangCap,
			kinhNghiem: dto.KinhNghiem
		);

		await _repo.AddAsync(nv);
	}

	public async Task<bool> CapNhatNhanVienAsync(int nhanVienID, CapNhatNhanVienDTO dto)
	{
		var nv = await _repo.GetByIdAsync(nhanVienID);
		if (nv == null) return false;

		nv.CapNhatThongTin(
			chucVuID: dto.ChucVuID,
			phongChucNangID: dto.PhongChucNangID,
			ngayVaoLam: dto.NgayVaoLam,
			bangCap: dto.BangCap,
			kinhNghiem: dto.KinhNghiem
		);

		await _repo.UpdateAsync(nv);
		return true;
	}
	public async Task<bool> CapNhatTrangThaiAsync(int nhanVienID, string trangThai)
	{
		var nv = await _repo.GetByIdAsync(nhanVienID);
		if (nv == null) return false;

		nv.CapNhatTrangThai(trangThai);
		await _repo.UpdateAsync(nv);
		return true;
	}

	public async Task<List<NhanVienResponseDTO>> LayDanhSachAsync()
	{
		var list = await _repo.GetAllAsync();
		return list.Select(MapToResponse).ToList();
	}
	public async Task<PagedResult<NhanVienResponseDTO>> DanhSachNhanVienPagedAsync(int pageNumber, int pageSize)
	{
		var (data, totalCount) = await _repo.GetPageAsync(pageNumber, pageSize);

		return new PagedResult<NhanVienResponseDTO>
		{
			Items = data.Select(MapToResponse).ToList(),
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
	public async Task<NhanVienResponseDTO?> LayTheoIDAsync(int nhanVienID)
	{
		var nv = await _repo.GetByIdAsync(nhanVienID);
		if (nv == null) return null;

		return MapToResponse(nv);
	}
	public async Task<List<NhanVienResponseDTO>> SearchAsync(string keyword)
	{
		var list = await _repo.SearchAsync(keyword);
		return list.Select(MapToResponse).ToList();
	}

	private static NhanVienResponseDTO MapToResponse(NhanVien nv)
	{
		return new NhanVienResponseDTO
		{
			NhanVienID = nv.NhanVienID,
			HoTen = nv.ThongTinCaNhan?.HoTen,
			Email = nv.ThongTinCaNhan?.EmailLienHe,
			TenChucVu = nv.TenChucVu,
			TrangThai = nv.TrangThai
		};
	}
}
