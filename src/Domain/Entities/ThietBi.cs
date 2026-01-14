namespace Domain.Entities;

public class ThietBi
{
	public int Id { get; private set; }
	public string TenTB { get; private set; }
	public string? LoaiTB { get; private set; }
	public string TinhTrang { get; private set; }
	public DateTime NgayNhap { get; private set; }

	// Tạo mới
	public ThietBi(string tenTB, string? loaiTB)
	{
		if (string.IsNullOrWhiteSpace(tenTB))
			throw new ArgumentException("Tên thiết bị không hợp lệ");

		TenTB = tenTB;
		LoaiTB = loaiTB;
		TinhTrang = "Hoạt động";
		NgayNhap = DateTime.UtcNow;
	}

	// Map từ DB
	public ThietBi(
		int id,
		string tenTB,
		string? loaiTB,
		string tinhTrang,
		DateTime ngayNhap)
	{
		Id = id;
		TenTB = tenTB;
		LoaiTB = loaiTB;
		TinhTrang = tinhTrang;
		NgayNhap = ngayNhap;
	}

	public void CapNhat(string tenTB, string? loaiTB)
	{
		if (string.IsNullOrWhiteSpace(tenTB))
			throw new ArgumentException("Tên thiết bị không hợp lệ");

		TenTB = tenTB;
		LoaiTB = loaiTB;
	}
	public void ChuyenTinhTrang(string trangThaiMoi)
	{
		if (string.IsNullOrWhiteSpace(trangThaiMoi))
			throw new ArgumentException("Trạng thái mới không hợp lệ");
		TinhTrang = trangThaiMoi;
	}
}
