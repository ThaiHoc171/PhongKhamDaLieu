using Domain.Enums;

namespace Domain.Entities;

public class TaiKham
{
	public int TaiKhamID { get; private set; }
	public int PhienKhamID { get; private set; }
	public int BenhNhanID { get; private set; }
	public DateTime NgayDuKien { get; private set; }
	public string? LyDo { get; private set; }
	public TaiKhamEnum TrangThai { get; private set; }
	public int? CaKhamID { get; private set; }
	public DateTime NgayTao { get; private set; }

	// ==================== CONSTRUCTOR CREATE ====================
	public TaiKham(int phienKhamID, int benhNhanID, DateTime ngayDuKien, string? lyDo)
	{
		Validate(phienKhamID, benhNhanID, ngayDuKien);

		PhienKhamID = phienKhamID;
		BenhNhanID = benhNhanID;
		NgayDuKien = ngayDuKien;
		LyDo = lyDo;

		TrangThai = TaiKhamEnum.ChoKham;
	}

	// ==================== CONSTRUCTOR MAP DB ====================
	public TaiKham(int taiKhamID, int phienKhamID, int benhNhanID, DateTime ngayDuKien,
		string? lyDo, string? trangThai, int? caKhamID, DateTime ngayTao)
	{
		TaiKhamID = taiKhamID;
		PhienKhamID = phienKhamID;
		BenhNhanID = benhNhanID;
		NgayDuKien = ngayDuKien;
		LyDo = lyDo;
		TrangThai = TaiKhamExtensions.Parse(trangThai);
		CaKhamID = caKhamID;
		NgayTao = ngayTao;
	}

	// ==================== BUSINESS ====================

	public void CapNhatCaKham(int? caKhamID)
	{
		if (TrangThai != TaiKhamEnum.ChoKham)
			throw new InvalidOperationException("Chỉ được cập nhật ca khám khi đang chờ khám");

		CaKhamID = caKhamID;
	}

	public void CapNhatLyDo(string? lyDo)
	{
		LyDo = lyDo;
	}

	// ==================== BUSINESS - STATE ====================

	public void Complete()
	{
		if (TrangThai != TaiKhamEnum.ChoKham)
			throw new InvalidOperationException("Chỉ có thể hoàn thành khi đang khám");

		TrangThai = TaiKhamEnum.DaKham;
	}

	public void Cancel()
	{
		if (TrangThai == TaiKhamEnum.DaKham)
			throw new InvalidOperationException("Đã khám không thể hủy");

		if (TrangThai == TaiKhamEnum.DaHuy)
			throw new InvalidOperationException("Đã hủy trước đó");

		TrangThai = TaiKhamEnum.DaHuy;
	}

	// ==================== VALIDATION ====================

	private void Validate(int phienKhamID, int benhNhanID, DateTime ngayDuKien)
	{
		if (phienKhamID <= 0)
			throw new ArgumentException("PhienKhamID không hợp lệ");

		if (benhNhanID <= 0)
			throw new ArgumentException("BenhNhanID không hợp lệ");

		if (ngayDuKien.Date <= DateTime.Now.Date)
			throw new ArgumentException("Ngày tái khám phải lớn hơn hôm nay");
	}
}