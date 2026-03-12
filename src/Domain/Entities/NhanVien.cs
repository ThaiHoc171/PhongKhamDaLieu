namespace Domain.Entities;

public class NhanVien
{
	public int NhanVienID { get; private set; }
	public int ThongTinID { get; private set; }
	public int ChucVuID { get; private set; }
	public int PhongChucNangID { get; private set; }

	public DateTime? NgayVaoLam { get; private set; }
	public string BangCap { get; private set; }
	public string KinhNghiem { get; private set; }

	public string TrangThai { get; private set; }

	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }

	// CREATE
	public NhanVien( int thongTinID, int chucVuID, int phongChucNangID,
		DateTime? ngayVaoLam, string bangCap, string kinhNghiem)
	{
		if (thongTinID <= 0)
			throw new ArgumentException("ThongTinID không hợp lệ");

		if (chucVuID <= 0)
			throw new ArgumentException("ChucVuID không hợp lệ");

		if (phongChucNangID <= 0)
			throw new ArgumentException("PhongChucNangID không hợp lệ");

		if (string.IsNullOrWhiteSpace(bangCap))
			throw new ArgumentException("Bằng cấp không hợp lệ");

		if (string.IsNullOrWhiteSpace(kinhNghiem))
			throw new ArgumentException("Kinh nghiệm không hợp lệ");

		ThongTinID = thongTinID;
		ChucVuID = chucVuID;
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
	}

	// MAP DB
	public NhanVien( int nhanVienID, int thongTinID, int chucVuID, int phongChucNangID,
		DateTime? ngayVaoLam, string bangCap, string kinhNghiem, string trangThai,
		DateTime ngayTao, DateTime? ngayCapNhat)
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

	public void Update( int chucVuID, int phongChucNangID,
		DateTime? ngayVaoLam, string bangCap, string kinhNghiem)
	{
		if (chucVuID <= 0)
			throw new ArgumentException("Chức vụ không hợp lệ");

		if (phongChucNangID <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");

		if (string.IsNullOrWhiteSpace(bangCap))
			throw new ArgumentException("Bằng cấp không hợp lệ");

		if (string.IsNullOrWhiteSpace(kinhNghiem))
			throw new ArgumentException("Kinh nghiệm không hợp lệ");

		ChucVuID = chucVuID;
		PhongChucNangID = phongChucNangID;
		NgayVaoLam = ngayVaoLam;
		BangCap = bangCap;
		KinhNghiem = kinhNghiem;
	}

	public void Status(string trangThaiMoi)
	{
		if (trangThaiMoi != "Đang làm việc" && trangThaiMoi != "Nghỉ việc")
			throw new ArgumentException("Trạng thái không hợp lệ");

		TrangThai = trangThaiMoi;
	}
}