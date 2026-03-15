using Domain.Enums;
namespace Domain.Entities;
public class LieuTrinhDieuTri
{
	public int LieuTrinhID { get; private set; }
	public int BenhNhanID { get; private set; }
	public int PhienKhamID { get; private set; }
	public string TenLieuTrinh { get; private set; }
	public int TongSoBuoi { get; private set; }
	public LieuTrinhEnum TrangThai { get; private set; }
	public string? GhiChu { get; private set; }
	public DateTime NgayBatDau { get; private set; }
	public DateTime NgayKetThuc { get; private set; }
	// Tạo mới
	public LieuTrinhDieuTri(int benhNhanID, int phienKhamID, string tenLieuTrinh, 
		int tongSoBuoi,string? ghiChu,DateTime ngayBatDau, DateTime ngayKetThuc)
	{
		if (benhNhanID <= 0)
			throw new ArgumentException("BenhNhanID không hợp lệ");
		if (phienKhamID <= 0)
			throw new ArgumentException("PhienKhamID không hợp lệ");
		if (string.IsNullOrWhiteSpace(tenLieuTrinh))
			throw new ArgumentException("Tên liệu trình không hợp lệ");
		if (tongSoBuoi <= 0)
			throw new ArgumentException("Tổng số buổi phải > 0");
		if (ngayKetThuc.Date <= ngayBatDau.Date)
			throw new ArgumentException("Ngày bắt đầu - kết thúc không hợp lệ");
		BenhNhanID = benhNhanID;
		PhienKhamID = phienKhamID;
		TenLieuTrinh = tenLieuTrinh;
		TongSoBuoi = tongSoBuoi;
		GhiChu = ghiChu;
		NgayBatDau = ngayBatDau;
		NgayKetThuc = ngayKetThuc;
		TrangThai = LieuTrinhEnum.DangThucHien;
	}
	// Map DB
	public LieuTrinhDieuTri( int lieuTrinhID, int benhNhanID, int phienKhamID, string tenLieuTrinh,
		int tongSoBuoi, string trangThai, string? ghiChu, DateTime ngayBatDau, DateTime ngayKetThuc)
	{
		LieuTrinhID = lieuTrinhID;
		BenhNhanID = benhNhanID;
		PhienKhamID = phienKhamID;
		TenLieuTrinh = tenLieuTrinh;
		TongSoBuoi = tongSoBuoi;
		TrangThai = LieuTrinhExtensions.FromDb(trangThai);
		GhiChu = ghiChu;
		NgayBatDau = ngayBatDau;
		NgayKetThuc = ngayKetThuc;
	}
	// Business logic
	public void Update(string tenLieuTrinh, int tongSoBuoi, DateTime ngayKetThuc)
	{
		if (TrangThai != LieuTrinhEnum.DangThucHien)
			throw new InvalidOperationException("Không thể cập nhật khi liệu trình đã kết thúc");
		if (tongSoBuoi <= 0)
			throw new ArgumentException("Tổng số buổi không hợp lệ");
		if (ngayKetThuc <= NgayBatDau)
			throw new ArgumentException("Ngày kết thúc không hợp lệ");
		TenLieuTrinh = tenLieuTrinh;
		TongSoBuoi = tongSoBuoi;
		NgayKetThuc = ngayKetThuc;
	}
	public void Status(string? ghiChu)
	{
		GhiChu = ghiChu;
	}
	public void Complete()
	{
		if (TrangThai != LieuTrinhEnum.DangThucHien)
			throw new InvalidOperationException("Không thể hoàn thành");
		TrangThai = LieuTrinhEnum.HoanThanh;
	}
	public void Cancel(string? ghiChu)
	{
		if (TrangThai == LieuTrinhEnum.HoanThanh)
			throw new InvalidOperationException("Không thể huỷ liệu trình đã hoàn thành");
		TrangThai = LieuTrinhEnum.Huy;
		GhiChu = ghiChu;
	}
}