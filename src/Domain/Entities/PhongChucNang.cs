namespace Domain.Entities;
public class PhongChucNang
{
	public int Id { get; private set; }
	public string TenPhong { get; private set; }
	public string? LoaiPhong { get; private set; }
	public string? MoTa { get; private set; }
	public string TrangThai { get; private set; }
	public DateTime NgayTao { get; private set; }

	// Tạo mới
	public PhongChucNang(string tenPhong, string? loaiPhong, string? moTa)
	{
		if (string.IsNullOrWhiteSpace(tenPhong))
			throw new ArgumentException("Tên phòng không hợp lệ");

		TenPhong = tenPhong;
		LoaiPhong = loaiPhong;
		MoTa = moTa;
		TrangThai = "Hoạt động";
		NgayTao = DateTime.UtcNow;
	}

	// Map từ DB
	public PhongChucNang(
		int id,
		string tenPhong,
		string? loaiPhong,
		string? moTa,
		string trangThai,
		DateTime ngayTao)
	{
		Id = id;
		TenPhong = tenPhong;
		LoaiPhong = loaiPhong;
		MoTa = moTa;
		TrangThai = trangThai;
		NgayTao = ngayTao;
	}
	public void CapNhat(string tenPhong, string? loaiPhong, string? moTa)
	{
		if (string.IsNullOrWhiteSpace(tenPhong))
			throw new ArgumentException("Tên phòng không hợp lệ");
		TenPhong = tenPhong;
		LoaiPhong = loaiPhong;
		MoTa = moTa;
	}
	public void ChuyenTrangThai(string trangThaiMoi)
	{
		TrangThai = trangThaiMoi;
	}
}
