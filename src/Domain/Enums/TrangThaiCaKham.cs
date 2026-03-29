namespace Domain.Enums;

public enum TrangThaiCaKham
{
	Trong,
	DaDat,
	DaXacNhan,
	DangKham,
	HoanThanh,
	DaHuy,
	KhongDen
}
public static class TrangThaiCaKhamExtensions
{
	public static string ToDbValue(this TrangThaiCaKham tt)
		=> tt switch
		{
			TrangThaiCaKham.Trong => "Trống",
			TrangThaiCaKham.DaDat => "Đã đặt",
			TrangThaiCaKham.DaXacNhan => "Đã xác nhận",
			TrangThaiCaKham.DangKham => "Đang khám",
			TrangThaiCaKham.HoanThanh => "Hoàn thành",
			TrangThaiCaKham.DaHuy => "Đã hủy",
			TrangThaiCaKham.KhongDen => "Không đến",
			_ => throw new ArgumentOutOfRangeException()
		};
	public static TrangThaiCaKham FromDb(string value)
		=> value switch
		{
			"Trống" => TrangThaiCaKham.Trong,
			"Đã đặt" => TrangThaiCaKham.DaDat,
			"Đã xác nhận" => TrangThaiCaKham.DaXacNhan,
			"Đang khám" => TrangThaiCaKham.DangKham,
			"Hoàn thành" => TrangThaiCaKham.HoanThanh,
			"Đã hủy" => TrangThaiCaKham.DaHuy,
			"Không đến" => TrangThaiCaKham.KhongDen,
			_ => throw new ArgumentException("Trạng thái ca khám không hợp lệ")
		};
}