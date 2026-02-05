namespace Application.DTOs;

public class PCNThietBiRequestCreateDTO
{
	public int ThietBiID { get; set; }
	public int SoLuong { get; set; }
}
public class PCNThietBiRequestUpdateDTO
{
	public int ThietBiID { get; set; }
	public int SoLuong { get; set; }
}
public class PCNThietBiResponseDTO
{
	public int Id { get; init; }
	public int PhongChucNangID { get; init; }
	public NameResponseDTO ThietBi { get; init; } = null!;
	public int SoLuong { get; init; }
	public string TinhTrang { get; init; }
	public DateTime NgayNhap { get; init; }
}
