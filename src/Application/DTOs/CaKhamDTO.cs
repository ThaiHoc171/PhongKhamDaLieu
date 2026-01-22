using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class TaoCaKhamDTO
{
    public int PhongChucNangID { get; set; }
    public DateTime NgayKham { get; set; }
}
public class DangKyCaKhamDTO
{
    public int BenhNhanID { get; set; }
    public string LyDoKham { get; set; } = string.Empty;
    public DateTime NgayDat { get; set; }
    public string? GhiChu { get; set; }
}

public class CapNhatTrangThaiCaKhamDTO
{
    public string TrangThai { get; set; } = string.Empty;
}

public class CaKhamResponseDTO
{
    public int CaKhamID { get; set; }
    public int LichLamViecID { get; set; }
    public int PhongChucNangID { get; set; }
    public DateTime NgayKham { get; set; }
    public int KhungGioID { get; set; }

    public int? BenhNhanID { get; set; }
    public string? LyDoKham { get; set; }

    public string TrangThai { get; set; } = string.Empty;
    public DateTime? NgayDat { get; set; }
    public string? GhiChu { get; set; }
}


