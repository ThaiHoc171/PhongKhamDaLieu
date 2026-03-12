namespace Domain.Entities;

public class Thuoc
{
    public int ThuocID { get; private set; }
    public string TenThuoc { get; private set; }
    public string? HoatChat { get; private set; }

    private Thuoc() { }

    public Thuoc(string tenThuoc, string? hoatChat)
    {
        SetTenThuoc(tenThuoc);
        HoatChat = hoatChat;
    }

    public Thuoc(int id, string tenThuoc, string? hoatChat)
    {
        ThuocID = id;
        SetTenThuoc(tenThuoc);
        HoatChat = hoatChat;
    }

    public void CapNhat(string tenThuoc, string? hoatChat)
    {
        SetTenThuoc(tenThuoc);
        HoatChat = hoatChat;
    }

    private void SetTenThuoc(string tenThuoc)
    {
        if (string.IsNullOrWhiteSpace(tenThuoc))
            throw new ArgumentException("Tên thuốc không hợp lệ");

        TenThuoc = tenThuoc.Trim();
    }

    public void KiemTraTrungTen(IEnumerable<Thuoc> ds)
    {
        if (ds.Any(x =>
            x.ThuocID != ThuocID &&
            x.TenThuoc.Equals(TenThuoc, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ArgumentException("Tên thuốc đã tồn tại");
        }
    }
}