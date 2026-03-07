namespace Application.DTOs;

// Tạo mới CLS cho phiên khám
public class TaoPhienKhamCLSDTO
{
	public int PhienKhamID { get; set; }
	public int CLSID { get; set; }
	public int NhanVienChiDinhID { get; set; }
	public string? GhiChu { get; set; }
}
public class PhienKhamClsListReadModel
{
	public int PhienKhamCLSID { get; set; }
	public string? TenCLS { get; set; }
	public string TrangThai { get; set; } = default!;
	public string? KetQua { get; set; }
	public DateTime? NgayThucHien { get; set; }
	public string? GhiChu { get; set; }
}
public class PhienKhamClsReadModel
{
	public int PhienKhamCLSID { get; set; }
	public string? TenCLS { get; set; }
	public string TrangThai { get; set; } = default!;
	public string? KetQua { get; set; }
	public string? FileDinhKem { get; set; }
	public DateTime? NgayThucHien { get; set; }
	public NameResponseDTO NhanVienChiDinh { get; set; } = default!;
	public NameResponseDTO? NhanVienThucHien { get; set; }
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
