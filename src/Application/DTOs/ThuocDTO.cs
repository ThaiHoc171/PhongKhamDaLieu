namespace Application.DTOs;

public class ThuocRequestDTO
{
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}

public class ThuocUpdateDTO
{
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}

public class ThuocReadModel
{
    public int ThuocID { get; set; }
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}

public class ThuocListReadModel
{
    public int ThuocID { get; set; }
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}