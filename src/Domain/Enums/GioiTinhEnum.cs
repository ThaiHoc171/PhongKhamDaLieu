namespace Domain.Enums;
public enum GioiTinhEnum
{
	Nam,
	Nu,
	Khac
}
public static class GioiTinhExtensions
{
	public static string ToDbValue(this GioiTinhEnum gioiTinh)
		=> gioiTinh switch
		{
			GioiTinhEnum.Nam => "Nam",
			GioiTinhEnum.Nu => "Nữ",
			GioiTinhEnum.Khac => "Khác",
			_ => throw new ArgumentOutOfRangeException()
		};
	public static GioiTinhEnum FromDbValue(string value)
		=> value switch
		{
			"Nam" => GioiTinhEnum.Nam,
			"Nữ" => GioiTinhEnum.Nu,
			"Khác" => GioiTinhEnum.Khac,
			_ => throw new ArgumentException($"Giới tính không hợp lệ: {value}")
		};
	public static GioiTinhEnum ParseGioiTinhOrDefault(string? value)
	{
		if (string.IsNullOrWhiteSpace(value))
			return GioiTinhEnum.Khac;
		return GioiTinhExtensions.FromDbValue(value);
	}
}