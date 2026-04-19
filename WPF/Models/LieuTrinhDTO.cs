namespace WPF.Models;
public class LieuTrinhDieuTriRequestDTO
{
	public int PhienKhamID { get; set; }
	public string TenLieuTrinh { get; set; } = string.Empty;
	public int TongSoBuoi { get; set; }
	public string? GhiChu { get; set; }
	public DateTime NgayBatDau { get; set; }
}
public class LieuTrinhDieuTriUpdateDTO
{
	public string TenLieuTrinh { get; set; } = string.Empty;
	public int TongSoBuoi { get; set; }
	public DateTime NgayKetThuc { get; set; }
}
public class LieuTrinhStatusDTO
{
	public string? TrangThai { get; set; }
	public string? GhiChu { get; set; }
}
public class LieuTrinhDieuTriReadModel
{
	public int LieuTrinhID { get; set; }
	public NameHelper BenhNhan { get; init; } = default!;
	public int PhienKhamID { get; set; }
	public string TenLieuTrinh { get; set; } = default!;
	public int TongSoBuoi { get; set; }
	public string? TrangThai { get; set; }
	public string? GhiChu { get; set; }
	public DateTime? NgayBatDau { get; set; }
	public DateTime? NgayKetThuc { get; set; }
}
public class LieuTrinhDieuTriListReadModel
{
	public int LieuTrinhID { get; set; }
	public string TenLieuTrinh { get; set; } = default!;
	public string BenhNhan { get; set; } = string.Empty;
	public int TongSoBuoi { get; set; }
	public string? TrangThai { get; set; }
	public DateTime? NgayBatDau { get; set; }
	public DateTime? NgayKetThuc { get; set; }
}
