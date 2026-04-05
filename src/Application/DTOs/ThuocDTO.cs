namespace Application.DTOs;
public class ThuocRequestDTO
{
    [ExcelColumn("TenThuoc")]
	public string TenThuoc { get; set; } = string.Empty;
    [ExcelColumn("HoatChat")]
	public string HoatChat { get; set; } = string.Empty;
}
public class ThuocUpdateDTO
{
    public string TenThuoc { get; set; } = string.Empty;
	public string HoatChat { get; set; } = string.Empty;
}
public class ThuocReadModel
{
    public int ThuocID { get; set; }
	public string TenThuoc { get; set; } = default!;
    public string HoatChat { get; set; } = string.Empty;
}


