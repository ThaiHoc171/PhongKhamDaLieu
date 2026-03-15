namespace Domain.Enums;
public enum TaiKhamEnum
{
	ChoKham,
	DaKham,
	DaHuy
}
public static class TaiKhamExtensions
{
	public static TaiKhamEnum Parse(string? value)
	{
		return value switch
		{
			"Chờ khám" => TaiKhamEnum.ChoKham,
			"Đã khám" => TaiKhamEnum.DaKham,
			"Đã hủy" => TaiKhamEnum.DaHuy,
			_ => TaiKhamEnum.ChoKham
		};
	}
	public static string ToDbValue(this TaiKhamEnum trangThai)
	{
		return trangThai switch
		{
			TaiKhamEnum.ChoKham => "Chờ khám",
			TaiKhamEnum.DaKham => "Đã khám",
			TaiKhamEnum.DaHuy => "Đã hủy",
			_ => "Chờ khám"
		};
	}
}