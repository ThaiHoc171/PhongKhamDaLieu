using System;

namespace Clinic.WinForms.DTOs
{
	public class PhienKhamRequestDTO
	{
		public string TrieuChung { get; set; }
		public string GhiChu { get; set; }
		public int? PhongChucNangID { get; set; }
		public string HinhAnhJSON { get; set; }
	}
	public class PhienKhamReadModel
	{
		public int PhienKhamID { get; set; }
		public int CaKhamID { get; set; }
		public string BenhNhan { get; set; }
		public string NhanVien { get; set; }
		public int? PhongChucNangID { get; set; }
		public string TrieuChung { get; set; }
		public string GhiChu { get; set; }
		public string HinhAnhJSON { get; set; }
		public string ChanDoanCuoi { get; set; }
		public DateTime NgayKham { get; set; }
		public string TrangThai { get; set; }
	}
	public class PhienKhamListReadModel
	{
		public int PhienKhamID { get; set; }
		public int CaKhamID { get; set; }
		public string BenhNhan { get; set; }
		public string NhanVien { get; set; }
		public DateTime NgayKham { get; set; }
		public string TrangThai { get; set; }
		public string ChanDoanCuoi { get; set; }
	}
}
