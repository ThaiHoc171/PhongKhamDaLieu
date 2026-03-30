namespace Domain.Entities;

public class BaiViet
{
    public int BaiVietID { get; private set; }
    public string TieuDe { get; private set; }
    public string? TomTat { get; private set; }
    public string? NoiDung { get; private set; }
    public string? HinhAnh { get; private set; }
    public int? TacGiaID { get; private set; }
    public int? LoaiBenhID { get; private set; }
    public int LuotXem { get; private set; }
    public DateTime NgayDang { get; private set; }
    public DateTime? NgayCapNhat { get; private set; }
    public string TrangThai { get; private set; }

    public BaiViet(string tieuDe, string? tomTat, string? noiDung, string? hinhAnh, int? tacGiaID, int? loaiBenhID)
    {
        TieuDe = tieuDe;
        TomTat = tomTat;
        NoiDung = noiDung;
        HinhAnh = hinhAnh;
        TacGiaID = tacGiaID;
        LoaiBenhID = loaiBenhID;
    }
    public BaiViet(int baiVietID, string tieuDe, string? tomTat, string? noiDung, string? hinhAnh, int? tacGiaID, int? loaiBenhID, int luotXem, DateTime ngayDang, DateTime? ngayCapNhat, string trangThai)
    {
        BaiVietID = baiVietID;
        TieuDe = tieuDe;
        TomTat = tomTat;
        NoiDung = noiDung;
        HinhAnh = hinhAnh;
        TacGiaID = tacGiaID;
        LoaiBenhID = loaiBenhID;
        LuotXem = luotXem;
        NgayDang = ngayDang;
        NgayCapNhat = ngayCapNhat;
        TrangThai = trangThai;
    }
    public void CapNhat(string tieuDe, string? tomTat, string? noiDung, string? hinhAnh, int? loaiBenhID)
    {
        TieuDe = tieuDe;
        TomTat = tomTat;
        NoiDung = noiDung;
        HinhAnh = hinhAnh;
        LoaiBenhID = loaiBenhID;
        NgayCapNhat = DateTime.Now;
    }
}