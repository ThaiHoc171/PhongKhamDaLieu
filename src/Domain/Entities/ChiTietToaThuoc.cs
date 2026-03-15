namespace Domain.Entities;
public class ChiTietToaThuoc
{
	public int ChiTietToaThuocID { get; private set; }
	public int ToaThuocID { get; private set; }
	public int ThuocID { get; private set; }
	public string TenThuoc { get; private set; } = default!;
	public string? LieuDung { get; private set; }
	public int SoLuong { get; private set; }
	// Tạo mới
	public ChiTietToaThuoc(int thuocID, string? lieuDung, int soLuong)
	{
		if (thuocID <= 0)
			throw new ArgumentException("ThuocID không hợp lệ");
		if (soLuong <= 0)
			throw new ArgumentException("Số lượng phải > 0");
		ThuocID = thuocID;
		LieuDung = lieuDung;
		SoLuong = soLuong;
	}
	// Map DB
	public ChiTietToaThuoc(int chiTietToaThuocID ,int toaThuocID, 
		int thuocID, string tenThuoc, string? lieuDung, int soLuong)
	{
		ChiTietToaThuocID = chiTietToaThuocID;
		ToaThuocID = toaThuocID;
		ThuocID = thuocID;
		TenThuoc = tenThuoc;
		LieuDung = lieuDung;
		SoLuong = soLuong;
	}
	// Business method
	public void Update(string? lieuDung, int soLuong)
	{
		if (soLuong <= 0)
			throw new ArgumentException("Số lượng phải > 0");
		LieuDung = lieuDung;
		SoLuong = soLuong;
	}
}