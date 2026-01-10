using System.Collections.Generic;
using Domain.Entities;

namespace Application.Interfaces
{
	public interface IChucVuRepository
	{
		List<ChucVu> GetAll();
		ChucVu GetById(int id);
		void Add(ChucVu chucVu);
		void Update(ChucVu chucVu);
	}
}
