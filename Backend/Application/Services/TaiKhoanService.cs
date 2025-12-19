using System;
using System.Collections.Generic;
using Domain.DTO;
using Repository;

namespace Services
{
	public class TaiKhoanService
	{
		private readonly ITaiKhoanRepository _repo;
		public TaiKhoanService(ITaiKhoanRepository repo)
		{
			_repo = repo;
		}
		public TaiKhoanDTO DangNhap(string email, string password)
		{
			var tk = _repo.LayTaiKhoanTheoEmail(email);
			if(tk != null && Helper.Password.VerifyPassword(password, tk.PasswordHash))
			{
				return tk;
			}
			return null;
		}
		public List<TaiKhoanDTO> LayDanhSachTaiKhoan()
		{
			return _repo.LayDanhSachTaiKhoan();
		}	
	}
}
