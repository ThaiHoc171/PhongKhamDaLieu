using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class ThemBenhNhanDTO
{
	public int? ThongTinID { get; set; } // nếu null thì tạo ThongTinCaNhan mới
	public string? HoTen { get; set; }
	public DateTime? NgaySinh { get; set; }
	public string? GioiTinh { get; set; }
	public string? SDT { get; set; }
	public string? EmailLienHe { get; set; }
	public string? DiaChi { get; set; }
	public string? Avatar { get; set; }
	public string? LoaiDa { get; set; }
	public string? GhiChu { get; set; }
}

public class BenhNhanResponseDTO
{
	public int BenhNhanID { get; set; }
	public int ThongTinID { get; set; }
	public string? HoTen { get; set; }
	public string? SDT { get; set; }
	public string? EmailLienHe { get; set; }
	public string? LoaiDa { get; set; }
	public string? TrangThaiTheoDoi { get; set; }
	public string? GhiChu { get; set; }
}

public class CapNhatBenhNhanDTO
{
	public string LoaiDa { get; set; }
	public string GhiChu { get; set; }
}
