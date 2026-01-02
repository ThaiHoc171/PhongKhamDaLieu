using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;
using Domain.Entities;

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
		public bool DangKyTaiKhoan(TaiKhoanCreateDTO tk)
		{
			if(KiemTraEmailTonTai(tk.Email))
			{
				return false;
			}
			string passwordHash = Helper.Password.PassWordHash(tk.MatKhau);
			TaiKhoanCreateDTO taiKhoanCreateDTO = new TaiKhoanCreateDTO
			{
				Email = tk.Email,
				MatKhau = passwordHash,
				VaiTro = tk.VaiTro
			};
			return _repo.TaoTaiKhoan(taiKhoanCreateDTO);
		}
		public bool DoiMatKhau(DoiMatKhauDTO taikhoan)
		{
			TaiKhoanDTO tk = _repo.LayTaiKhoanTheoID(taikhoan.TaiKhoanID);
			if(tk == null || !Helper.Password.VerifyPassword(taikhoan.MatKhauCu, tk.PasswordHash))
			{
				return false;
			}
			string newPasswordHash = Helper.Password.PassWordHash(taikhoan.MatKhauMoi);
			return _repo.CapNhatMatKhau(taikhoan.TaiKhoanID, newPasswordHash);
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
		public bool ChuyenTrangThai(Status stt)
		{
			return _repo.ChuyenTrangThai(stt);
		}
	}
}
