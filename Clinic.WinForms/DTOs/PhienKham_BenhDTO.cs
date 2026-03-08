namespace Clinic.WinForms.DTOs
{
	public class PhienKham_BenhRequestDTO
	{
		public int PhienKhamID { get; set; }
		public int LoaiBenhID { get; set; }
		public string LoaiChanDoan { get; set; } = default;
		public string GhiChu { get; set; }
	}
	public class PhienKham_BenhReadModel
	{
		public int Id { get; set; }
		public int PhienKhamID { get; set; }
		public string LoaiBenh { get; set; } = default;
		public string LoaiChanDoan { get; set; } = default;
		public string GhiChu { get; set; }
	}
}
