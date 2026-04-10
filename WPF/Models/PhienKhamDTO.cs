

namespace WPF.Models;

public class PhienKhamUpdateDTO
{
	public string? TrieuChung { get; set; }
	public string? GhiChu { get; set; }
	public string? HinhAnh { get; set; }
}
public class PhienKhamRequestDTO
{
	public string? TrieuChung { get; set; }
	public string? GhiChu { get; set; }
	public int? PhongChucNangID { get; set; }
	public string? HinhAnh { get; set; }
}
public class PhienKhamReadModel
{
	public int PhienKhamID { get; set; }
	public int CaKhamID { get; set; }
	public NameHelper BenhNhan { get; init; } = default!;
	public NameHelper NhanVien { get; init; } = default!;
	public int? PhongChucNangID { get; set; }
	public string? TrieuChung { get; set; }
	public string? GhiChu { get; set; }
	public string? HinhAnh { get; set; }
	public string? ChanDoanCuoi { get; set; }
	public DateTime NgayKham { get; set; }
	public string TrangThai { get; set; } = default!;
}
public class PhienKhamReadListModel
{
	public int PhienKhamID { get; set; }
	public int CaKhamID { get; set; }
	public string? BenhNhan { get; set; }
	public string? NhanVien { get; set; }
	public DateTime NgayKham { get; set; }
	public string TrangThai { get; set; } = default!;
	public string? ChanDoanCuoi { get; set; } = default!;
}