using Application.DTOs;
using Domain.Entities;
namespace Application.Interfaces
{
    public interface IHoSoBenhAnRepository
    {
        //CUD
        Task AddAsync(HoSoBenhAn hoSoBenhAn);
        Task UpdateAsync(HoSoBenhAn hoSoBenhAn);
        //Read
        Task<(List<HoSoBenhAnListReadModel>, int)> GetPagedAsync(int page, int size);
        Task<(List<HoSoBenhAnListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size);
        Task<HoSoBenhAnReadModel?> GetDetailAsync(int id);
        Task<HoSoBenhAn?> GetByIdAsync(int id);
        Task<HoSoBenhAnReadModel?> GetByBenhNhanIdAsync(int benhNhanId);

    }
}