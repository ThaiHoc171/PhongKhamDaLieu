using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class NgayNghiResponseDTO
	{
		public int NgayNghiID { get; set; }
		public int NhanVienID { get; set; }
		public DateTime Ngay { get; set; }
		public string LyDo { get; set; }
	}
	public class NgayNghiRequestDTO
	{
		public int NhanVienID { get; set; }
		public DateTime Ngay { get; set; }
		public string LyDo { get; set; }
	}
}
