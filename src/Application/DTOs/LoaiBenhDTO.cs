namespace Application.DTOs;

public class LoaiBenhRequestDTO
{
    public string TenBenh { get; set; } = default!;
    public string? TenKhoaHoc { get; set; }
    public string? NhomBenh { get; set; }
    public string? MoTa { get; set; }
    public string? DoPhoBien { get; set; }
    public string? MucDoNghiemTrong { get; set; }
}
public class LoaiBenhUpdateDTO
{
    public string TenBenh { get; set; } = default!;
    public string? TenKhoaHoc { get; set; }
    public string? NhomBenh { get; set; }
    public string? MoTa { get; set; }
    public string? DoPhoBien { get; set; }
    public string? MucDoNghiemTrong { get; set; }
}
public class LoaiBenhReadModel
{
    public int LoaiBenhID { get; set; }
    public string TenBenh { get; set; } = default!;
    public string? TenKhoaHoc { get; set; }
    public string? NhomBenh { get; set; }
    public string? MoTa { get; set; }
    public string? DoPhoBien { get; set; }
    public string? MucDoNghiemTrong { get; set; }
    public DateTime NgayTao { get; set; }
}
public class LoaiBenhListReadModel
{
    public int LoaiBenhID { get; set; }
    public string TenBenh { get; set; } = default!;
    public string? NhomBenh { get; set; }
    public string? MucDoNghiemTrong { get; set; }
}