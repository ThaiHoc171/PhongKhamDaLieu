using Domain.Enums;
namespace Domain.Entities;
public class PhongChucNang
{
	public int PhongChucNangID { get; private set; }
	public string TenPhong { get; private set; }
	public string? MoTa { get; private set; }
	public TinhTrang TrangThai { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }
	// =========================
	// Constructor tạo mới
	// =========================
	public PhongChucNang(string tenPhong, string? moTa)
	{
		if (string.IsNullOrWhiteSpace(tenPhong))
			throw new ArgumentException("Tên phòng không hợp lệ");
		TenPhong = tenPhong;
		MoTa = moTa;
	}
	// =========================
	// Constructor map DB
	// =========================
	public PhongChucNang(
		int phongChucNangID,
		string tenPhong,
		string? moTa,
		string trangThai,
		DateTime ngayTao,
		DateTime? ngayCapNhat)
	{
		PhongChucNangID = phongChucNangID;
		TenPhong = tenPhong;
		MoTa = moTa;
		TrangThai = TinhTrangExtensions.FromDb(trangThai);
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
	}
	// =========================
	// Business Methods
	// =========================
	public void CapNhat(string tenPhong, string? moTa)
	{
		if (string.IsNullOrWhiteSpace(tenPhong))
			throw new ArgumentException("Tên phòng không hợp lệ");
		TenPhong = tenPhong;
		MoTa = moTa;
		NgayCapNhat = DateTime.UtcNow;
	}
	public void ChuyenTrangThai(TinhTrang trangThaiMoi)
	{
		if (TrangThai == TinhTrang.Hong && trangThaiMoi == TinhTrang.HoatDong)
			throw new InvalidOperationException("Phòng đang hỏng, cần bảo trì trước");
		TrangThai = trangThaiMoi;
		NgayCapNhat = DateTime.UtcNow;
	}
	public void BaoTri()
	{
		TrangThai = TinhTrang.BaoTri;
		NgayCapNhat = DateTime.UtcNow;
	}
	public void BaoHong()
	{
		TrangThai = TinhTrang.Hong;
		NgayCapNhat = DateTime.UtcNow;
	}
}