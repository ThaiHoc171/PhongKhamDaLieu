namespace Domain.Enums;
public enum LieuTrinhEnum
{
	DangThucHien,
	HoanThanh,
	Huy
}
public static class LieuTrinhExtensions
{
	public static LieuTrinhEnum FromDb(string? value)
	{
		return value switch
		{
			"Đang thực hiện" => LieuTrinhEnum.DangThucHien,
			"Hoàn thành" => LieuTrinhEnum.HoanThanh,
			"Huỷ" => LieuTrinhEnum.Huy,
			_ => LieuTrinhEnum.DangThucHien
		};
	}
	public static string ToDb(this LieuTrinhEnum value)
	{
		return value switch
		{
			LieuTrinhEnum.DangThucHien => "Đang thực hiện",
			LieuTrinhEnum.HoanThanh => "Hoàn thành",
			LieuTrinhEnum.Huy => "Huỷ",
			_ => "Đang thực hiện"
		};
	}
}