using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;


namespace Domain.Repository
{
	public interface IPhongChucNangRepository
	{
		List<PhongChucNangDTO> DanhSachPhongChucNang();
		List<PhongChucNangDTO> TimKiem(string keyword);
		PhongChucNangDTO GetPhongByID(int ID);
		bool ThemPhongChucNang(PhongChucNangCreateDTO pcn);
		bool CapNhatPhongChucNang(PhongChucNangDTO pcn);
		bool ChuyenTrangThai(Status stt);
	}
}
