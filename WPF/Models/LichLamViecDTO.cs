namespace WPF.Models;

public class LichLamViecRequest
{
	public int NhanVienID { get; set; }
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecReadModel
{
	public int LichLamViecID { get; set; }
	public NameHelper NhanVien { get; init; } = null!;
	public string ChucVu { get; init; } = null!;
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
	public List<LichLamViecReadModel> LichLamViecs { get; set; } = new();
}