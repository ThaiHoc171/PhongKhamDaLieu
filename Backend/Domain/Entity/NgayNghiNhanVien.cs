using System;

namespace Domain.Entity
{
	public class NgayNghiNhanVien
	{
		public int NhanVienID { get; }
		public DateTime Ngay { get; }

		public NgayNghiNhanVien(int nhanVienID, DateTime ngay)
		{
			NhanVienID = nhanVienID;
			Ngay = ngay.Date;
		}
	}

}
