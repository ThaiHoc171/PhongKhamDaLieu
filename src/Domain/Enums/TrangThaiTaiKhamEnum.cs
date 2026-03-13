namespace Domain.Enums;

public enum TrangThaiTaiKhamEnum
{
	ChoKham,
	DaKham,
	DaHuy
}
public static class TrangThaiTaiKhamExtensions
{
	public static TrangThaiTaiKhamEnum Parse(string? value)
	{
		return value switch
		{
			"Chờ khám" => TrangThaiTaiKhamEnum.ChoKham,
			"Đã khám" => TrangThaiTaiKhamEnum.DaKham,
			"Đã hủy" => TrangThaiTaiKhamEnum.DaHuy,
			_ => TrangThaiTaiKhamEnum.ChoKham
		};
	}

	public static string ToDbValue(this TrangThaiTaiKhamEnum trangThai)
	{
		return trangThai switch
		{
			TrangThaiTaiKhamEnum.ChoKham => "Chờ khám",
			TrangThaiTaiKhamEnum.DaKham => "Đã khám",
			TrangThaiTaiKhamEnum.DaHuy => "Đã hủy",
			_ => "Chờ khám"
		};
	}
}