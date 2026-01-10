using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
	public interface IThongTinCaNhanRepository
	{
		int Add(ThongTinCaNhan thongTin);
		void Update(ThongTinCaNhan thongTin);
		ThongTinCaNhan GetById(int thongTinID);
		List<ThongTinCaNhan> GetAllByLoai(string loai);
	}
}

