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
		public TaiKhoanController(TaiKhoanService taiKhoan)
		{
			_taiKhoan = taiKhoan;
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
		[HttpGet("DangKy")]
		public IActionResult DangKy([FromBody] TaiKhoanDTO taikhoan)
		{
			var result = _taiKhoan.DangKy(taikhoan);
			if (result)
			{
				return Ok("Đăng ký thành công.");
			}
			return BadRequest("Email đã tồn tại.");
		}
		[HttpGet("DoiMatKhau")]
		public IActionResult DoiMatKhau([FromBody] int ID,string password, string newpassword)
		{
			var result = _taiKhoan.DoiMatKhau(ID,password,newpassword);
			if (result)
			{
				return Ok("Đổi mật khẩu thành công.");
			}
			return BadRequest("Đổi mật khẩu thất bại.");
		}
		[HttpGet("ResetMatKhau")]
		public IActionResult ResetMatKhau([FromBody] int ID, string newpassword)
		{
			var result = _taiKhoan.ResetMatKhau(ID,newpassword);
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
