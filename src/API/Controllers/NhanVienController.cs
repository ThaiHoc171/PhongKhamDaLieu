using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class NhanVienController : ControllerBase
	{
		private readonly NhanVienService _service;

		public NhanVienController(NhanVienService service)
		{
			_service = service;
		}

		// POST: api/NhanVien
		[HttpPost]
		public async Task<IActionResult> TaoNhanVien([FromBody] TaoNhanVienDTO dto)
		{
			try
			{
				await _service.TaoNhanVienAsync(dto);
				return Ok(new { message = "Tạo nhân viên thành công." });
			} catch (Exception ex)
			{
				return BadRequest(new { message = "Tạo nhân viên thất bại.", Message = ex.Message });
			}
		}

		// PUT: api/NhanVien/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> CapNhatNhanVien(
			int id,
			[FromBody] CapNhatNhanVienDTO dto)
		{
			var result = await _service.CapNhatNhanVienAsync(id, dto);

			if (!result)
				return NotFound(new { message = "Nhân viên không tồn tại." });

			return Ok(new { message = "Cập nhật nhân viên thành công." });
		}

		// PUT: api/NhanVien/{id}/trangthai
		[HttpPut("{id}/trangthai")]
		public async Task<IActionResult> CapNhatTrangThai(
			int id,
			[FromBody] string trangThai)
		{
			var result = await _service.CapNhatTrangThaiAsync(id, trangThai);

			if (!result)
				return NotFound(new { message = "Nhân viên không tồn tại." });

			return Ok(new { message = "Cập nhật trạng thái thành công." });
		}

		// GET: api/NhanVien
		[HttpGet]
		public async Task<IActionResult> LayDanhSach()
		{
			var list = await _service.LayDanhSachAsync();
			return Ok(list);
		}
		[HttpGet("search")]
		public async Task<IActionResult> TimKiemNhanVien([FromQuery] string keyword)
		{
			var list = await _service.SearchAsync(keyword);
			return Ok(list);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> LayNhanVienById(int id)
		{
			var nv = await _service.LayTheoIDAsync(id);
			if (nv == null)
				return NotFound(new { message = "Nhân viên không tồn tại." });
			return Ok(nv);
		}
	}
}
