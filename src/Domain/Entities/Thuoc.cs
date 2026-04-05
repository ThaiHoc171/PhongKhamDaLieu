namespace Domain.Entities;

public class Thuoc
{
	public int ThuocID { get; private set; }
	public string TenThuoc { get; private set; }
	public string HoatChat { get; private set; }

	// Tạo mới
	public Thuoc(string tenThuoc, string hoatChat)
	{
		Validate(tenThuoc, hoatChat);

		TenThuoc = tenThuoc.Trim();
		HoatChat = hoatChat.Trim();
	}
	// Map DB
	public Thuoc(int thuocID, string tenThuoc, string hoatChat)
	{
		ThuocID = thuocID;
		TenThuoc = tenThuoc;
		HoatChat = hoatChat;
	}

	public void CapNhat(string tenThuoc, string hoatChat)
	{
		Validate(tenThuoc, hoatChat);

		TenThuoc = tenThuoc.Trim();
		HoatChat = hoatChat.Trim();
	}
	private void Validate(string tenThuoc, string hoatChat)
	{
		if (string.IsNullOrWhiteSpace(tenThuoc))
			throw new ArgumentException("Tên thuốc không hợp lệ");

		if (string.IsNullOrWhiteSpace(hoatChat))
			throw new ArgumentException("Hoạt chất không hợp lệ");
	}
}