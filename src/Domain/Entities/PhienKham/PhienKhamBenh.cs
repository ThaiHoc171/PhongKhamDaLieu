using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class PhienKhamBenh
{
	public int PhienKham_BenhID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int LoaiBenhID { get; private set; }
	public string? LoaiChuanDoan { get; private set; }
	public string? GhiChu { get; private set; }

	// Constructor map từ db
	public PhienKhamBenh(int phienKham_BenhID, int phienKhamID, int loaiBenhID, string? loaiChuanDoan, string? ghiChu)
	{
		PhienKham_BenhID = phienKham_BenhID;
		PhienKhamID = phienKhamID;
		LoaiBenhID = loaiBenhID;
		LoaiChuanDoan = loaiChuanDoan;
		GhiChu = ghiChu;
	}
	// Constructor tạo mới
	public PhienKhamBenh(int phienKhamID, int loaiBenhID, string? loaiChuanDoan, string? ghiChu)
	{
		if (string.IsNullOrWhiteSpace(loaiChuanDoan))
		{
			throw new ArgumentException("Loại chẩn đoán không hợp lệ");
		}
		if (loaiBenhID <= 0)
		{
			throw new ArgumentException("Loại bệnh ID không hợp lệ");
		}
		PhienKhamID = phienKhamID;
		LoaiBenhID = loaiBenhID;
		LoaiChuanDoan = loaiChuanDoan;
		GhiChu = ghiChu;
	}
	// Cập nhật loại chẩn đoán và ghi chú (chỉ có thể cập nhật khi phiên kham chưa kết thúc)
	public void CapNhatLoaiChuanDoan(string? loaiChuanDoan, string ghiChu)
	{
		if (string.IsNullOrWhiteSpace(loaiChuanDoan))
		{
			throw new ArgumentException("Loại chẩn đoán không hợp lệ");
		}
		LoaiChuanDoan = loaiChuanDoan;
		GhiChu = ghiChu;
	}
}