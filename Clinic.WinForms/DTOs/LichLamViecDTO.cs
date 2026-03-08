using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class LichLamViecResponseDTO
	{
		public int LichLamViecID { get; set; }
		public string TenChucVu { get; set; } = string.Empty;
		public int PhongChucNangID { get; set; }
		public NameHelper NhanVien { get; set; } = default;
		public DateTime Ngay { get; set; }
		public int CaLamViec { get; set; }
		public string GhiChu { get; set; }
	}


	public class LichLamViecCaNhanResponseDTO
	{
		public int Page { get; set; }
		public DateTime TuanBatDau { get; set; }
		public DateTime TuanKetThuc { get; set; }
		public List<LichLamViecItemDTO> LichLamViecs { get; set; } = default;
	}

	public class LichLamViecItemDTO
	{
		public int LichLamViecID { get; set; }
		public NameHelper NhanVien { get; set; } = default;
		public DateTime Ngay { get; set; }
		public int CaLamViec { get; set; }
		public string GhiChu { get; set; }
	}
	public class LichLamViecRequestDTO
	{
		public int Thang { get; set; }
		public int Nam { get; set; }

		public List<LichLamViecCreateItemDTO> LichLamViecs { get; set; }
			= new List<LichLamViecCreateItemDTO>();
	}

	public class LichLamViecCreateItemDTO
	{
		public int NhanVienID { get; set; }
		public int ChucVuID { get; set; }
		public DateTime Ngay { get; set; }
		public int CaLamViec { get; set; }
		public string GhiChu { get; set; }
	}
}
