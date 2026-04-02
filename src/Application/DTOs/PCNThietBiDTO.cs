namespace Application.DTOs;

public class PCNThietBiUpdateDTO
{
    public int TongSoLuong { get; set; }
}
public class PCNThietBiRequestDTO
{
    public int PhongChucNangID { get; set; }
    public int ThietBiID { get; set; }
}
public class PCNThietBiReadModel
{
    public int PCN_TB_ID { get; set; }
    public NameResponseDTO PhongChucNang { get; set; }
    public NameResponseDTO ThietBi { get; init; } = default!;
    public int TongSoLuong { get; set; }
}
public class PCNThietBiReadListModel
{
    public int PCN_TB_ID { get; set; }
    public string? PhongChucNang { get; set; }
    public string? ThietBi { get; set; }
    public int TongSoLuong { get; set; }
}