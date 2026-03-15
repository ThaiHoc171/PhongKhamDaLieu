namespace Domain.Entities;
public class ToaThuoc
{
	public int ToaThuocID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int NhanVienKeDonID { get; private set; }
	public DateTime NgayLap { get; private set; }
	public string? GhiChu { get; private set; }
	public ToaThuoc(int phienKhamID, int nhanVienKeDonID, string? ghiChu)
	{
		if (phienKhamID <= 0)
			throw new ArgumentException("PhienKhamID không hợp lệ");
		if (nhanVienKeDonID <= 0)
			throw new ArgumentException("NhanVienKeDonID không hợp lệ");
		PhienKhamID = phienKhamID;
		NhanVienKeDonID = nhanVienKeDonID;
		GhiChu = ghiChu;
	}
	// Map DB
	public ToaThuoc(int toaThuocID, int phienKhamID, int nhanVienKeDonID, DateTime ngayLap,	string? ghiChu)
	{
		ToaThuocID = toaThuocID;
		PhienKhamID = phienKhamID;
		NhanVienKeDonID = nhanVienKeDonID;
		NgayLap = ngayLap;
		GhiChu = ghiChu;
	}
	public void Note(string? ghiChu)
	{
		GhiChu = ghiChu;
	}
}