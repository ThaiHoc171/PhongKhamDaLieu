using System;
using System.Collections.Generic;
using Application.Interfaces;
using Application.DTOs;
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
		public TaiKhoanResponseDTO DangNhap(LoginRequestDTO dto)
		{
			var tk = _repo.GetByEmail(dto.Email);
			if(tk == null) return null;
			if(!Helper.Password.VerifyPassword(dto.MatKhau, tk.MatKhau))
				return null;
			return new TaiKhoanResponseDTO
			{
				Id = tk.Id,
				Email = tk.Email,
				VaiTro = tk.Vaitro,
				TrangThai = tk.TrangThai
			};
		}
		public void DangKy(ThemTaiKhoanDTO taiKhoan)
		{
			var hash = Helper.Password.PassWordHash(taiKhoan.MatKhau);
			var tk = new TaiKhoan(taiKhoan.Email, hash, taiKhoan.VaiTro);
			_repo.Add(tk);
		}
		public bool DoiMatKhau(int taiKhoanId, DoiMatKhauDTO dto)
		{
			var tk = _repo.GetById(taiKhoanId);
			if (tk == null) return false;

			if (!Helper.Password.VerifyPassword(dto.MatKhauCu, tk.MatKhau))
				return false;

			var hashMoi = Helper.Password.PassWordHash(dto.MatKhauMoi);
			tk.DoiMatKhau(hashMoi);
			_repo.Update(tk);
			return true;
		}
		public bool CapNhatTrangThai(int taiKhoanId, string trangThaiMoi)
		{
			var tk = _repo.GetById(taiKhoanId);
			if (tk == null) return false;

			tk.CapNhatTrangThai(trangThaiMoi);
			_repo.Update(tk);
			return true;
		}
		public List<TaiKhoanResponseDTO> LayTatCaTaiKhoan()
		{
			var list = _repo.GetAll();
			var result = new List<TaiKhoanResponseDTO>();
			foreach (var tk in list)
			{
				result.Add(new TaiKhoanResponseDTO
				{
					Id = tk.Id,
					Email = tk.Email,
					VaiTro = tk.Vaitro,
					TrangThai = tk.TrangThai
				});
			}
			return result;
		}
		public TaiKhoanResponseDTO LayTaiKhoanTheoId(int id)
		{
			var tk = _repo.GetById(id);
			if (tk == null) return null;

			return new TaiKhoanResponseDTO
			{
				Id = tk.Id,
				Email = tk.Email,
				VaiTro = tk.Vaitro,
				TrangThai = tk.TrangThai
			};
		}
	}
}
