using Domain.Enums;
namespace Application.DTOs;

public class ChiTietPCNThietBiUpdateDTO
{
    public string MaTaiSan { get; set; } = string.Empty;
	public string? TinhTrang { get; set; }
	public string? GhiChu { get; set; }
}
public class ChiTietPCNThietBiImport
{
    [ExcelColumn("PhongChucNangID")]
	public int PhongChucNangID { get; set; }
    [ExcelColumn("ThietBiID")]
	public int ThietBiID { get; set; }
    [ExcelColumn("MaTaiSan")]
	public string MaTaiSan { get; set; } = default!;
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
    public string PhongChucNang { get; init; } = string.Empty;
	public string ThietBi { get; init; } = string.Empty;
}
public class ChiTietPCNThietBiListReadModel
{
    public int ChiTietID { get; set; }
    public string MaTaiSan { get; set; } = default!;
    public string TinhTrang { get; set; } = default!;
    public DateTime NgayNhap { get; set; }
}