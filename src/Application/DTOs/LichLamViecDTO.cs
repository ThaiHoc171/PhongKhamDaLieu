using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs;

public class LichLamViecDTO
{
	public int NhanVienID { get; set; }
	public int ChucVuID { get; set; }
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecRespondDTO
{
	public int LichLamViecID { get; set; }
	public int NhanVienID { get; set; }
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }
}
public class LichLamViecBatchDTO
{
	public int Thang { get; set; }
	public int Nam { get; set; }
	public List<LichLamViecDTO> LichLamViecs { get; set; } = new();
}

public class WeekLichLamViecDTO
{
	public int Page { get; set; }
	public DateTime TuanBatDau { get; set; }
	public DateTime TuanKetThuc { get; set; }
	public List<LichLamViecRespondDTO> LichLamViecs { get; set; } = new();
}
