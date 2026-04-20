using Domain.Enums;
namespace Domain.Entities;
public class BuoiDieuTri
{
	public int BuoiDieuTriID { get; private set; }
	public int LieuTrinhID { get; private set; }
	public int CaKhamID { get; private set; }
	public int SoBuoi { get; private set; }
	public DateTime? NgayDuKien { get; private set; }
	public DateTime? NgayThucHien { get; private set; }
	public int? NhanVienID { get; private set; }
	public TrangThaiBuoiDieuTriEnum TrangThai { get; private set; }
	public string? GhiChu { get; private set; }
	public string? HinhAnhJSON { get; private set; }
	public BuoiDieuTri(
		int lieuTrinhID,
		int caKhamID,
		int soBuoi,
		DateTime? ngayDuKien)
	{
		if (lieuTrinhID <= 0)
			throw new ArgumentException("LieuTrinhID không hợp lệ");
		if (caKhamID <= 0)
			throw new ArgumentException("CaKhamID không hợp lệ");
		if (soBuoi <= 0)
			throw new ArgumentException("Số buổi không hợp lệ");
		LieuTrinhID = lieuTrinhID;
		CaKhamID = caKhamID;
		SoBuoi = soBuoi;
		NgayDuKien = ngayDuKien;
		TrangThai = TrangThaiBuoiDieuTriEnum.ChoXuLy;
	}
	public BuoiDieuTri(
		int buoiDieuTriID,
		int lieuTrinhID,
		int caKhamID,
		int soBuoi,
		DateTime? ngayDuKien,
		DateTime? ngayThucHien,
		int? nhanVienID,
		string trangThai,
		string? ghiChu,
		string? hinhAnhJSON)
	{
		BuoiDieuTriID = buoiDieuTriID;
		LieuTrinhID = lieuTrinhID;
		CaKhamID = caKhamID;
		SoBuoi = soBuoi;
		NgayDuKien = ngayDuKien;
		NgayThucHien = ngayThucHien;
		NhanVienID = nhanVienID;
		TrangThai = TrangThaiBuoiDieuTriExtensions.FromDb(trangThai);
		GhiChu = ghiChu;
		HinhAnhJSON = hinhAnhJSON;
	}
	public void Start(int nhanVienID)
	{
		if (TrangThai != TrangThaiBuoiDieuTriEnum.ChoXuLy)
			throw new InvalidOperationException("Buổi điều trị không thể bắt đầu");
		NhanVienID = nhanVienID;
		TrangThai = TrangThaiBuoiDieuTriEnum.DangThucHien;
	}
	public void Complete(DateTime ngayThucHien)
	{
		if (TrangThai != TrangThaiBuoiDieuTriEnum.DangThucHien)
			throw new InvalidOperationException("Buổi điều trị chưa được bắt đầu");
		if (NgayDuKien.HasValue && ngayThucHien < NgayDuKien)
			throw new InvalidOperationException(
				$"Không thể thực hiện trước ngày dự kiến ({NgayDuKien:dd/MM/yyyy})"
			);
		NgayThucHien = ngayThucHien;
		TrangThai = TrangThaiBuoiDieuTriEnum.HoanThanh;
	}
	public void Cancel()
	{
		if (TrangThai == TrangThaiBuoiDieuTriEnum.HoanThanh)
			throw new InvalidOperationException("Không thể huỷ buổi điều trị đã hoàn thành");
		TrangThai = TrangThaiBuoiDieuTriEnum.DaHuy;
	}
	public void CapNhatHinhAnh(string? hinhAnhJSON)
	{
		HinhAnhJSON = hinhAnhJSON;
	}
}