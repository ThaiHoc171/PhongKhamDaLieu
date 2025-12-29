using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
	public interface IBenhNhanRepository
	{
		List<BenhNhanListDTO> LayDanhSachBenhNhan();
		List<BenhNhanListDTO> LayBenhNhanByKeyWord(string keyword);
		BenhNhanDetailDTO LayBenhNhanByID(int benhNhanID);
		bool ThemBenhNhan(BenhNhanCreateDTO bn);
		bool CapNhatBenhNhan(BenhNhanCreateDTO bn);
		bool XoaBenhNhan(int benhNhanID);
	}
}
