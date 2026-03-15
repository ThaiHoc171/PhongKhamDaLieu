using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces
{
	public interface INhanVienRepository
	{
		Task AddAsync(NhanVien nhanVien);
		Task UpdateAsync(NhanVien nhanVien);
        Task<NhanVien?> GetByIdAsync(int nhanVienID);
		Task<NhanVienDetailReadModel?> GetDetailAsync(int id);
		Task<(List<NhanVienListReadModel>, int)> GetPageAsync(int pageNumber, int pageSize);
		Task<(List<NhanVienListReadModel>, int)> SearchAsync(string keyword, int pageNumber, int pageSize);
		Task<int> GetIdAsync(int taiKhoanId);
		Task<List<NameResponseDTO>> GetComboboxAsync(int chucVuId);
	}
}
