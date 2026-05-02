namespace HoanMyClinic.Models;
public class KhungGioKhamRequest
{
	public int CaLamViec { get; set; }
	public TimeSpan GioBatDau { get; set; }
	public TimeSpan GioKetThuc { get; set; }
}
public class KhungGioKhamReadModel
{
	public int KhungGioID { get; set; }
	public string TenKhung { get; set; } = "";
	public int CaLamViec { get; set; }
	public TimeSpan GioBatDau { get; set; }
	public TimeSpan GioKetThuc { get; set; }
}
