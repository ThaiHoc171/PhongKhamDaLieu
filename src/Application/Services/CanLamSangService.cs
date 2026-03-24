using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using OfficeOpenXml;

namespace Application.Services;

public class CanLamSangService
{
	private readonly ICanLamSangRepository _repo;

	public CanLamSangService(ICanLamSangRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<bool>> AddAsync(CanLamSangRequest dto)
	{
		var validate = Validate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);

		var entity = new CanLamSang(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem, dto.TrangThai);

		int row = await _repo.AddAsync(entity);

		if (row == 0)
			return ApiResponse<bool>.Fail("Tạo cận lâm sàng thất bại");

		return ApiResponse<bool>.SuccessResponse(true, "Tạo cận lâm sàng thành công");
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, CanLamSangRequest dto)
	{
		var validate = Validate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);

		var entity = await _repo.GetByIdAsync(id);

		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy cận lâm sàng");

		entity.CapNhat(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem, dto.TrangThai);

		int row = await _repo.UpdateAsync(entity);

		if (row == 0)
			return ApiResponse<bool>.Fail("Cập nhật cận lâm sàng thất bại");

		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật cận lâm sàng thành công");
	}

	public async Task<ApiResponse<PagedResult<CanLamSangListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size);

		var result = new PagedResult<CanLamSangListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<CanLamSangListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<CanLamSangReadModel>> GetDetailAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<CanLamSangReadModel>.Fail("Không tìm thấy cận lâm sàng");

		return ApiResponse<CanLamSangReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<CanLamSangListReadModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<CanLamSangListReadModel>>
				.Fail("Keyword không hợp lệ");

		var (items, total) =
			await _repo.SearchPagedAsync(keyword.Trim(), page, size);

		var result = new PagedResult<CanLamSangListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<CanLamSangListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<CanLamSangListReadModel>>> GetByLoaiXetNghiemAsync(string loai)
	{
		if (string.IsNullOrWhiteSpace(loai))
			return ApiResponse<List<CanLamSangListReadModel>>
				.Fail("Loại xét nghiệm không hợp lệ");

		var result = await _repo.GetByLoaiXetNghiemAsync(loai);

		return ApiResponse<List<CanLamSangListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var data = await _repo.GetComboboxAsync();

		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}

	public async Task<ApiResponse<int>> ImportExcelAsync(Stream stream)
	{
		ExcelPackage.License.SetNonCommercialPersonal("ClinicApp");

		using var package = new ExcelPackage(stream);

		var sheet = package.Workbook.Worksheets.FirstOrDefault();

		if (sheet == null)
			return ApiResponse<int>.Fail("File Excel không hợp lệ");

		if (sheet.Dimension == null)
			return ApiResponse<int>.Fail("File Excel không có dữ liệu");

		int rowCount = sheet.Dimension.Rows;

		int success = 0;
		int fail = 0;

		for (int row = 2; row <= rowCount; row++)
		{
			try
			{
				var tenCLS = sheet.Cells[row, 1].Text?.Trim();
				var moTa = sheet.Cells[row, 2].Text?.Trim();
				var loaiXetNghiem = sheet.Cells[row, 3].Text?.Trim();
				var trangThai = sheet.Cells[row, 4].Text?.Trim();

				var dto = new CanLamSangRequest
				{
					TenCLS = tenCLS!,
					MoTa = moTa!,
					LoaiXetNghiem = loaiXetNghiem!,
					TrangThai = trangThai!
				};

				var validate = Validate(dto);

				if (!validate.Success)
				{
					fail++;
					continue;
				}

				var entity = new CanLamSang(dto.TenCLS, dto.MoTa, dto.LoaiXetNghiem, dto.TrangThai);

				var rows = await _repo.AddAsync(entity);

				if (rows > 0)
					success++;
				else
					fail++;
			}
			catch
			{
				fail++;
				continue;
			}
		}

		return ApiResponse<int>.SuccessResponse(
			success,
			$"Import thành công {success}/{rowCount - 1} cận lâm sàng. Lỗi {fail} dòng"
		);
	}

	private ApiResponse<bool> Validate(CanLamSangRequest dto)
	{
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

		if (string.IsNullOrWhiteSpace(dto.TenCLS))
			return ApiResponse<bool>.Fail("Tên cận lâm sàng không hợp lệ");

		if (string.IsNullOrWhiteSpace(dto.LoaiXetNghiem))
			return ApiResponse<bool>.Fail("Loại xét nghiệm không hợp lệ");

		if (dto.TrangThai != "Hoạt động" && dto.TrangThai != "Vô hiệu")
			return ApiResponse<bool>.Fail("Trạng thái không hợp lệ");

		return ApiResponse<bool>.SuccessResponse(true);
	}
}