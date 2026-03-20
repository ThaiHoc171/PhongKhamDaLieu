using Domain.Enums;

namespace Domain.Entities;

public class ChiTietPCNThietBi
{
    public int ChiTietID { get; private set; }
    public int PCN_TB_ID { get; private set; }
    public string MaTaiSan { get; private set; }
    public DateTime NgayNhap { get; private set; }
    public TinhTrang TinhTrang { get; private set; }
    public string? GhiChu { get; private set; }
    public ChiTietPCNThietBi(int pcnTbId, string maTaiSan, string? ghiChu)
    {
        if (pcnTbId <= 0)
            throw new ArgumentException("PCN thiết bị không hợp lệ");

        if (string.IsNullOrWhiteSpace(maTaiSan))
            throw new ArgumentException("Mã tài sản không hợp lệ");

        PCN_TB_ID = pcnTbId;
        MaTaiSan = maTaiSan.Trim();
        GhiChu = ghiChu;
        NgayNhap = DateTime.Now;
        TinhTrang = TinhTrang.HoatDong;
    }
    public ChiTietPCNThietBi(
        int chiTietId,
        int pcnTbId,
        string maTaiSan,
        DateTime ngayNhap,
        string tinhTrangDb,
        string? ghiChu)
    {
        if (chiTietId <= 0)
            throw new ArgumentException("Chi tiết ID không hợp lệ");

        if (pcnTbId <= 0)
            throw new ArgumentException("PCN thiết bị không hợp lệ");

        if (string.IsNullOrWhiteSpace(maTaiSan))
            throw new ArgumentException("Mã tài sản không hợp lệ");

        ChiTietID = chiTietId;
        PCN_TB_ID = pcnTbId;
        MaTaiSan = maTaiSan.Trim();
        NgayNhap = ngayNhap;
        TinhTrang = TinhTrangExtensions.FromDb(tinhTrangDb);
        GhiChu = ghiChu;
    }
    public void ChuyenTinhTrang(TinhTrang tinhTrangMoi)
    {
        if (TinhTrang == tinhTrangMoi)
            return;

        // Rule: hỏng không thể dùng lại trực tiếp
        if (TinhTrang == TinhTrang.Hong && tinhTrangMoi == TinhTrang.HoatDong)
            throw new InvalidOperationException("Thiết bị hỏng cần bảo trì trước");

        TinhTrang = tinhTrangMoi;
    }
    public void CapNhatGhiChu(string? ghiChu)
    {
        GhiChu = ghiChu;
    }
    public bool DangSuDung()
    {
        return TinhTrang == TinhTrang.HoatDong;
    }
}