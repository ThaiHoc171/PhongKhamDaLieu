namespace Domain.Entities;

public class CanLamSang
{
	public int CanLamSangID { get; private set; }
	public string TenCLS { get; private set; }
	public string? MoTa { get; private set; }
	public decimal? Gia { get; private set; }
	public string LoaiXetNghiem { get; private set; }
	public DateTime NgayTao { get; private set; }
	public string TrangThai { get; private set; }

	// Tạo mới
	public CanLamSang(string tenCLS, string? moTa, decimal? gia, string loaiXetNghiem)
	{
		if (string.IsNullOrWhiteSpace(tenCLS))
			throw new ArgumentException("Tên CLS không hợp lệ");
		if (gia.HasValue && gia < 0)
			throw new ArgumentException("Giá CLS không được nhỏ hơn 0");

		TenCLS = tenCLS;
		MoTa = moTa;
		Gia = gia;
		LoaiXetNghiem = loaiXetNghiem;
		TrangThai = "Hoạt động";
		NgayTao = DateTime.UtcNow;
	}

	// Map DB
	public CanLamSang(
		int id, string tenCLS, string? moTa, decimal? gia,
		string loaiXetNghiem, DateTime ngayTao, string trangThai)
	{
		CanLamSangID = id;
		TenCLS = tenCLS;
		MoTa = moTa;
		Gia = gia;
		LoaiXetNghiem = loaiXetNghiem;
		NgayTao = ngayTao;
		TrangThai = trangThai;
	}

	public void CapNhat(string tenCLS, string? moTa, decimal? gia, string loaiXetNghiem)
	{
		if (gia.HasValue && gia < 0)
			throw new ArgumentException("Giá CLS không được nhỏ hơn 0");
		TenCLS = tenCLS;
		MoTa = moTa;
		Gia = gia;
		LoaiXetNghiem = loaiXetNghiem;
	}

	public void CapNhatTrangThai(string trangThai)
	{
		TrangThai = trangThai;
	}
}
