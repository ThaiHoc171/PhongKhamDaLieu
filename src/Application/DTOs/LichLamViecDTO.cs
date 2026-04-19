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
public class LichLamViecReadListModel
{
	public int LichLamViecID { get; set; }
	public NameResponseDTO NhanVien { get; init; } = null!;
	public string ChucVu { get; init; } =string.Empty;
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string TenPhong { get; set; } = string.Empty;
	public string? GhiChu { get; set; }
}
public class LichLamViecReadWeekModel
{
	public int Page { get; set; }
	public DateTime TuanBatDau { get; set; }
	public DateTime TuanKetThuc { get; set; }
	public List<LichLamViecReadListModel> LichLamViecs { get; set; } = new();
}
public class LichLamViecForGenerateDTO
{
	public int LichLamViecID { get; set; }
	public int NhanVienID { get; set; }
	public int ChucVuID { get; set; }
	public int? PhongChucNangID { get; set; }
	public int CaLamViec { get; set; }
}