using System;

namespace Domain.Entity
{
	public class LichLamViecNhanVien
	{
		public int NhanVienID { get; }
		public DateTime Ngay { get; }
		public int CaLamViec { get; }

		public LichLamViecNhanVien(int nhanVienID, DateTime ngay, int caLamViec)
		{
			NhanVienID = nhanVienID;
			Ngay = ngay.Date;
			CaLamViec = caLamViec;
		}
	}

}
