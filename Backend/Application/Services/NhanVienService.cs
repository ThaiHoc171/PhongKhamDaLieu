using System;
using System.Collections.Generic;
using Domain.DTO;
using Repository;

namespace Services
{
	public class NhanVienService
	{
		private readonly INhanVienRepository _repo;
		public NhanVienService(INhanVienRepository repo)
		{
			_repo = repo;
		}
		public List<NhanVienListDTO> LayDanhSachNhanVien()
		{
			return _repo.LayDanhSachNhanVien();
		}
		public NhanVienDetailDTO LayNhanVienTheoID(int nhanVienID)
		{
			return _repo.LayNhanVienByID(nhanVienID);
		}
		public List<NhanVienListDTO> LayNhanVienTheoTuKhoa(string keyword)
		{
			return _repo.LayNhanVienByKeyWord(keyword);
		}
		public bool ThemNhanVien(NhanVienCreateDTO nv)
		{
			return _repo.ThemNhanVien(nv);
		}
		public bool CapNhatNhanVien(NhanVienDetailDTO nv)
		{
			return _repo.CapNhatNhanVien(nv);
		}
		public bool XoaNhanVien(int nhanVienID)
		{
			return _repo.XoaNhanVien(nhanVienID);
		}
	}
}
