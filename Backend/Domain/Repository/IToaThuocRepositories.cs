using Domain.DTO;
using System.Collections.Generic;


namespace Domain.Repository
{
	public interface IToaThuocRepository
	{
		List<ToaThuocDTO> DanhSachToaThuoc();
		ToaThuocDTO LayToaThuocByID(int toaThuocID);
		List<ToaThuocDTO> LayToaThuocByPhienKhamID(int phienKhamID);

		bool ThemToaThuoc(ToaThuocCreateDTO toaThuoc);
		bool CapNhatToaThuoc(ToaThuocDTO toaThuoc);
	}
}
