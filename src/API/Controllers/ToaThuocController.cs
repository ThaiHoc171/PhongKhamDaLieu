using Application.DTO;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ToaThuocController : ControllerBase
	{
		private readonly ToaThuocService _toaThuocService;

		public ToaThuocController(ToaThuocService toaThuocService)
		{
			_toaThuocService = toaThuocService;
		}

		/// <summary>
		/// Tạo toa thuốc mới cho phiên khám
		/// </summary>
		[HttpPost]
		public async Task<IActionResult> TaoToaThuoc([FromBody] TaoToaThuocDTO dto)
		{
			if (dto == null || dto.Thuoc == null || !dto.Thuoc.Any())
			{
				return BadRequest(new
				{
					message = "Danh sách thuốc không được rỗng."
				});
			}

			try
			{
				var toaThuocID = await _toaThuocService.TaoToaThuocAsync(dto);

				return Ok(new
				{
					message = "Tạo toa thuốc thành công.",
					toaThuocID
				});
			}
			catch (ArgumentException ex)
			{
				return BadRequest(new { message = ex.Message });
			}
			catch
			{
				return StatusCode(500, new
				{
					message = "Lỗi hệ thống khi tạo toa thuốc."
				});
			}
		}

		/// <summary>
		/// Xem chi tiết toa thuốc theo phiên khám
		/// </summary>
		[HttpGet("phien-kham/{phienKhamID}")]
		public async Task<IActionResult> XemTheoPhienKham(int phienKhamID)
		{
			var result =
				await _toaThuocService.XemTheoPhienKhamAsync(phienKhamID);

			if (result == null)
			{
				return NotFound(new
				{
					message = "Phiên khám chưa có toa thuốc."
				});
			}

			return Ok(result);
		}
	}
}
