using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IKhungGioKhamRepository
{
    //-- CUD
    Task<int> AddAsync(KhungGioKham khungGio);
    Task UpdateAsync(KhungGioKham entity);
    Task DeleteAsync(int id);
    //-- READ
    Task<KhungGioKham?> GetByIdAsync(int id);
    Task<List<KhungGioKhamListReadModel>> GetAllAsync();
    Task<KhungGioKhamReadModel?> GetDetailAsync(int id);
    Task<List<int>> GetKhungGioIdsByCaLamViecAsync(int caLamViec);
    Task<List<(int Id, string Ten)>> GetIdAndNameAsync();
    Task<int> CountKhungGioKhamAsync();
}