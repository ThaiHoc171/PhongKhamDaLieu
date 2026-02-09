using Domain.Enums;

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
	public string? ChanDoanCuoi { get; private set; }
	public DateTime NgayKham { get; private set; }
	public TrangThaiKhamEnum TrangThai { get; private set; } = default!;

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
		string? chanDoanCuoi,
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
		ChanDoanCuoi = chanDoanCuoi;
		NgayKham = ngayKham;
		TrangThai = TrangThaiKhamExtensions.FromDb(trangThai);
	}
	// Map Lite
	public PhienKham(
		int phienKhamID,
		int caKhamID,
		int benhNhanID,
		int nhanVienID,
		DateTime ngayKham,
		string trangThai,
		string chanDoanCuoi)
	{
		PhienKhamID = phienKhamID;
		CaKhamID = caKhamID;
		BenhNhanID = benhNhanID;
		NhanVienID = nhanVienID;
		NgayKham = ngayKham;
		TrangThai = TrangThaiKhamExtensions.FromDb(trangThai);
		ChanDoanCuoi = chanDoanCuoi;
	}
	// Nghiệp vụ
	public void CapNhat(
		string? trieuChung,
		string? ghiChu,
		int? phongChucNangID,
		string? hinhAnhJSON)
	{
		if (TrangThai != TrangThaiKhamEnum.DangKham)
			throw new InvalidOperationException("Phiên khám đã kết thúc");

		TrieuChung = trieuChung;
		GhiChu = ghiChu;
		PhongChucNangID = phongChucNangID;
		HinhAnhJSON = hinhAnhJSON;
	}

	public void KetThuc(string chanDoanCuoi)
	{
		if (TrangThai != TrangThaiKhamEnum.DangKham)
			throw new InvalidOperationException("Không thể kết thúc");

		ChanDoanCuoi = chanDoanCuoi;
		TrangThai = TrangThaiKhamEnum.HoanThanh;
	}

	public void Huy()
	{
		if (TrangThai == TrangThaiKhamEnum.HoanThanh)
			throw new InvalidOperationException("Không thể huỷ");

		TrangThai = TrangThaiKhamEnum.HuyKham;
	}
}
