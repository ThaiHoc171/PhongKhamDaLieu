namespace WPF.Models;
public class ThietBiRequest
{
	public string TenTB { get; set; } = default!;
	public string LoaiTB { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
}
public class ThietBiReadListModel
{
	public int ThietBiID { get; set; }
	public string TenTB { get; set; } = default!;
	public string LoaiTB { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
}
public class ThietBiReadModel
{
	public int ThietBiID { get; set; }
	public string TenTB { get; set; } = default!;
	public string LoaiTB { get; set; } = default!;
	public string TrangThai { get; set; } = default!;
	public DateTime NgayTao { get; set; }
	public DateTime? NgayCapNhat { get; set; }
}