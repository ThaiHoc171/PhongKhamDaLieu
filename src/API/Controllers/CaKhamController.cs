using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;
using Application.Common;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CaKhamController : ControllerBase
{
	private readonly CaKhamService _service;

	public CaKhamController(CaKhamService service)
	{
		_service = service;
	}

	[Authorize(Policy = "LeTanOnly")]
	[HttpPost]
	public async Task<ActionResult<ApiResponse<int>>> TaoMoi([FromBody] TaoCaKhamDTO dto)
	{
		var result = await _service.TaoCaKhamAsync(dto);

		return Ok(ApiResponse<int>.SuccessResponse(
			result,
			"Tạo ca khám thành công"));
	}

	[Authorize]
	[HttpPut("{id}/dangky")]
	public async Task<ActionResult<ApiResponse<object>>> DangKy(int id, [FromBody] DangKyCaKhamDTO dto)
	{
		var result = await _service.DangKyKhamAsync(id, dto);

		return Ok(ApiResponse<object>.SuccessResponse(
			null,
			"Đăng ký ca khám thành công"));
	}

	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{id}/trangthai")]
	public async Task<ActionResult<ApiResponse<object>>> CapNhatTrangThai(int id, [FromQuery] string TrangThai)
	{
		await _service.UpdateTrangThaiAsync(id, TrangThai);

		return Ok(ApiResponse<object>.SuccessResponse(
			null,
			"Cập nhật trạng thái thành công"));
	}

	[Authorize]
	[HttpGet("{id}")]
	public async Task<ActionResult<ApiResponse<CaKhamReadModel>>> GetById(int id)
	{
		var result = await _service.LayCaKhamTheoIdAsync(id);

		if (result == null)
			return NotFound(ApiResponse<CaKhamReadModel>
				.Fail("Ca khám không tồn tại"));

		return Ok(ApiResponse<CaKhamReadModel>
			.SuccessResponse(result));
	}

	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpGet]
	public async Task<ActionResult<ApiResponse<PagedResult<CaKhamListReadModel>>>> GetPaged( [FromQuery] DateTime ngayKham, 
		[FromQuery] string trangThai, [FromQuery] string loaiCaKham, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
	{
		var result = await _service.GetCaKhamPagedAsync(ngayKham,trangThai,loaiCaKham,pageNumber,pageSize);

		return Ok(ApiResponse<PagedResult<CaKhamListReadModel>>
			.SuccessResponse(result));
	}

	[Authorize(Roles = "Bệnh nhân")]
	[HttpGet("benhnhan/{thongTinID}")]
	public async Task<ActionResult<ApiResponse<PagedResult<CaKhamListReadModel>>>> GetByBenhNhan(int thongTinID, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
	{
		var result = await _service.GetByBenhNhanAsync(
			thongTinID,
			pageNumber,
			pageSize);

		return Ok(ApiResponse<PagedResult<CaKhamListReadModel>>
			.SuccessResponse(result));
	}

	[Authorize]
	[HttpGet("kiemtra-dadangky")]
	public async Task<ActionResult<ApiResponse<bool>>> KiemTraDaDangKy(	DateTime ngay, int khungGioId, string loaiCaKham, int benhNhanId)
	{
		var result = await _service.CheckBenhNhanDaDangKyAsync(
			ngay,
			khungGioId,
			loaiCaKham,
			benhNhanId);

		return Ok(ApiResponse<bool>.SuccessResponse(result));
	}

	[Authorize]
	[HttpGet("khunggio-trong")]
	public async Task<ActionResult<ApiResponse<List<int>>>> GetKhungGioConTrong( DateTime ngayKham, string loaiCaKham)
	{
		var result = await _service.GetKhungGioConTrongAsync(ngayKham,loaiCaKham);

		return Ok(ApiResponse<List<int>>
			.SuccessResponse(result));
	}

	[Authorize]
	[HttpGet("ca-trong")]
	public async Task<ActionResult<ApiResponse<int>>> GetCaTrong( DateTime ngayKham, int khungGioId, string loaiCaKham)
	{
		var result = await _service.GetCaKhamAsync(
			ngayKham,
			khungGioId,
			loaiCaKham);

		return Ok(ApiResponse<int>
			.SuccessResponse(result));
	}

	[Authorize(Policy = "BacSiOrLeTan")]
	[HttpGet("combobox")]
	public async Task<ActionResult<ApiResponse<List<NameResponseDTO>>>> GetCombobox( DateTime ngayKham, string trangThai)
	{
		var result = await _service.GetComboboxAsync(
			trangThai,
			ngayKham);

		return Ok(ApiResponse<List<NameResponseDTO>>
			.SuccessResponse(result));
	}
}