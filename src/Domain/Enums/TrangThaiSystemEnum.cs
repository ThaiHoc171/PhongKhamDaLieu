namespace Domain.Enums;

public enum TrangThaiSystem
{
	HoatDong,
	VoHieu
}
public static class TrangThaiSystemExtensions
{
	public static string ToDbValue(this TrangThaiSystem tt)
		=> tt switch
		{
			TrangThaiSystem.HoatDong => "Hoạt động",
			TrangThaiSystem.VoHieu => "Vô hiệu",
			_ => throw new ArgumentOutOfRangeException()
		};
	public static TrangThaiSystem FromDb(string value)
		=> value switch
		{
			"Hoạt động" => TrangThaiSystem.HoatDong,
			"Vô hiệu" => TrangThaiSystem.VoHieu,
			_ => throw new ArgumentException("Trạng thái không hợp lệ")
		};
}

