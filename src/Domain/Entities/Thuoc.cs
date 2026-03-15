namespace Domain.Entities;
public class Thuoc
{
    public int ThuocID { get; private set; }
    public string TenThuoc { get; private set; } = default!;
    public string? HoatChat { get; private set; }
    // Tạo mới
    public Thuoc(string tenThuoc, string? hoatChat)
    {
        TenThuoc = tenThuoc;
        HoatChat = hoatChat;
    }
    // Map DB
    public Thuoc(int thuocID, string tenThuoc, string? hoatChat)
    {
        ThuocID = thuocID;
        TenThuoc = tenThuoc;
        HoatChat = hoatChat;
    }
    public void CapNhat(string tenThuoc, string? hoatChat)
    {
        TenThuoc = tenThuoc;
        HoatChat = hoatChat;
    }
}