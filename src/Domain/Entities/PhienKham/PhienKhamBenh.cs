using Domain.Enums;

namespace Domain.Entities;

public class PhienKhamBenh
{
	public int PhienKham_BenhID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int LoaiBenhID { get; private set; }
	public LoaiChanDoanEnum LoaiChanDoan { get; private set; }
	public string? GhiChu { get; private set; }

	// Tạo mới
	public PhienKhamBenh(int phienKhamID, int loaiBenhID, string loaiChanDoan, string? ghiChu)
	{
		Validate(phienKhamID, loaiBenhID, loaiChanDoan);

		PhienKhamID = phienKhamID;
		LoaiBenhID = loaiBenhID;
		LoaiChanDoan = LoaiChanDoanExtensions.FromDb(loaiChanDoan);
		GhiChu = ghiChu;
	}

	// Map DB
	public PhienKhamBenh(int phienKham_BenhID, int phienKhamID, int loaiBenhID, string loaiChanDoan, string? ghiChu)
	{
		PhienKham_BenhID = phienKham_BenhID;
		PhienKhamID = phienKhamID;
		LoaiBenhID = loaiBenhID;
		LoaiChanDoan = LoaiChanDoanExtensions.FromDb(loaiChanDoan);
		GhiChu = ghiChu;
	}

	public void CapNhat(int loaiBenhID, string loaiChanDoan, string? ghiChu)
	{
		Validate(PhienKhamID, loaiBenhID, loaiChanDoan);

		LoaiBenhID = loaiBenhID;
		LoaiChanDoan = LoaiChanDoanExtensions.FromDb(loaiChanDoan);
		GhiChu = ghiChu;
	}

	private void Validate(int phienKhamID, int loaiBenhID, string loaiChanDoan)
	{
		if (phienKhamID <= 0)
			throw new ArgumentException("Phiên khám không hợp lệ");

		if (loaiBenhID <= 0)
			throw new ArgumentException("Loại bệnh không hợp lệ");

		if (string.IsNullOrWhiteSpace(loaiChanDoan))
			throw new ArgumentException("Loại chẩn đoán không hợp lệ");

		LoaiChanDoanExtensions.FromDb(loaiChanDoan);
	}
}