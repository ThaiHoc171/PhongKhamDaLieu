namespace Application.DTOs;

public class ThuocRequestDTO
{
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}

public class ThuocUpdateDTO
{
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}

public class ThuocReadModel
{
    public int ThuocID { get; set; }
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}
<<<<<<< HEAD

public class ThuocListReadModel
{
    public int ThuocID { get; set; }
    public string TenThuoc { get; set; } = default!;
    public string? HoatChat { get; set; }
}
=======
>>>>>>> b6fe134a0932485c4184e88bdd2f4a2d06609a6a
