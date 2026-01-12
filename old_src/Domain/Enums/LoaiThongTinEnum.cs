namespace Domain.Enums;
public enum LoaiThongTinEnum
{
	NhanVien,
	BenhNhan
}
public static class LoaiThongTinExtensions
{
	public static string ToDbValue(this LoaiThongTinEnum loai)
		=> loai switch
		{
			LoaiThongTinEnum.NhanVien => "Nhân viên",
			LoaiThongTinEnum.BenhNhan => "Bệnh nhân",
			_ => throw new ArgumentOutOfRangeException()
		};

	public static LoaiThongTinEnum ToEnum(string value)
		=> value switch
		{
			"Nhân viên" => LoaiThongTinEnum.NhanVien,
			"Bệnh nhân" => LoaiThongTinEnum.BenhNhan,
			_ => throw new ArgumentException($"Loại thông tin không hợp lệ: {value}")
		};
}