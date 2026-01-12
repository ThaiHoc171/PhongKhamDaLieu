using Microsoft.AspNetCore.Mvc;
using Services;
using Domain.DTO;

namespace API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PhienKhamController : ControllerBase
	{
		private readonly PhienKhamService _service;
		public PhienKhamController(PhienKhamService service)
		{
			_service = service;
		}
		[HttpGet("DanhSachPhienKham")]
		public IActionResult DanhSach(int? Id)
		{
			return new OkObjectResult(_service.DanhSachPhienKham(Id));
		}
		[HttpGet("Phienkham{id}")]
		public IActionResult PhienKham(int Id)
		{
			var result = _service.LayPhienKham(Id);
			return Ok(result);
		}
		[HttpPost("TaoPhienKham")]
		public IActionResult TaoPhienKham([FromBody] TaoPhienKhamDTO pk)
		{
			var success = _service.TaoPhienKham(pk);
			if (success)
				return Ok();
			else
				return BadRequest("Có lỗi khi tạo Phiên Khám.");
		}
		[HttpPost("KetThucPhienKham")]
		public IActionResult KetThucPhienKham([FromBody] KetThucPhienKhamDTO pk)
		{
			var success = _service.KetThucPhienKham(pk);
			if (success)
				return Ok();
			else
				return BadRequest("Có lỗi khi kết thúc Phiên Khám.");
		}
		[HttpPut("HuyPhienKham{id}")]
		public IActionResult HuyPhienKham(int id)
		{
			var success = _service.HuyPhienKham(id);
			if (success)
				return Ok();
			else
				return BadRequest("Có lỗi khi hủy Phiên Khám.");
		}

	}
}
