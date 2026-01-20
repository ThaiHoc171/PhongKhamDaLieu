namespace Domain.Entities;

public class CaKham
{
    public int CaKhamID { get; private set; }
    public int LichLamViecID { get; private set; }
    public int PhongChucNangID { get; private set; }
    public DateTime NgayKham { get; private set; }
    public int KhungGioID { get; private set; }
    public int? BenhNhanID { get; private set; }
    public string? LyDoKham { get; private set; }
    public string TrangThai { get; private set; }
    public DateTime? NgayDat { get; private set; }
    public string? GhiChu { get; private set; }

    public CaKham(int lichLamViecID, int phongChucNangID, DateTime ngayKham, int khungGioID, string trangThai = "Trống")
    {
        if (lichLamViecID <= 0) throw new ArgumentException("LichLamViecID không hợp lệ");

        if (phongChucNangID <= 0) throw new ArgumentException("LichLamViecID không hợp lệ");

        if (ngayKham.Date < DateTime.Today)
            throw new ArgumentException("Ngày khám không được nhỏ hơn ngày hiện tại");

        if (khungGioID <= 0) throw new ArgumentException("KhungGioID không hợp lệ");

        LichLamViecID = lichLamViecID;
        PhongChucNangID = phongChucNangID;
        NgayKham = ngayKham;
        KhungGioID = khungGioID;
        TrangThai = trangThai;
    }

    public CaKham(
        int caKhamID,
        int lichLamViecID,
        int phongChucNangID,
        DateTime ngayKham,
        int khungGioID,
        int? benhNhanID,
        string? lyDoKham,
        string trangThai,
        DateTime? ngayDat,
        string? ghiChu
    )
    {
        CaKhamID = caKhamID;
        LichLamViecID = lichLamViecID;
        PhongChucNangID = phongChucNangID;
        NgayKham = ngayKham;
        KhungGioID = khungGioID;
        BenhNhanID = benhNhanID;
        LyDoKham = lyDoKham;
        TrangThai = trangThai;
        NgayDat = ngayDat;
        GhiChu = ghiChu;
    }


    public void DangKyKham(int benhNhanID, string lyDoKham, DateTime ngayDat, string ghiChu)
    {
        if (TrangThai != "Trống")
            throw new Exception("Ca khám đã được đặt");

        BenhNhanID = benhNhanID;
        LyDoKham = lyDoKham;
        TrangThai = "Đã đặt";
        NgayDat = ngayDat;
        GhiChu = ghiChu;
    }

    public void CapNhatTrangThai(string trangThaiMoi)
    {
        if (string.IsNullOrWhiteSpace(trangThaiMoi))
            throw new ArgumentException("Trạng thái không hợp lệ");

        TrangThai = trangThaiMoi;
    }
}


