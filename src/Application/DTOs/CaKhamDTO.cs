namespace Application.DTOs;
public class TaoCaKhamDTO
{
    public DateTime NgayKham { get; set; }
    public DateTime NgayKetThuc {  get; set; }
}
public class DangKyCaKhamDTO
{
    public int ThongTinID { get; set; }
    public string LyDoKham { get; set; } = string.Empty;
    public DateTime NgayDat { get; set; }
    public string? GhiChu { get; set; }
}
public class CaKhamListReadModel
{
	public int CaKhamID { get; set; }
	public string TenKhungGio { get; set; } = string.Empty;
	public string TenPhong { get; set; } = string.Empty;
	public string? HoTen { get; set; }
	public string? LyDoKham { get; set; }
	public string TrangThai { get; set; } = string.Empty;
}
public class CaKhamReadModel
{
	public int CaKhamID { get; set; }
	public string LoaiCaKham { get; set; } = string.Empty;
	public int LichLamViecID { get; set; }
	public string TenKhungGio { get; set; } = string.Empty;
	public string TenPhong { get; set; } = string.Empty;
	public string? HoTen { get; set; }
	public string? LyDoKham { get; set; }
	public string TrangThai { get; set; } = string.Empty;
	public DateTime? NgayDat { get; set; }
	public DateTime NgayKham { get; set; }
	public string? GhiChu { get; set; }
}
