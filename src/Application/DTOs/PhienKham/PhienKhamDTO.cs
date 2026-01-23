namespace Application.DTOs;

public class PhienKhamCreateDTO
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
