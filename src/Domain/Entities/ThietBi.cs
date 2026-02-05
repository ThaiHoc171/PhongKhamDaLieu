namespace Domain.Entities;

public class ThietBi
{
	public int Id { get; private set; }
	public string TenTB { get; private set; }
	public string? LoaiTB { get; private set; }

	// Tạo mới
	public ThietBi(string tenTB, string? loaiTB)
	{
		if (string.IsNullOrWhiteSpace(tenTB))
			throw new ArgumentException("Tên thiết bị không hợp lệ");

		TenTB = tenTB;
		LoaiTB = loaiTB;
	}

	// Map từ DB
	public ThietBi(
		int id,
		string tenTB,
		string? loaiTB)
	{
		Id = id;
		TenTB = tenTB;
		LoaiTB = loaiTB;
	}

	public void CapNhat(string tenTB, string? loaiTB)
	{
		if (string.IsNullOrWhiteSpace(tenTB))
			throw new ArgumentException("Tên thiết bị không hợp lệ");

		TenTB = tenTB;
		LoaiTB = loaiTB;
	}
}
