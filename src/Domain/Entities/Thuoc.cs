namespace Domain.Entities;

public class Thuoc
{
	public int ThuocID { get; private set; }
	public string TenThuoc { get; private set; }
	public string? HoatChat { get; private set; }

	// Tạo mới
	public Thuoc(string tenThuoc, string? hoatChat)
	{
		if (string.IsNullOrWhiteSpace(tenThuoc))
			throw new ArgumentException("Tên thuốc không hợp lệ");

		TenThuoc = tenThuoc;
		HoatChat = hoatChat;
	}

	// Map DB
	public Thuoc(int id, string tenThuoc, string? hoatChat)
	{
		ThuocID = id;
		TenThuoc = tenThuoc;
		HoatChat = hoatChat;
	}

	public void CapNhat(string tenThuoc, string? hoatChat)
	{
		if (string.IsNullOrWhiteSpace(tenThuoc))
			throw new ArgumentException("Tên thuốc không hợp lệ");

		TenThuoc = tenThuoc;
		HoatChat = hoatChat;
	}
	public void KiemTraTrungTen(IEnumerable<Thuoc> danhSachThuoc)
	{
		if (danhSachThuoc.Any(t =>
			t.ThuocID != this.ThuocID &&
			string.Equals(t.TenThuoc.Trim(), TenThuoc.Trim(), StringComparison.OrdinalIgnoreCase)))
		{
			throw new ArgumentException("Tên thuốc đã tồn tại");
		}
	}

}
