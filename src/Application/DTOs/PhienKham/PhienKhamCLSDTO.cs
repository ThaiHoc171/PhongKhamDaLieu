namespace Application.DTOs;

public class PkClsRequestDTO
{
	public int PhienKhamID { get; set; }
	public int CLSID { get; set; }
	public int NhanVienChiDinhID { get; set; }
	public string? GhiChu { get; set; }
}

public class AcceptClsDTO
{
	public int NhanVienThucHienID { get; set; }
}

public class PkClsUpdateRequestDTO
{
	public string? KetQua { get; set; }
	public string? FileDinhKem { get; set; }
	public string? GhiChu { get; set; }
}

public class PhienKhamClsListReadModel
{
	public int PhienKhamCLSID { get; init; }
	public string TenCLS { get; init; } = default!;
	public string TrangThai { get; init; } = default!;
	public string? KetQua { get; init; }
	public DateTime? NgayThucHien { get; init; }
	public string? GhiChu { get; init; }
}

public class PhienKhamClsReadModel
{
	public int PhienKhamCLSID { get; init; }
	public string TenCLS { get; init; } = default!;
	public string TrangThai { get; init; } = default!;
	public string? KetQua { get; init; }
	public string? FileDinhKem { get; init; }
	public DateTime? NgayThucHien { get; init; }

	public string NhanVienChiDinh { get; init; }
	public NameResponseDTO? NhanVienThucHien { get; init; }

	public string? GhiChu { get; init; }
}