using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class ThuocService
{
    private readonly IThuocRepository _repo;

    public ThuocService(IThuocRepository repo)
    {
        _repo = repo;
    }

    public async Task<PagedResult<ThuocListReadModel>> DanhSachAsync(int page, int size)
    {
        var items = await _repo.GetPagedAsync(page, size);
        var total = await _repo.CountAsync();

        return new PagedResult<ThuocListReadModel>
        {
            Items = items,
            TotalCount = total,
            PageNumber = page,
            PageSize = size
        };
    }

    public async Task<List<ThuocListReadModel>> TimKiemAsync(string keyword)
    {
        return await _repo.SearchAsync(keyword);
    }

    public async Task<List<ThuocComboboxReadModel>> ComboboxAsync()
    {
        return await _repo.GetComboboxAsync();
    }

    public async Task<Thuoc?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task ThemAsync(ThuocRequestDTO dto)
    {
        var entity = new Thuoc(dto.TenThuoc, dto.HoatChat);

        var ds = await _repo.GetAllAsync();
        entity.KiemTraTrungTen(ds);

        await _repo.AddAsync(entity);
    }

    public async Task<bool> CapNhatAsync(int id, ThuocRequestDTO dto)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return false;

        entity.CapNhat(dto.TenThuoc, dto.HoatChat);

        var ds = await _repo.GetAllAsync();
        entity.KiemTraTrungTen(ds);

        await _repo.UpdateAsync(entity);

        return true;
    }
}