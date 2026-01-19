using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class LichLamViec
{
	public int LichLamViecID { get; set; }
	public int NhanVienID { get; set; }
	public DateTime Ngay { get; set; }
	public int CaLamViec { get; set; }
	public string? GhiChu { get; set; }

	//Constructor Tạo
	public LichLamViec(int nhanVienID, DateTime ngay, int caLamViec, string? ghiChu, bool IsNgayNghi)
	{
		if (ngay.Date < DateTime.Now.Date)
		{
			throw new ArgumentException("Ngày làm việc không được là ngày trong quá khứ.");
		}
		if (IsNgayNghi)
		{
			throw new ArgumentException("Ngày làm việc không được trùng với ngày nghỉ đã được thiết lập.");
		}
		if (caLamViec < 1 || caLamViec > 2)
		{
			throw new ArgumentException("Ca làm việc không hợp lệ.");
		}
		NhanVienID = nhanVienID;
		Ngay = ngay;
		CaLamViec = caLamViec;
		GhiChu = ghiChu;
	}
	// Constructor Map dữ liệu từ DB
	public LichLamViec(int lichLamViecID, int nhanVienID,  DateTime ngay, int caLamViec, string? ghiChu)
	{
		LichLamViecID = lichLamViecID;
		NhanVienID = nhanVienID;
		Ngay = ngay;
		CaLamViec = caLamViec;
		GhiChu = ghiChu;
	}
}
