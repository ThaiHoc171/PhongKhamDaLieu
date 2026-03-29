namespace Domain.Entities;

public class LichLamViec
{
	public int LichLamViecID { get; private set; }
	public int NhanVienID { get; private set; }
	public DateTime Ngay { get; private set; }
	public int CaLamViec { get; private set; }
	public string? GhiChu { get; private set; }

	// Constructor tạo mới
	public LichLamViec(int nhanVienID, DateTime ngay, int caLamViec, string? ghiChu)
	{
		Validate(nhanVienID, ngay, caLamViec);

		NhanVienID = nhanVienID;
		Ngay = ngay.Date;
		CaLamViec = caLamViec;
		GhiChu = ghiChu;
	}

	// Constructor map DB
	public LichLamViec(int lichLamViecID,int nhanVienID, DateTime ngay, int caLamViec, string? ghiChu)
	{
		LichLamViecID = lichLamViecID;
		NhanVienID = nhanVienID;
		Ngay = ngay.Date;
		CaLamViec = caLamViec;
		GhiChu = ghiChu;
	}
	private void Validate(int nhanVienID, DateTime ngay, int caLamViec)
	{
		if (nhanVienID <= 0)
			throw new ArgumentException("Nhân viên không hợp lệ");

		if (ngay.Date < DateTime.Today)
			throw new ArgumentException("Ngày làm việc không được là ngày trong quá khứ");

		if (caLamViec < 1 || caLamViec > 2)
			throw new ArgumentException("Ca làm việc không hợp lệ");
	}
}