using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class NgayNghiNhanVienService
{
	private readonly INgayNghiNhanVienRepository _repo;

	public NgayNghiNhanVienService(INgayNghiNhanVienRepository repo)
	{
		_repo = repo;
	}

	public async Task ThemNgayNghiAsync(NgayNghiRequestDTO dto)
	{
		if (await _repo.IsNgayNghiAsync(dto.NhanVienID, dto.Ngay))
			throw new InvalidOperationException("Nhân viên đã có ngày nghỉ trong ngày này.");

		var entity = new NgayNghiNhanVien(
			dto.NhanVienID,
			dto.Ngay,
			dto.LyDo
		);

		await _repo.AddAsync(entity);
	}

	public async Task<List<NgayNghiResponseDTO>> GetByNhanVienAsync(int nhanVienID)
	{
		var list = await _repo.GetByNhanVienIdAsync(nhanVienID);

		return list.Select(e => new NgayNghiResponseDTO
		{
			NgayNghiID = e.NgayNghiID,
			NhanVienID = e.NhanVienID,
			Ngay = e.Ngay,
			LyDo = e.LyDo
		}).ToList();
	}
	public async Task<bool> CapNhatNgayNghiAsync(int id, string? lyDo)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return false;

		entity.CapNhatLyDo(lyDo);
		await _repo.UpdateAsync(entity);

		return true;
	}
	public async Task<List<NgayNghiResponseDTO>> GetByMonthAsync(int thang, int nam)
	{
		if (thang < 1 || thang > 12)
			throw new ArgumentException("Tháng không hợp lệ.");

		if (nam < 2000)
			throw new ArgumentException("Năm không hợp lệ.");
		var list = await _repo.GetByMonthAsync(thang, nam);

		return list.Select(e => new NgayNghiResponseDTO
		{
			NgayNghiID = e.NgayNghiID,
			NhanVienID = e.NhanVienID,
			Ngay = e.Ngay,
			LyDo = e.LyDo
		}).ToList();
	}
}
