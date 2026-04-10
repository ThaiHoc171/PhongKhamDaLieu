namespace WPF.Models;

public class CaKhamGenerate
{
	public DateTime TuNgay { get; set; }
	public DateTime DenNgay { get; set; }
}
public class CaKhamTrangThaiDTO
{
	public string TrangThai { get; set; } = default!;
	public string? GhiChu { get; set; }
}
public class CaKhamRegister
{
	public int ThongTinID { get; set; }
	public string LyDoKham { get; set; } = string.Empty;
	public DateTime NgayDat { get; set; }
	public string? GhiChu { get; set; }
}
public class AssignLichLamViecReport
{
	public DateTime TuNgay { get; set; }
	public DateTime DenNgay { get; set; }
	public int TongCaChuaGan { get; set; }
	public int SoCaDaCapNhat { get; set; }
	public bool DaThucHien => SoCaDaCapNhat > 0;
	public string Message { get; set; } = string.Empty;
}
public class CaKhamListReadModel
{
	public int CaKhamID { get; set; }
	public DateTime NgayKham { get; set; }
	public string TenKhungGio { get; set; } = string.Empty;
	public string? TenPhong { get; set; }
	public string? HoTen { get; set; }
	public string? LyDoKham { get; set; }
	public string TrangThai { get; set; } = string.Empty;
}
public class CaKhamReadModel
{
	public int CaKhamID { get; set; }
	public string LoaiCaKham { get; set; } = string.Empty;
	public int? LichLamViecID { get; set; }
	public string TenKhungGio { get; set; } = string.Empty;
	public string? TenPhong { get; set; }
	public string? HoTen { get; set; }
	public string? LyDoKham { get; set; }
	public string TrangThai { get; set; } = string.Empty;
	public DateTime? NgayDat { get; set; }
	public DateTime NgayKham { get; set; }
	public string? GhiChu { get; set; }
}