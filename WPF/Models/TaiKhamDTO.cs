namespace HoanMyClinic.Models;
public class TaiKhamRequestDTO
{
	public int PhienKhamID { get; set; }
	public int CaKhamID { get; set; }
	public DateTime NgayDuKien { get; set; }
	public string? LyDo { get; set; }
}
public class TaiKhamUpdateRequestDTO
{
	public string? TrangThai { get; set; }
	public int? CaKhamID { get; set; }
}
public class TaiKhamReadListModel
{
	public int TaiKhamID { get; set; }
	public NameHelper BenhNhan { get; set; } = default!;
	public DateTime NgayDuKien { get; set; }
	public string? LyDo { get; set; }
	public string? TrangThai { get; set; }
}
public class TaiKhamReadModel
{
	public int TaiKhamID { get; set; }
	public int PhienKhamID { get; set; }
	public NameHelper BenhNhan { get; set; } = default!;
	public DateTime NgayDuKien { get; set; }
	public string? LyDo { get; set; }
	public string? TrangThai { get; set; }
	public int? CaKhamID { get; set; }
	public DateTime NgayTao { get; set; }
}
