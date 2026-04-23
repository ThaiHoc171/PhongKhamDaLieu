using WPF.Models;
namespace WPF.Common;
public static class Session
{
	public static int UserId { get; set; }
	public static string Email { get; set; } = string.Empty;
	public static string? VaiTro { get; set; }
	public static string Token { get; set; } = string.Empty;
	public static string RefreshToken { get; set; } = string.Empty;
	public static NameHelper HoTen { get; set; } = default!;
	public static int? NhanVienId { get; set; } 
	public static string? ChucVu { get; set; }
	public static List<string> Permissions { get; set; } = new List<string>();

	public static void Clear()
	{
		Token = "";
		UserId = 0;
		Email = "";
		NhanVienId = 0;
		VaiTro = "";
		RefreshToken = "";
		HoTen = new NameHelper();
		NhanVienId = 0;
		ChucVu = "";
		Permissions = new List<string>();
	}
}