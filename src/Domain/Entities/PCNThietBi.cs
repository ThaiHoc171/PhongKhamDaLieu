namespace Domain.Entities;

public class PCNThietBi
{
    public int PCN_TB_ID { get; private set; }
    public int PhongChucNangID { get; private set; }
    public int ThietBiID { get; private set; }
    public int TongSoLuong { get; private set; }
    public PCNThietBi(int phongChucNangId, int thietBiId)
    {
        PhongChucNangID = phongChucNangId;
        ThietBiID = thietBiId;
        TongSoLuong = 0;
    }
    public PCNThietBi(
        int pcnTbId,
        int phongChucNangId,
        int thietBiId,
        int tongSoLuong)
    {
        PCN_TB_ID = pcnTbId;
        PhongChucNangID = phongChucNangId;
        ThietBiID = thietBiId;
        TongSoLuong = tongSoLuong;
    }
    public void CapNhatSoLuong(int soLuongMoi)
    {
        if (soLuongMoi < 0)
            throw new InvalidOperationException("Số lượng không hợp lệ");

        TongSoLuong = soLuongMoi;
    }
    public bool CoTheXoa()
    {
        return TongSoLuong == 0;
    }
}