namespace Application.DTOs;

// Tạo mới CLS cho phiên khám
public class TaoPhienKhamCLSDTO
{
	public int PhienKhamID { get; set; }
	public int CLSID { get; set; }
	public int NhanVienChiDinhID { get; set; }
	public string? GhiChu { get; set; }
}

// Nhận thực hiện CLS
public class NhanThucHienCLSDTO
{
	public int NhanVienThucHienID { get; set; }
}

// Cập nhật kết quả CLS
public class CapNhatKetQuaCLSDTO
{
	public string? KetQua { get; set; }
	public string? FileDinhKem { get; set; }
	public string? GhiChu { get; set; }
}
