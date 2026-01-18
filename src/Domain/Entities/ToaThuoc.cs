namespace Domain.Entities;

public class ToaThuoc
{
	public int ToaThuocID { get; }
	public int PhienKhamID { get; }
	public int NhanVienKeDonID { get; }
	public DateTime NgayLap { get; }
	public string? GhiChu { get; }

	// tạo mới
	public ToaThuoc(int phienKhamID, int nhanVienKeDonID, string? ghiChu)
	{
		PhienKhamID = phienKhamID;
		NhanVienKeDonID = nhanVienKeDonID;
		GhiChu = ghiChu;
	}

	// map DB
	public ToaThuoc(int id, int phienKhamID, int nhanVienKeDonID, DateTime ngayLap, string? ghiChu)
	{
		ToaThuocID = id;
		PhienKhamID = phienKhamID;
		NhanVienKeDonID = nhanVienKeDonID;
		NgayLap = ngayLap;
		GhiChu = ghiChu;
	}
}
