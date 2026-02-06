namespace Application.DTOs;

public class PCNThietBiResponseDTO
{
	public int PCN_TB_ID { get; set; }
	public int PhongChucNangID { get; set; }
	public int ThietBiID { get; set; }
	public int TongSoLuong { get; set; }
}

public class PCNThietBiCreateDTO
{
	public int PhongChucNangID { get; set; }
	public int ThietBiID { get; set; }
}
