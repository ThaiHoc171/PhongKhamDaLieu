using Domain.DTO;
using System.Collections.Generic;


namespace Domain.Repository
{
	public interface ILoaiBenhRepository
	{
		List<LoaiBenhDTO> DanhSachLoaiBenh();
		LoaiBenhDTO LayLoaiBenhByID(int LoaiBenhID);
		List<LoaiBenhDTO> LayLoaiBenhByTen(string tenBenh);
		bool ThemLoaiBenh(ThemLoaiBenhDTO lb);
		bool CapNhatLoaiBenh(LoaiBenhDTO lb);
	}
}
