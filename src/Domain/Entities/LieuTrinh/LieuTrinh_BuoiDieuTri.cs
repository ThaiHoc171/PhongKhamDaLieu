namespace Domain.Entities;

public class LieuTrinh_BuoiDieuTri
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

    // Tạo mới
    public LieuTrinh_BuoiDieuTri(int lieuTrinhID, int caKhamID, int soBuoi, DateTime? ngayDuKien, DateTime? ngayThucHien, int? nhanVienID)
    {
        if (lieuTrinhID <= 0) throw new ArgumentException("LieuTrinhID không hợp lệ");
        if (caKhamID <= 0) throw new ArgumentException("CaKhamID không hợp lệ");
        if (soBuoi <= 0) throw new ArgumentException("Số buổi không hợp lệ");


        var ngayThuc = ngayThucHien
        ?? throw new Exception("Chưa có ngày thực hiện");

        var ngayDuKienThuc = ngayDuKien
            ?? throw new Exception("Chưa có ngày dự kiến");

        if (ngayThuc < ngayDuKienThuc)
            throw new Exception("Ngày thực hiện không được trước ngày dự kiến");

        LieuTrinhID = lieuTrinhID;
        CaKhamID = caKhamID;
        SoBuoi = soBuoi;
        NgayDuKien = ngayDuKien;
        NgayThucHien = ngayThucHien;
        NhanVienID = nhanVienID;
        TrangThai = "Chờ xử lý";
    }

    public LieuTrinh_BuoiDieuTri(
        int buoiDieuTriID,
        int lieuTrinhID,
        int caKhamID,
        int soBuoi,
        DateTime? ngayDuKien,
        DateTime? ngayThucHien,
        int? nhanVienID,
        string trangThai,
        string? ghiChu,
        string? hinhAnhJSON)
    {
        BuoiDieuTriID = buoiDieuTriID;
        LieuTrinhID = lieuTrinhID;
        CaKhamID = caKhamID;
        SoBuoi = soBuoi;
        NgayDuKien = ngayDuKien;
        NgayThucHien = ngayThucHien;
        NhanVienID = nhanVienID;
        TrangThai = trangThai;
        GhiChu = ghiChu;
        HinhAnhJSON = hinhAnhJSON;
    }

    public void CapNhatTrangThai(string trangThai, int? nhanVienID, DateTime? ngayThucHien, string? ghiChu)
    {
        TrangThai = trangThai;
        NhanVienID = nhanVienID;
        NgayThucHien = ngayThucHien;
        GhiChu = ghiChu;
    }
    public void CapNhatNgayThucHien(DateTime ngayThucHien)
    {
        if (ngayThucHien < NgayDuKien)
            throw new Exception(
                $"Không thể thực hiện điều trị trước ngày dự kiến ({NgayDuKien:dd/MM/yyyy})"
            );

        NgayThucHien = ngayThucHien;
    }
}
