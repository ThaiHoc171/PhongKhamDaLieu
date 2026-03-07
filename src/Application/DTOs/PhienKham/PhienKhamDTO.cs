namespace Application.DTOs;

public class PhienKhamRequestDTO
{
	public int CaKhamID { get; set; }
	public int BenhNhanID { get; set; }
	public int NhanVienID { get; set; }
	public int? PhongChucNangID { get; set; }
	public string? TrieuChung { get; set; }
	public string? GhiChu { get; set; }
	public string? HinhAnhJSON { get; set; }
}

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
	public NameResponseDTO BenhNhan { get; set; } = default!;
	public NameResponseDTO NhanVien { get; set; } = default!;
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
	public NameResponseDTO BenhNhan { get; set; } = default!;
	public NameResponseDTO NhanVien { get; set; } = default!;
	public DateTime NgayKham { get; set; }
	public string TrangThai { get; set; } = default!;
	public string ChanDoanCuoi { get; set; } = default!;
}