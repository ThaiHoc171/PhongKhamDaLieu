namespace WPF.Models;
public class ThongKeFilterRequest
{
    /// <summary>day | week | month | year</summary>
    public string LoaiKhoang { get; set; } = "month";
    public DateTime? TuNgay { get; set; }
    public DateTime? DenNgay { get; set; }
    public int? Nam { get; set; }
    public int? Thang { get; set; }
}

// ─── BỆNH NHÂN & CA KHÁM ───────────────────────────────────────────────────

public class TongQuanBenhNhanReadModel
{
    public int TongBenhNhan         { get; set; }
    public int BenhNhanMoi          { get; set; }
    public int BenhNhanTaiKham      { get; set; }
    public int BenhNhanCoTaiKhoan   { get; set; }
}

public class BenhNhanTheoNgayReadModel
{
    public DateTime Ngay    { get; set; }
    public int SoBenhNhanMoi { get; set; }
}

public class BenhNhanTheoGioiTinhReadModel
{
    public string GioiTinh { get; set; } = string.Empty;
    public int    SoLuong  { get; set; }
}

public class BenhNhanTheoDoTuoiReadModel
{
    public string NhomTuoi { get; set; } = string.Empty;
    public int    SoLuong  { get; set; }
}

public class TongQuanCaKhamReadModel
{
    public int TongCaKham      { get; set; }
    public int HoanThanh       { get; set; }
    public int DaHuy           { get; set; }
    public int KhongDen        { get; set; }
    public int DangKham        { get; set; }
}

public class CaKhamTheoKhoangReadModel
{
    public string NhanX   { get; set; } = string.Empty;   // "T2", "01", "Tháng 3", ...
    public DateTime TuNgay { get; set; }
    public int SoKham      { get; set; }
    public int SoDieuTri   { get; set; }
}


public class TongQuanPhienKhamReadModel
{
    public int TongPhienKham   { get; set; }
    public int HoanThanh       { get; set; }
    public int DangKham        { get; set; }
    public int DangCho         { get; set; }
    public int DaHuy           { get; set; }
}

public class PhienKhamTheoNgayReadModel
{
    public DateTime Ngay       { get; set; }
    public int SoHoanThanh     { get; set; }
    public int SoDangKham      { get; set; }
    public int SoDangCho       { get; set; }
    public int SoDaHuy         { get; set; }
}

public class PhienKhamTheoPhongReadModel
{
    public string TenPhong     { get; set; } = string.Empty;
    public int    SoPhienKham  { get; set; }
}

public class PhienKhamTheoLoaiBenhReadModel
{
    public string TenBenh      { get; set; } = string.Empty;
    public int    SoLuong      { get; set; }
    public string NhomBenh     { get; set; } = string.Empty;
}


public class TongQuanToaThuocReadModel
{
    public int    TongToaThuoc       { get; set; }
    public int    TongLuotThuoc      { get; set; }   
    public int    TrungBinhThuocPerToa { get; set; }
}

public class ToaThuocTheoKhoangReadModel
{
    public string   NhanX        { get; set; } = string.Empty;
    public DateTime TuNgay       { get; set; }
    public int      SoToaThuoc   { get; set; }
    public int      SoLuotThuoc  { get; set; }
}

public class TopThuocReadModel
{
    public int    ThuocID    { get; set; }
    public string TenThuoc   { get; set; } = string.Empty;
    public string HoatChat   { get; set; } = string.Empty;
    public int    TongSoLan  { get; set; }   // số lần xuất hiện trong ChiTietToaThuoc
    public int    TongSoLuong { get; set; }  // tổng số lượng
}

public class TopBacSiKeDonReadModel
{
    public string HoTen       { get; set; } = string.Empty;
    public int    SoToaThuoc  { get; set; }
}


public class TongQuanNhanVienReadModel
{
    public int TongNhanVien    { get; set; }
    public int DangLamViec     { get; set; }
    public int NghiViec        { get; set; }
}

public class NhanVienTheoChucVuReadModel
{
    public string TenChucVu { get; set; } = string.Empty;
    public int    SoLuong   { get; set; }
}

public class NhanVienTheoPhongReadModel
{
    public string TenPhong { get; set; } = string.Empty;
    public int    SoLuong  { get; set; }
}

public class HieuSuatBacSiReadModel
{
    public int    NhanVienID   { get; set; }
    public string HoTen        { get; set; } = string.Empty;
    public string TenChucVu    { get; set; } = string.Empty;
    public int    SoPhienKham  { get; set; }
    public int    SoHoanThanh  { get; set; }
    public int    SoToaThuoc   { get; set; }
    public double TiLeHoanThanh =>
        SoPhienKham > 0 ? Math.Round((double)SoHoanThanh / SoPhienKham * 100, 1) : 0;
}

public class NgayNghiNhanVienReadModel
{
    public string HoTen    { get; set; } = string.Empty;
    public int    SoNgayNghi { get; set; }
}
