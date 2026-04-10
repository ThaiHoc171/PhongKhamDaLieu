namespace WPF.Models;
public class CanLamSangRequest
{
	public string TenCLS { get; set; } = "";
	public string MoTa { get; set; } = "";
	public string LoaiXetNghiem { get; set; } = "";
	public string TrangThai { get; set; } = "";
}
public class CanLamSangReadModel
{
	public int CanLamSangID { get; set; }
	public string TenCLS { get; set; } = "";
	public string? MoTa { get; set; }
	public string LoaiXetNghiem { get; set; } = "";
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
	public string TrangThai { get; set; } = "";
}
public class CanLamSangListReadModel
{
	public int CanLamSangID { get; set; }
	public string TenCLS { get; set; } = "";
	public string LoaiXetNghiem { get; set; } = "";
	public string TrangThai { get; set; } = "";
}

