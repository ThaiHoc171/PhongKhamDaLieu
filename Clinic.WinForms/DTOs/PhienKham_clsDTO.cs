using System;

namespace Clinic.WinForms.DTOs
{
	public class PhienKham_clsRequestDTO
	{
		public int PhienKhamID { get; set; }
		public int CLSID { get; set; }
		public int NhanVienChiDinhID { get; set; }
		public string GhiChu { get; set; }
	}
	public class PhienKham_ClsListReadModel
	{
		public int PhienKhamCLSID { get; set; }
		public string TenCLS { get; set; }
		public string TrangThai { get; set; } = default;
		public string KetQua { get; set; }
		public DateTime? NgayThucHien { get; set; }
		public string GhiChu { get; set; }
	}
	public class PhienKham_ClsReadModel
	{
		public int PhienKhamCLSID { get; set; }
		public string TenCLS { get; set; }
		public string TrangThai { get; set; } = default;
		public string KetQua { get; set; }
		public string FileDinhKem { get; set; }
		public DateTime? NgayThucHien { get; set; }
		public string NhanVienChiDinh { get; set; } = default;
		public string NhanVienThucHien { get; set; }
		public string GhiChu { get; set; }
	}
	public class NhanThucHienCLSDTO
	{
		public int NhanVienThucHienID { get; set; }
	}


	public class CapNhatKetQuaCLSDTO
	{
		public string KetQua { get; set; }
		public string FileDinhKem { get; set; }
		public string GhiChu { get; set; }
	}

}
