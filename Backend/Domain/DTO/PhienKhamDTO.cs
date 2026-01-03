using System;

namespace Domain.DTO
{
	public class ListPhienKhamDTO
	{
		public int PhienKhamID { get; set; }
		public int CaKhamID { get; set; }
		public string TenBenhNhan { get; set; }
		public string TenBacSi { get; set; }
		public string TrieuChung { get; set; }
		public DateTime NgayKham { get; set; }
		public string TrangThai { get; set; }
	}
	public class PhienKhamDetailDTO
	{
		public int PhienKhamID { get; set; }
		public int CaKhamID { get; set; }
		public int BenhNhanID { get; set; }
		public string TenBenhNhan { get; set; }
		public int BacSiID { get; set; }
		public string TenBacSi { get; set; }
		public string TenPhongChucNang { get; set; }
		public string TrieuChung { get; set; }
		public string GhiChu { get; set; }
		public string HinhAnhJSON { get; set; }
		public string ChanDoanCuoi { get; set; }
		public DateTime NgayKham { get; set; }
		public string TrangThai { get; set; }
	}
	public class TaoPhienKhamDTO
	{
		public int CaKhamID { get; set; }
		public int BenhNhanID { get; set; }
		public int NhanVienID { get; set; }
		public int PhongChucNangID { get; set; }
		public DateTime NgayKham { get; set; }
	}
	public class KetThucPhienKhamDTO
	{
		public int PhienKhamID { get; set; }
		public string TrieuChung { get; set; }
		public string GhiChu { get; set; }
		public string HinhAnhJSON { get; set; }
		public string ChanDoanCuoi { get; set; }
	}
}
