namespace Domain.Entities;

public class CanLamSang
{
    public int CanLamSangID { get; private set; }
    public string TenCLS { get; private set; }
    public string? MoTa { get; private set; }
    public string LoaiXetNghiem { get; private set; }
    public DateTime NgayTao { get; private set; }
    public string TrangThai { get; private set; }

    public CanLamSang(string tenCLS, string? moTa, string loaiXetNghiem)
    {
        if (string.IsNullOrWhiteSpace(tenCLS))
            throw new ArgumentException("Tên cận lâm sàng không hợp lệ");
        TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        NgayTao = DateTime.UtcNow;
        TrangThai = "Hoạt động";
    }
    public CanLamSang(int canLamSangID, string tenCLS, string? moTa, string loaiXetNghiem, DateTime ngayTao, string trangThai)
    {
        CanLamSangID = canLamSangID;
        TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        NgayTao = ngayTao;
        TrangThai = trangThai;
    }
    public void CapNhat(string tenCLS, string? moTa, string loaiXetNghiem, string trangThai)
    {
        if (TrangThai == "Ngừng sử dụng")
            throw new InvalidOperationException("CLS đã ngừng sử dụng");
        if (string.IsNullOrWhiteSpace(tenCLS))
            throw new ArgumentException("Tên CLS không hợp lệ");
        TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        TrangThai = trangThai;
    }
    public void NgungSuDung()
    {
        if (TrangThai == "Ngừng sử dụng")
            throw new InvalidOperationException("CLS đã ngừng");
        TrangThai = "Ngừng sử dụng";
    }
    public void KichHoat()
    {
        if (TrangThai == "Hoạt động")
            throw new InvalidOperationException("CLS đã hoạt động");
        TrangThai = "Hoạt động";
    }
}