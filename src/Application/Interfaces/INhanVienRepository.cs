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
		Task<(List<NhanVien> Data, int TotalCount)> SearchAsync(string keyword, int pageNumber, int pageSize);
		Task<string?> GetNameByIdAsync(int id);
		Task<NhanVien> GetForAuthAsync(int TaiKhoanId);
		Task<List<(int Id, string Name)>> GetDropdownAsync(int chucVuId);
		Task<(List<NhanVien> Data, int TotalCount)> GetPageAsync(int pageNumber, int pageSize);
	}
}
