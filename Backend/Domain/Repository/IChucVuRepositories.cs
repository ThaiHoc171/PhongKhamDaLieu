using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface IChucVuRepository
	{
		List<ChucVuDTO> DanhSachChucVu();
		ChucVuDTO LayChucVuByID(int chucvuID);
		bool ThemChucVu(ThemChucVuDTO cv);
		bool CapNhatChucVu(ChucVuDTO cv);
		bool VoHieuHoaChucVu(int chucvuID);
	}
}
