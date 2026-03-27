namespace Application.DTOs;
public class ThietBiImport
{
	[ExcelColumn("TenTB")]
	public string TenTB { get; set; } = "";

	[ExcelColumn("LoaiTB")]
	public string LoaiTB { get; set; } = "";
	[ExcelColumn("TrangThai")]
	public string TrangThai { get; set; } = "";
}
public class ThietBiRequest
{
    public string TenTB { get; set; } = "";
    public string LoaiTB { get; set; } = "";	
	public string TrangThai { get; set; } = "";
}
public class ThietBiReadModel
{
    public int ThietBiID { get; set; }
    public string TenTB { get; set; } = "";
    public string LoaiTB { get; set; } = "";
    public string TrangThai { get; set; } = "";
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}
public class ThietBiReadListModel
{
	public int ThietBiID { get; set; }
	public string TenTB { get; set; } = "";
	public string LoaiTB { get; set; } = "";
	public string TrangThai { get; set; } = "";

}
