using Domain.Entities;

public interface IPhongKhamRepository
{
    //CUD
    Task UpdateAsync(PhongKham entity);
    //Read
    Task<PhongKham?> GetByIdAsync(int id);
    Task<PhongKhamReadModel?> GetDetailAsync();
}