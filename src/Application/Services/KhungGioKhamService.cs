using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class KhungGioKhamService
{
    private readonly IKhungGioKhamRepository _repo;

    public KhungGioKhamService(IKhungGioKhamRepository repo)
    {
        _repo = repo;
    }
    public async Task<ApiResponse<List<KhungGioKhamListReadModel>>> GetAllAsync()
    {
        var list = await _repo.GetAllAsync();
        return ApiResponse<List<KhungGioKhamListReadModel>>.SuccessResponse(list);
    }
    public async Task<ApiResponse<KhungGioKhamReadModel>> GetByIdAsync(int id)
    {
        if (id <= 0)
            return ApiResponse<KhungGioKhamReadModel>.Fail("ID không hợp lệ");
        var result = await _repo.GetDetailAsync(id);
        if (result == null)
            return ApiResponse<KhungGioKhamReadModel>.Fail("Khung giờ khám không tồn tại");
        return ApiResponse<KhungGioKhamReadModel>.SuccessResponse(result);
    }
    public async Task<ApiResponse<int>> TaoAsync(KhungGioKhamRequestDTO dto)
    {
        if (dto.GioBatDau >= dto.GioKetThuc)
            return ApiResponse<int>.Fail("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");
        var entity = new KhungGioKham(
            dto.CaLamViec,
            dto.GioBatDau,
            dto.GioKetThuc,
            dto.TenKhung
        );
        var danhSach = await _repo.GetAllAsync();
        if (danhSach.Any(x =>
            x.CaLamViec == dto.CaLamViec &&
            !(dto.GioKetThuc <= x.GioBatDau ||
              dto.GioBatDau >= x.GioKetThuc)))
        {
            return ApiResponse<int>.Fail("Khung giờ khám bị trùng trong cùng ca làm việc");
        }
        var id = await _repo.AddAsync(entity);
        return ApiResponse<int>.SuccessResponse(id);
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, KhungGioKhamRequestDTO dto)
    {
        if (id <= 0)
            return ApiResponse<bool>.Fail("ID không hợp lệ");
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.Fail("Khung giờ khám không tồn tại");
        if (dto.GioBatDau >= dto.GioKetThuc)
            return ApiResponse<bool>.Fail("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");
        entity.CapNhat(
            dto.CaLamViec,
            dto.GioBatDau,
            dto.GioKetThuc,
            dto.TenKhung
        );
        var danhSach = await _repo.GetAllAsync();
        if (danhSach.Any(x =>
            x.KhungGioID != id &&
            x.CaLamViec == dto.CaLamViec &&
            !(dto.GioKetThuc <= x.GioBatDau ||
              dto.GioBatDau >= x.GioKetThuc)))
        {
            return ApiResponse<bool>.Fail("Khung giờ khám bị trùng trong cùng ca làm việc");
        }
        await _repo.UpdateAsync(entity);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
    {
        var list = await _repo.GetIdAndNameAsync();
        var result = list.Select(x => new NameResponseDTO
        {
            Id = x.Id,
            Name = x.Ten
        }).ToList();
        return ApiResponse<List<NameResponseDTO>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<int>> CountAsync()
    {
        var total = await _repo.CountKhungGioKhamAsync();
        return ApiResponse<int>.SuccessResponse(total);
    }
    public async Task<ApiResponse<List<int>>> GetByCaLamViecAsync(int caLamViec)
    {
        var list = await _repo.GetKhungGioIdsByCaLamViecAsync(caLamViec);
        return ApiResponse<List<int>>.SuccessResponse(list);
    }
}