using Application.Common;
using Application.DTOs;
using Domain.Entities;
using OfficeOpenXml;
namespace Application.Services;
public class ThuocService
{
	private readonly IThuocRepository _repo;
	public ThuocService(IThuocRepository repo)
	{
		_repo = repo;
	}
	public async Task<ApiResponse<int>> AddAsync(ThuocRequestDTO dto)
	{
		var validate = ValidateCreate(dto);
		if (!validate.Success)
			return ApiResponse<int>.Fail(validate.Message);
		var entity = new Thuoc(dto.TenThuoc, dto.HoatChat);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id, "Tạo thuốc thành công");
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThuocUpdateDTO dto)
	{
		var validate = ValidateUpdate(dto);
		if (!validate.Success)
			return ApiResponse<bool>.Fail(validate.Message);
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy thuốc");
		entity.CapNhat(dto.TenThuoc, dto.HoatChat);
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thuốc thành công");
	}
	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Không tìm thấy thuốc");
		await _repo.DeleteAsync(id);
		return ApiResponse<bool>.SuccessResponse(true, "Xóa thuốc thành công");
	}
	public async Task<ApiResponse<ThuocReadModel>> GetDetailAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<ThuocReadModel>.Fail("Không tìm thấy thuốc");
		return ApiResponse<ThuocReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ThuocListReadModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.GetPagedAsync(page, size);
		var result = new PagedResult<ThuocListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThuocListReadModel>>
			.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ThuocListReadModel>>> SearchAsync(
		string keyword,
		int page,
		int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ThuocListReadModel>>
				.Fail("Keyword không hợp lệ");
		var (items, total) =
			await _repo.SearchPagedAsync(keyword.Trim(), page, size);
		var result = new PagedResult<ThuocListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThuocListReadModel>>
			.SuccessResponse(result);
	}
    public async Task<ApiResponse<List<NameResponseDTO>>> GetIdAndNameAsync()
    {
        var list = await _repo.GetIdAndNameAsync();
        var result = list.Select(x => new NameResponseDTO
        {
            Id = x.Id,
            Name = x.Ten
        }).ToList();
        return ApiResponse<List<NameResponseDTO>>.SuccessResponse(result);
    }
    public async Task<ApiResponse<int>> ImportExcelAsync(Stream stream)
	{
		ExcelPackage.License.SetNonCommercialPersonal("ClinicApp");
		using var package = new ExcelPackage(stream);
		var sheet = package.Workbook.Worksheets.FirstOrDefault();
		if (sheet == null)
			return ApiResponse<int>.Fail("File Excel không hợp lệ");
		var rowCount = sheet.Dimension.Rows;
		int success = 0;
		for (int row = 2; row <= rowCount; row++)
		{
			var tenThuoc = sheet.Cells[row, 1].Text?.Trim();
			var hoatChat = sheet.Cells[row, 2].Text?.Trim();
			if (string.IsNullOrWhiteSpace(tenThuoc))
				continue;
			var entity = new Thuoc(tenThuoc, hoatChat);
			await _repo.AddAsync(entity);
			success++;
		}
		return ApiResponse<int>.SuccessResponse(success, "Import thuốc thành công");
	}
	private ApiResponse<bool> ValidateCreate(ThuocRequestDTO dto)
	{
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.TenThuoc))
			return ApiResponse<bool>.Fail("Tên thuốc không được để trống");
		return ApiResponse<bool>.SuccessResponse(true);
	}
	private ApiResponse<bool> ValidateUpdate(ThuocUpdateDTO dto)
	{
		if (dto == null)
			return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.TenThuoc))
			return ApiResponse<bool>.Fail("Tên thuốc không được để trống");
		return ApiResponse<bool>.SuccessResponse(true);
	}
}