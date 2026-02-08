namespace Domain.Entities;

public class LieuTrinhDieuTri
{
    public int LieuTrinhID { get; set; }
    public int BenhNhanID { get; set; }
    public int PhienKhamID { get; set; }
    public string TenLieuTrinh { get; set; }
    public int TongSoBuoi { get; set; }
    public string? TrangThai {  get; set; }
    public string? GhiChu { get; set; }
    public DateTime NgayBatDau { get; set; }
    public DateTime NgayKetThuc { get; set; }

    public LieuTrinhDieuTri(int benhNhanID, int phienKhamID, string tenLieuTrinh, int tongSoBuoi, string? ghiChu, DateTime ngayBatDau, DateTime ngayKetThuc)
    {
        if (phienKhamID <= 0) throw new ArgumentException("PhienKhamID không hợp lệ");

        if (benhNhanID <= 0) throw new ArgumentException("BenhNhanID không hợp lệ");

        if (ngayKetThuc.Date <= ngayBatDau.Date) throw new ArgumentException("Ngày bắt đầu - kết thúc không hợp lệ");

        BenhNhanID = benhNhanID;
        PhienKhamID = phienKhamID;
        TenLieuTrinh = tenLieuTrinh;
        TongSoBuoi = tongSoBuoi;
        GhiChu = ghiChu;
        NgayBatDau = ngayBatDau;
        NgayKetThuc = ngayKetThuc;
    }
    public LieuTrinhDieuTri(int lieuTrinhID, int benhNhanID, int phienKhamID, string tenLieuTrinh, int tongSoBuoi, string? trangThai, string? ghiChu, DateTime ngayBatDau, DateTime ngayKetThuc)
    {
        LieuTrinhID = lieuTrinhID;
        BenhNhanID = benhNhanID;
        PhienKhamID = phienKhamID;
        TenLieuTrinh = tenLieuTrinh;
        TongSoBuoi = tongSoBuoi;
        TrangThai = trangThai;
        GhiChu = ghiChu;
        NgayBatDau = ngayBatDau;
        NgayKetThuc = ngayKetThuc;
    }
    public void CapNhat(string tenLieuTrinh, int tongSoBuoi, DateTime ngayBatDau, DateTime ngayKetThuc)
    {
        TenLieuTrinh = tenLieuTrinh;
        TongSoBuoi = tongSoBuoi;
        NgayBatDau = ngayBatDau;
        NgayKetThuc = ngayKetThuc;
    }
    public void CapNhatTrangThai(string? trangThai, string? ghiChu)
    {
        TrangThai = trangThai;
        GhiChu = ghiChu;
    }
}
