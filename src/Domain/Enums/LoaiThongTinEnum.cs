namespace Domain.Enums;
public enum LoaiThongTinEnum
{
	NhanVien,
	BenhNhan,
	Khach
}
public static class LoaiThongTinExtensions
{
	public static string ToDbValue(this LoaiThongTinEnum loai)
		=> loai switch
		{
			LoaiThongTinEnum.NhanVien => "Nhân viên",
			LoaiThongTinEnum.BenhNhan => "Bệnh nhân",
			LoaiThongTinEnum.Khach => "Khách",
			_ => throw new ArgumentOutOfRangeException()
		};
	public static LoaiThongTinEnum ToEnum(string value)
		=> value switch
		{
			"Nhân viên" => LoaiThongTinEnum.NhanVien,
			"Bệnh nhân" => LoaiThongTinEnum.BenhNhan,
			"Khách" => LoaiThongTinEnum.Khach,
			_ => throw new ArgumentException($"Loại thông tin không hợp lệ: {value}")
		};
}