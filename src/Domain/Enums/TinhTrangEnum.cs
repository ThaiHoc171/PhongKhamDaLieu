
using System.Threading.Tasks;

namespace Domain.Enums;

public enum TinhTrang
{
	HoatDong,
	Hong,
	BaoTri
}

public static class TinhTrangExtensions
{
	public static string ToDbValue(this TinhTrang tt)
		=> tt switch
		{
			TinhTrang.HoatDong => "Hoạt động",
			TinhTrang.Hong => "Hỏng",
			TinhTrang.BaoTri => "Bảo trì",
			_ => throw new ArgumentOutOfRangeException()
		};

	public static TinhTrang FromDb(string value)
		=> value switch
		{
			"Hoạt động" => TinhTrang.HoatDong,
			"Hỏng" => TinhTrang.Hong,
			"Bảo trì" => TinhTrang.BaoTri,
			_ => throw new ArgumentException("Tình trạng không hợp lệ")
		};
}
