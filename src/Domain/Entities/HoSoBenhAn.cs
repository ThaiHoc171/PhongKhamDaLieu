namespace Domain.Entities;
public class HoSoBenhAn
{
    public int HoSoBenhAnID { get; set; }
    public int BenhNhanID { get; set; }
    public string? BenhNen { get; set; }
    public string? DiUng { get; set; }
    public string? TienSuBenh { get; set; }
    public string? TienSuGiaDinh { get; set; }
    public string? ThoiQuenSong { get; set; }
    public string? ThongTinKhac { get; set; }
    public DateTime NgayTao { get; set; }
    public DateTime NgayCapNhat { get; set; }

    public HoSoBenhAn(int benhNhanID, string? benhNen, string? diUng, string? tienSuBenh, string? tienSuGiaDinh, string? thoiQuenSong, string? thongTinKhac, DateTime ngayTao, DateTime ngayCapNhat)
    {
        if (benhNhanID <= 0) throw new ArgumentException("benhNhanID không hợp lệ");

        BenhNhanID = benhNhanID;
        BenhNen = benhNen;
        DiUng = diUng;
        TienSuBenh = tienSuBenh;
        TienSuGiaDinh = tienSuGiaDinh;
        ThoiQuenSong = thoiQuenSong;
        ThongTinKhac = thongTinKhac;
        NgayTao = ngayTao;
        NgayCapNhat = ngayCapNhat;
    }

    public HoSoBenhAn(int hoSoBenhAnID, int benhNhanID, string? benhNen, string? diUng, string? tienSuBenh, string? tienSuGiaDinh, string? thoiQuenSong, string? thongTinKhac, DateTime ngayTao, DateTime ngayCapNhat)
    {
        HoSoBenhAnID = hoSoBenhAnID;
        BenhNhanID = benhNhanID;
        BenhNen = benhNen;
        DiUng = diUng;
        TienSuBenh = tienSuBenh;
        TienSuGiaDinh = tienSuGiaDinh;
        ThoiQuenSong = thoiQuenSong;
        ThongTinKhac = thongTinKhac;
        NgayTao = ngayTao;
        NgayCapNhat = ngayCapNhat;
    }

    public void CapNhatThongTin(string? benhNen, string? diUng, string? tienSuBenh, string? tienSuGiaDinh, string? thoiQuenSong, string? thongTinKhac, DateTime ngayCapNhat)
    {
        BenhNen = benhNen;
        DiUng = diUng;
        TienSuBenh = tienSuBenh;
        TienSuGiaDinh = tienSuGiaDinh;
        ThoiQuenSong = thoiQuenSong;
        ThongTinKhac = thongTinKhac;
        NgayCapNhat = ngayCapNhat;
    }
}

