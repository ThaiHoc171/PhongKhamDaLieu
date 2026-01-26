using Application.DTOs;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PCNThietBiController : ControllerBase
{
	private readonly PCNThietBiService _service;

	public PCNThietBiController(PCNThietBiService service)
	{
		_service = service;
	}


	[HttpGet]
	public async Task<IActionResult> GetByPCN(int pcnId)
	{
		try
		{
			var result = await _service.GetByPCNAsync(pcnId);
			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest("Lỗi khi lấy danh sách thiết bị phòng chức năng" + ex.Message);
		}
	}

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		try
		{
			var result = await _service.GetByIdAsync(id);
			if (result == null)
				return NotFound(new { message = "Không tìm thấy thiết bị phòng chức năng" });

			return Ok(result);
		}
		catch (Exception ex)
		{
			return BadRequest("Lỗi khi lấy thiết bị phòng chức năng" + ex.Message);
		}
	}


	[HttpPost]
	public async Task<IActionResult> Create(
		int pcnId,
		[FromBody] PCNThietBiRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return BadRequest(new { message = "Dữ liệu không hợp lệ" });

			var id = await _service.AddAsync(pcnId, dto);

			return Ok(new
			{
				message = "Thêm thiết bị phòng chức năng thành công",
				id
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return BadRequest("Lỗi khi thêm thiết bị phòng chức năng" + ex.Message);
		}
	}

	[HttpPut("{id:int}")]
	public async Task<IActionResult> Update(
		int id,
		[FromBody] PCNThietBiRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return BadRequest(new { message = "Dữ liệu không hợp lệ" });

			var success = await _service.UpdateAsync(id, dto);
			if (!success)
				return NotFound(new { message = "Không tìm thấy thiết bị phòng chức năng" });

			return Ok(new
			{
				message = "Cập nhật thiết bị phòng chức năng thành công"
			});
		}
		catch (ArgumentException ex)
		{
			return BadRequest(new { message = ex.Message });
		}
		catch (Exception ex)
		{
			return BadRequest("Lỗi khi cập nhật thiết bị phòng chức năng" + ex.Message);
		}
	}


	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		try
		{
			var success = await _service.DeleteAsync(id);
			if (!success)
				return NotFound(new { message = "Không tìm thấy thiết bị phòng chức năng" });

			return Ok(new
			{
				message = "Xóa thiết bị phòng chức năng thành công"
			});
		}
		catch (Exception ex)
		{
			return BadRequest("Lỗi khi xóa thiết bị phòng chức năng" + ex.Message);
		}
	}
}
