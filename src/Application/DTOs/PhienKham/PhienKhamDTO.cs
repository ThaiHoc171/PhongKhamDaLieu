namespace Application.DTOs;

public class PhienKhamUpdateDTO
{
	public string? TrieuChung { get; set; }
	public string? GhiChu { get; set; }
	public int? PhongChucNangID { get; set; }
	public string? HinhAnhJSON { get; set; }
}
public class PhienKhamReadModel
{
	public int PhienKhamID { get; set; }
	public int CaKhamID { get; set; }
	public NameResponseDTO BenhNhan { get; init; } = default!;
	public string? NhanVien { get; set; }
	public int? PhongChucNangID { get; set; }
	public string? TrieuChung { get; set; }
	public string? GhiChu { get; set; }
	public string? HinhAnhJSON { get; set; }
	public string? ChanDoanCuoi { get; set; }
	public DateTime NgayKham { get; set; }
	public string TrangThai { get; set; } = default!;
}
public class PhienKhamListReadModel
{
	public int PhienKhamID { get; set; }
	public int CaKhamID { get; set; }
	public string? BenhNhan { get; set; }
	public string? NhanVien { get; set; }
	public DateTime NgayKham { get; set; }
	public string TrangThai { get; set; } = default!;
	public string ChanDoanCuoi { get; set; } = default!;
}