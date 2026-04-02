using Domain.Enums;
 namespace Domain.Entities;
 public class ChiTietPCNThietBi
{
	public int ChiTietID { get; private set; }
	public int PCN_TB_ID { get; private set; }
	public string MaTaiSan { get; private set; }
	public DateTime NgayNhap { get; private set; }
	public TinhTrang TinhTrang { get; private set; }
	public string? GhiChu { get; private set; }
 	// Constructor tạo mới
	public ChiTietPCNThietBi(int pcnTbId, string maTaiSan, string? ghiChu)
	{
		Validate(pcnTbId, maTaiSan);
 		PCN_TB_ID = pcnTbId;
		MaTaiSan = maTaiSan.Trim();
		GhiChu = ghiChu;
		TinhTrang = TinhTrang.HoatDong;
	}
 	// Constructor map DB
	public ChiTietPCNThietBi( int chiTietId, int pcnTbId, string maTaiSan, DateTime ngayNhap, string tinhTrangDb, string? ghiChu)
	{
		ChiTietID = chiTietId;
		PCN_TB_ID = pcnTbId;
		MaTaiSan = maTaiSan;
		NgayNhap = ngayNhap;
		TinhTrang = TinhTrangExtensions.FromDb(tinhTrangDb);
		GhiChu = ghiChu;
	}
 	// Business methods
 	public void ChuyenTinhTrang(TinhTrang tinhTrangMoi)
	{
		if (TinhTrang == tinhTrangMoi)
			return;
 		if (TinhTrang == TinhTrang.Hong && tinhTrangMoi == TinhTrang.HoatDong)
			throw new InvalidOperationException("Thiết bị hỏng cần bảo trì trước");
 		TinhTrang = tinhTrangMoi;
	}
 	public void CapNhatGhiChu(string? ghiChu)
	{
		GhiChu = ghiChu;
	}
 	public bool DangSuDung()
	{
		return TinhTrang == TinhTrang.HoatDong;
	}
 	private void Validate(int pcnTbId, string maTaiSan)
	{
		if (pcnTbId <= 0)
			throw new ArgumentException("PCN thiết bị không hợp lệ");
 		if (string.IsNullOrWhiteSpace(maTaiSan))
			throw new ArgumentException("Mã tài sản không hợp lệ");
	}
}