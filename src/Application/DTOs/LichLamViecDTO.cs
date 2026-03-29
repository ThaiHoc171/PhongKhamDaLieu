namespace Application.DTOs;
public class LichLamViecImport
{
	[ExcelColumn("NhanVienID")]
	public int NhanVienID { get; set; }
	[ExcelColumn("Ngay")]
	public DateTime Ngay { get; set; }
	[ExcelColumn("CaLamViec")]
	public int CaLamViec { get; set; }
	[ExcelColumn("GhiChu")]
	public string? GhiChu { get; set; }
}
public class LichLamViecRequest
{
	public int NhanVienID { get; set; }
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecReadListModel
{
	public int LichLamViecID { get; set; }
	public NameResponseDTO NhanVien { get; init; } = null!;
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecReadModel
{
	public int LichLamViecID { get; set; }
	public NameResponseDTO ChucVu { get; init; } = null!;
	public NameResponseDTO PhongChucNang { get; init; } = null!;
	public NameResponseDTO NhanVien { get; init; } = null!;
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecReadWeekModel
{
	public int Page { get; set; }
	public DateTime TuanBatDau { get; set; }
	public DateTime TuanKetThuc { get; set; }
	public List<LichLamViecReadListModel> LichLamViecs { get; set; } = new();
}