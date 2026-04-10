namespace WPF.Models;
public class LoaiBenhRequest
{
	public string TenBenh { get; set; } = string.Empty;
	public string TenKhoaHoc { get; set; } = string.Empty;
	public string NhomBenh { get; set; } = string.Empty;
	public string MoTa { get; set; } = string.Empty;
	public string DoPhoBien { get; set; } = string.Empty;
	public string MucDoNghiemTrong { get; set; } = string.Empty;
}
public class LoaiBenhReadModel
{
	public int LoaiBenhID { get; set; }
	public string TenBenh { get; set; } = default!;
	public string TenKhoaHoc { get; set; } = string.Empty;
	public string NhomBenh { get; set; } = string.Empty;
	public string MoTa { get; set; } = string.Empty;
	public string DoPhoBien { get; set; } = string.Empty;
	public string MucDoNghiemTrong { get; set; } = string.Empty;
	public DateTime NgayTao { get; set; }
}
public class LoaiBenhListReadModel
{
	public int LoaiBenhID { get; set; }
	public string TenBenh { get; set; } = default!;
	public string NhomBenh { get; set; } = string.Empty;
	public string MucDoNghiemTrong { get; set; } = string.Empty;
}