namespace Application.Interfaces;
public interface IChucVuQuyenRepository
{
	Task<List<int>> GetByChucVuAsync(int chucVuId);
	Task<List<string>> GetNameByChucVuAsync(int chucVuId);
	Task AddAsync(int chucVuId, int quyenId);
	Task DeleteAsync(int chucVuId, int quyenId);
	Task DeleteAllAsync(int chucVuId);
}