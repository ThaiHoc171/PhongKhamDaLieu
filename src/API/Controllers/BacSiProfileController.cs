using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/bacsiprofile")]
[Authorize]
public class BacSiProfileController : ControllerBase
{
    private readonly BacSiProfileService _service;
    public BacSiProfileController(BacSiProfileService service)
    {
        _service = service;
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> TaoMoi([FromBody] BacSiProfileRequestDTO dto)
    {
        var result = await _service.TaoMoiAsync(dto);
        return Ok(result);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> CapNhat(
        int id,
        [FromBody] BacSiProfileUpdateDTO dto)
    {
        var result = await _service.CapNhatAsync(id, dto);
        return Ok(result);
    }
    [HttpGet]
    public async Task<IActionResult> GetPaged(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _service.GetByIdAsync(id);
        return Ok(result);
    }
    [HttpGet("nhanvien/{nhanVienId}")]
    public async Task<IActionResult> GetByNhanVien(int nhanVienId)
    {
        var result = await _service.GetByNhanVienAsync(nhanVienId);
        return Ok(result);
    }
    [HttpGet("search")]
    public async Task<IActionResult> Search(
        [FromQuery] string keyword,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await _service.SearchAsync(keyword, pageNumber, pageSize);
        return Ok(result);
    }
}