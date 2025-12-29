using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IPhongChucNangRepository
	{
		List<PhongChucNangDTO> DanhSachPhongChucNang();
		List<PhongChucNangDTO> TimKiem(string keyword);
		PhongChucNangDTO GetPhongByID(int ID);
		bool ThemPhongChucNang(PhongChucNangCreateDTO pcn);
		bool CapNhatPhongChucNang(PhongChucNangDTO pcn);
		bool XoaPhongChucNang(int phongChucNangID);
	}
}
