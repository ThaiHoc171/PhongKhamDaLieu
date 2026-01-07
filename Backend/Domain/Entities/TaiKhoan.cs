using System;

namespace Domain.Entities
{
	public class TaiKhoan
	{
		public int Id { get; private set; }
		public string Email { get; private set; }
		public string MatKhau { get; private set; }
		public string Vaitro { get; private set; }
		public string TrangThai { get; private set; }
		public DateTime NgayTao { get; private set; }

		public TaiKhoan(string email, string matkhau, string vaitro)
		{
			Email = email;
			MatKhau = matkhau;
			Vaitro = vaitro;
			TrangThai = "Hoạt động";
			NgayTao = DateTime.Now;
		}
		public TaiKhoan(int id, string email,  string matkhau, string vaitro, string trangthai, DateTime ngaytao)
		{
			Id = id;
			Email = email;
			MatKhau = matkhau;
			Vaitro = vaitro;
			TrangThai = trangthai;
			NgayTao = ngaytao;
		}
		public void DoiMatKhau(string newHash)
		{
			MatKhau = newHash;
		}

		public void CapNhatTrangThai(string trangthai)
		{
			TrangThai = trangthai;
		}
	}
}

