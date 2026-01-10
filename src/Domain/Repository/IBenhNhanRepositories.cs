using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repository
{
	public interface IBenhNhanRepository
	{
		List<BenhNhanListDTO> LayDanhSachBenhNhan();
		List<BenhNhanListDTO> LayBenhNhanByKeyWord(string keyword);
		BenhNhanDetailDTO LayBenhNhanByID(int benhNhanID);
		bool ThemThongTinBenhNhan(HoSoBenhNhanDTO hs);
		bool ThemBenhNhan(ThemBenhNhanDTO bn);
		bool CapNhatBenhNhan(BenhNhanUpdateDTO bn);
		bool ChuyenTrangThai(Status stt);
	}
}
