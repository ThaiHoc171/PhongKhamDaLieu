using Domain.Enums;
namespace Application.DTOs;

public class ChiTietPCNThietBiUpdateDTO
{
    public TinhTrang TinhTrang { get; set; }
    public string? GhiChu { get; set; }
}
public class ChiTietPCNThietBiRequestDTO
{
    public int PhongChucNangID { get; set; }
    public int ThietBiID { get; set; }
    public string MaTaiSan { get; set; } = default!;
    public string? GhiChu { get; set; }
}
public class ChiTietPCNThietBiReadModel
{
    public int ChiTietID { get; set; }
    public string MaTaiSan { get; set; } = default!;
    public DateTime NgayNhap { get; set; }
    public string TinhTrang { get; set; } = default!;
    public string? GhiChu { get; set; }
    public NameResponseDTO? PhongChucNang { get; init; }
    public NameResponseDTO? ThietBi { get; init; }
}
public class ChiTietPCNThietBiListReadModel
{
    public int ChiTietID { get; set; }
    public string MaTaiSan { get; set; } = default!;
    public string TinhTrang { get; set; } = default!;
    public DateTime NgayNhap { get; set; }
}