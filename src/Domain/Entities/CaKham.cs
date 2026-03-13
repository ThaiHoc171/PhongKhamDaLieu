namespace Domain.Entities;
public class CaKham
{
	public int CaKhamID { get; private set; }
	public string LoaiCaKham { get; private set; }
	public int? LichLamViecID { get; private set; }
	public int KhungGioID { get; private set; }
	public int? PhongChucNangID { get; private set; }
	public int? ThongTinID { get; private set; }
	public string? LyDoKham { get; private set; }
	public string TrangThai { get; private set; }
	public DateTime? NgayDat { get; private set; }
	public DateTime NgayKham { get; private set; }
	public string? GhiChu { get; private set; }
	public CaKham( string loaiCaKham, int khungGioID, DateTime ngayKham)
	{
		if (khungGioID <= 0)
			throw new ArgumentException("Khung giờ không hợp lệ");
		if (ngayKham.Date < DateTime.Today)
			throw new ArgumentException("Ngày khám không hợp lệ");
		LoaiCaKham = loaiCaKham;
		KhungGioID = khungGioID;
		NgayKham = ngayKham.Date;
		TrangThai = "Trống";
	}
	public CaKham(int caKhamID, string loaiCaKham, int? lichLamViecID, int khungGioID, int? phongChucNangID,
		int? thongTinID, string? lyDoKham, string trangThai, DateTime? ngayDat, DateTime ngayKham, string? ghiChu)
	{
		CaKhamID = caKhamID;
		LoaiCaKham = loaiCaKham;
		LichLamViecID = lichLamViecID;
		KhungGioID = khungGioID;
		PhongChucNangID = phongChucNangID;
		ThongTinID = thongTinID;
		LyDoKham = lyDoKham;
		TrangThai = trangThai;
		NgayDat = ngayDat;
		NgayKham = ngayKham;
		GhiChu = ghiChu;
	}
	public void GanNhanVien(int lichLamViecID)
	{
		LichLamViecID = lichLamViecID;
	}
	public void GanPhong(int phongChucNangID)
	{
		PhongChucNangID = phongChucNangID;
	}
	public void DangKyKham(
		int thongTinID,
		string lyDoKham,
		DateTime ngayDat,
		string? ghiChu)
	{
		if (TrangThai != "Trống")
			throw new InvalidOperationException("Ca khám đã được đặt");
		ThongTinID = thongTinID;
		LyDoKham = lyDoKham;
		NgayDat = ngayDat;
		GhiChu = ghiChu;
		TrangThai = "Đã đặt";
	}
	public void HuyDangKy()
	{
		if (TrangThai != "Đã đặt")
			throw new InvalidOperationException("Ca khám chưa được đặt");
		ThongTinID = null;
		LyDoKham = null;
		NgayDat = null;
		GhiChu = null;
		TrangThai = "Trống";
	}
	public void CapNhatTrangThai(string trangThaiMoi)
	{
		if (string.IsNullOrWhiteSpace(trangThaiMoi))
			throw new ArgumentException("Trạng thái không hợp lệ");
		TrangThai = trangThaiMoi;
	}
}