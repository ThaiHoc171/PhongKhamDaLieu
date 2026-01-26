

namespace Domain.Entities;

public class PCN_TB
{
	public int Id { get; private set; }
	public int PCN_Id { get; private set; }
	public int TB_Id { get; private set; }
	public int SoLuong { get; private set; }
	public string? GhiChu { get; private set; }

	// Tạo mới
	public PCN_TB(int pCN_Id, int tB_Id, int soLuong, string? ghiChu)
	{
		if (soLuong < 0)
			throw new ArgumentException("Số lượng không hợp lệ");
		PCN_Id = pCN_Id;
		TB_Id = tB_Id;
		SoLuong = soLuong;
		GhiChu = ghiChu;
	}
	// Map từ DB
	public PCN_TB(int id, int pCN_TB_Id, int tB_Id, int soLuong, string? ghiChu)
	{
		Id = id;
		PCN_Id = pCN_TB_Id;
		TB_Id = tB_Id;
		SoLuong = soLuong;
		GhiChu = ghiChu;
	}
	public void CapNhat(int tB_Id,  int soLuong, string? ghiChu)
	{
		if (soLuong < 0)
			throw new ArgumentException("Số lượng không hợp lệ");
		TB_Id = tB_Id;
		SoLuong = soLuong;
		GhiChu = ghiChu;
	}
}
