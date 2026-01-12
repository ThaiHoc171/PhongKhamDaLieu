using System;

namespace Domain.Entities;

public class BenhNhan
{
	public int BenhNhanID { get; private set; }
	public int ThongTinID { get; private set; }
	public string LoaiDa { get; private set; }
	public string TrangThaiTheoDoi { get; private set; }
	public string GhiChu { get; private set; }

	// Constructor dùng khi tạo mới từ DTO
	public BenhNhan(int thongTinID, string loaiDa, string trangThaiTheoDoi = "Chưa điều trị", string ghiChu = "")
	{
		if (thongTinID <= 0) throw new ArgumentException("ThongTinID không hợp lệ");

		ThongTinID = thongTinID;
		LoaiDa = loaiDa;
		TrangThaiTheoDoi = trangThaiTheoDoi;
		GhiChu = ghiChu;
	}

	// Constructor dùng khi map từ DB
	public BenhNhan(int benhNhanID, int thongTinID, string loaiDa, string trangThaiTheoDoi, string ghiChu)
	{
		BenhNhanID = benhNhanID;
		ThongTinID = thongTinID;
		LoaiDa = loaiDa;
		TrangThaiTheoDoi = trangThaiTheoDoi;
		GhiChu = ghiChu;
	}

	public void CapNhat(string loaiDa, string ghiChu)
	{
		LoaiDa = loaiDa;
		GhiChu = ghiChu;
	}
	public void CapNhatTrangThai(string trangThaiMoi)
	{
		if (string.IsNullOrWhiteSpace(trangThaiMoi))
			throw new ArgumentException("Trạng thái không hợp lệ");

		TrangThaiTheoDoi = trangThaiMoi;
	}
}
