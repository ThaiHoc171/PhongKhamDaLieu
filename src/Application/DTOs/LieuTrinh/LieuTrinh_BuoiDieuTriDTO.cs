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
    public int BuoiDieuTriID { get; set; }
    public int LieuTrinhID { get; set; }
    public int CaKhamID { get; set; }
    public int SoBuoi { get; set; }
    public DateTime? NgayDuKien { get; set; }
    public DateTime? NgayThucHien { get; set; }
    public int? NhanVienID { get; set; }
    public string TrangThai { get; set; } = "";
    public string? GhiChu { get; set; }
    public string? HinhAnhJSON { get; set; }
}

