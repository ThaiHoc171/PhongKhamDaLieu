namespace Domain.Entities;

public class TaiKham
{
    public int TaiKhamID { get; set; }
    public int PhienKhamID { get; set; }
    public int BenhNhanID { get; set; }
    public DateTime NgayDuKien {  get; set; }
    public string? LyDo {  get; set; }
    public string? TrangThai {  get; set; }
    public int? CaKhamID { get; set; }
    public DateTime NgayTao { get; set; }

    public TaiKham(int phienKhamID, int benhNhanID, DateTime ngayDuKien, string? lyDo)
    {
        if (phienKhamID <= 0) throw new ArgumentException("phienKhamID không hợp lệ");

        if (benhNhanID <= 0) throw new ArgumentException("benhNhanID không hợp lệ");

        if (ngayDuKien.Date <= DateTime.Now.Date) throw new ArgumentException("Ngày dự kiến tái khám không hợp lệ");

        PhienKhamID = phienKhamID;
        BenhNhanID = benhNhanID;
        NgayDuKien = ngayDuKien;
        LyDo = lyDo;
    }

    public TaiKham(int taiKhamID, int phienKhamID, int benhNhanID, DateTime ngayDuKien, string? lyDo, string? trangThai, int? caKhamID, DateTime ngayTao)
    {
        TaiKhamID = taiKhamID;
        PhienKhamID = phienKhamID;
        BenhNhanID = benhNhanID;
        NgayDuKien = ngayDuKien;
        LyDo = lyDo;
        TrangThai = trangThai;
        CaKhamID = caKhamID;
        NgayTao = ngayTao;
    }

    public void CapNhat(DateTime ngayDuKien, string? lyDo, string? trangThai, int? caKhamID)
    {
        if (ngayDuKien.Date <= DateTime.Now.Date)
            throw new ArgumentException("Ngày dự kiến tái khám không hợp lệ");
        NgayDuKien = ngayDuKien;
        LyDo = lyDo;
        TrangThai = trangThai;
        CaKhamID = caKhamID;
    }
}
