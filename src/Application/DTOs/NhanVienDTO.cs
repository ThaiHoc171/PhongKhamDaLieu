using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class TaoNhanVienDTO
{
	public ThemThongTinCaNhanDTO? ThongTin { get; set; }
	public int ChucVuID { get; set; }
	public DateTime? NgayVaoLam { get; set; }
	public string? BangCap { get; set; }
	public string? KinhNghiem { get; set; }
}

public class CapNhatNhanVienDTO
{
	public int ChucVuID { get; set; }
	public DateTime? NgayVaoLam { get; set; }
	public string? BangCap { get; set; }
	public string? KinhNghiem { get; set; }
}
public class NhanVienResponseDTO
{
	public int NhanVienID { get; set; }
	public string? HoTen { get; set; }
	public string? Email { get; set; }
	public string? TenChucVu { get; set; }
	public string? TrangThai { get; set; }
}

