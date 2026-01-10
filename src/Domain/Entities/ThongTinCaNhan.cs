using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ThongTinCaNhan
	{
		public int ThongTinID { get; private set; }
		public int? TaiKhoanID { get; private set; }

		public string HoTen { get; private set; }
		public DateTime? NgaySinh { get; private set; }
		public string GioiTinh { get; private set; }

		public string SDT { get; private set; }
		public string EmailLienHe { get; private set; }
		public string DiaChi { get; private set; }
		public string Avatar { get; private set; }

		public string Loai { get; private set; } // Bệnh nhân | Nhân viên

		public DateTime NgayTao { get; private set; }
		public DateTime NgayCapNhat { get; private set; }

		//Tạo mới
		public ThongTinCaNhan(
			string hoTen,
			DateTime? ngaySinh,
			string gioiTinh,
			string sdt,
			string emailLienHe,
			string diaChi,
			string avatar,
			string loai,
			int? taiKhoanID = null
		)
		{
			HoTen = hoTen;
			NgaySinh = ngaySinh;
			GioiTinh = gioiTinh;
			SDT = sdt;
			EmailLienHe = emailLienHe;
			DiaChi = diaChi;
			Avatar = avatar;
			Loai = loai;
			TaiKhoanID = taiKhoanID;

			NgayTao = DateTime.Now;
			NgayCapNhat = DateTime.Now;
		}

		//Đọc từ DB
		public ThongTinCaNhan(
			int thongTinID,
			int? taiKhoanID,
			string hoTen,
			DateTime? ngaySinh,
			string gioiTinh,
			string sdt,
			string emailLienHe,
			string diaChi,
			string avatar,
			string loai,
			DateTime ngayTao,
			DateTime ngayCapNhat
		)
		{
			ThongTinID = thongTinID;
			TaiKhoanID = taiKhoanID;
			HoTen = hoTen;
			NgaySinh = ngaySinh;
			GioiTinh = gioiTinh;
			SDT = sdt;
			EmailLienHe = emailLienHe;
			DiaChi = diaChi;
			Avatar = avatar;
			Loai = loai;
			NgayTao = ngayTao;
			NgayCapNhat = ngayCapNhat;
		}

		public void CapNhat(
			string hoTen,
			DateTime? ngaySinh,
			string gioiTinh,
			string sdt,
			string emailLienHe,
			string diaChi,
			string avatar
		)
		{
			HoTen = hoTen;
			NgaySinh = ngaySinh;
			GioiTinh = gioiTinh;
			SDT = sdt;
			EmailLienHe = emailLienHe;
			DiaChi = diaChi;
			Avatar = avatar;
			NgayCapNhat = DateTime.Now;
		}
	}
}
