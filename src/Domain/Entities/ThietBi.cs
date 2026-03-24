namespace Domain.Entities;
public class ThietBi
{
    public int ThietBiID { get; private set; }
    public string TenTB { get; private set; } = default!;
    public string? LoaiTB { get; private set; }
    public string? TrangThai {get; private set; }
    // Tạo mới
    public ThietBi(string tenTB, string loaiTB, string trangThai)
    {
        TenTB = tenTB;
        LoaiTB = loaiTB;
        TrangThai = trangThai;
    }
    // Map DB
    public ThietBi(int thietBiID, string tenTB, string loaiTB, string trangThai)
    {
        ThietBiID = thietBiID;
        TenTB = tenTB;
        LoaiTB = loaiTB;
        TrangThai = trangThai;
    }
    public void CapNhat(string tenTB, string? loaiTB, string trangThai)
    {
        TenTB = tenTB;
        LoaiTB = loaiTB;
        TrangThai = trangThai;
    }
}