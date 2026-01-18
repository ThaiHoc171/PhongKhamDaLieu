namespace Domain.Entities;

public class BacSiProfile
{
	public int BacSiProfileID { get; private set; }
	public int NhanVienID { get; private set; }
	public string? GioiThieu { get; private set; }
	public string? ChuyenMon { get; private set; }
	public string? ThanhTuu { get; private set; }
	public string? HinhAnh { get; private set; }
	public string? KinhNghiem { get; private set; }
	public DateTime NgayCapNhat { get; private set; }

	// tạo mới
	public BacSiProfile(
		int nhanVienID,
		string? gioiThieu,
		string? chuyenMon,
		string? thanhTuu,
		string? hinhAnh,
		string? kinhNghiem)
	{
		NhanVienID = nhanVienID;
		GioiThieu = gioiThieu;
		ChuyenMon = chuyenMon;
		ThanhTuu = thanhTuu;
		HinhAnh = hinhAnh;
		KinhNghiem = kinhNghiem;
		NgayCapNhat = DateTime.Now;
	}

	// map DB
	public BacSiProfile(
		int bacSiProfileID,
		int nhanVienID,
		string? gioiThieu,
		string? chuyenMon,
		string? thanhTuu,
		string? hinhAnh,
		string? kinhNghiem,
		DateTime ngayCapNhat)
	{
		BacSiProfileID = bacSiProfileID;
		NhanVienID = nhanVienID;
		GioiThieu = gioiThieu;
		ChuyenMon = chuyenMon;
		ThanhTuu = thanhTuu;
		HinhAnh = hinhAnh;
		KinhNghiem = kinhNghiem;
		NgayCapNhat = ngayCapNhat;
	}

	public void CapNhat(
		string? gioiThieu,
		string? chuyenMon,
		string? thanhTuu,
		string? hinhAnh,
		string? kinhNghiem)
	{
		GioiThieu = gioiThieu;
		ChuyenMon = chuyenMon;
		ThanhTuu = thanhTuu;
		HinhAnh = hinhAnh;
		KinhNghiem = kinhNghiem;
		NgayCapNhat = DateTime.Now;
	}
}
