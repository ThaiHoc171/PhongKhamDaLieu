using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IChiTietToaThuocRepository
	{
		List<ChiTietToaThuocDTO> DanhSachChiTietToaThuoc();
		ChiTietToaThuocDTO LayChiTietToaThuocByID(int chiTietToaThuocID);
		List<ChiTietToaThuocDTO> LayChiTietToaThuocByToaThuocID(int toaThuocID);
		bool ThemChiTietToaThuoc(ChiTietToaThuocCreateDTO ct);
		bool CapNhatChiTietToaThuoc(ChiTietToaThuocDTO ct);
	}
}
