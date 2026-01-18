namespace Domain.Entities;

public class ChiTietToaThuoc
{
	public int ChiTietToaThuocID { get; }
	public int ToaThuocID { get; }
	public int ThuocID { get; }
	public string TenThuoc { get; }
	public string? LieuDung { get; }
	public int SoLuong { get; }

	// tạo mới
	public ChiTietToaThuoc(int thuocID, string? lieuDung, int soLuong)
	{
		if (soLuong <= 0)
			throw new ArgumentException("Số lượng phải > 0");

		ThuocID = thuocID;
		LieuDung = lieuDung;
		SoLuong = soLuong;
	}

	// map DB
	public ChiTietToaThuoc(
		int id, int toaThuocID, int thuocID,
		string tenThuoc, string? lieuDung, int soLuong)
	{
		ChiTietToaThuocID = id;
		ToaThuocID = toaThuocID;
		ThuocID = thuocID;
		TenThuoc = tenThuoc;
		LieuDung = lieuDung;
		SoLuong = soLuong;
	}
}
