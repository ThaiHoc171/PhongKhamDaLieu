using Application.DTOs;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/phong-chuc-nang/{phongChucNangId:int}/thiet-bi")]
public class PCNThietBiController : ControllerBase
{
	private readonly PCNThietBiService _service;

	public PCNThietBiController(PCNThietBiService service)
	{
		_service = service;
	}

	// ===================== QUERY =====================

	// GET: api/phong-chuc-nang/{phongChucNangId}/thiet-bi
	[HttpGet]
	public async Task<IActionResult> GetByPhongChucNang(int phongChucNangId)
	{
		var result = await _service.GetByPhongChucNangAsync(phongChucNangId);
		return Ok(result);
	}

	// GET: api/phong-chuc-nang/{phongChucNangId}/thiet-bi/{id}
	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetById(int id)
	{
		var result = await _service.GetByIdAsync(id);
		if (result == null)
			return NotFound(new { message = "Không tìm thấy thiết bị phòng chức năng" });

		return Ok(result);
	}

	// GET: api/phong-chuc-nang/{phongChucNangId}/thiet-bi/tong
	[HttpGet("tong")]
	public async Task<IActionResult> GetTongTheoPhong(int phongChucNangId)
	{
		var result = await _service.GetTongTheoPhongAsync(phongChucNangId);
		if (result == null)
			return NotFound(new { message = "Không tìm thấy phòng chức năng" });

		return Ok(result);
	}

	// GET: api/phong-chuc-nang/{phongChucNangId}/thiet-bi/nhap
	[HttpGet("nhap")]
	public async Task<IActionResult> GetThietBiNhap(int phongChucNangId)
	{
		var result = await _service.GetThietBiNhapAsync(phongChucNangId);
		return Ok(result);
	}

	// ===================== COMMAND =====================

	// POST: api/phong-chuc-nang/{phongChucNangId}/thiet-bi
	[HttpPost]
	public async Task<IActionResult> Create(
		int phongChucNangId,
		[FromBody] PCNThietBiRequestCreateDTO dto)
	{
		var result = await _service.CreateAsync(phongChucNangId, dto);

		return CreatedAtAction(
			nameof(GetById),
			new { phongChucNangId, id = result.Id },
			result
		);
	}

	// PUT: api/phong-chuc-nang/{phongChucNangId}/thiet-bi/{id}
	[HttpPut("{id:int}")]
	public async Task<IActionResult> UpdateSoLuong(
		int id,
		[FromBody] PCNThietBiRequestUpdateDTO dto)
	{
		var success = await _service.UpdateAsync(id, dto);
		if (!success)
			return NotFound(new { message = "Không tìm thấy thiết bị phòng chức năng" });

		return Ok(new { message = "Cập nhật số lượng thành công" });
	}

	// PATCH: api/phong-chuc-nang/{phongChucNangId}/thiet-bi/{id}/trang-thai
	[HttpPut("{id:int}/trang-thai")]
	public async Task<IActionResult> ChuyenTrangThai(
		int id,
		[FromBody] TinhTrang trangThaiMoi)
	{
		var success = await _service.ChuyenTrangThaiAsync(id, trangThaiMoi);
		if (!success)
			return NotFound(new { message = "Không tìm thấy thiết bị phòng chức năng" });

		return Ok(new { message = "Chuyển trạng thái thành công" });
	}

	// DELETE: api/phong-chuc-nang/{phongChucNangId}/thiet-bi/{id}
	[HttpDelete("{id:int}")]
	public async Task<IActionResult> Delete(int id)
	{
		var success = await _service.DeleteAsync(id);
		if (!success)
			return NotFound(new { message = "Không tìm thấy thiết bị phòng chức năng" });

		return Ok(new { message = "Xóa thiết bị phòng chức năng thành công" });
	}
}
