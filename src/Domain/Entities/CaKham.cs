namespace Domain.Entities;

public class CaKham
{
    public int CaKhamID { get; private set; }
    public string LoaiCaKham { get; private set; }
    public int LichLamViecID { get; private set; }
    public int KhungGioID { get; private set; }
    public int PhongChucNangID { get; private set; }
    public int? BenhNhanID { get; private set; }
    public string? LyDoKham { get; private set; }
    public string TrangThai { get; private set; }
    public DateTime? NgayDat { get; private set; }
    public DateTime NgayKham { get; private set; }
    public string? GhiChu { get; private set; }

    public CaKham(string loaiCaKham, int lichLamViecID, int phongChucNangID, int khungGioID, DateTime ngayKham, string trangThai = "Trống")
    {
        if (lichLamViecID <= 0) throw new ArgumentException("LichLamViecID không hợp lệ");

        if (phongChucNangID <= 0) throw new ArgumentException("LichLamViecID không hợp lệ");

        if (khungGioID <= 0) throw new ArgumentException("KhungGioID không hợp lệ");

        LoaiCaKham = loaiCaKham;
        LichLamViecID = lichLamViecID;
        PhongChucNangID = phongChucNangID;
        KhungGioID = khungGioID;
        NgayKham = ngayKham;
        TrangThai = trangThai;
    }

    public CaKham(
        int caKhamID,
        string loaiCaKham,
        int lichLamViecID,
        int khungGioID,
        int phongChucNangID,
        int? benhNhanID,
        string? lyDoKham,
        string trangThai,
        DateTime? ngayDat,
        DateTime ngayKham,
        string? ghiChu
    )
    {
        CaKhamID = caKhamID;
        LoaiCaKham = loaiCaKham;
        LichLamViecID = lichLamViecID;
        PhongChucNangID = phongChucNangID;
        KhungGioID = khungGioID;
        BenhNhanID = benhNhanID;
        LyDoKham = lyDoKham;
        TrangThai = trangThai;
        NgayDat = ngayDat;
        NgayKham = ngayKham;
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


