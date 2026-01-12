using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
	public interface IPhienKhamRepositories
	{
		List<ListPhienKhamDTO> DanhSachPhienKham(int? Id);
		PhienKhamDetailDTO GetPhienKhambyID(int PhienKhamID);
		bool TaoPhienKham(TaoPhienKhamDTO pk);
		bool KetThucPhienKham(KetThucPhienKhamDTO pk);
		bool HuyPhienKham(int PhienKhamID);
	}
}
