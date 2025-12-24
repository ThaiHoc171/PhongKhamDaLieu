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
		public NhanVienDetailDTO LayThongTinTheoID(int nhanVienID)
		{
			return _repo.LayThongTinTheoID(nhanVienID);
		}
		public bool ThemNhanVien(NhanVienDetailDTO nv)
		{
			return _repo.ThemNhanVien(nv);
		}
	}
}
