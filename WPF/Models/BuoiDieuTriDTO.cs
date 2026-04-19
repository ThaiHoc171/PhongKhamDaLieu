
namespace WPF.Models;
public class BuoiDieuTriRequestDTO
{
	public int LieuTrinhID { get; set; }
	public int CaKhamID { get; set; }
}
public class BuoiDieuTriUpdateDTO
{
	public int? NhanVienID { get; set; }
	public DateTime? NgayThucHien { get; set; }
	public string? GhiChu { get; set; }
}
public class BuoiDieuTriReadModel
{
	public int BuoiDieuTriID { get; set; }
	public int LieuTrinhID { get; set; }
	public int CaKhamID { get; set; }
	public int SoBuoi { get; set; }
	public DateTime? NgayDuKien { get; set; }
	public DateTime? NgayThucHien { get; set; }
	public int? NhanVienID { get; set; }
	public string TrangThai { get; set; } = default!;
	public string? GhiChu { get; set; }
	public string? HinhAnhJSON { get; set; }
}
public class BuoiDieuTriListReadModel
{
	public int BuoiDieuTriID { get; set; }
	public int LieuTrinhID { get; set; }
	public int CaKhamID { get; set; }
	public int SoBuoi { get; set; }
	public DateTime? NgayDuKien { get; set; }
	public string TrangThai { get; set; } = default!;
}