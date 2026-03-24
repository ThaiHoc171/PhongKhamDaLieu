namespace Domain.Entities;

public class CanLamSang
{
    public int CanLamSangID { get; private set; }
    public string TenCLS { get; private set; }
    public string? MoTa { get; private set; }
    public string LoaiXetNghiem { get; private set; }
    public DateTime NgayTao { get; private set; }
    public string TrangThai { get; private set; }

    public CanLamSang(string tenCLS, string moTa, string loaiXetNghiem, string trangThai)
    {
        if (string.IsNullOrWhiteSpace(tenCLS))
            throw new ArgumentException("Tên cận lâm sàng không hợp lệ");
        TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        NgayTao = DateTime.UtcNow;
        TrangThai = trangThai;
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
        if (string.IsNullOrWhiteSpace(tenCLS))
            throw new ArgumentException("Tên cận lâm sàng không hợp lệ");
        TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        TrangThai = trangThai;
    }
}