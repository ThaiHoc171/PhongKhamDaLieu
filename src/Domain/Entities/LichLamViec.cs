namespace Domain.Entities;
public class LichLamViec
{
	public int LichLamViecID { get; private set; }
	public int NhanVienID { get; private set; }
	public DateTime Ngay { get; private set; }
	public int CaLamViec { get; private set; }
	public string? GhiChu { get; private set; }
	// Constructor tạo mới
	public LichLamViec(
		int nhanVienID,
		DateTime ngay,
		int caLamViec,
		string? ghiChu)
	{
		SetNgay(ngay);
		SetCaLamViec(caLamViec);
		NhanVienID = nhanVienID;
		GhiChu = ghiChu;
	}
	// Constructor map DB
	public LichLamViec(
		int lichLamViecID,
		int nhanVienID,
		DateTime ngay,
		int caLamViec,
		string? ghiChu)
	{
		LichLamViecID = lichLamViecID;
		NhanVienID = nhanVienID;
		Ngay = ngay.Date;
		CaLamViec = caLamViec;
		GhiChu = ghiChu;
	}
	public void Update(DateTime ngay, int caLamViec, string? ghiChu)
	{
		SetNgay(ngay);
		SetCaLamViec(caLamViec);
		GhiChu = ghiChu;
	}
	public void DoiNgay(DateTime ngay)
	{
		SetNgay(ngay);
	}
	public void DoiCa(int caLamViec)
	{
		SetCaLamViec(caLamViec);
	}
	public void CapNhatGhiChu(string? ghiChu)
	{
		GhiChu = ghiChu;
	}
	private void SetNgay(DateTime ngay)
	{
		if (ngay.Date < DateTime.Today)
			throw new ArgumentException("Ngày làm việc không được là ngày trong quá khứ.");
		Ngay = ngay.Date;
	}
	private void SetCaLamViec(int caLamViec)
	{
		if (caLamViec < 1 || caLamViec > 2)
			throw new ArgumentException("Ca làm việc không hợp lệ.");
		CaLamViec = caLamViec;
	}
}