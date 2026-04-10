using WPF.Models;
namespace WPF.Common;
public static class Session
{
	public static int UserId { get; set; }
	public static string? VaiTro { get; set; }
	public static string Token { get; set; } = default!;
	public static string RefreshToken { get; set; } = default!;
	public static NameHelper HoTen { get; set; } = default!;
	public static int? NhanVienId { get; set; } 
	public static string? ChucVu { get; set; }

	public static void Clear()
	{
		Token = "";
		UserId = 0;
		NhanVienId = 0;
		VaiTro = "";
		RefreshToken = "";
		HoTen = new NameHelper();
		NhanVienId = 0;
		ChucVu = "";
	}
}