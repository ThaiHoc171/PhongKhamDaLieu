namespace Application.ReadModels
{
	public class PCNThietBiReadModel
	{
		public int Id { get; init; }
		public int PCN_Id { get; init; }
		public int TB_Id { get; init; }
		public string? TB_Name { get; init; }
		public int SoLuong { get; init; }
		public string? GhiChu { get; init; }
	}
}
