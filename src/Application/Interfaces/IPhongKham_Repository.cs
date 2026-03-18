using Domain.Entities;

public interface IPhongKhamRepository
{
    Task<int> AddAsync(PhongKham entity);
    Task UpdateAsync(PhongKham entity);

    Task<PhongKham?> GetByIdAsync(int id);
    Task<PhongKhamReadModel?> GetDetailAsync(int id);
    Task<(List<PhongKhamListReadModel>, int)> GetPagedAsync(int page, int size);
}