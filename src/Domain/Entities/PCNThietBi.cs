using Domain.Enums;

namespace Domain.Entities;

public class PCNThietBi
{
	public int Id { get; private set; }
	public int PhongChucNangID { get; private set; }
	public int ThietBiID { get; private set; }
	public int SoLuong { get; private set; }
	public TinhTrang TinhTrang { get; private set; }
	public DateTime NgayNhap { get; private set; }

	public PCNThietBi(int phongChucNangId, int thietBiId, int soLuong)
	{
		if (phongChucNangId <= 0)
			throw new ArgumentException("Phòng chức năng không hợp lệ");

		if (thietBiId <= 0)
			throw new ArgumentException("Thiết bị không hợp lệ");

		if (soLuong <= 0)
			throw new ArgumentException("Số lượng phải lớn hơn 0");

		PhongChucNangID = phongChucNangId;
		ThietBiID = thietBiId;
		SoLuong = soLuong;
		// domain default
		TinhTrang = TinhTrang.HoatDong;
		NgayNhap = DateTime.UtcNow;
	}


	public PCNThietBi(
		int id,
		int phongChucNangId,
		int thietBiId,
		int soLuong,
		string tinhTrangDb,
		DateTime ngayNhap)
	{
		if (soLuong < 0)
			throw new ArgumentException("Số lượng không hợp lệ");
		if (phongChucNangId <= 0 || thietBiId <= 0)
			throw new ArgumentException("Dữ liệu DB không hợp lệ");

		Id = id;
		PhongChucNangID = phongChucNangId;
		ThietBiID = thietBiId;
		SoLuong = soLuong;
		TinhTrang = TinhTrangExtensions.FromDb(tinhTrangDb);
		NgayNhap = ngayNhap;
	}


	public void CapNhatSoLuong(int soLuong)
	{
		if (soLuong < 0)
			throw new ArgumentException("Số lượng không hợp lệ");

		SoLuong = soLuong;
	}

	public void ChuyenTinhTrang(TinhTrang tinhTrangMoi)
	{
		// rule ví dụ: đang hỏng thì không cho hoạt động ngay
		if (TinhTrang == TinhTrang.Hong && tinhTrangMoi == TinhTrang.HoatDong)
			throw new InvalidOperationException("Thiết bị hỏng cần bảo trì trước");

		TinhTrang = tinhTrangMoi;
	}

	public bool CanXoa()
	{
		return SoLuong == 0;
	}
}
