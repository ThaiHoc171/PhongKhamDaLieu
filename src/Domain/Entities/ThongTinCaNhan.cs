using Domain.Enums;
using Domain.ValueObjects;
namespace Domain.Entities;
public class ThongTinCaNhan
{
	public int ThongTinID { get; private set; }
	public int? TaiKhoanID { get; private set; }
	public string HoTen { get; private set; }
	public DateTime NgaySinh { get; private set; }
	public GioiTinhEnum GioiTinh { get; private set; }
	public string SDT { get; private set; }
	public string? EmailLienHe { get; private set; }
	public string DiaChi { get; private set; }
	public string? Avatar { get; private set; }
	public LoaiThongTinEnum Loai { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }

	// Constructor tạo mới
	public ThongTinCaNhan(string hoTen, DateTime ngaySinh, GioiTinhEnum gioiTinh, string sdt, 
		string? emailLienHe, string diaChi, string? avatar, LoaiThongTinEnum loai, int? taiKhoanID)
	{
		Validate(hoTen, ngaySinh, sdt);

		TaiKhoanID = taiKhoanID;
		HoTen = hoTen;
		NgaySinh = ngaySinh;
		GioiTinh = gioiTinh;
		SDT = sdt;
		EmailLienHe = string.IsNullOrWhiteSpace(emailLienHe) ? null : Email.Create(emailLienHe).Value;
		DiaChi = diaChi;
		Avatar = avatar;
		Loai = loai;
	}

	// Constructor map DB
	public ThongTinCaNhan( int thongTinID, int? taiKhoanID, string hoTen, DateTime ngaySinh, GioiTinhEnum gioiTinh,
		string sdt, string? emailLienHe, string diaChi, string? avatar, LoaiThongTinEnum loai, DateTime ngayTao, DateTime? ngayCapNhat)
	{
		ThongTinID = thongTinID;
		TaiKhoanID = taiKhoanID;
		HoTen = hoTen;
		NgaySinh = ngaySinh;
		GioiTinh = gioiTinh;
		SDT = sdt;
		EmailLienHe = emailLienHe;
		DiaChi = diaChi;
		Avatar = avatar;
		Loai = loai;
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
	}

	public void CapNhat( string hoTen, DateTime ngaySinh, GioiTinhEnum gioiTinh, string sdt, 
		string? emailLienHe, string diaChi, string? avatar, LoaiThongTinEnum loai)
	{
		Validate(hoTen, ngaySinh, sdt);
		HoTen = hoTen;
		NgaySinh = ngaySinh;
		GioiTinh = gioiTinh;
		SDT = sdt;
		EmailLienHe = string.IsNullOrWhiteSpace(emailLienHe) ? null : Email.Create(emailLienHe).Value;
		DiaChi = diaChi;
		Avatar = avatar;
		Loai = loai;

		NgayCapNhat = DateTime.UtcNow;
	}
	public void CapNhatTaiKhoan(int taiKhoanID, string emailLienHe)
	{
		TaiKhoanID = taiKhoanID;
		EmailLienHe = emailLienHe;
		NgayCapNhat = DateTime.UtcNow;
	}
	private void Validate(string hoTen,DateTime ngaySinh,string sdt)
	{
		if (string.IsNullOrWhiteSpace(hoTen))
			throw new ArgumentException("Họ tên không hợp lệ");
		if (ngaySinh > DateTime.UtcNow)
			throw new ArgumentException("Ngày sinh không hợp lệ");
		if (string.IsNullOrWhiteSpace(sdt))
			throw new ArgumentException("SĐT không hợp lệ");
	}
}