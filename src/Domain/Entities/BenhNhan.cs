
namespace Domain.Entities;

public class BenhNhan
{
	public int BenhNhanID { get; private set; }
	public int ThongTinID { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime NgayCapNhat { get; private set; }
	public string GhiChu { get; private set; }

	public ThongTinCaNhan? ThongTinCaNhan { get; private set; }

	// Constructor dùng khi tạo mới từ DTO
	public BenhNhan(int thongTinID, string ghiChu = "")
	{
		if (thongTinID <= 0) throw new ArgumentException("ThongTinID không hợp lệ");

		ThongTinID = thongTinID;
		GhiChu = ghiChu;
	}

	// Constructor dùng khi map từ DB
	public BenhNhan(int benhNhanID, string ghiChu, ThongTinCaNhan? thongTinCaNhan)
	{
		BenhNhanID = benhNhanID;
		GhiChu = ghiChu;
		ThongTinCaNhan = thongTinCaNhan;
	}
	public BenhNhan(int benhNhanID, int thongTinID, string ghiChu, DateTime ngayTao, DateTime ngayCapNhat)
	{
		BenhNhanID = benhNhanID;
		ThongTinID= thongTinID;
		GhiChu = ghiChu;
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
	}
    public void CapNhat(string ghiChu)
	{
		GhiChu = ghiChu;
	}
}
