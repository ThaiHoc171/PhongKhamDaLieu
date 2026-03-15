using Domain.Enums;
namespace Domain.Entities;
public class PhienKham
{
	public int PhienKhamID { get; private set; }
	public int CaKhamID { get; private set; }
	public int BenhNhanID { get; private set; }
	public int NhanVienID { get; private set; }
	public int PhongChucNangID { get; private set; }
	public string? TrieuChung { get; private set; }
	public string? GhiChu { get; private set; }
	public string? HinhAnh { get; private set; }
	public string? ChanDoanCuoi { get; private set; }
	public DateTime NgayKham { get; private set; }
	public TrangThaiKhamEnum TrangThai { get; private set; }
	// Tạo mới
	public PhienKham( int caKhamID, int benhNhanID, int nhanVienID, int phongChucNangID)
	{
		CaKhamID = caKhamID;
		BenhNhanID = benhNhanID;
		NhanVienID = nhanVienID;
		PhongChucNangID = phongChucNangID;
	}
	// Map DB
	public PhienKham( int phienKhamID, int caKhamID, int benhNhanID, int nhanVienID, int phongChucNangID,
		string? trieuChung,	string? ghiChu,	string? hinhAnh, string? chanDoanCuoi, DateTime ngayKham,string trangThai)
	{
		PhienKhamID = phienKhamID;
		CaKhamID = caKhamID;
		BenhNhanID = benhNhanID;
		NhanVienID = nhanVienID;
		PhongChucNangID = phongChucNangID;
		TrieuChung = trieuChung;
		GhiChu = ghiChu;
		HinhAnh = hinhAnh;
		ChanDoanCuoi = chanDoanCuoi;
		NgayKham = ngayKham;
		TrangThai = TrangThaiKhamExtensions.FromDb(trangThai);
	}
	public void CapNhat(string? trieuChung, string? ghiChu, string? hinhAnh)
	{
		if (TrangThai != TrangThaiKhamEnum.DangKham)
			throw new InvalidOperationException("Phiên khám đã kết thúc");
		TrieuChung = trieuChung;
		GhiChu = ghiChu;
		HinhAnh = hinhAnh;
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