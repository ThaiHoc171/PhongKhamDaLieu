using System;

namespace DTO
{
	public class BacSiProfileDTO
	{
		public int BacSiProfileID { get; set; }
		public int NhanVienID { get; set; }
		public string GioiThieu { get; set; }
		public string ChuyenMon { get; set; }
		public string ThanhTuu { get; set; }
		public string HinhAnh { get; set; }
		public string KinhNghiem { get; set; }
		public DateTime NgayCapNhat { get; set; }
	}
}
