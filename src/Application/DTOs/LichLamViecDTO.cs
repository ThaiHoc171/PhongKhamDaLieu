namespace Application.DTOs;
public class LichLamViecRequestDTO
{
	public int NhanVienID { get; set; }
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecUpdateRequestDTO
{
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecReadModel
{
	public int LichLamViecID { get; set; }
	public NameResponseDTO NhanVien { get; init; } = null!;
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecChucVuReadModel
{
	public int LichLamViecID { get; set; }
	public NameResponseDTO ChucVu { get; init; } = null!;
	public NameResponseDTO PhongChucNang { get; init; } = null!;
	public NameResponseDTO NhanVien { get; init; } = null!;
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class WeekLichLamViecReadModel
{
	public int Page { get; set; }
	public DateTime TuanBatDau { get; set; }
	public DateTime TuanKetThuc { get; set; }
	public List<LichLamViecReadModel> LichLamViecs { get; set; } = new();
}