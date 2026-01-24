namespace Application.ReadModels;
public class PhienKhamReadModel 
{ 
	public int PhienKhamID { get; set; } 
	public int CaKhamID { get; set; } 
	public int BenhNhanID { get; set; } 
	public string TenBenhNhan { get; set; } 
	public int NhanVienID { get; set; } 
	public string TenNhanVien { get; set; } 
	public DateTime NgayKham { get; set; } 
	public string TrangThai { get; set; } = default!; 
	public string? ChuanDoanCuoi { get; set; } 
}
public class PhienKhamBenhReadModel
{
	public int Id { get; init; }
	public int PhienKhamID { get; init; }
	public int LoaiBenhID { get; init; }
	public string TenLoaiBenh { get; init; } = default!;
	public string LoaiChanDoan { get; init; } = default!;
	public string? GhiChu { get; init; }
}
public class PhienKhamCLSReadModel
{
	public int Id { get; init; }
	public int PhienKhamID { get; init; }
	public int CLSID { get; init; }
	public string TenCLS { get; init; } = default!;
	public string? KetQua { get; init; }
	public string? FiledinhKem { get; init; }
	public DateTime NgayThucHien { get; init; }
	public int NhanVienThucHienID { get; init; }
	public string NhanVienThucHienHoTen { get; init; } = default!;
	public string? GhiChu { get; init; }
	public string TrangThai { get; init; } = default!;
}
public class PhienKhamThietBiReadModel
{
	public int Id { get; init; }
	public int PhienKhamID { get; init; }
	public int ThietBiID { get; init; }
	public string TenThietBi { get; init; } = default!;
	public int SoLuong { get; init; }
	public string? GhiChu { get; init; }
}