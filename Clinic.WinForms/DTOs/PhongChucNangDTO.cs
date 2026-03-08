using System;

namespace Clinic.WinForms.DTOs
{
	public class PhongChucNangResponseDTO
	{
		public int Id { get; set; }
		public string TenPhong { get; set; } = default;
		public string LoaiPhong { get; set; }
		public string MoTa { get; set; }
		public string TrangThai { get; set; } = default;
		public DateTime NgayTao { get; set; }
		public DateTime? NgayCapNhat { get; set; }
	}
	public class PhongChucNangRequestDTO
	{
		public string TenPhong { get; set; } = default;
		public string LoaiPhong { get; set; }
		public string MoTa { get; set; }
	}
	public class PCNUpdateDTO
	{
		public string TenPhong { get; set; } = default;
		public string LoaiPhong { get; set; }
		public string MoTa { get; set; }
	}
}
