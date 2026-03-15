namespace Application.DTOs;
public class ThietBiRequestDTO
{
    public string TenTB { get; set; } = default!;
    public string? LoaiTB { get; set; }
}
public class ThietBiUpdateDTO
{
    public string TenTB { get; set; } = default!;
    public string? LoaiTB { get; set; }
}
public class ThietBiReadModel
{
    public int ThietBiID { get; set; }
    public string TenTB { get; set; } = default!;
    public string? LoaiTB { get; set; }
}
public class ThietBiListReadModel
{
    public int ThietBiID { get; set; }
    public string TenTB { get; set; } = default!;
    public string? LoaiTB { get; set; }
}