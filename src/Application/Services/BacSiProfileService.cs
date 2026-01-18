using Application.DTO;
using Application.Repository;
using Domain.Entities;

namespace Application.Services;

public class BacSiProfileService
{
	private readonly IBacSiProfileRepository _repository;

	public BacSiProfileService(IBacSiProfileRepository repository)
	{
		_repository = repository;
	}

	public async Task<BacSiProfileDTO?> GetByNhanVienAsync(int nhanVienID)
	{
		var entity = await _repository.GetByNhanVienIdAsync(nhanVienID);
		if (entity == null) return null;

		return new BacSiProfileDTO
		{
			BacSiProfileID = entity.BacSiProfileID,
			NhanVienID = entity.NhanVienID,
			GioiThieu = entity.GioiThieu,
			ChuyenMon = entity.ChuyenMon,
			ThanhTuu = entity.ThanhTuu,
			HinhAnh = entity.HinhAnh,
			KinhNghiem = entity.KinhNghiem,
			NgayCapNhat = entity.NgayCapNhat
		};
	}

	// 🔹 TẠO MỚI
	public async Task TaoMoiAsync(int nhanVienID, BacSiProfileRequestDTO dto)
	{
		var existed = await _repository.GetByNhanVienIdAsync(nhanVienID);
		if (existed != null)
			throw new InvalidOperationException("Bác sĩ đã có profile");

		var profile = new BacSiProfile(
			nhanVienID,
			dto.GioiThieu,
			dto.ChuyenMon,
			dto.ThanhTuu,
			dto.HinhAnh,
			dto.KinhNghiem
		);

		await _repository.AddAsync(profile);
	}

	// 🔹 CẬP NHẬT
	public async Task CapNhatAsync(int nhanVienID, BacSiProfileRequestDTO dto)
	{
		var entity = await _repository.GetByNhanVienIdAsync(nhanVienID);
		if (entity == null)
			throw new InvalidOperationException("Không tìm thấy profile bác sĩ");

		entity.CapNhat(
			dto.GioiThieu,
			dto.ChuyenMon,
			dto.ThanhTuu,
			dto.HinhAnh,
			dto.KinhNghiem
		);

		await _repository.UpdateAsync(entity);
	}
}
