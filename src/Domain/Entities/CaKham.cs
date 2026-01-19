namespace Domain.Entities;

public class CaKham
{
    public int CaKhamID { get; private set; }
    public int LichLamViecID { get; private set; }
    public int PhongChucNangID { get; private set; }
    public DateTime NgayKham { get; private set; }
    public int KhungGioID { get; private set; }
    public int BenhNhanID { get; private set; }
    public string? LyDoKham { get; private set; }
    public string TrangThai { get; private set; }
    public DateTime NgayDat { get; private set; }
    public string? GhiChu { get; private set; }
}
