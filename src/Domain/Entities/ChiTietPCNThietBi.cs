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

	// Contructor tạo mới

	public ChiTietPCNThietBi(
		int pcnTbId,
		string maTaiSan,
		string? ghiChu)
	{
		if (pcnTbId <= 0)
			throw new ArgumentException("PCN thiết bị không hợp lệ");

		if (string.IsNullOrWhiteSpace(maTaiSan))
			throw new ArgumentException("Mã tài sản không hợp lệ");

		PCN_TB_ID = pcnTbId;
		MaTaiSan = maTaiSan.Trim();
		GhiChu = ghiChu;

		// domain default
		TinhTrang = TinhTrang.HoatDong;
	}


	// Contructor Map từ DB
	public ChiTietPCNThietBi(
		int chiTietId,
		int pcnTbId,
		string maTaiSan,
		DateTime ngayNhap,
		string tinhTrangDb,
		string? ghiChu)
	{
		if (chiTietId <= 0 || pcnTbId <= 0)
			throw new ArgumentException("Dữ liệu DB không hợp lệ");

		if (string.IsNullOrWhiteSpace(maTaiSan))
			throw new ArgumentException("Mã tài sản DB không hợp lệ");

		ChiTietID = chiTietId;
		PCN_TB_ID = pcnTbId;
		MaTaiSan = maTaiSan;
		NgayNhap = ngayNhap;
		TinhTrang = TinhTrangExtensions.FromDb(tinhTrangDb);
		GhiChu = ghiChu;
	}

	// Nghiệp vụ
	public void ChuyenTinhTrang(TinhTrang tinhTrangMoi)
	{
		// rule ví dụ
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
}
