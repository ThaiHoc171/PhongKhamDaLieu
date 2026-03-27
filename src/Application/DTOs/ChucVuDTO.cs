namespace Application.DTOs;
public class ChucVuImport
{
	[ExcelColumn("TenChucVu")]
	public string TenChucVu { get; set; } = "";
	[ExcelColumn("MoTa")]
	public string MoTa { get; set; } = "";
	[ExcelColumn("TrangThai")]
	public string TrangThai { get; set; } = "";
}
public class ChucVuRequest
{
    public string TenChucVu { get; set; } = "";
    public string MoTa { get; set; } = "";
	public string TrangThai { get; set; } = "";
}
public class ChucVuReadModel
{
    public int ChucVuID { get; set; }
    public string TenChucVu { get; set; } = "";
    public string MoTa { get; set; } = "";
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
	public string TrangThai { get; set; } = "";
}
public class ChucVuListReadModel
{
    public int ChucVuID { get; set; }
    public string TenChucVu { get; set; } = "";
    public string TrangThai { get; set; } = "";
}