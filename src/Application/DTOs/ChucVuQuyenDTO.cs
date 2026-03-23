namespace Application.DTOs;
public class ChucVuQuyenDTO
{
	public int ChucVuID { get; set; }
	public List<int> QuyenIDs { get; set; } = new();
}
public class QuyenChecklistDTO
{
	public int QuyenID { get; set; }
	public string TenQuyen { get; set; } = "";
	public string Module { get; set; } = "";
	public bool Checked { get; set; }
}
public class QuyenReadModel
{
	public int QuyenID { get; set; }
	public string TenQuyen { get; set; } = "";
	public string Module { get; set; } = "";
}