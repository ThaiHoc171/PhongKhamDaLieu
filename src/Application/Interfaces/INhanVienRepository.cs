using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces
{
	public interface INhanVienRepository
	{
		Task<int> AddAsync(NhanVien nhanVien);
		Task<int> UpdateAsync(NhanVien nhanVien);
        Task<NhanVien?> GetByIdAsync(int nhanVienID);
		Task<NhanVienReadModel?> GetDetailAsync(int id);
		Task<(List<NhanVienReadListModel>, int)> GetPagedAsync(int pageNumber, int pageSize);
		Task<(List<NhanVienReadListModel>, int)> SearchAsync(string keyword, int pageNumber, int pageSize);
		Task<int> GetIdAsync(int taiKhoanId);
		Task<List<NameResponseDTO>> GetComboboxAsync(int chucVuId);
		Task<List<NameResponseDTO>> GetComboboxDoctorAsync();
	}
}
