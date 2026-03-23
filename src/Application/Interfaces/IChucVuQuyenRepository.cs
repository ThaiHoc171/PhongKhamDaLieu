namespace Application.Interfaces;
public interface IChucVuQuyenRepository
{
	Task<List<int>> GetByChucVuAsync(int chucVuId);
	Task<List<string>> GetNameByChucVuAsync(int chucVuId);
	Task AddRangeAsync(int chucVuId, IEnumerable<int> quyenIds);
	Task DeleteRangeAsync(int chucVuId, IEnumerable<int> quyenIds);
}