	using Application.DTOs;
	using Domain.Entities;

	namespace Application.Interfaces;

	public interface INgayNghiNhanVienRepository
	{
		Task AddAsync(NgayNghiNhanVien entity);
		Task UpdateAsync(NgayNghiNhanVien entity);
		Task<NgayNghiNhanVien?> GetByIdAsync(int id);
		Task<NgayNghiReadModel?> GetDetailAsync(int id);
		Task<List<NgayNghiReadModel>> GetByNhanVienIdAsync(int nhanVienID);
		Task<List<NgayNghiReadModel>> GetByMonthAsync(int thang, int nam);
		Task<bool> IsNgayNghiAsync(int nhanVienID, DateTime ngay);
	}
