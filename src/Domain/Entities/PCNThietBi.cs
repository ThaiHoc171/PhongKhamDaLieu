namespace Domain.Entities;
 public class PCNThietBi
{
	public int PCN_TB_ID { get; private set; }
	public int PhongChucNangID { get; private set; }
	public int ThietBiID { get; private set; }
	public int TongSoLuong { get; private set; }
 	// Constructor tạo mới
	public PCNThietBi(int phongChucNangId, int thietBiId)
	{
		Validate(phongChucNangId, thietBiId);
 		PhongChucNangID = phongChucNangId;
		ThietBiID = thietBiId;
		TongSoLuong = 0;
	}
 	// Constructor map DB
	public PCNThietBi( int pcnTbId, int phongChucNangId, int thietBiId, int tongSoLuong)
	{
		PCN_TB_ID = pcnTbId;
		PhongChucNangID = phongChucNangId;
		ThietBiID = thietBiId;
		TongSoLuong = tongSoLuong;
	}
 	// Business method
	public void Update(int soLuongMoi)
	{
		if (soLuongMoi < 0)
			throw new ArgumentException("Số lượng không hợp lệ");
 		TongSoLuong = soLuongMoi;
	}
 	public bool IsDelete()
	{
		return TongSoLuong == 0;
	}
 	private void Validate(int phongChucNangId, int thietBiId)
	{
		if (phongChucNangId <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");
 		if (thietBiId <= 0)
			throw new ArgumentException("Thiết bị không hợp lệ");
	}
}