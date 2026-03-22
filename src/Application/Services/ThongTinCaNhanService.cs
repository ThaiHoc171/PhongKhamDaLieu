using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class ThongTinCaNhanService
{
	private readonly IThongTinCaNhanRepository _repo;
	public ThongTinCaNhanService(IThongTinCaNhanRepository repo)
	{
		_repo = repo;
	}
	private static GioiTinhEnum ParseGioiTinh(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			return GioiTinhEnum.Khac;
		return GioiTinhExtensions.ToEnum(value);
	}
    private static LoaiThongTinEnum ParseLoai(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return LoaiThongTinEnum.Khach;
        return LoaiThongTinExtensions.ToEnum(value);
    }
    public async Task<ApiResponse<int>> AddKhachAsync(ThongTinRequestDTO dto)
	{
        var isExist = await _repo.ExistsByEmailAsync(dto.EmailLienHe, dto.SDT);
        if (isExist)
            return ApiResponse<int>.Fail("Email hoặc số điện thoại đã tồn tại");
        var entity = new ThongTinCaNhan(
			dto.HoTen,
			dto.NgaySinh,
			ParseGioiTinh(dto.GioiTinh),
			dto.SDT,
			dto.EmailLienHe,
			dto.DiaChi,
			dto.Avatar,
			LoaiThongTinEnum.Khach,
			dto.TaiKhoanID
		);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id, "Tạo thông tin thành công");
	}
	public async Task<ApiResponse<List<ThongTinCaNhanResponseDTO>>> DanhSachKhachAsync()
	{
		var list = await _repo.GetAllByLoaiAsync(LoaiThongTinEnum.Khach);
		var result = list.Select(e => new ThongTinCaNhanResponseDTO
		{
			ThongTinID = e.ThongTinID,
			TaiKhoanID = e.TaiKhoanID,
			HoTen = e.HoTen,
			SDT = e.SDT,
			EmailLienHe = e.EmailLienHe,
			Loai = e.Loai
		}).ToList();
		return ApiResponse<List<ThongTinCaNhanResponseDTO>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<ThongTinCaNhanResponseDTO?>> GetDetailAsync(int id)
	{
		var data = await _repo.GetDetailAsync(id);
		if (data == null)
			return ApiResponse<ThongTinCaNhanResponseDTO?>.Fail("Không tìm thấy.");
		var result = new ThongTinCaNhanResponseDTO
		{
			ThongTinID = data.ThongTinID,
			TaiKhoanID = data.TaiKhoanID,
			HoTen = data.HoTen,
			NgaySinh = data.NgaySinh,
			GioiTinh = data.GioiTinh ?? "",
			SDT = data.SDT,
			EmailLienHe = data.EmailLienHe,
			DiaChi = data.DiaChi,
			Avatar = data.Avatar,
			Loai = data.Loai
		};
		return ApiResponse<ThongTinCaNhanResponseDTO?>.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThongTinUpdateRequestDTO dto)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy.");
		entity.CapNhat(
			dto.HoTen,
			dto.NgaySinh,
			ParseGioiTinh(dto.GioiTinh),
			dto.SDT,
			dto.EmailLienHe,
			dto.DiaChi,
			dto.Avatar,
			ParseLoai(dto.Loai)
		);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thông tin thành công");
	}
	public async Task<ApiResponse<List<NameResponseDTO>>> GetCombobox()
	{
		var result = await _repo.GetComboboxAsync();
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(result);
	}
}