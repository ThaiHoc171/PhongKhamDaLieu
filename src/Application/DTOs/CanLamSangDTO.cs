namespace Application.DTOs;
public class CanLamSangImport
{
    [ExcelColumn("TenCLS")]
	public string TenCLS { get; set; } = "";
    [ExcelColumn("MoTa")]
	public string MoTa { get; set; } = "";
    [ExcelColumn("LoaiXetNghiem")]
	public string LoaiXetNghiem { get; set; } = "";
    [ExcelColumn("TrangThai")]
	public string TrangThai { get; set; } = "";
}
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
    public string MoTa { get; set; } = "";
    public string LoaiXetNghiem { get; set; } = "";
    public string TrangThai { get; set; } = "";
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}
public class CanLamSangReadListModel
{
    public int CanLamSangID { get; set; }
    public string TenCLS { get; set; } = "";
    public string LoaiXetNghiem { get; set; } = "";
    public string TrangThai { get; set; } = "";
}
