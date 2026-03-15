namespace Application.DTOs;

public class BacSiProfileRequestDTO
{
    public int NhanVienID { get; set; }
    public string? GioiThieu { get; set; }
    public string? ChuyenMon { get; set; }
    public string? ThanhTuu { get; set; }
    public string? HinhAnh { get; set; }
    public string? KinhNghiem { get; set; }
}
public class BacSiProfileUpdateDTO
{
    public int BacSiProfileID { get; set; }
    public string? GioiThieu { get; set; }
    public string? ChuyenMon { get; set; }
    public string? ThanhTuu { get; set; }
    public string? HinhAnh { get; set; }
    public string? KinhNghiem { get; set; }
}
public class BacSiProfileReadModel
{
    public int BacSiProfileID { get; set; }
    public int NhanVienID { get; set; }
    public string? GioiThieu { get; set; }
    public string? ChuyenMon { get; set; }
    public string? ThanhTuu { get; set; }
    public string? HinhAnh { get; set; }
    public string? KinhNghiem { get; set; }
    public DateTime NgayCapNhat { get; set; }
}
public class BacSiProfileListReadModel
{
    public int BacSiProfileID { get; set; }
    public int NhanVienID { get; set; }
    public string? ChuyenMon { get; set; }
    public string? HinhAnh { get; set; }
    public DateTime NgayCapNhat { get; set; }
}