using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class TaoCaKhamDTO
	{
		public DateTime NgayKham { get; set; }
		public DateTime NgayKetThuc { get; set; }
	}
	public class DangKyCaKhamDTO
	{
		public int ThongTinID { get; set; }
		public string LyDoKham { get; set; }
		public DateTime NgayDat { get; set; }
		public string GhiChu { get; set; }
	}
	public class CaKhamListReadModel
	{
		public int CaKhamID { get; set; }
		public string TenKhungGio { get; set; }
		public string TenPhong { get; set; }
		public string HoTen { get; set; }
		public string LyDoKham { get; set; }
		public string TrangThai { get; set; }
	}
	public class CaKhamReadModel
	{
		public int CaKhamID { get; set; }
		public string LoaiCaKham { get; set; }
		public int LichLamViecID { get; set; }
		public string TenKhungGio { get; set; }
		public string TenPhong { get; set; }
		public string HoTen { get; set; }
		public string LyDoKham { get; set; }
		public string TrangThai { get; set; }
		public DateTime? NgayDat { get; set; }
		public DateTime NgayKham { get; set; }
		public string GhiChu { get; set; }
	}

}
