using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;
using System.Collections.Generic;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TaiKhoanController : ControllerBase
	{
		private readonly TaiKhoanService _taiKhoanService;

		public TaiKhoanController(TaiKhoanService taiKhoanService)
		{
			_taiKhoanService = taiKhoanService;
		}
		// Đăng nhập
		// POST: api/TaiKhoan/dangnhap
		[HttpPost("dangnhap")]
		public ActionResult<TaiKhoanResponseDTO> DangNhap([FromBody] LoginRequestDTO login)
		{
			var result = _taiKhoanService.DangNhap(login);
			if (result == null)
				return Unauthorized("Email hoặc mật khẩu không đúng.");
			return Ok(result);
		}
		// Đăng ký tài khoản
		// POST: api/TaiKhoan/dangky
		[HttpPost("dangky")]
		public IActionResult DangKy([FromBody] ThemTaiKhoanDTO tk)
		{
			_taiKhoanService.DangKy(tk);
			return Ok("Đăng ký thành công.");
		}
		// Đổi mật khẩu
		// PUT: api/TaiKhoan/{id}/doimatkhau
		[HttpPut("{id}/doimatkhau")]
		public IActionResult DoiMatKhau(int id, [FromBody] DoiMatKhauDTO tk)
		{
			var result = _taiKhoanService.DoiMatKhau(id, tk);
			if (!result)
				return BadRequest("Đổi mật khẩu thất bại. Mật khẩu cũ không đúng hoặc tài khoản không tồn tại.");
			return Ok("Đổi mật khẩu thành công.");
		}
		// Cập nhật trạng thái
		// PUT: api/TaiKhoan/{id}/trangthai
		[HttpPut("{id}/trangthai")]
		public IActionResult CapNhatTrangThai(int id, [FromBody] string trangThaiMoi)
		{
			var result = _taiKhoanService.CapNhatTrangThai(id, trangThaiMoi);
			if (!result)
				return BadRequest("Cập nhật trạng thái thất bại. Tài khoản không tồn tại.");
			return Ok("Cập nhật trạng thái thành công.");
		}
		// Lấy tất cả tài khoản
		// GET: api/TaiKhoan
		[HttpGet]
		public ActionResult<List<TaiKhoanResponseDTO>> LayTatCaTaiKhoan()
		{
			var result = _taiKhoanService.LayTatCaTaiKhoan();
			return Ok(result);
		}
		// Lấy tài khoản theo Id
		// GET: api/TaiKhoan/{id}
		[HttpGet("{id}")]
		public ActionResult<TaiKhoanResponseDTO> LayTaiKhoanTheoId(int id)
		{
			var result = _taiKhoanService.LayTaiKhoanTheoId(id);
			if (result == null)
				return NotFound("Tài khoản không tồn tại.");
			return Ok(result);
		}
	}
}
