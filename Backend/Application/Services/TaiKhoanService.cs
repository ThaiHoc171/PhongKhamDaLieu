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
		public TaiKhoanResponseDto DangNhap(LoginRequestDto dto)
		{
			var tk = _repo.GetByEmail(dto.Email);
			if(tk == null) return null;
			if(!Helper.Password.VerifyPassword(dto.MatKhau, tk.MatKhau))
				return null;
			return new TaiKhoanResponseDto
			{
				Id = tk.Id,
				Email = tk.Email,
				Role = tk.Vaitro,
				Status = tk.TrangThai
			};
		}
		public void DangKy(TaiKhoan taiKhoan)
		{
			var hash = Helper.Password.PassWordHash(taiKhoan.MatKhau);
			var tk = new TaiKhoan(taiKhoan.Email, hash, taiKhoan.Vaitro);
			_repo.Add(tk);
		}
		public bool DoiMatKhau(int taiKhoanId, DoiMatKhauDto dto)
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
		public List<TaiKhoanResponseDto> LayTatCaTaiKhoan()
		{
			var list = _repo.GetAll();
			var result = new List<TaiKhoanResponseDto>();
			foreach (var tk in list)
			{
				result.Add(new TaiKhoanResponseDto
				{
					Id = tk.Id,
					Email = tk.Email,
					Role = tk.Vaitro,
					Status = tk.TrangThai
				});
			}
			return result;
		}
		public TaiKhoanResponseDto LayTaiKhoanTheoId(int id)
		{
			var tk = _repo.GetById(id);
			if (tk == null) return null;

			return new TaiKhoanResponseDto
			{
				Id = tk.Id,
				Email = tk.Email,
				Role = tk.Vaitro,
				Status = tk.TrangThai
			};
		}
	}
}
