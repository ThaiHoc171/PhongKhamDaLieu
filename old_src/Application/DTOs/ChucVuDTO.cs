namespace Application.DTOs
{
	public class ChucVuDTO
	{
		public int ChucVuID { get; set; }
		public string TenChucVu { get; set; } = default!;
		public string? MoTa { get; set; }
		public DateTime NgayTao { get; set; }
	}

	public class ThemChucVuDTO
	{
		public string TenChucVu { get; set; } = default!;
		public string? MoTa { get; set; }
	}

	public class CapNhatChucVuDTO
	{
		public string TenChucVu { get; set; } = default!;
		public string? MoTa { get; set; }
	}
}
