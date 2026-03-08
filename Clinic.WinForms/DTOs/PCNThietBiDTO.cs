using System;

namespace Clinic.WinForms.DTOs
{
	public class PCNThietBiResponseDTO
	{
		public int PCN_TB_ID { get; set; }
		public int PhongChucNangID { get; set; }
		public NameHelper ThietBi { get; set; } = null;
		public int TongSoLuong { get; set; }
	}

	public class PCNThietBiCreateDTO
	{
		public int PhongChucNangID { get; set; }
		public int ThietBiID { get; set; }
	}


	public class ChiTietPCNThietBiResponseDTO
	{
		public int ChiTietID { get; set; }
		public string MaTaiSan { get; set; } = default;
		public DateTime NgayNhap { get; set; }
		public string TinhTrang { get; set; }
		public string GhiChu { get; set; }
	}

	public class ChiTietPCNThietBiCreateDTO
	{
		public int PhongChucNangID { get; set; }
		public int ThietBiID { get; set; }
		public string MaTaiSan { get; set; } = default;
		public string GhiChu { get; set; }
	}

	public class ChiTietPCNThietBiUpdateDTO
	{
		public string TinhTrang { get; set; }
		public string GhiChu { get; set; }
	}
}
