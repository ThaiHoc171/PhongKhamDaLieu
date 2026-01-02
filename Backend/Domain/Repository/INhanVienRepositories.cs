using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Repository
{
	public interface INhanVienRepository
	{
		List<NhanVienListDTO> LayDanhSachNhanVien();

		NhanVienDetailDTO LayNhanVienByID(int nhanVienID);
		List<NhanVienListDTO> LayNhanVienByKeyWord(string keyword);
		bool ThemNhanVien(NhanVienCreateDTO nv);
		bool CapNhatNhanVien(NhanVienUpdateDTO nv);
		bool ChuyenTrangThai(Status stt);
	}
}
