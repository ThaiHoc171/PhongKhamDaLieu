using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class TaoTaiKhamDTO
{
    public int PhienKhamID { get; set; }
    public int BenhNhanID { get; set; }
    public DateTime NgayDuKien { get; set; }
    public string? LyDo { get; set; }
}

public class CapNhatTaiKhamDTO
{
    public DateTime NgayDuKien { get; set; }
    public string? LyDo { get; set; }
    public string? TrangThai { get; set; }
    public int? CaKhamID { get; set; }
}

public class TaiKhamResponeDTO
{
    public int TaiKhamID { get; set; }
    public int PhienKhamID { get; set; }
    public int BenhNhanID { get; set; }
    public DateTime NgayDuKien { get; set; }
    public string? LyDo { get; set; }
    public string? TrangThai { get; set; }
    public int? CaKhamID { get; set; }
    public DateTime NgayTao { get; set; }
}

