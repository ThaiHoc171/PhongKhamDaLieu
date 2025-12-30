using System;
using Services;
using Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.Data;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class TaiKhoanController : ControllerBase
	{
		private readonly TaiKhoanService _taiKhoan;
		private readonly IConfiguration _config;
		public TaiKhoanController(TaiKhoanService taiKhoan, IConfiguration config)
		{
			_taiKhoan = taiKhoan;
			_config = config;
		}
		[HttpPost("DangNhap")]
		public IActionResult DangNhap([FromBody] LoginRequest request)
		{
			var result = _taiKhoan.DangNhap(request.Email, request.PasswordHash);
			if (result != null)
			{
				return Ok(result);
			}
			return Unauthorized("Tài Khoản hoặc mật khẩu không đúng.");
		}
		[HttpGet("DanhSach")]
		public IActionResult DanhSach()
		{
			var result = _taiKhoan.LayDanhSachTaiKhoan();
			return Ok(result);
		}
		[HttpPost("DangKy")]
		public IActionResult DangKy([FromBody] TaiKhoanCreateDTO taikhoan)
		{
			var result = _taiKhoan.DangKyTaiKhoan(taikhoan.Email, taikhoan.MatKhau, taikhoan.VaiTro);
			if (result)
			{
				return Ok("Đăng ký thành công.");
			}
			return BadRequest("Email đã tồn tại.");
		}
		[HttpPut("DoiMatKhau")]
		public IActionResult DoiMatKhau([FromBody] DoiMatKhauDTO tk)
		{
			var result = _taiKhoan.DoiMatKhau(tk.TaiKhoanID,tk.MatKhauCu,tk.MatKhauMoi);
			if (result)
			{
				return Ok("Đổi mật khẩu thành công.");
			}
			return BadRequest("Đổi mật khẩu thất bại.");
		}
		[HttpPut("ResetMatKhau")]
		public IActionResult ResetMatKhau([FromBody] int ID)
		{
			string defaultPassword = _config["DefaultPassword"];
			var result = _taiKhoan.ResetMatKhau(ID, defaultPassword);
			if (result)
			{
				return Ok("Reset mật khẩu thành công.");
			}
			return BadRequest("Reset mật khẩu thất bại.");
		}
		public class LoginRequest
		{
			public string Email { get; set; }
			public string PasswordHash { get; set; }
		}
	}
}
