namespace Domain.Enums;

public enum TrangThaiKhamEnum
{
	DangKham,
	HoanThanh,
	HuyKham
}
public static class TrangThaiKhamExtensions
{
	public static string ToDbValue(this TrangThaiKhamEnum trangThai)
		=> trangThai switch
		{
			TrangThaiKhamEnum.DangKham => "Đang khám",
			TrangThaiKhamEnum.HoanThanh => "Hoàn thành",
			TrangThaiKhamEnum.HuyKham => "Đã hủy",
			_ => throw new ArgumentOutOfRangeException()
		};
	public static TrangThaiKhamEnum FromDb(string value)
		=> value switch
		{
			"Đang khám" => TrangThaiKhamEnum.DangKham,
			"Hoàn thành" => TrangThaiKhamEnum.HoanThanh,
			"Đã hủy" => TrangThaiKhamEnum.HuyKham,
			_ => throw new ArgumentException($"Trạng thái khám không hợp lệ: {value}")
		};
}