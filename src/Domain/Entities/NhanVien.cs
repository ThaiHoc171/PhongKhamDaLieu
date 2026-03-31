using Domain.Enums;

namespace Domain.Entities;

public class NhanVien
{
	public int NhanVienID { get; private set; }
	public int ThongTinID { get; private set; }
	public int ChucVuID { get; private set; }
	public int PhongChucNangID { get; private set; }
	public DateTime NgayVaoLam { get; private set; }
	public string BangCap { get; private set; }
	public string KinhNghiem { get; private set; }
	public string TrangThai { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }

	// CREATE
	public NhanVien(int thongTinID, int chucVuID, int phongChucNangID, DateTime ngayVaoLam,
		string bangCap, string kinhNghiem, string trangThai)
	{
		Validate(thongTinID, chucVuID, phongChucNangID, bangCap, kinhNghiem, trangThai);

		ThongTinID = thongTinID;
		ChucVuID = chucVuID;
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
		TrangThai = trangThai;
	}

	// MAP DB
	public NhanVien(int nhanVienID, int thongTinID, int chucVuID, int phongChucNangID, DateTime ngayVaoLam,
		string bangCap, string kinhNghiem, string trangThai, DateTime ngayTao, DateTime? ngayCapNhat)
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
	}

	public void CapNhat(int chucVuID, int phongChucNangID, DateTime ngayVaoLam, string bangCap, string kinhNghiem, string trangThai)
	{
		Validate(ThongTinID, chucVuID, phongChucNangID, bangCap, kinhNghiem, trangThai);

		ChucVuID = chucVuID;
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
		TrangThai = trangThai;

		NgayCapNhat = DateTime.UtcNow;
	}

	private void Validate( int thongTinID, int chucVuID, int phongChucNangID, string bangCap, string kinhNghiem, string trangThai)
	{
		if (thongTinID <= 0)
			throw new ArgumentException("ThongTinID không hợp lệ");

		if (chucVuID <= 0)
			throw new ArgumentException("Chức vụ không hợp lệ");

		if (phongChucNangID <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");

		if (string.IsNullOrWhiteSpace(bangCap))
			throw new ArgumentException("Bằng cấp không hợp lệ");

		if (string.IsNullOrWhiteSpace(kinhNghiem))
			throw new ArgumentException("Kinh nghiệm không hợp lệ");

		if (string.IsNullOrWhiteSpace(trangThai))
			throw new ArgumentException("Trạng thái không hợp lệ");

		TrangThaiSystemExtensions.FromDb(trangThai);
	}
}