namespace Application.DTOs;

public class NgayNghiRequestDTO
{
	public int NhanVienID { get; set; }
	public DateTime Ngay { get; set; }
	public string? LyDo { get; set; }
}

public class NgayNghiResponseDTO
{
	public int NgayNghiID { get; set; }
	public int NhanVienID { get; set; }
	public DateTime Ngay { get; set; }
	public string? LyDo { get; set; }
}
