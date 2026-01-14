namespace Application.DTOs;

public class ChucVuResponseDTO
{
	public int ChucVuID { get; set; }
	public string TenChucVu { get; set; } = default!;
	public string? MoTa { get; set; }
	public DateTime NgayTao { get; set; }
}

public class ChucVuRequestDTO
{
	public string TenChucVu { get; set; } = default!;
	public string? MoTa { get; set; }
}
