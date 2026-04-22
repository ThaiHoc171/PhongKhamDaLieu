namespace Application.DTOs.Dashboard;

/// <summary>6 KPI cards trên đầu dashboard</summary>
public class DashboardKpiReadModel
{
    public int    BenhNhanHomNay      { get; set; }
    public int    CaKhamConLai        { get; set; }
    public int    LieuTrinhDangChay   { get; set; }
    public int    XetNghiemChoKetQua  { get; set; }
    public int    ToaThuocHomNay      { get; set; }
    public double DoChinhXacAI        { get; set; }
}

/// <summary>1 ngày trong biểu đồ cột ca khám theo tuần</summary>
public class CaKhamTheoNgayReadModel
{
    public DateTime Ngay      { get; set; }
    public int      SoKham    { get; set; }
    public int      SoDieuTri { get; set; }
}

/// <summary>1 phần trong donut trạng thái ca khám</summary>
public class TrangThaiCaKhamReadModel
{
    public string TrangThai { get; set; } = string.Empty;
    public int    SoLuong   { get; set; }
}

/// <summary>1 bệnh trong top bệnh phổ biến</summary>
public class TopBenhReadModel
{
    public string TenBenh { get; set; } = string.Empty;
    public int    SoLuong { get; set; }
}

/// <summary>1 bác sĩ trong bảng xếp hạng</summary>
public class TopBacSiReadModel
{
    public string HoTen       { get; set; } = string.Empty;
    public string TenChucVu   { get; set; } = string.Empty;
    public int    SoPhienKham { get; set; }
}

/// <summary>1 liệu trình trong progress panel</summary>
public class LieuTrinhProgressReadModel
{
    public int    LieuTrinhID     { get; set; }
    public string TenLieuTrinh    { get; set; } = string.Empty;
    public string TenBenhNhan     { get; set; } = string.Empty;
    public int    TongSoBuoi      { get; set; }
    public int    SoBuoiHoanThanh { get; set; }
    /// <summary>Tính % tiến độ, dùng trực tiếp ở frontend</summary>
    public double PhanTramTienDo  =>
        TongSoBuoi > 0 ? Math.Round((double)SoBuoiHoanThanh / TongSoBuoi * 100, 1) : 0;
}

/// <summary>1 dòng trong feed hoạt động gần đây</summary>
public class HoatDongReadModel
{
    public DateTime ThoiGian   { get; set; }
    public string   LoaiSuKien { get; set; } = string.Empty;
    public string   MoTa       { get; set; } = string.Empty;
}
