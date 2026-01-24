using Domain.Entities;

namespace Application.Interfaces;

public interface IHoSoBenhAnRepository
{
    Task<List<HoSoBenhAn>> GetAllAsync();
    Task<HoSoBenhAn?> GetByIdAsync(int hoSoBenhAnID);
    
    Task<HoSoBenhAn?> GetByBenhNhanIdAsync(int benhNhanID);
    Task<int> AddAsync(HoSoBenhAn hoSoBenhAn);
    Task UpdateAsync(HoSoBenhAn hoSoBenhAn);
}

