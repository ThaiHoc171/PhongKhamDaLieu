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

	// Constructor MAP từ DB
	public PhienKhamCLS(int phienKhamCLSID,int phienKhamID,int clsID,string trangThai,
		string? ketQua,string? fileDinhKem,DateTime? ngayThucHien,int nhanVienChiDinhID,
		int? nhanVienThucHienID,string? ghiChu)
	{
		PhienKhamCLSID = phienKhamCLSID;
		PhienKhamID = phienKhamID;
		CLSID = clsID;
		TrangThai = TrangThaiCLSExtensions.ToEnum(trangThai);
		KetQua = ketQua;
		FileDinhKem = fileDinhKem;
		NgayThucHien = ngayThucHien;
		NhanVienChiDinhID = nhanVienChiDinhID;
		NhanVienThucHienID = nhanVienThucHienID;
		GhiChu = ghiChu;
	}

	// Constructor TẠO MỚI
	public PhienKhamCLS(int phienKhamID,int clsID,int nhanVienChiDinhID,string? ghiChu)
	{
		PhienKhamID = phienKhamID;
		CLSID = clsID;
		NhanVienChiDinhID = nhanVienChiDinhID;
		GhiChu = ghiChu;

		TrangThai = TrangThaiCLSEnum.DangCho;
	}

	// Nghiệp vụ
	public void NhanPhienKhamCLS(int nhanVienThucHienID)
	{
		if (TrangThai != TrangThaiCLSEnum.DangCho)
			throw new InvalidOperationException("Chỉ được nhận CLS khi đang chờ xử lý");

		TrangThai = TrangThaiCLSEnum.DangThucHien;
		NhanVienThucHienID = nhanVienThucHienID;
		NgayThucHien = DateTime.Now;
	}

	public void CapNhatKetQua(string? ketQua, string? fileDinhKem, string? ghiChu)
	{
		if (TrangThai != TrangThaiCLSEnum.DangThucHien)
			throw new InvalidOperationException("CLS chưa được xử lý");

		TrangThai = TrangThaiCLSEnum.HoanThanh;
		KetQua = ketQua;
		FileDinhKem = fileDinhKem;
		GhiChu = ghiChu;
	}

	public void HuyPhienKhamCLS()
	{
		if (TrangThai == TrangThaiCLSEnum.HoanThanh)
			throw new InvalidOperationException("CLS đã hoàn thành, không thể hủy");

		TrangThai = TrangThaiCLSEnum.DaHuy;
	}
}
