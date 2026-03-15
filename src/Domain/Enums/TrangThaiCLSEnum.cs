namespace Domain.Enums;
public enum TrangThaiCLSEnum
{
	DangCho,
	DangThucHien,
	HoanThanh,
	DaHuy
}
public static class TrangThaiCLSExtensions
{
	public static string ToDbValue(this TrangThaiCLSEnum trangThai)
		=> trangThai switch
		{
			TrangThaiCLSEnum.DangCho => "Đang chờ",
			TrangThaiCLSEnum.DangThucHien => "Đang thực hiện",
			TrangThaiCLSEnum.HoanThanh => "Hoàn thành",
			TrangThaiCLSEnum.DaHuy => "Đã hủy",
			_ => throw new ArgumentOutOfRangeException()
		};
	public static TrangThaiCLSEnum ToEnum(string value)
		=> value switch
		{
			"Đang chờ" => TrangThaiCLSEnum.DangCho,
			"Đang thực hiện" => TrangThaiCLSEnum.DangThucHien,
			"Hoàn thành" => TrangThaiCLSEnum.HoanThanh,
			"Đã hủy" => TrangThaiCLSEnum.DaHuy,
			_ => throw new ArgumentException($"Trạng thái CLS không hợp lệ: {value}")
		};
}