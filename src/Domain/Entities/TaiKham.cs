using Domain.Enums;

namespace Domain.Entities;

public class TaiKham
{
    public int TaiKhamID { get; private set; }
    public int PhienKhamID { get; private set; }
    public int BenhNhanID { get; private set; }
    public DateTime NgayDuKien { get; private set; }
    public string? LyDo { get; private set; }
    public TrangThaiTaiKhamEnum TrangThai { get; private set; }
    public int? CaKhamID { get; private set; }
    public DateTime NgayTao { get; private set; }

    // Constructor tạo mới
    public TaiKham(int phienKhamID, int benhNhanID, DateTime ngayDuKien, string? lyDo)
    {
        if (phienKhamID <= 0)
            throw new ArgumentException("PhienKhamID không hợp lệ");

        if (benhNhanID <= 0)
            throw new ArgumentException("BenhNhanID không hợp lệ");

        if (ngayDuKien.Date <= DateTime.Now.Date)
            throw new ArgumentException("Ngày dự kiến tái khám phải lớn hơn hôm nay");

        PhienKhamID = phienKhamID;
        BenhNhanID = benhNhanID;
        NgayDuKien = ngayDuKien;
        LyDo = lyDo;
        TrangThai = TrangThaiTaiKhamEnum.ChoKham;
    }

    // Constructor map DB
    public TaiKham(int taiKhamID, int phienKhamID, int benhNhanID, DateTime ngayDuKien,
        string? lyDo, string? trangThai, int? caKhamID, DateTime ngayTao)
    {
        TaiKhamID = taiKhamID;
        PhienKhamID = phienKhamID;
        BenhNhanID = benhNhanID;
        NgayDuKien = ngayDuKien;
        LyDo = lyDo;
        TrangThai = TrangThaiTaiKhamExtensions.Parse(trangThai);
        CaKhamID = caKhamID;
        NgayTao = ngayTao;
    }

    // Business Logic
    public void CapNhatCaKham(int? caKhamID)
    {
        CaKhamID = caKhamID;
    }

    public void DoiTrangThai(TrangThaiTaiKhamEnum trangThai)
    {
        TrangThai = trangThai;
    }

    public void CapNhatLyDo(string? lyDo)
    {
        LyDo = lyDo;
    }
}