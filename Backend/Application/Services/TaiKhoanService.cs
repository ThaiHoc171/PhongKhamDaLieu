using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

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
			TaiKhoanDTO tk = _repo.LayTaiKhoanTheoEmail(email);
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
		public TaiKhoanDTO LayTaiKhoanTheoEmail(string email)
		{
			return _repo.LayTaiKhoanTheoEmail(email);
		}
		public bool KiemTraEmailTonTai(string email)
		{
			TaiKhoanDTO tk = _repo.LayTaiKhoanTheoEmail(email);
			return tk != null;
		}
		public bool DangKyTaiKhoan(string email, string password, string vaitro)
		{
			if(KiemTraEmailTonTai(email))
			{
				return false;
			}
			string passwordHash = Helper.Password.PassWordHash(password);
			TaiKhoanCreateDTO taiKhoanCreateDTO = new TaiKhoanCreateDTO
			{
				Email = email,
				MatKhau = passwordHash,
				VaiTro = vaitro
			};
			return _repo.TaoTaiKhoan(taiKhoanCreateDTO);
		}
		public bool DoiMatKhau(int taiKhoanID,string password, string newPassword)
		{
			TaiKhoanDTO tk = _repo.LayTaiKhoanTheoID(taiKhoanID);
			if(tk == null || !Helper.Password.VerifyPassword(password, tk.PasswordHash))
			{
				return false;
			}
			string newPasswordHash = Helper.Password.PassWordHash(newPassword);
			return _repo.CapNhatMatKhau(taiKhoanID, newPasswordHash);
		}
		public bool ResetMatKhau(int taiKhoanID, string newPassword)
		{
			TaiKhoanDTO tk = _repo.LayTaiKhoanTheoID(taiKhoanID);
			if(tk == null)
			{
				return false;
			}
			string newPasswordHash = Helper.Password.PassWordHash(newPassword);
			return _repo.CapNhatMatKhau(taiKhoanID, newPasswordHash);
		}
	}
}
