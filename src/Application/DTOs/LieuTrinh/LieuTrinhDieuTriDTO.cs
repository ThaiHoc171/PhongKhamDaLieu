namespace Application.DTOs;

public class TaoLieuTrinhDieuTriDTO
{
    public int PhienKhamID { get; set; }
    public string TenLieuTrinh { get; set; }
    public int TongSoBuoi { get; set; }
    public string? GhiChu { get; set; }
    public DateTime NgayBatDau { get; set; }
}
public class CapNhatLieuTrinhDieuTriDTO
{
    public string TenLieuTrinh { get; set; }
    public int TongSoBuoi { get; set; }
    public DateTime NgayKetThuc { get; set; }
}
public class CapNhatTrangThaiLieuTrinhDieuTriDTO
{
    public string? TrangThai { get; set; }
    public string? GhiChu { get; set; }
}
public class LieuTrinhDieuTriResponeDTO
{
    public int LieuTrinhID { get; set; }
    public int BenhNhanID { get; set; }
    public int PhienKhamID { get; set; }
    public string TenLieuTrinh { get; set; }
    public int TongSoBuoi { get; set; }
    public string? TrangThai { get; set; }
    public string? GhiChu { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }
}
