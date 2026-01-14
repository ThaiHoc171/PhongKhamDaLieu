namespace Domain.Entities;

public class LoaiBenh
{
	public int LoaiBenhID { get; private set; }
	public string TenBenh { get; private set; }
	public string? TenKhoaHoc { get; private set; }
	public string? NhomBenh { get; private set; }
	public string? MoTa { get; private set; }
	public string? DoPhoBien { get; private set; }
	public string? MucDoNghiemTrong { get; private set; }
	public DateTime NgayTao { get; private set; }

	// Tạo mới
	public LoaiBenh(
		string tenBenh,
		string? tenKhoaHoc,
		string? nhomBenh,
		string? moTa,
		string? doPhoBien,
		string? mucDoNghiemTrong)
	{
		if (string.IsNullOrWhiteSpace(tenBenh))
			throw new ArgumentException("Tên bệnh không hợp lệ");

		TenBenh = tenBenh.Trim();
		TenKhoaHoc = tenKhoaHoc?.Trim();
		NhomBenh = nhomBenh;
		MoTa = moTa;
		DoPhoBien = doPhoBien;
		MucDoNghiemTrong = mucDoNghiemTrong;
		NgayTao = DateTime.UtcNow;
	}

	// Map DB
	public LoaiBenh(
		int id,
		string tenBenh,
		string? tenKhoaHoc,
		string? nhomBenh,
		string? moTa,
		string? doPhoBien,
		string? mucDoNghiemTrong,
		DateTime ngayTao)
	{
		LoaiBenhID = id;
		TenBenh = tenBenh;
		TenKhoaHoc = tenKhoaHoc;
		NhomBenh = nhomBenh;
		MoTa = moTa;
		DoPhoBien = doPhoBien;
		MucDoNghiemTrong = mucDoNghiemTrong;
		NgayTao = ngayTao;
	}

	public void CapNhat(
		string tenBenh,
		string? tenKhoaHoc,
		string? nhomBenh,
		string? moTa,
		string? doPhoBien,
		string? mucDoNghiemTrong)
	{
		if (string.IsNullOrWhiteSpace(tenBenh))
			throw new ArgumentException("Tên bệnh không hợp lệ");

		TenBenh = tenBenh.Trim();
		TenKhoaHoc = tenKhoaHoc?.Trim();
		NhomBenh = nhomBenh;
		MoTa = moTa;
		DoPhoBien = doPhoBien;
		MucDoNghiemTrong = mucDoNghiemTrong;
	}

	// không trùng tên bệnh & tên khoa học
	public void KiemTraTrung(
		IEnumerable<LoaiBenh> danhSach)
	{
		if (danhSach.Any(x =>
			x.LoaiBenhID != LoaiBenhID &&
			string.Equals(x.TenBenh, TenBenh, StringComparison.OrdinalIgnoreCase)))
		{
			throw new ArgumentException("Tên bệnh đã tồn tại");
		}

		if (!string.IsNullOrWhiteSpace(TenKhoaHoc) &&
			danhSach.Any(x =>
				x.LoaiBenhID != LoaiBenhID &&
				!string.IsNullOrWhiteSpace(x.TenKhoaHoc) &&
				string.Equals(x.TenKhoaHoc, TenKhoaHoc, StringComparison.OrdinalIgnoreCase)))
		{
			throw new ArgumentException("Tên khoa học đã tồn tại");
		}
	}
}
