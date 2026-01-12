using System;

namespace Domain.Entities;

public class NhanVien
{
	public int NhanVienID { get; private set; }
	public int ThongTinID { get; private set; }
	public int ChucVuID { get; private set; }
	public DateTime? NgayVaoLam { get; private set; }
	public string BangCap { get; private set; }
	public string KinhNghiem { get; private set; }
	public string TrangThai { get; private set; }

	// Navigation / dữ liệu liên kết
	public ThongTinCaNhan? ThongTinCaNhan { get; private set; }
	public string? TenChucVu { get; private set; }

	public NhanVien(int thongTinID, int chucVuID, DateTime? ngayVaoLam, string bangCap, string kinhNghiem)
	{
		if (thongTinID <= 0)
			throw new ArgumentException("ThongTinID không hợp lệ");

		if (chucVuID <= 0)
			throw new ArgumentException("ChucVuID không hợp lệ");

		if (string.IsNullOrWhiteSpace(bangCap))
			throw new ArgumentException("Bằng cấp không hợp lệ");

		if (string.IsNullOrWhiteSpace(kinhNghiem))
			throw new ArgumentException("Kinh nghiệm không hợp lệ");

		ThongTinID = thongTinID;
		ChucVuID = chucVuID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
		TrangThai = "Đang làm việc";
	}


	 // CONSTRUCTOR – MAP TỪ DB
	public NhanVien(
		int nhanVienID,
		int thongTinID,
		int chucVuID,
		DateTime? ngayVaoLam,
		string bangCap,
		string kinhNghiem,
		string trangThai,
		string? tenChucVu,
		ThongTinCaNhan? thongTinCaNhan)
	{
		NhanVienID = nhanVienID;
		ThongTinID = thongTinID;
		ChucVuID = chucVuID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
		TrangThai = trangThai;
		TenChucVu = tenChucVu;
		ThongTinCaNhan = thongTinCaNhan;
	}

	public void CapNhatThongTin(
		int chucVuID,
		DateTime? ngayVaoLam,
		string bangCap,
		string kinhNghiem)
	{
		if (chucVuID <= 0)
			throw new ArgumentException("Chức vụ không hợp lệ");

		if (string.IsNullOrWhiteSpace(bangCap))
			throw new ArgumentException("Bằng cấp không hợp lệ");

		if (string.IsNullOrWhiteSpace(kinhNghiem))
			throw new ArgumentException("Kinh nghiệm không hợp lệ");

		ChucVuID = chucVuID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
	}

	public void CapNhatTrangThai(string trangThaiMoi)
	{
		if (string.IsNullOrWhiteSpace(trangThaiMoi))
			throw new ArgumentException("Trạng thái không hợp lệ");

		TrangThai = trangThaiMoi;
	}
}

