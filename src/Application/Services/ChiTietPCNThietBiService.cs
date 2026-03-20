using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class ChiTietPCNThietBiService
{
    private readonly IChiTietPCNThietBiRepository _repo;
    private readonly IPCNThietBiRepository _pcnRepo;
    public ChiTietPCNThietBiService(
        IChiTietPCNThietBiRepository repo,
        IPCNThietBiRepository pcnRepo)
    {
        _repo = repo;
        _pcnRepo = pcnRepo;
    }
    public async Task<ApiResponse<int>> TaoMoiAsync(ChiTietPCNThietBiRequestDTO dto)
    {
        var pcn = await _pcnRepo.GetByPhongAndThietBiAsync(
            dto.PhongChucNangID,
            dto.ThietBiID);
        if (pcn == null)
        {
            pcn = new PCNThietBi(dto.PhongChucNangID, dto.ThietBiID);
            await _pcnRepo.AddAsync(pcn);
            pcn = await _pcnRepo.GetByPhongAndThietBiAsync(
                dto.PhongChucNangID,
                dto.ThietBiID);
            if (pcn == null)
                return ApiResponse<int>.Fail("Không thể tạo PCN thiết bị");
        }
        var entity = new ChiTietPCNThietBi(
            pcn.PCN_TB_ID,
            dto.MaTaiSan,
            dto.GhiChu);
        var id = await _repo.AddAsync(entity);
        pcn.CapNhatSoLuong(pcn.TongSoLuong + 1);
        await _pcnRepo.UpdateAsync(pcn);
        return ApiResponse<int>.SuccessResponse(id);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, ChiTietPCNThietBiUpdateDTO dto)
    {
        var entity = await _repo.GetByIdAsync(id);

        if (entity == null)
            return ApiResponse<bool>.Fail("Chi tiết thiết bị không tồn tại");
        try
        {
            entity.CapNhatGhiChu(dto.GhiChu);
            entity.ChuyenTinhTrang(dto.TinhTrang);
        }
        catch (InvalidOperationException ex)
        {
            return ApiResponse<bool>.Fail(ex.Message);
        }
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<bool>> XoaAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Chi tiết thiết bị không tồn tại");
        if (entity.TinhTrang == TinhTrang.HoatDong)
            return ApiResponse<bool>.Fail("Không thể xoá thiết bị đang hoạt động");
        await _repo.DeleteAsync(id);
        var pcn = await _pcnRepo.GetByIdAsync(entity.PCN_TB_ID);
        if (pcn == null)
            return ApiResponse<bool>.Fail("PCN thiết bị không tồn tại");
        pcn.CapNhatSoLuong(pcn.TongSoLuong - 1);
        if (pcn.CoTheXoa())
            await _pcnRepo.DeleteAsync(pcn.PCN_TB_ID);
        else
            await _pcnRepo.UpdateAsync(pcn);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<ChiTietPCNThietBiReadModel>> GetByIdAsync(int id)
    {
        var result = await _repo.GetDetailAsync(id);
        if (result == null)
            return ApiResponse<ChiTietPCNThietBiReadModel>.Fail("Không tồn tại");
        return ApiResponse<ChiTietPCNThietBiReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<PagedResult<ChiTietPCNThietBiListReadModel>>> GetPagedAsync(int pcnTbId, int pageNumber, int pageSize)
    {
        var (items, totalCount) =
            await _repo.GetPagedAsync(pcnTbId, pageNumber, pageSize);
        return ApiResponse<PagedResult<ChiTietPCNThietBiListReadModel>>.SuccessResponse(
            new PagedResult<ChiTietPCNThietBiListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
    public async Task<ApiResponse<PagedResult<ChiTietPCNThietBiListReadModel>>> SearchAsync(int pcnTbId, string keyword, int pageNumber, int pageSize)
    {
        var (items, totalCount) =
            await _repo.SearchPagedAsync(pcnTbId, keyword, pageNumber, pageSize);
        return ApiResponse<PagedResult<ChiTietPCNThietBiListReadModel>>.SuccessResponse(
            new PagedResult<ChiTietPCNThietBiListReadModel>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
    }
    public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync(int pcnTbId)
    {
        var list = await _repo.GetComboboxAsync(pcnTbId);

        return ApiResponse<List<NameResponseDTO>>.SuccessResponse(
            list.Select(x => new NameResponseDTO
            {
                Id = x.Id,
                Name = x.Ten
            }).ToList()
        );
    }
}