using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
namespace Application.Services;

public class PCNThietBiService
{
	private readonly IPCNThietBiRepository _repo;
	public PCNThietBiService(IPCNThietBiRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<PagedResult<PCNThietBiReadModel>>> GetPagedAsync(int page, int size, int? phongChucNangID)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size, phongChucNangID);
		var result = new PagedResult<PCNThietBiReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<PCNThietBiReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PagedResult<PCNThietBiReadModel>>> SearchAsync(string keyword, int page, int size, int? phongChucNangID)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<PCNThietBiReadModel>>.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size, phongChucNangID);
		var result = new PagedResult<PCNThietBiReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<PCNThietBiReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync(int pcnId)
	{
		var data = await _repo.GetComboboxAsync(pcnId);
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(data);
	}
}