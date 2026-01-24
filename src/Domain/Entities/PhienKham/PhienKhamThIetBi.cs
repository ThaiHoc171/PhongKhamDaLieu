
namespace Domain.Entities;

public class PhienKhamThietBi
{
	public int PhienKhamThietBiID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int ThietBiID { get; private set; }
	public int SoLuong { get; private set; }
	public string? GhiChu { get; private set; }

	// Tạo mới
	public PhienKhamThietBi(int phienKhamID, int thietBiID, int soLuong, string? ghiChu)
	{
		if (soLuong <= 0)
			throw new ArgumentException("Số lượng thiết bị phải lớn hơn 0");

		PhienKhamID = phienKhamID;
		ThietBiID = thietBiID;
		SoLuong = soLuong;
		GhiChu = ghiChu;
	}

	// Map từ DB
	public PhienKhamThietBi(
		int phienKhamThietBiID,
		int phienKhamID,
		int thietBiID,
		int soLuong,
		string? ghiChu)
	{
		PhienKhamThietBiID = phienKhamThietBiID;
		PhienKhamID = phienKhamID;
		ThietBiID = thietBiID;
		SoLuong = soLuong;
		GhiChu = ghiChu;
	}

	public void CapNhat(int soLuong, string? ghiChu)
	{
		if (soLuong <= 0)
			throw new ArgumentException("Số lượng thiết bị phải lớn hơn 0");

		SoLuong = soLuong;
		GhiChu = ghiChu;
	}
}
