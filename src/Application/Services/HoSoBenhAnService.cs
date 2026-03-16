using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class HoSoBenhAnService
{
    private readonly IHoSoBenhAnRepository _repo;
    public HoSoBenhAnService(IHoSoBenhAnRepository repo)
    {
        _repo = repo;
    }
    public async Task<ApiResponse<int>> TaoAsync(HoSoBenhAnRequestDTO dto)
    {
        if (dto.BenhNhanID <= 0)
            return ApiResponse<int>.Fail("BenhNhanID không hợp lệ");
        var tonTai = await _repo.GetByBenhNhanIdAsync(dto.BenhNhanID);
        if (tonTai != null)
            return ApiResponse<int>.Fail("Bệnh nhân đã có hồ sơ bệnh án");
        var entity = new HoSoBenhAn(
            dto.BenhNhanID,
            dto.BenhNen,
            dto.DiUng,
            dto.TienSuBenh,
            dto.TienSuGiaDinh,
            dto.ThoiQuenSong,
            dto.ThongTinKhac,
            DateTime.UtcNow,
            DateTime.UtcNow
        );
        await _repo.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(1);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, HoSoBenhAnUpdateDTO dto)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Hồ sơ bệnh án không tồn tại");
        entity.CapNhatThongTin(
            dto.BenhNen,
            dto.DiUng,
            dto.TienSuBenh,
            dto.TienSuGiaDinh,
            dto.ThoiQuenSong,
            dto.ThongTinKhac,
            DateTime.UtcNow
        );
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<HoSoBenhAnReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);
        if (result == null)
            return ApiResponse<HoSoBenhAnReadModel>.Fail("Hồ sơ bệnh án không tồn tại");
        return ApiResponse<HoSoBenhAnReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<HoSoBenhAnReadModel?>> GetByBenhNhanIdAsync(int benhNhanId)
    {
        if (benhNhanId <= 0)
            return ApiResponse<HoSoBenhAnReadModel?>.Fail("ID bệnh nhân không hợp lệ");
        var entity = await _repo.GetByBenhNhanIdAsync(benhNhanId);
        if (entity == null)
            return ApiResponse<HoSoBenhAnReadModel?>.SuccessResponse(null);
        var result = new HoSoBenhAnReadModel
        {
            HoSoBenhAnID = entity.HoSoBenhAnID,
            BenhNhanID = entity.BenhNhanID,
            BenhNen = entity.BenhNen,
            DiUng = entity.DiUng,
            TienSuBenh = entity.TienSuBenh,
            TienSuGiaDinh = entity.TienSuGiaDinh,
            ThoiQuenSong = entity.ThoiQuenSong,
            ThongTinKhac = entity.ThongTinKhac,
            NgayTao = entity.NgayTao,
            NgayCapNhat = entity.NgayCapNhat
        };
        return ApiResponse<HoSoBenhAnReadModel?>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<HoSoBenhAnListReadModel>>> GetPagedAsync(int page, int size)
    {
        var (items, total) = await _repo.GetPagedAsync(page, size);
        return ApiResponse<PagedResult<HoSoBenhAnListReadModel>>.SuccessResponse(
            new PagedResult<HoSoBenhAnListReadModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = page,
                PageSize = size
            });
    }
    public async Task<ApiResponse<PagedResult<HoSoBenhAnListReadModel>>> SearchAsync(string keyword, int page, int size)
    {
        if (string.IsNullOrWhiteSpace(keyword))
            return ApiResponse<PagedResult<HoSoBenhAnListReadModel>>.Fail("Từ khóa không hợp lệ");
        var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);
        return ApiResponse<PagedResult<HoSoBenhAnListReadModel>>.SuccessResponse(
            new PagedResult<HoSoBenhAnListReadModel>
            {
                Items = items,
                TotalCount = total,
                PageNumber = page,
                PageSize = size
            });
    }
}