namespace Domain.Entities;
public class KhungGioKham
{
    public int KhungGioID { get; private set; }
    public int CaLamViec { get; private set; }
    public TimeSpan GioBatDau { get; private set; }
    public TimeSpan GioKetThuc { get; private set; }
    public string? TenKhung { get; private set; }

    public KhungGioKham(int caLamViec, TimeSpan gioBatDau, TimeSpan gioKetThuc)
    {
        if (caLamViec is not (1 or 2))
            throw new ArgumentException("Ca làm việc không hợp lệ (1 hoặc 2)");
        if (gioBatDau >= gioKetThuc)
            throw new ArgumentException("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");
        CaLamViec = caLamViec;
        GioBatDau = gioBatDau;
        GioKetThuc = gioKetThuc;
    }
    public KhungGioKham(int khungGioID, int caLamViec, TimeSpan gioBatDau, TimeSpan gioKetThuc, string? tenKhung)
    {
        KhungGioID = khungGioID;
        CaLamViec = caLamViec;
        GioBatDau = gioBatDau;
        GioKetThuc = gioKetThuc;
        TenKhung = tenKhung;
    }
    public void CapNhat(int caLamViec, TimeSpan gioBatDau, TimeSpan gioKetThuc)
    {
        if (caLamViec is not (1 or 2))
            throw new ArgumentException("Ca làm việc không hợp lệ");
        if (gioBatDau >= gioKetThuc)
            throw new ArgumentException("Giờ bắt đầu phải nhỏ hơn giờ kết thúc");
        CaLamViec = caLamViec;
        GioBatDau = gioBatDau;
        GioKetThuc = gioKetThuc;
    }
    public bool KiemTraTrung(KhungGioKham other)
    {
        if (other == null)
            throw new ArgumentNullException(nameof(other));
        if (CaLamViec != other.CaLamViec)
            return false;
        return GioBatDau < other.GioKetThuc
            && GioKetThuc > other.GioBatDau;
    }
}