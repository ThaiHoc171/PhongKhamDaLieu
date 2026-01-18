namespace Domain.Entities;

public class PhienKham
{
	public int PhienKhamID { get; private set; }
	public int CaKhamID { get; private set; }
	public int BenhNhanID { get; private set; }
	public int NhanVienID { get; private set; }
	public int? PhongChucNangID { get; private set; }
	public string? TrieuChung { get; private set; }
	public string? GhiChu { get; private set; }
	public string? HinhAnhJSON { get; private set; }
	public string? ChuanDoanCuoi { get; private set; }
	public DateTime NgayKham { get; private set; }
	public string TrangThai { get; private set; } = default!;

	// Tạo mới
	public PhienKham(
		int caKhamID,
		int benhNhanID,
		int nhanVienID,
		int? phongChucNangID,
		string? trieuChung,
		string? ghiChu,
		string? hinhAnhJSON)
	{
		CaKhamID = caKhamID;
		BenhNhanID = benhNhanID;
		NhanVienID = nhanVienID;
		PhongChucNangID = phongChucNangID;
		TrieuChung = trieuChung;
		GhiChu = ghiChu;
		HinhAnhJSON = hinhAnhJSON;
		TrangThai = "Đang khám";
	}

	// Map DB
	public PhienKham(
		int phienKhamID,
		int caKhamID,
		int benhNhanID,
		int nhanVienID,
		int? phongChucNangID,
		string? trieuChung,
		string? ghiChu,
		string? hinhAnhJSON,
		string? chuanDoanCuoi,
		DateTime ngayKham,
		string trangThai)
	{
		PhienKhamID = phienKhamID;
		CaKhamID = caKhamID;
		BenhNhanID = benhNhanID;
		NhanVienID = nhanVienID;
		PhongChucNangID = phongChucNangID;
		TrieuChung = trieuChung;
		GhiChu = ghiChu;
		HinhAnhJSON = hinhAnhJSON;
		ChuanDoanCuoi = chuanDoanCuoi;
		NgayKham = ngayKham;
		TrangThai = trangThai;
	}
	// Nghiệp vụ
	public void CapNhat(
		string? trieuChung,
		string? ghiChu,
		int? phongChucNangID,
		string? hinhAnhJSON)
	{
		if (TrangThai != "Đang khám")
			throw new InvalidOperationException("Phiên khám đã kết thúc");

		TrieuChung = trieuChung;
		GhiChu = ghiChu;
		PhongChucNangID = phongChucNangID;
		HinhAnhJSON = hinhAnhJSON;
	}

	public void KetThuc(string chuanDoanCuoi)
	{
		if (TrangThai != "Đang khám")
			throw new InvalidOperationException("Không thể kết thúc");

		ChuanDoanCuoi = chuanDoanCuoi;
		TrangThai = "Hoàn thành";
	}

	public void Huy()
	{
		if (TrangThai == "Hoàn thành")
			throw new InvalidOperationException("Không thể huỷ");

		TrangThai = "Đã hủy";
	}
}
