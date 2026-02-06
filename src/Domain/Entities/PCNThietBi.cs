namespace Domain.Entities;

public class PCNThietBi
{
	public int PCN_TB_ID { get; private set; }
	public int PhongChucNangID { get; private set; }
	public int ThietBiID { get; private set; }
	public int TongSoLuong { get; private set; }

	// Tạo mới
	public PCNThietBi(int phongChucNangId, int thietBiId)
	{
		if (phongChucNangId <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");

		if (thietBiId <= 0)
			throw new ArgumentException("Thiết bị không hợp lệ");

		PhongChucNangID = phongChucNangId;
		ThietBiID = thietBiId;
		TongSoLuong = 0; // DB default cũng là 0
	}

	// Map từ DB
	public PCNThietBi(
		int pcnTbId,
		int phongChucNangId,
		int thietBiId,
		int tongSoLuong)
	{
		if (pcnTbId <= 0 || phongChucNangId <= 0 || thietBiId <= 0)
			throw new ArgumentException("Dữ liệu DB không hợp lệ");

		if (tongSoLuong < 0)
			throw new ArgumentException("Tổng số lượng không hợp lệ");

		PCN_TB_ID = pcnTbId;
		PhongChucNangID = phongChucNangId;
		ThietBiID = thietBiId;
		TongSoLuong = tongSoLuong;
	}

	// Nghiệp vụ
	public void CapNhatSoLuong(int soLuongMoi)
	{
		if (soLuongMoi < 0)
			throw new ArgumentException("Tổng số lượng không hợp lệ");

		TongSoLuong = soLuongMoi;
	}

	public bool CoTheXoa()
	{
		return TongSoLuong == 0;
	}
}
