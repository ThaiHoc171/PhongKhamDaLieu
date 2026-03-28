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
    public string? FCMToken { get; private set; }
    // Constructor tạo mới
    public TaiKhoan(string email, string matKhau, VaiTroEnum vaiTro)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentException("Email không hợp lệ");
		if (string.IsNullOrWhiteSpace(matKhau))
			throw new ArgumentException("Mật khẩu không hợp lệ");
		Email = email;
		MatKhau = matKhau;
		VaiTro = vaiTro.ToDbValue();
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
        string? fcmToken = null)
    {
        TaiKhoanID = taiKhoanID;
        Email = email;
        MatKhau = matKhau;
        VaiTro = vaiTro;
        TrangThai = trangThai;
        NgayTao = ngayTao;
        FCMToken = fcmToken; 
    }
    public void ChangePassword(string matKhauMoi)
	{
		if (string.IsNullOrWhiteSpace(matKhauMoi))
			throw new ArgumentException("Mật khẩu mới không hợp lệ");
		MatKhau = matKhauMoi;
	}
	public void Lock()
	{
		if (TrangThai == "Bị khóa")
			throw new InvalidOperationException("Tài khoản đã bị khóa");
		TrangThai = "Bị khóa";
	}
	public void Unlock()
	{
		if (TrangThai == "Hoạt động")
			throw new InvalidOperationException("Tài khoản đã hoạt động");
		TrangThai = "Hoạt động";
	}
    public void UpdateFcmToken(string? token)
    {
        FCMToken = token;
    }
    public VaiTroEnum Role()
		=> VaiTroExtensions.ToEnum(VaiTro);
}