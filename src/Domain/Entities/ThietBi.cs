namespace Domain.Entities;

public class ThietBi
{
    public int ThietBiID { get; private set; }
    public string TenTB { get; private set; } = default!;
    public string? LoaiTB { get; private set; }

    // Tạo mới
    public ThietBi(string tenTB, string? loaiTB)
    {
        TenTB = tenTB;
        LoaiTB = loaiTB;
    }

    // Map DB
    public ThietBi(int thietBiID, string tenTB, string? loaiTB)
    {
        ThietBiID = thietBiID;
        TenTB = tenTB;
        LoaiTB = loaiTB;
    }

    public void CapNhat(string tenTB, string? loaiTB)
    {
        TenTB = tenTB;
        LoaiTB = loaiTB;
    }
}