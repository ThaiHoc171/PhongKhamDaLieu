using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IThuocRepository
	{
		List<ThuocDTO> DanhSachThuoc();
		List<ThuocDTO> DanhSachThuocTheoKeywords(string kw);
		ThuocDTO LayThuocTheoID(int thuocid);
		bool ThemThuoc(ThuocCreateDTO thuoc);
		bool CapNhatThuoc(ThuocDTO thuoc);
	}
}
