
namespace Domain.Entities;

public class PhienKhamCLS
{
	public int PhienKhamCLSID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int CLSID { get; private set; }
	public string TrangThai { get; private set; } = default!;
	public string? KetQua { get; private set; }
	public string? FileDinhKem { get; private set; }
	public DateTime NgayThucHien { get; private set; }
	public int NhanVienChiDinhID { get; private set; }
	public int NhanVienThucHienID { get; private set; }
	public string? GhiChu { get; private set; }

	//Constructor map từ db
	public PhienKhamCLS(int phienKhamCLSID, int phienKhamID, int cLSID, string trangThai, string? ketQua, string? fileDinhKem, 
						DateTime ngayThucHien, int nhanVienChiDinhID, int nhanVienThucHienID,  string? ghiChu)
	{
		PhienKhamCLSID = phienKhamCLSID;
		PhienKhamID = phienKhamID;
		CLSID = cLSID;
		TrangThai = trangThai;
		KetQua = ketQua;
		FileDinhKem = fileDinhKem;
		NgayThucHien = ngayThucHien;
		NhanVienChiDinhID = nhanVienChiDinhID;
		NhanVienThucHienID = nhanVienThucHienID;
		GhiChu = ghiChu;
	}
	//Constructor tạo mới
	public PhienKhamCLS( int phienKhamID, int cLSID, int nhanVienChiDinhID, string? ghiChu)
	{
		PhienKhamID = phienKhamID;
		CLSID = cLSID;
		NhanVienChiDinhID = nhanVienChiDinhID;
		GhiChu = ghiChu;
	}
	public void NhanPhienKhamCLS(int nhanVienThucHienID)
	{
		if (TrangThai != "Đang chờ")
			throw new InvalidOperationException("Chỉ được nhận CLS khi đang chờ xử lý");

		TrangThai = "Đang thực hiện";
		NhanVienThucHienID = nhanVienThucHienID;
	}
	public void CapNhatKetQua(string? ketQua, string? fileDinhKem,string? ghiChu)
	{
		if (TrangThai != "Đang thực hiện")
			throw new InvalidOperationException("CLS chưa được xử lý");
		TrangThai = "Hoàn thành";
		KetQua = ketQua;
		FileDinhKem = fileDinhKem;
		GhiChu = ghiChu;
	}
	public void HuyPhienKhamCLS()
	{
		TrangThai = "Đã hủy";
	}
}
