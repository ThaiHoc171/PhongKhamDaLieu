namespace Domain.Entity
{
	public class NhanVien
	{
		public int NhanVienID { get; private set; }
		public int ChucVuID { get; private set; }

		public NhanVien(int nhanVienID, int chucVuID)
		{
			NhanVienID = nhanVienID;
			ChucVuID = chucVuID;
		}
	}

}
