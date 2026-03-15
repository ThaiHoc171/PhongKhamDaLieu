using Domain.Enums;
namespace Domain.Entities;
public class PhienKhamCLS
{
	public int PhienKhamCLSID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int CLSID { get; private set; }
	public TrangThaiCLSEnum TrangThai { get; private set; }
	public string? KetQua { get; private set; }
	public string? FileDinhKem { get; private set; }
	public DateTime? NgayThucHien { get; private set; }
	public int NhanVienChiDinhID { get; private set; }
	public int? NhanVienThucHienID { get; private set; }
	public string? GhiChu { get; private set; }
	// MAP DB
	public PhienKhamCLS(int phienKhamCLSID, int phienKhamID, int clsID, TrangThaiCLSEnum trangThai, string? ketQua,
		string? fileDinhKem, DateTime? ngayThucHien, int nhanVienChiDinhID, int? nhanVienThucHienID, string? ghiChu)
	{
		PhienKhamCLSID = phienKhamCLSID;
		PhienKhamID = phienKhamID;
		CLSID = clsID;
		TrangThai = trangThai;
		KetQua = ketQua;
		FileDinhKem = fileDinhKem;
		NgayThucHien = ngayThucHien;
		NhanVienChiDinhID = nhanVienChiDinhID;
		NhanVienThucHienID = nhanVienThucHienID;
		GhiChu = ghiChu;
	}
	// CREATE
	public PhienKhamCLS(int phienKhamID, int clsID, int nhanVienChiDinhID, string? ghiChu)
	{
		if (phienKhamID <= 0)
			throw new ArgumentException("PhienKhamID không hợp lệ");
		if (clsID <= 0)
			throw new ArgumentException("CLSID không hợp lệ");
		PhienKhamID = phienKhamID;
		CLSID = clsID;
		NhanVienChiDinhID = nhanVienChiDinhID;
		GhiChu = ghiChu;
		TrangThai = TrangThaiCLSEnum.DangCho;
	}
	// nhận CLS
	public void Accept(int nhanVienThucHienID)
	{
		if (TrangThai != TrangThaiCLSEnum.DangCho)
			throw new InvalidOperationException("CLS không ở trạng thái chờ");
		TrangThai = TrangThaiCLSEnum.DangThucHien;
		NhanVienThucHienID = nhanVienThucHienID;
		NgayThucHien = DateTime.Now;
	}
	// cập nhật kết quả
	public void Complete(string? ketQua, string? fileDinhKem, string? ghiChu)
	{
		if (TrangThai != TrangThaiCLSEnum.DangThucHien)
			throw new InvalidOperationException("CLS chưa được thực hiện");
		TrangThai = TrangThaiCLSEnum.HoanThanh;
		KetQua = ketQua;
		FileDinhKem = fileDinhKem;
		GhiChu = ghiChu;
	}
	// hủy
	public void Cancel()
	{
		if (TrangThai == TrangThaiCLSEnum.HoanThanh)
			throw new InvalidOperationException("CLS đã hoàn thành");
		TrangThai = TrangThaiCLSEnum.DaHuy;
	}
}