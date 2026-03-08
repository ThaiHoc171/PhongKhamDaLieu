using System;
using System.Collections.Generic;

namespace Clinic.WinForms.DTOs
{
	public class ToaThuocRequestDTO
	{
		public int PhienKhamID { get; set; }
		public int NhanVienKeDonID { get; set; }
		public string GhiChu { get; set; }
		public List<ChiTietToaThuocRequestDTO> Thuoc { get; set; }
	}

	public class ChiTietToaThuocRequestDTO
	{
		public int ThuocID { get; set; }
		public string LieuDung { get; set; }
		public int SoLuong { get; set; }
	}
	public class ToaThuocResponseDTO
	{
		public int ToaThuocID { get; set; }
		public DateTime NgayLap { get; set; }
		public string NguoiLap { get; set; }
		public string GhiChu { get; set; }
	}

	public class ChiTietToaThuocResponseDTO
	{
		public string TenThuoc { get; set; }
		public string LieuDung { get; set; }
		public int SoLuong { get; set; }
	}

}
