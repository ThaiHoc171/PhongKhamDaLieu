namespace HoanMyClinic.Models
{
	public class NgayNghiRequestDTO
	{
		public int NhanVienID { get; set; }
		public DateTime Ngay { get; set; }
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
		public NameHelper NhanVien { get; init; } = default!;
		public DateTime Ngay { get; set; }
		public string? LyDo { get; set; }
	}
}
