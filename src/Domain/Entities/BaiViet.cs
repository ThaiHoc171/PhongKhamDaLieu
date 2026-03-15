namespace Domain.Entities;

public class BaiViet
{
    public int BaiVietID { get; set; }
    public string TieuDe { get; set; } = "";
    public string? TomTat { get; set; }
    public string? NoiDung { get; set; }
    public string? HinhAnh { get; set; }
    public int? TacGiaID { get; set; }
    public int? LoaiBenhID { get; set; }
    public int LuotXem { get; set; }
    public DateTime NgayDang { get; set; }
    public DateTime? NgayCapNhat { get; set; }
    public string TrangThai { get; set; } = "Bản nháp";

    public void TangLuotXem()
    {
        LuotXem++;
    }
    public void CapNhat(
        string tieuDe,
        string tomTat,
        string noiDung,
        string hinhAnh,
        int? loaiBenhID)
    {
        TieuDe = tieuDe;
        TomTat = tomTat;
        NoiDung = noiDung;
        HinhAnh = hinhAnh;
        LoaiBenhID = loaiBenhID;
        NgayCapNhat = DateTime.Now;
    }
}