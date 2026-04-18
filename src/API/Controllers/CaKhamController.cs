using Application.Common;
using Application.DTOs;
using Application.Services;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers;
[ApiController]
[Route("api/cakham")]
[Authorize]
public class CaKhamController : ControllerBase
{
	private readonly CaKhamService _service;
	public CaKhamController(CaKhamService service)
	{
		_service = service;
	}
	[HttpPost]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<ActionResult<ApiResponse<int>>> Create([FromBody] CaKhamRequest request)
	{
		var response = await _service.GenerateAsync(request);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}

    [HttpPut("{id}/trang-thai")]
    [Authorize(Policy = "LICH_WRITE")]
    public async Task<IActionResult> UpdateTrangThai(int id, [FromBody] CaKhamTrangThaiDTO request)
    {
		var result = await _service.StatusAsync(id, request.TrangThai, request.GhiChu);

		if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }
    [HttpGet("{id}")]
	[Authorize(Policy = "LICH_READ")]
	public async Task<ActionResult<ApiResponse<CaKhamReadModel>>> GetDetail(int id)
	{
		var response = await _service.GetDetailAsync(id);
		if (!response.Success)
			return NotFound(response);
		return Ok(response);
	}
	[HttpGet]
	[Authorize(Policy = "LICH_READ")]
	public async Task<ActionResult<ApiResponse<PagedResult<CaKhamListReadModel>>>> 
		List([FromQuery] DateTime ngayKham, [FromQuery] string trangThai,[FromQuery] string loaiCaKham,
		[FromQuery] int pageNumber = 1,	[FromQuery] int pageSize = 15)
	{
		var response = await _service.GetPagedAsync( ngayKham,trangThai, loaiCaKham, pageNumber, pageSize);
		return Ok(response);
	}
	[HttpGet("search/by-thongtin/{thongTinId}")]
	[Authorize(Policy = "LICH_READ")]
	public async Task<ActionResult<ApiResponse<PagedResult<CaKhamListReadModel>>>> 
		SearchByThongTin(int thongTinId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
	{
		var response = await _service.GetByThongTinAsync(thongTinId, pageNumber, pageSize);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[HttpGet("choxacnhan")]
	[Authorize(Policy = "LICH_READ")]
	public async Task<ActionResult<ApiResponse<PagedResult<CaKhamListReadModel>>>> CaKhamChoXacNhan([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 15)
	{
		var response = await _service.GetChoXacNhanAsync(pageNumber, pageSize);
		return Ok(response);
	}
	[HttpGet("khunggio-trong")]
    [Authorize(Policy = "LICH_READ")]
    public async Task<ActionResult<ApiResponse<List<int>>>>GetKhungGioConTrong([FromQuery] DateTime ngayKham, [FromQuery] string loaiCaKham)
    {
        var response = await _service.GetKhungGioConTrongAsync(ngayKham, loaiCaKham);
        if (!response.Success)
            return BadRequest(response);
        return Ok(response);
    }
    [HttpGet("ca-trong")]
    [Authorize(Policy = "LICH_READ")]
    public async Task<ActionResult<ApiResponse<int>>>GetCaKhamTrong([FromQuery] DateTime ngayKham,[FromQuery] int khungGioId,[FromQuery] string loaiCaKham)
    {
        var response = await _service.GetCaKhamAsync(ngayKham, khungGioId, loaiCaKham);
        if (!response.Success)
            return BadRequest(response);
        return Ok(response);
    }
    [HttpGet("check-dadangky")]
	[Authorize(Policy = "LICH_READ")]
	public async Task<ActionResult<ApiResponse<bool>>>CheckDaDangKy([FromQuery] DateTime ngay, [FromQuery] int khungGioId, [FromQuery] string loaiCaKham, [FromQuery] int thongTinId)
    {
        var response = await _service.CheckThongTinDaDangKyAsync(
            ngay,
            khungGioId,
            loaiCaKham,
            thongTinId);
        return Ok(response);
    }
    [HttpPut("{id}/register")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<ActionResult<ApiResponse<bool>>> Register(int id, [FromBody] CaKhamRegisterDTO request)
	{
		var response = await _service.RegisterAsync(id, request);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
	[HttpPut("{id}/cancel")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<ActionResult<ApiResponse<bool>>> Cancel(int id)
	{
		var response = await _service.CancelAsync(id);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}
    [HttpPost("assign-lich")]
	[Authorize(Policy = "LICH_WRITE")]
	public async Task<ActionResult<ApiResponse<AssignLichLamViecReport>>> 
		AssignLichLamViec([FromBody] CaKhamRequest request)
	{
		var response = await _service.AssignLichLamViecAsync(request);
		if (!response.Success)
			return BadRequest(response);
		return Ok(response);
	}

}