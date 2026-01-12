using System;


namespace Domain.Entities
{
	public class NhanVien
	{
		public int NhanVienID { get; private set; }
		public int ThongTinID { get; private set; }
		public int ChucVuID { get; private set; }
		public DateTime? NgayVaoLam { get; private set; }
		public string BangCap { get; private set; }
		public string KinhNghiem { get; private set; }
		public string TrangThai { get; private set; }

		// Dữ liệu liên kết 
		public ThongTinCaNhan ThongTinCaNhan { get; private set; }
		public string TenChucVu { get; private set; }

		// Tạo mới
		public NhanVien(int thongTinID, int chucVuID,DateTime? ngayVaoLam, string bangCap, string kinhNghiem)
		{
			ThongTinID = thongTinID;
			ChucVuID = chucVuID;
			NgayVaoLam = ngayVaoLam;
			BangCap = bangCap;
			KinhNghiem = kinhNghiem;
			TrangThai = "Đang làm việc";
		}
		// Cập nhật 
		public void CapNhat(int chucVuID, DateTime? ngayVaoLam, string bangCap, string kinhNghiem)
		{
			ChucVuID = chucVuID;
			NgayVaoLam = ngayVaoLam;
			BangCap = bangCap;
			KinhNghiem = kinhNghiem;
		}

		// Đọc từ DB
		public NhanVien(int nhanVienID,int thongTinID,int chucVuID, DateTime? ngayVaoLam,
			string bangCap,string kinhNghiem,string trangThai, string tenChucVu, ThongTinCaNhan thongTinCaNhan)
		{
			NhanVienID = nhanVienID;
			ThongTinID = thongTinID;
			ChucVuID = chucVuID;
			NgayVaoLam = ngayVaoLam;
			BangCap = bangCap;
			KinhNghiem = kinhNghiem;
			TrangThai = trangThai;
			TenChucVu = tenChucVu;
			ThongTinCaNhan = thongTinCaNhan;
		}
		public void CapNhatTrangThai(string trangThai)
		{
			if (!string.IsNullOrWhiteSpace(trangThai))
				TrangThai = trangThai;
		}
	}
}

