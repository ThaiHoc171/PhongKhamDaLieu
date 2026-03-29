using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces;

public interface IKhungGioKhamRepository
{
    //-- CUD
    Task<int> AddAsync(KhungGioKham khungGio);
    Task<int> UpdateAsync(KhungGioKham entity);
    Task<int> DeleteAsync(int id);
    //-- READ
    Task<List<int>> ListKhungGioID();
	Task<KhungGioKham?> GetByIdAsync(int id);
    Task<List<KhungGioKhamReadModel>> GetAllAsync();
    Task<KhungGioKhamReadModel?> GetDetailAsync(int id);
    Task<List<int>> GetKhungGioIdsByCaLamViecAsync(int caLamViec);
    Task<List<NameResponseDTO>> GetComboboxAsync();
	Task<int> CountKhungGioKhamAsync();
}