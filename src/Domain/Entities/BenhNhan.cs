namespace Domain.Entities;

public class BenhNhan
{
	public int BenhNhanID { get; private set; }
	public int ThongTinID { get; private set; }
	public string GhiChu { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }

	public BenhNhan(int thongTinID, string? ghiChu)
	{
		Validate(thongTinID);

		ThongTinID = thongTinID;
		GhiChu = ghiChu ?? "";
	}
	public BenhNhan(int benhNhanID, int thongTinID, string ghiChu, DateTime ngayTao, DateTime? ngayCapNhat)
	{
		BenhNhanID = benhNhanID;
		ThongTinID = thongTinID;
		GhiChu = ghiChu;
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
	}

	public void CapNhat(string? ghiChu)
	{
		GhiChu = ghiChu ?? "";
		NgayCapNhat = DateTime.UtcNow;
	}

	private void Validate(int thongTinID)
	{
		if (thongTinID <= 0)
			throw new ArgumentException("ThongTinID không hợp lệ");
	}
}