namespace Domain.Enums;
public enum TrangThaiBuoiDieuTriEnum
{
	ChoXuLy,
	DangThucHien,
	HoanThanh,
	DaHuy
}
public static class TrangThaiBuoiDieuTriExtensions
{
	public static TrangThaiBuoiDieuTriEnum FromDb(string value)
	{
		return value switch
		{
			"Chờ xử lý" => TrangThaiBuoiDieuTriEnum.ChoXuLy,
			"Đang thực hiện" => TrangThaiBuoiDieuTriEnum.DangThucHien,
			"Hoàn thành" => TrangThaiBuoiDieuTriEnum.HoanThanh,
			"Đã huỷ" => TrangThaiBuoiDieuTriEnum.DaHuy,
			_ => throw new ArgumentException($"Trạng thái không hợp lệ: {value}")
		};
	}
	public static string ToDb(this TrangThaiBuoiDieuTriEnum value)
	{
		return value switch
		{
			TrangThaiBuoiDieuTriEnum.ChoXuLy => "Chờ xử lý",
			TrangThaiBuoiDieuTriEnum.DangThucHien => "Đang thực hiện",
			TrangThaiBuoiDieuTriEnum.HoanThanh => "Hoàn thành",
			TrangThaiBuoiDieuTriEnum.DaHuy => "Đã huỷ",
			_ => throw new ArgumentException("Trạng thái không hợp lệ")
		};
	}
}