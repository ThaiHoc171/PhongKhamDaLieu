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
	public int PhongChucNangID { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }

	// Navigation / dữ liệu liên kết
	public ThongTinCaNhan? ThongTinCaNhan { get; private set; }
	public string? TenChucVu { get; private set; }

	public NhanVien(int thongTinID, int chucVuID, int phongChucNangID, DateTime? ngayVaoLam, string bangCap, string kinhNghiem)
	{
		if (thongTinID <= 0)
			throw new ArgumentException("ThongTinID không hợp lệ");

		if (chucVuID <= 0)
			throw new ArgumentException("ChucVuID không hợp lệ");

		if (string.IsNullOrWhiteSpace(bangCap))
			throw new ArgumentException("Bằng cấp không hợp lệ");

		if (string.IsNullOrWhiteSpace(kinhNghiem))
			throw new ArgumentException("Kinh nghiệm không hợp lệ");
		if (phongChucNangID <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");
		ThongTinID = thongTinID;
		ChucVuID = chucVuID;
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
	}


	 // CONSTRUCTOR – MAP TỪ DB
	public NhanVien(
		int nhanVienID,
		int thongTinID,
		int chucVuID,
		int phongChucNangID,
		DateTime? ngayVaoLam,
		string bangCap,
		string kinhNghiem,
		string trangThai,
		DateTime ngayTao,
		DateTime? ngayCapNhat,
		string? tenChucVu,
		ThongTinCaNhan? thongTinCaNhan)
	{
		NhanVienID = nhanVienID;
		ThongTinID = thongTinID;
		ChucVuID = chucVuID;
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
		TrangThai = trangThai;
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
		TenChucVu = tenChucVu;
		ThongTinCaNhan = thongTinCaNhan;
	}
	// CONSTRUCTOR CHO LIST / SEARCH
	public NhanVien(
		int nhanVienID,
		int thongTinID,
		int chucVuID,
		int phongChucNangID,
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
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
		TrangThai = trangThai;
		TenChucVu = tenChucVu;
		ThongTinCaNhan = thongTinCaNhan;
	}
	// Dbcontext cho auth
	public NhanVien(int nhanVienID, int chucVuID, int thongTinID)
	{
		NhanVienID = nhanVienID;
		ChucVuID = chucVuID;
        ThongTinID = thongTinID;
    }
	public void CapNhatThongTin(
		int chucVuID,
		int phongChucNangID,
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
		if (phongChucNangID <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");
		ChucVuID = chucVuID;
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
	}

	public void CapNhatTrangThai(string trangThaiMoi)
	{
		if (trangThaiMoi != "Đang làm việc" &&	trangThaiMoi != "Nghỉ việc")
			throw new ArgumentException("Trạng thái không hợp lệ");

		TrangThai = trangThaiMoi;
	}
}

