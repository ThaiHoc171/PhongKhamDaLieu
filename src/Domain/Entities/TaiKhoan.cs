using Domain.Enums;

namespace Domain.Entities;

public class TaiKhoan
{
	public int TaiKhoanID { get; private set; }
	public string Email { get; private set; }
	public string MatKhau { get; private set; }
	public string VaiTro { get; private set; }
	public string TrangThai { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }
	public string? FCMToken { get; private set; }

	// Constructor tạo mới
	public TaiKhoan(string email, string matKhau, string vaiTro)
	{
		Validate(email, matKhau, vaiTro, "Hoạt động");

		Email = email;
		MatKhau = matKhau;
		VaiTro = vaiTro;
		TrangThai = "Hoạt động";
	}

	// Constructor map DB
	public TaiKhoan(
		int taiKhoanID,
		string email,
		string matKhau,
		string vaiTro,
		string trangThai,
		DateTime ngayTao,
		DateTime? ngayCapNhat = null,
		string? fcmToken = null)
	{
		Validate(email, matKhau, vaiTro, trangThai);

		TaiKhoanID = taiKhoanID;
		Email = email;
		MatKhau = matKhau;
		VaiTro = vaiTro;
		TrangThai = trangThai;
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
		FCMToken = fcmToken;
	}

	// Business methods

	public void ChangePassword(string matKhauMoi)
	{
		if (string.IsNullOrWhiteSpace(matKhauMoi))
			throw new ArgumentException("Mật khẩu mới không hợp lệ");

		MatKhau = matKhauMoi;
		NgayCapNhat = DateTime.UtcNow;
	}

	public void Lock()
	{
		if (TrangThai == "Bị khóa")
			throw new InvalidOperationException("Tài khoản đã bị khóa");

		TrangThai = "Bị khóa";
		NgayCapNhat = DateTime.UtcNow;
	}

	public void Unlock()
	{
		if (TrangThai == "Hoạt động")
			throw new InvalidOperationException("Tài khoản đã được kích hoạt trước đó");

		TrangThai = "Hoạt động";
		NgayCapNhat = DateTime.UtcNow;
	}

	public void UpdateFcmToken(string? token)
	{
		FCMToken = token;
		NgayCapNhat = DateTime.UtcNow;
	}

	// Validate central
	private void Validate(string email, string matKhau, string vaiTro, string trangThai)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentException("Email không hợp lệ");

		if (string.IsNullOrWhiteSpace(matKhau))
			throw new ArgumentException("Mật khẩu không hợp lệ");

		if (string.IsNullOrWhiteSpace(vaiTro))
			throw new ArgumentException("Vai trò không hợp lệ");

		if (string.IsNullOrWhiteSpace(trangThai))
			throw new ArgumentException("Trạng thái không hợp lệ");

		// validate enum mapping
		VaiTroExtensions.ToEnum(vaiTro);
		TrangThaiSystemExtensions.FromDb(trangThai);
	}
}