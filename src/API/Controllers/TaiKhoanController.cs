using Microsoft.AspNetCore.Mvc;
using Application.DTOs;
using Services;
using Microsoft.AspNetCore.Authorization;
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
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequestDTO dto)
        {
            var result = await _taiKhoanService.RefreshTokenAsync(dto.RefreshToken);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }
        // POST: api/TaiKhoan/dangnhap
        [AllowAnonymous]
		[HttpPost("dangnhap")]
		public async Task<IActionResult> DangNhap([FromBody] LoginRequestDTO login)
		{
			var result = await _taiKhoanService.DangNhapAsync(login);
			if (result == null)
				return Unauthorized(new { message = "Email hoặc mật khẩu không đúng." });
			return Ok(result);
		}
		// POST: api/TaiKhoan/dangky
		[AllowAnonymous]
		[HttpPost("dangky")]
		public async Task<IActionResult> DangKy([FromBody] ThemTaiKhoanDTO dto)
		{
			await _taiKhoanService.DangKyAsync(dto);
			return Ok(new { message = "Đăng ký thành công." });
		}
		[Authorize]
		// PUT: api/TaiKhoan/{id}/doimatkhau
		[HttpPut("{id}/doimatkhau")]
		public async Task<IActionResult> DoiMatKhau(int id, [FromBody] DoiMatKhauDTO dto)
		{
			var result = await _taiKhoanService.DoiMatKhauAsync(id, dto);
			if (!result)
				return BadRequest(new
				{
					message = "Đổi mật khẩu thất bại. Mật khẩu cũ không đúng hoặc tài khoản không tồn tại."
				});
			return Ok(new { message = "Đổi mật khẩu thành công." });
		}
		[Authorize(Roles = "Admin")]
		// PUT: api/TaiKhoan/{id}/resetmatkhau
		[HttpPut("{id}/resetmatkhau")]
		public async Task<IActionResult> ResetMatKhau(int id)
		{
			var result = await _taiKhoanService.ResetMatKhauAsync(id);
			if (!result)
				return NotFound(new { message = "Tài khoản không tồn tại." });
			return Ok(new
			{
				message = "Reset mật khẩu thành công. Mật khẩu đã được đặt về mặc định."
			});
		}
		// GET: api/TaiKhoan
		[Authorize(Roles = "Admin")]
		[HttpGet]
		public async Task<IActionResult> LayTatCaTaiKhoan()
		{
			var result = await _taiKhoanService.LayTatCaAsync();
			return Ok(result);
		}
		// GET: api/TaiKhoan/{id}
		[Authorize]
		[HttpGet("{id}")]
		public async Task<IActionResult> LayTaiKhoanTheoId(int id)
		{
			var result = await _taiKhoanService.LayTaiKhoanTheoIdAsync(id);
			if (result == null)
				return NotFound(new { message = "Tài khoản không tồn tại." });
			return Ok(result);
		}
		// PUT: api/TaiKhoan/{id}/capnhattrangthai
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}/capnhattrangthai")]
		public async Task<IActionResult> CapNhatTrangThai(int id, [FromBody] string trangThaiMoi)
		{
			var result = await _taiKhoanService.CapNhatTrangThaiAsync(id, trangThaiMoi);
			if (!result)
				return NotFound(new { message = "Tài khoản không tồn tại." });
			return Ok(new { message = "Cập nhật trạng thái tài khoản thành công." });
		}
	}
}
