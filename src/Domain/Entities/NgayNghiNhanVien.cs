namespace Domain.Entities;

public class NgayNghiNhanVien
{
	public int NgayNghiID { get; private set; }
	public int NhanVienID { get; private set; }
	public DateTime Ngay { get; private set; }
	public string? LyDo { get; private set; }

	// Constructor tạo mới
	public NgayNghiNhanVien(int nhanVienID, DateTime ngay, string? lyDo)
	{
		SetDate(ngay);
		NhanVienID = nhanVienID;
		LyDo = lyDo;
	}

	// Constructor map DB
	public NgayNghiNhanVien(
		int ngayNghiID,
		int nhanVienID,
		DateTime ngay,
		string? lyDo)
	{
		NgayNghiID = ngayNghiID;
		NhanVienID = nhanVienID;
		Ngay = ngay.Date;
		LyDo = lyDo;
	}


	public void Update(DateTime ngay, string? lyDo)
	{
		SetDate(ngay);
		LyDo = lyDo;
	}

	public void UpdateCause(string? lyDo)
	{
		LyDo = lyDo;
	}

	private void SetDate(DateTime ngay)
	{
		if (ngay.Date < DateTime.Today)
			throw new ArgumentException("Ngày nghỉ không được là ngày trong quá khứ.");

		Ngay = ngay.Date;
	}
}