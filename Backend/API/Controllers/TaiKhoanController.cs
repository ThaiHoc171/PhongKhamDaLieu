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
		public class LoginRequest
		{
			public string Email { get; set; }
			public string PasswordHash { get; set; }
		}
	}
}
