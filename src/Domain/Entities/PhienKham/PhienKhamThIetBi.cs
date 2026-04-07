namespace Domain.Entities;

public class PhienKhamThietBi
{
	public int PhienKhamThietBiID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int ChiTietID { get; private set; }
	public string? GhiChu { get; private set; }

	public PhienKhamThietBi(int phienKhamID, int chiTietID, string? ghiChu)
	{
		Validate(phienKhamID, chiTietID);

		PhienKhamID = phienKhamID;
		ChiTietID = chiTietID;
		GhiChu = ghiChu;
	}

	public PhienKhamThietBi(int phienKhamThietBiID, int phienKhamID, int chiTietID, string? ghiChu)
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
	private void Validate(int phienKhamID, int chiTietID)
	{
		if (phienKhamID <= 0)
			throw new ArgumentException("Phiên khám không hợp lệ");

		if (chiTietID <= 0)
			throw new ArgumentException("Chi tiết thiết bị không hợp lệ");
	}
}