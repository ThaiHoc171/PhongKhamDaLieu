using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class NhanVienService
{
	private readonly INhanVienRepository _repo;
	private readonly IThongTinCaNhanRepository _thongTinRepo;
	private readonly ITaiKhoanRepository _taiKhoanRepo;
	private readonly IConfiguration _config;

	public NhanVienService(
		INhanVienRepository repo,
		IThongTinCaNhanRepository thongTinRepo,
		ITaiKhoanRepository taiKhoanRepo,
		IConfiguration config)
	{
		_repo = repo;
		_thongTinRepo = thongTinRepo;
		_taiKhoanRepo = taiKhoanRepo;
		_config = config;
	}

	private static GioiTinhEnum ParseGioiTinh(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			return GioiTinhEnum.Khac;

		return GioiTinhExtensions.FromDbValue(value);
	}

	public async Task<ApiResponse<int>> AddAsync(NhanVienRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<int>.Fail("Dữ liệu không hợp lệ");
			if (dto.ThongTin.EmailLienHe == null)
				return ApiResponse<int>.Fail("Email liên hệ không được để trống");

			var defaultPassword = _config["DefaultPassword"];

			if (string.IsNullOrWhiteSpace(defaultPassword))
				return ApiResponse<int>.Fail("Chưa cấu hình mật khẩu mặc định");

			var hash = Helper.Password.PassWordHash(defaultPassword);

			var taiKhoan = new TaiKhoan(
				dto.ThongTin.EmailLienHe,
				hash,
				VaiTroEnum.NhanVien
			);

			int taiKhoanID = await _taiKhoanRepo.AddAsync(taiKhoan);
			if (taiKhoanID == 0)
				return ApiResponse<int>.Fail("Không tạo được tài khoản");

			var thongTin = new ThongTinCaNhan(
				dto.ThongTin.HoTen,
				dto.ThongTin.NgaySinh,
				GioiTinhExtensions.FromDbValue(dto.ThongTin.GioiTinh),
				dto.ThongTin.SDT,
				dto.ThongTin.EmailLienHe,
				dto.ThongTin.DiaChi,
				dto.ThongTin.Avatar,
				LoaiThongTinEnum.NhanVien,
				taiKhoanID
			);

			int? thongTinId = await _thongTinRepo.AddAsync(thongTin);

			if (thongTinId == null)
				return ApiResponse<int>.Fail("Khởi tạo thông tin cá nhân thất bại");

			var nv = new NhanVien(
				thongTinId.Value,
				dto.ChucVuID,
				dto.PhongChucNangID,
				dto.NgayVaoLam ?? DateTime.UtcNow,
				dto.BangCap,
				dto.KinhNghiem,
				"Đang làm việc"
			);

			int row = await _repo.AddAsync(nv);

			if (row == 0)
				return ApiResponse<int>.Fail("Tạo nhân viên thất bại");

			return ApiResponse<int>.SuccessResponse(nv.NhanVienID, "Tạo nhân viên thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<int>.Fail("Email đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, NhanVienRequestUpdateDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var nv = await _repo.GetByIdAsync(id);

			if (nv == null)
				return ApiResponse<bool>.Fail("Nhân viên không tồn tại");

			nv.CapNhat(
				dto.ChucVuID,
				dto.PhongChucNangID,
				dto.NgayVaoLam ?? nv.NgayVaoLam,
				dto.BangCap,
				dto.KinhNghiem,
				nv.TrangThai
			);

			int row = await _repo.UpdateAsync(nv);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật nhân viên thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật nhân viên thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> StatusAsync(int id, string trangThai)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var nv = await _repo.GetByIdAsync(id);

			if (nv == null)
				return ApiResponse<bool>.Fail("Nhân viên không tồn tại");

			nv.CapNhat(
				nv.ChucVuID,
				nv.PhongChucNangID,
				nv.NgayVaoLam,
				nv.BangCap,
				nv.KinhNghiem,
				trangThai
			);

			int row = await _repo.UpdateAsync(nv);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật trạng thái thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật trạng thái thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<NhanVienReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<NhanVienReadModel>.Fail("ID không hợp lệ");

		var data = await _repo.GetDetailAsync(id);

		if (data == null)
			return ApiResponse<NhanVienReadModel>.Fail("Nhân viên không tồn tại");

		return ApiResponse<NhanVienReadModel>.SuccessResponse(data);
	}

	public async Task<ApiResponse<PagedResult<NhanVienReadListModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<NhanVienReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<NhanVienReadListModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<NhanVienReadListModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<NhanVienReadListModel>>
				.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchAsync(keyword.Trim(), page, size);

		var result = new PagedResult<NhanVienReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<NhanVienReadListModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync(int chucVuId)
	{
		var data = await _repo.GetComboboxAsync(chucVuId);
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}
}