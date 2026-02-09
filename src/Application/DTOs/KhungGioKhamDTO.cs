namespace Application.DTOs
{
	public class KhungGioKhamRequestDTO
	{
		public int CaLamViec { get; set; }
		public TimeSpan GioBatDau { get; set; }
		public TimeSpan GioKetThuc { get; set; }
		public string? TenKhung { get; set; }
	}

	public class KhungGioKhamResponseDTO
	{
		public int KhungGioID { get; set; }
		public int CaLamViec { get; set; }
		public TimeSpan GioBatDau { get; set; }
		public TimeSpan GioKetThuc { get; set; }
		public string? TenKhung { get; set; }
	}
}
