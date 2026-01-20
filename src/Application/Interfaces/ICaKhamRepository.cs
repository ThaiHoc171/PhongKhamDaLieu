using Domain.Entities;

namespace Application.Interfaces;
public interface ICaKhamRepository
{
    Task<CaKham?> GetByIdAsync(int caKhamID);

    Task<List<CaKham>> GetAllAsync();

    Task<List<CaKham>> GetCaKhamConTrongAsync();

    Task<List<CaKham>> GetByNgayAsync(DateTime ngayKham);

    Task<List<CaKham>> GetByBenhNhanAsync(int benhNhanID);

    Task<int> AddAsync(CaKham caKham);

    Task UpdateAsync(CaKham caKham);

    Task UpdateTrangThaiAsync(int caKhamID, string trangThai);
}

