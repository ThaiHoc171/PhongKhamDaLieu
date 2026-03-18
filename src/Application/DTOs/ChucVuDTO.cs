namespace Application.DTOs;
public class ChucVuRequestDTO
{
    public string TenChucVu { get; set; } = "";
    public string? MoTa { get; set; }
}
public class ChucVuUpdateDTO
{
    public string TenChucVu { get; set; } = "";
    public string? MoTa { get; set; }
}
public class ChucVuReadModel
{
    public int ChucVuID { get; set; }
    public string TenChucVu { get; set; } = "";
    public string? MoTa { get; set; }
    public DateTime NgayTao { get; set; }
    public string TrangThai { get; set; } = "";
}
public class ChucVuListReadModel
{
    public int ChucVuID { get; set; }
    public string TenChucVu { get; set; } = "";
    public string TrangThai { get; set; } = "";
}