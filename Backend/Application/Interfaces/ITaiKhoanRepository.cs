using Domain.Entities;
using System.Collections.Generic;

namespace Application.Interfaces
{
	public interface ITaiKhoanRepository
	{
		TaiKhoan GetByEmail(string email);
		TaiKhoan GetById(int id);
		List<TaiKhoan> GetAll();
		void Add(TaiKhoan taiKhoan);
		void Update(TaiKhoan taiKhoan);
	}

}
