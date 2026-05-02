	using Application.DTOs;
	using Domain.Entities;
	namespace Application.Interfaces;
	public interface INgayNghiNhanVienRepository
	{
		Task AddAsync(NgayNghiNhanVien entity);
		Task UpdateAsync(NgayNghiNhanVien entity);
		Task DeleteAsync(int id);
		Task BulkInsertAsync(List<NgayNghiNhanVien> list);
		Task<NgayNghiNhanVien?> GetByIdAsync(int id);
		Task<NgayNghiReadModel?> GetDetailAsync(int id);
		Task<(List<NgayNghiReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
		Task<(List<NgayNghiReadModel>, int)> GetPagedAsync(int page, int size);
		Task<bool> ExistsAsync(int nhanVienID, DateTime ngay);
	}
