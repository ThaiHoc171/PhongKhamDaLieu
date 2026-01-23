using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Application.Services;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PhienKhamController : ControllerBase
{
	private readonly PhienKhamService _service;

	public PhienKhamController(PhienKhamService service)
	{
		_service = service;
	}

	// POST: api/PhienKham
	[HttpPost]
	public async Task<IActionResult> TaoMoi([FromBody] PhienKhamCreateDTO dto)
	{
		try
		{
			var phienKhamId = await _service.TaoMoiAsync(dto);

			return Ok(new
			{
				message = "Tạo phiên khám thành công.",
				phienKhamId
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
	}

	// PUT: api/PhienKham/{id}
	[HttpPut("{id}")]
	public async Task<IActionResult> CapNhat(int id, [FromBody] PhienKhamUpdateDTO dto)
	{
		try
		{
			await _service.CapNhatAsync(id, dto);

			return Ok(new
			{
				message = "Cập nhật phiên khám thành công."
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
	}

	// PUT: api/PhienKham/{id}/ket-thuc
	[HttpPut("{id}/ket-thuc")]
	public async Task<IActionResult> KetThuc(int id, [FromBody] string chuanDoanCuoi)
	{
		try
		{
			await _service.KetThucAsync(id, chuanDoanCuoi);

			return Ok(new
			{
				message = "Kết thúc phiên khám thành công."
			});
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
	}
	// GET: api/PhienKham
	[HttpGet]
	public async Task<IActionResult> GetAll()
	{
		try
		{
			return Ok(await _service.LayTatCaAsync());
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
	}

	// GET: api/PhienKham/{id}
	[HttpGet("{id}")]
	public async Task<IActionResult> GetById(int id)
	{
		try
		{
			var result = await _service.LayTheoIdAsync(id);
			if (result == null)
				return NotFound("Phiên khám không tồn tại");

			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
	}

	// GET: api/PhienKham/filter
	[HttpGet("filter")]
	public async Task<IActionResult> Filter(
		[FromQuery] DateTime? tuNgay,
		[FromQuery] DateTime? denNgay,
		[FromQuery] string? trangThai,
		[FromQuery] int? nhanVienID)
	{
		try
		{
			var result = await _service.LocAsync(tuNgay, denNgay, trangThai, nhanVienID);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest(new
			{
				message = ex.Message
			});
		}
	}

}
