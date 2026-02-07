using Domain.Entities;

namespace Application.Interfaces;

public interface INgayNghiNhanVienRepository
{
	Task AddAsync(NgayNghiNhanVien entity);
	Task UpdateAsync(NgayNghiNhanVien entity);
	Task<NgayNghiNhanVien?> GetByIdAsync(int id);
	Task<List<NgayNghiNhanVien>> GetByNhanVienIdAsync(int nhanVienID);
	Task<bool> IsNgayNghiAsync(int nhanVienID, DateTime ngay);
}
