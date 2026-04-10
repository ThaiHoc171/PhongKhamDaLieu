namespace WPF.Models;

public class PhongChucNangRequestDTO
{
	public string TenPhong { get; set; } = default!;
	public string? MoTa { get; set; }
}
public class PhongChucNangReadModel
{
	public int PhongChucNangID { get; set; }
	public string TenPhong { get; set; } = default!;
	public string? MoTa { get; set; }
	public string TrangThai { get; set; } = default!;
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}
public class PhongChucNangReadListModel
{
	public int PhongChucNangID { get; set; }
	public string TenPhong { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
}