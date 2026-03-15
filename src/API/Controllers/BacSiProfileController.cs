using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Application.DTO;
using Application.Services;
using System.Security.Claims;
namespace API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BacSiProfileController : ControllerBase
{
	private readonly BacSiProfileService _service;
	public BacSiProfileController(BacSiProfileService service)
	{
		_service = service;
	}
    [Authorize(Roles = "Admin")]
    [HttpGet("{nhanVienID}")]
	public async Task<IActionResult> Get(int nhanVienID)
	{
		var result = await _service.GetByNhanVienAsync(nhanVienID);
		return result == null ? NotFound() : Ok(result);
	}
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _service.GetAllAsync();
        return Ok(data);
    }
    [Authorize(Policy = "BacSiOnly")]
	[HttpPost("{nhanVienID}")]
	public async Task<IActionResult> Create(int nhanVienID, BacSiProfileRequestDTO dto)
	{
		if (!CoQuyenChinhChu(nhanVienID))
			return Forbid();
		try
		{
			await _service.TaoMoiAsync(nhanVienID, dto);
			return Ok(new { message = "Tạo hồ sơ bác sĩ thành công" });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(ex.Message);
		}
	}
	[Authorize(Policy = "BacSiOnly")]
	[HttpPut("{nhanVienID}")]
	public async Task<IActionResult> Update(int nhanVienID, BacSiProfileRequestDTO dto)
	{
		if (!CoQuyenChinhChu(nhanVienID))
			return Forbid();
		try
		{
			await _service.CapNhatAsync(nhanVienID, dto);
			return Ok(new { message = "Cập nhật hồ sơ bác sĩ thành công" });
		}
		catch (InvalidOperationException ex)
		{
			return BadRequest(ex.Message);
		}
	}
	private bool CoQuyenChinhChu(int nhanVienID)
	{
		// Admin full quyền
		if (User.IsInRole("Admin"))
			return true;
		var claim = User.FindFirst("NhanVienID");
		if (claim == null)
			return false;
		return int.Parse(claim.Value) == nhanVienID;
	}
}
