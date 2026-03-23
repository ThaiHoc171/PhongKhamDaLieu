using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class PhongKhamService
{
    private readonly IPhongKhamRepository _repo;

    public PhongKhamService(IPhongKhamRepository repo)
    {
        _repo = repo;
    }
    public async Task<ApiResponse<bool>> CapNhatAsync(int id, PhongKhamUpdateDTO dto)
    {
        var pk = await _repo.GetByIdAsync(id);
        if (pk == null)
            return ApiResponse<bool>.Fail("Phòng khám không tồn tại");
        if (string.IsNullOrWhiteSpace(dto.TenPhongKham))
            return ApiResponse<bool>.Fail("Tên phòng khám không hợp lệ");
        pk.CapNhat(
            dto.TenPhongKham,
            dto.GioiThieu,
            dto.DiaChi,
            dto.Hotline,
            dto.Email,
            dto.Website,
            dto.HinhAnhBanner
        );
        await _repo.UpdateAsync(pk);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<bool>> DoiTrangThaiAsync(int id, PhongKhamUpdateTrangThaiDTO dto)
    {
        var pk = await _repo.GetByIdAsync(id);
        if (pk == null)
            return ApiResponse<bool>.Fail("Phòng khám không tồn tại");
        var valid = new[] { "Hoạt động", "Đóng cửa", "Ngưng hoạt động" };
        if (!valid.Contains(dto.TrangThai))
            return ApiResponse<bool>.Fail("Trạng thái không hợp lệ");
        pk.DoiTrangThai(dto.TrangThai);
        await _repo.UpdateAsync(pk);
        return ApiResponse<bool>.SuccessResponse(true);
    }
    public async Task<ApiResponse<PhongKhamReadModel>> GetDetailAsync()
    {
        var result = await _repo.GetDetailAsync();
        if (result == null)
            return ApiResponse<PhongKhamReadModel>.Fail("Không tìm thấy phòng khám");
        return ApiResponse<PhongKhamReadModel>.SuccessResponse(result);
    }
}