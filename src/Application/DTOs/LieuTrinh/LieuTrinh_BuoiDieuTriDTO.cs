namespace Application.DTOs;

public class TaoBuoiDieuTriDTO
{
    public int LieuTrinhID { get; set; }
    public int CaKhamID { get; set; }
}

public class CapNhatTrangThaiBuoiDieuTriDTO
{
    public string TrangThai { get; set; } = null!;
    public int? NhanVienID { get; set; }
    public DateTime? NgayThucHien { get; set; }
    public string? GhiChu { get; set; }
}

public class BuoiDieuTriResponeDTO
{
    public int BuoiDieuTriID { get; private set; }
    public int LieuTrinhID { get; private set; }
    public int CaKhamID { get; private set; }
    public int SoBuoi { get; private set; }
    public DateTime? NgayDuKien { get; private set; }
    public DateTime? NgayThucHien { get; private set; }
    public int? NhanVienID { get; private set; }
    public string TrangThai { get; private set; }
    public string? GhiChu { get; private set; }
    public string? HinhAnhJSON { get; private set; }
}

