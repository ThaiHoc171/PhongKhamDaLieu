using Domain.Enums;
using Domain.ValueObjects;

namespace Domain.Entities;

public class ThongTinCaNhan
{
	public int ThongTinID { get; private set; }
	public int? TaiKhoanID { get; private set; }

	public string HoTen { get; private set; } = null!;
	public DateTime? NgaySinh { get; private set; }
	public string? GioiTinh { get; private set; }

	public string SDT { get; private set; } = null!;
	public string EmailLienHe { get; private set; } = null!;
	public string? DiaChi { get; private set; }
	public string? Avatar { get; private set; }

	public string Loai { get; private set; } = null!;

	public DateTime? NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }

	// Tạo mới (DÙNG ENUM)
	public ThongTinCaNhan(string hoTen,DateTime? ngaySinh,GioiTinhEnum gioiTinh,string sdt,string emailLienHe,
		string? diaChi,string? avatar,LoaiThongTinEnum loai,int? taiKhoanID)
	{
		if (string.IsNullOrWhiteSpace(hoTen))
			throw new ArgumentException("Họ tên không hợp lệ");

		if (string.IsNullOrWhiteSpace(sdt))
			throw new ArgumentException("SĐT không hợp lệ");
        TaiKhoanID = taiKhoanID;
        HoTen = hoTen;
		NgaySinh = ngaySinh;
		GioiTinh = gioiTinh.ToDbValue();
		SDT = sdt;
		EmailLienHe = Email.Create(emailLienHe).Value;
		DiaChi = diaChi;
		Avatar = avatar;
		Loai = loai.ToDbValue();
	}

	// Map từ DB
	public ThongTinCaNhan(
		int thongTinID,
		int? taiKhoanID,
		string hoTen,
		DateTime? ngaySinh,
		string? gioiTinh,
		string sdt,
		string emailLienHe,
		string? diaChi,
		string? avatar,
		string loai,
		DateTime ngayTao,
		DateTime? ngayCapNhat
	)
	{
		ThongTinID = thongTinID;
		TaiKhoanID = taiKhoanID;
		HoTen = hoTen;
		NgaySinh = ngaySinh;
		GioiTinh = gioiTinh;
		SDT = sdt;
		EmailLienHe = Email.Create(emailLienHe).Value;
		DiaChi = diaChi;
		Avatar = avatar;
		Loai = loai;
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
	}

	public LoaiThongTinEnum LayLoai()
		=> LoaiThongTinExtensions.ToEnum(Loai);

	public void CapNhat(
		string hoTen,
		DateTime? ngaySinh,
		GioiTinhEnum gioiTinh,
		string sdt,
		string emailLienHe,
		string? diaChi,
		string? avatar
	)
	{
		if (string.IsNullOrWhiteSpace(hoTen))
			throw new ArgumentException("Họ tên không hợp lệ");

		if (string.IsNullOrWhiteSpace(sdt))
			throw new ArgumentException("SĐT không hợp lệ");
		HoTen = hoTen;
		NgaySinh = ngaySinh;
		GioiTinh = gioiTinh.ToDbValue();
		SDT = sdt;
		EmailLienHe = Email.Create(emailLienHe).Value;
		DiaChi = diaChi;
		Avatar = avatar;
	}
}
