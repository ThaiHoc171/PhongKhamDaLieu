using Domain.Enums;
namespace Domain.Entities;

public class PhienKhamBenh
{
	public int PhienKham_BenhID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int LoaiBenhID { get; private set; }
	public LoaiChanDoanEnum LoaiChanDoan { get; private set; }
	public string? GhiChu { get; private set; }

	// Constructor map từ db
	public PhienKhamBenh(int phienKham_BenhID, int phienKhamID, int loaiBenhID, LoaiChanDoanEnum loaiChanDoan, string? ghiChu)
	{
		PhienKham_BenhID = phienKham_BenhID;
		PhienKhamID = phienKhamID;
		LoaiBenhID = loaiBenhID;
		LoaiChanDoan = loaiChanDoan;
		GhiChu = ghiChu;
	}
	// Constructor tạo mới
	public PhienKhamBenh(int phienKhamID, int loaiBenhID, LoaiChanDoanEnum loaiChanDoan, string? ghiChu)
	{
		if (loaiBenhID <= 0)
		{
			throw new ArgumentException("Loại bệnh ID không hợp lệ");
		}
		PhienKhamID = phienKhamID;
		LoaiBenhID = loaiBenhID;
		LoaiChanDoan = loaiChanDoan;
		GhiChu = ghiChu;
	}
	// Cập nhật loại chẩn đoán và ghi chú (chỉ có thể cập nhật khi phiên kham chưa kết thúc)
	public void CapNhat(LoaiChanDoanEnum loaiChanDoan, string? ghiChu)
	{
		LoaiChanDoan = loaiChanDoan;
		GhiChu = ghiChu;
	}
}