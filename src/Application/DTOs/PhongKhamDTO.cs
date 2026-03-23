public class PhongKhamRequestDTO
{
    public string TenPhongKham { get; set; } = default!;
    public string? GioiThieu { get; set; }
    public string? DiaChi { get; set; }
    public string? Hotline { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? HinhAnhBanner { get; set; }
}

public class PhongKhamUpdateDTO : PhongKhamRequestDTO { }
public class PhongKhamUpdateTrangThaiDTO
{
    public string TrangThai { get; set; } = default!;
}
public class PhongKhamReadModel
{
    public int PhongKhamID { get; set; }
    public string TenPhongKham { get; set; } = default!;
    public string? GioiThieu { get; set; }
    public string? DiaChi { get; set; }
    public string? Hotline { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? HinhAnhBanner { get; set; }
    public string TrangThai { get; set; } = default!;
    public DateTime NgayTao { get; set; }
    public DateTime NgayCapNhat { get; set; }
}

public class PhongKhamListReadModel
{
    public int PhongKhamID { get; set; }
    public string TenPhongKham { get; set; } = default!;
    public string TrangThai { get; set; } = default!;
}