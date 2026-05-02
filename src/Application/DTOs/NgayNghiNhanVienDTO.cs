namespace Application.DTOs;
public class NgayNghiRequestDTO
{
	[ExcelColumn ("NhanVienID")]
	public int NhanVienID { get; set; }
	[ExcelColumn("Ngay")]
	public DateTime Ngay { get; set; }
	[ExcelColumn("lyDo")]
	public string? LyDo { get; set; }
}
public class NgayNghiUpdateRequestDTO
{
	public DateTime Ngay { get; set; }
	public string? LyDo { get; set; }
}
public class NgayNghiReadModel
{
	public int NgayNghiID { get; set; }
	public NameResponseDTO NhanVien { get; init; } = default!; //int Id - string Name
	public DateTime Ngay { get; set; }
	public string? LyDo { get; set; }
}
