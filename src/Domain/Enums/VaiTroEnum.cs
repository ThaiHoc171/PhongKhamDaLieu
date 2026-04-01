namespace Domain.Enums;
public enum VaiTroEnum
{
    Admin,
    NhanVien,
    BenhNhan,
    Khach
}
public static class VaiTroExtensions
{
    public static string ToDbValue(this VaiTroEnum vaiTro)
        => vaiTro switch
        {
            VaiTroEnum.Admin => "Admin",
            VaiTroEnum.NhanVien => "Nhân viên",
            VaiTroEnum.BenhNhan => "Bệnh nhân",
            VaiTroEnum.Khach => "Khách",
            _ => throw new ArgumentOutOfRangeException()
        };
    public static VaiTroEnum ToEnum(string value)
    {
		value = value.Trim();
		return value switch
		{
			"Admin" => VaiTroEnum.Admin,
			"Nhân viên" => VaiTroEnum.NhanVien,
			"Bệnh nhân" => VaiTroEnum.BenhNhan,
			"Khách" => VaiTroEnum.Khach,
			_ => throw new ArgumentException($"Vai trò không hợp lệ: {value}")
		};
	}
}