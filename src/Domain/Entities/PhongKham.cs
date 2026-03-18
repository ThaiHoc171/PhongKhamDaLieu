namespace Domain.Entities;

public class PhongKham
{
    public int PhongKhamID { get; private set; }
    public string TenPhongKham { get; private set; }
    public string? GioiThieu { get; private set; }
    public string? DiaChi { get; private set; }
    public string? Hotline { get; private set; }
    public string? Email { get; private set; }
    public string? Website { get; private set; }
    public string? HinhAnhBanner { get; private set; }
    public string TrangThai { get; private set; }
    public DateTime NgayTao { get; private set; }
    public DateTime NgayCapNhat { get; private set; }
    public PhongKham(string tenPhongKham, string? gioiThieu, string? diaChi,
        string? hotline, string? email, string? website, string? hinhAnhBanner)
    {
        TenPhongKham = tenPhongKham;
        GioiThieu = gioiThieu;
        DiaChi = diaChi;
        Hotline = hotline;
        Email = email;
        Website = website;
        HinhAnhBanner = hinhAnhBanner;
        TrangThai = "Hoạt động";
    }
    public PhongKham(int id, string ten, string? gioiThieu, string? diaChi,
        string? hotline, string? email, string? website, string? banner,
        string trangThai, DateTime ngayTao, DateTime ngayCapNhat)
    {
        PhongKhamID = id;
        TenPhongKham = ten;
        GioiThieu = gioiThieu;
        DiaChi = diaChi;
        Hotline = hotline;
        Email = email;
        Website = website;
        HinhAnhBanner = banner;
        TrangThai = trangThai;
        NgayTao = ngayTao;
        NgayCapNhat = ngayCapNhat;
    }
    public void CapNhat(string ten, string? gioiThieu, string? diaChi,
        string? hotline, string? email, string? website, string? banner)
    {
        TenPhongKham = ten;
        GioiThieu = gioiThieu;
        DiaChi = diaChi;
        Hotline = hotline;
        Email = email;
        Website = website;
        HinhAnhBanner = banner;
        NgayCapNhat = DateTime.Now;
    }
    public void DoiTrangThai(string trangThai)
    {
        TrangThai = trangThai;
        NgayCapNhat = DateTime.Now;
    }
}