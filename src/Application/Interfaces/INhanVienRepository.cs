using Domain.Entities;

namespace Application.Interfaces
{
	public interface INhanVienRepository
	{
		Task AddAsync(NhanVien nhanVien);
		Task UpdateAsync(NhanVien nhanVien);
        Task<int?> GetPhongChucNangIdByNhanVienIdAsync(int nhanVienId);
        Task<NhanVien?> GetByIdAsync(int nhanVienID);
		Task<List<NhanVien>> GetAllAsync();
		Task<List<NhanVien>> SearchAsync(string keyword);
	}
}
