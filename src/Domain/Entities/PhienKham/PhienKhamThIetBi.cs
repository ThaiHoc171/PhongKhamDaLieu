namespace Domain.Entities;
public class PhienKhamThietBi
{
	public int PhienKhamThietBiID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int ChiTietID { get; private set; }
	public string? GhiChu { get; private set; }
	// Tạo mới
	public PhienKhamThietBi(int phienKhamID, int chiTietID, string? ghiChu)
	{
		PhienKhamID = phienKhamID;
		ChiTietID = chiTietID;
		GhiChu = ghiChu;
	}
	// Map từ DB
	public PhienKhamThietBi(
		int phienKhamThietBiID,
		int phienKhamID,
		int chiTietID,
		string? ghiChu)
	{
		PhienKhamThietBiID = phienKhamThietBiID;
		PhienKhamID = phienKhamID;
		ChiTietID = chiTietID;
		GhiChu = ghiChu;
	}
	public void CapNhatGhiChu(string? ghiChu)
	{
		GhiChu = ghiChu;
	}
}
