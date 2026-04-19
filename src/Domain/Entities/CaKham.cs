using Domain.Enums;

namespace Domain.Entities;

public class CaKham
{
	public int CaKhamID { get; private set; }
	public string LoaiCaKham { get; private set; }
	public int? NhanVienID { get; private set; }
	public int? LichLamViecID { get; private set; }
	public int KhungGioID { get; private set; }
	public int? PhongChucNangID { get; private set; }
	public int? ThongTinID { get; private set; }
	public string? LyDoKham { get; private set; }
	public string TrangThai { get; private set; }
	public DateTime? NgayDat { get; private set; }
	public DateTime NgayKham { get; private set; }
	public string? GhiChu { get; private set; }

	public CaKham(string loaiCaKham,int? nhanVienID, int? lichLamViecID,int? phongChucNangID, int khungGioID, DateTime ngayKham)
	{
		Validate(loaiCaKham, khungGioID, ngayKham);

		LoaiCaKham = loaiCaKham;
		KhungGioID = khungGioID;
		NgayKham = ngayKham.Date;
		NhanVienID = nhanVienID;
		LichLamViecID = lichLamViecID;
		PhongChucNangID = phongChucNangID;
		TrangThai = TrangThaiCaKham.Trong.ToDbValue();
	}

	public CaKham(int caKhamID, string loaiCaKham,int? nhanVienID, int? lichLamViecID, int khungGioID, int? phongChucNangID, int? thongTinID,
		string? lyDoKham, string trangThai, DateTime? ngayDat, DateTime ngayKham, string? ghiChu)
	{
		CaKhamID = caKhamID;
		LoaiCaKham = loaiCaKham;
		NhanVienID = nhanVienID;
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
		if (lichLamViecID <= 0)
			throw new ArgumentException("Lịch làm việc không hợp lệ");

		LichLamViecID = lichLamViecID;
	}

	public void GanPhong(int phongChucNangID)
	{
		if (phongChucNangID <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");

		PhongChucNangID = phongChucNangID;
	}

	public void DangKyKham(int thongTinID, string lyDoKham, DateTime ngayDat, string? ghiChu)
	{
		if (TrangThai != TrangThaiCaKham.Trong.ToDbValue())
			throw new InvalidOperationException("Ca khám đã được đặt");

		if (thongTinID <= 0)
			throw new ArgumentException("Thông tin bệnh nhân không hợp lệ");

		if (string.IsNullOrWhiteSpace(lyDoKham))
			throw new ArgumentException("Lý do khám không hợp lệ");

		ThongTinID = thongTinID;
		LyDoKham = lyDoKham;
		NgayDat = ngayDat;
		GhiChu = ghiChu;

		TrangThai = TrangThaiCaKham.DaDat.ToDbValue();
	}

	public void HuyDangKy()
	{
		if (TrangThai == TrangThaiCaKham.Trong.ToDbValue() || TrangThai == TrangThaiCaKham.HoanThanh.ToDbValue() || TrangThai == TrangThaiCaKham.DaHuy.ToDbValue())
			throw new InvalidOperationException("Ca khám chưa được đặt");
		var cutOffTime = NgayKham.AddDays(-1);
		if (DateTime.Now <= cutOffTime)
		{
			ThongTinID = null;
			LyDoKham = null;
			NgayDat = null;
			GhiChu = null;
			TrangThai = TrangThaiCaKham.Trong.ToDbValue(); 
		}
		else
			TrangThai = TrangThaiCaKham.DaHuy.ToDbValue();
	}

	public void CapNhatTrangThai(string trangThaiMoi)
	{
		if (string.IsNullOrWhiteSpace(trangThaiMoi))
			throw new ArgumentException("Trạng thái không hợp lệ");

		TrangThaiCaKhamExtensions.FromDb(trangThaiMoi);

		TrangThai = trangThaiMoi;
	}

	private void Validate(string loaiCaKham, int khungGioID, DateTime ngayKham)
	{
		if (string.IsNullOrWhiteSpace(loaiCaKham))
			throw new ArgumentException("Loại ca khám không hợp lệ");

		if (loaiCaKham != "Khám" && loaiCaKham != "Điều trị")
			throw new ArgumentException("Loại ca khám không hợp lệ");

		if (khungGioID <= 0)
			throw new ArgumentException("Khung giờ không hợp lệ");

		if (ngayKham.Date < DateTime.Today)
			throw new ArgumentException("Ngày khám không hợp lệ");
	}
}