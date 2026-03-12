using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;


namespace Application.Services;

public class NhanVienService
{
	private readonly INhanVienRepository _repo;
	private readonly IThongTinCaNhanRepository _thongTinRepo;
	private readonly ITaiKhoanRepository _taiKhoanRepo;
	private readonly IConfiguration _config;
	public NhanVienService(INhanVienRepository repo, IThongTinCaNhanRepository thongTinRepo, 
		IConfiguration config, ITaiKhoanRepository taiKhoanRepository)
	{
		_repo = repo;
		_thongTinRepo = thongTinRepo;
		_config = config;
		_taiKhoanRepo = taiKhoanRepository;
	}

	private static GioiTinhEnum ParseGioiTinh(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			return GioiTinhEnum.Khac;
		return GioiTinhExtensions.ToEnum(value);
	}
	public async Task<ApiResponse<int>> AddNhanVienAsync(NhanVienRequestDTO dto)
	{
		var defaultPassword = _config["DefaultPassword"];
		if (string.IsNullOrWhiteSpace(defaultPassword))
			return ApiResponse<int>.Fail("Chưa cấu hình mật khẩu mặc định.");
		var hash = Helper.Password.PassWordHash(defaultPassword);
		var taiKhoan = new TaiKhoan(dto.ThongTin.EmailLienHe, hash, VaiTroEnum.NhanVien);
		await _taiKhoanRepo.AddAsync(taiKhoan);
		var created = await _taiKhoanRepo.GetByEmailAsync(dto.ThongTin.EmailLienHe);
		if (created == null)
			return ApiResponse<int>.Fail("Không tạo được tài khoản.");
		var entity = new ThongTinCaNhan(
			dto.ThongTin.HoTen,
			dto.ThongTin.NgaySinh,
			ParseGioiTinh(dto.ThongTin.GioiTinh),
			dto.ThongTin.SDT,
			dto.ThongTin.EmailLienHe,
			dto.ThongTin.DiaChi,
			dto.ThongTin.Avatar,
			LoaiThongTinEnum.NhanVien,
			created.Id
		);
		int? id = await _thongTinRepo.AddAsync(entity);

		if (id == null)
			return ApiResponse<int>.Fail("Lỗi khởi tạo hồ sơ!");

		var thongTinID = id.Value;

		var nv = new NhanVien(
			thongTinID: thongTinID,
			chucVuID: dto.ChucVuID,
			phongChucNangID: dto.PhongChucNangID,
			ngayVaoLam: dto.NgayVaoLam,
			bangCap: dto.BangCap,
			kinhNghiem: dto.KinhNghiem
		);

		await _repo.AddAsync(nv);

		return ApiResponse<int>.SuccessResponse(nv.NhanVienID, "Tạo nhân viên thành công");
	}


	public async Task<ApiResponse<bool>> UpdateAsync(int nhanVienID, NhanVienRequestUpdateDTO dto)
	{
		var nv = await _repo.GetByIdAsync(nhanVienID);

		if (nv == null)
			return ApiResponse<bool>.Fail("Nhân viên không tồn tại");

		nv.Update(
			chucVuID: dto.ChucVuID,
			phongChucNangID: dto.PhongChucNangID,
			ngayVaoLam: dto.NgayVaoLam,
			bangCap: dto.BangCap,
			kinhNghiem: dto.KinhNghiem
		);

		await _repo.UpdateAsync(nv);

		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công");
	}


	public async Task<ApiResponse<bool>> StatusAsync(int nhanVienID, string trangThai)
	{
		var nv = await _repo.GetByIdAsync(nhanVienID);

		if (nv == null)
			return ApiResponse<bool>.Fail("Nhân viên không tồn tại");

		nv.Status(trangThai);

		await _repo.UpdateAsync(nv);

		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật trạng thái thành công");
	}
	public async Task<ApiResponse<NhanVienDetailReadModel>> GetDetailAsync(int nhanVienID)
	{
		var nv = await _repo.GetDetailAsync(nhanVienID);

		if (nv == null)
			return ApiResponse<NhanVienDetailReadModel>.Fail("Không tìm thấy nhân viên");

		return ApiResponse<NhanVienDetailReadModel>.SuccessResponse(nv);
	}

	public async Task<ApiResponse<PagedResult<NhanVienListReadModel>>> 
		GetPagedAsync(int pageNumber, int pageSize)
	{
		if (pageNumber <= 0) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (data, totalCount) = await _repo.GetPageAsync(pageNumber, pageSize);

		var result = new PagedResult<NhanVienListReadModel>
		{
			Items = data,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};

		return ApiResponse<PagedResult<NhanVienListReadModel>>
			.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<NhanVienListReadModel>>> 
		SearchAsync(string keyword, int pageNumber, int pageSize)
	{
		if (pageNumber <= 0) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (data, totalCount) =
			await _repo.SearchAsync(keyword ?? "", pageNumber, pageSize);

		var result = new PagedResult<NhanVienListReadModel>
		{
			Items = data,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};

		return ApiResponse<PagedResult<NhanVienListReadModel>>
			.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync(int chucVuId)
	{
		var data = await _repo.GetComboboxAsync(chucVuId);

		return ApiResponse<List<NameResponseDTO>>
			.SuccessResponse(data);
	}
}