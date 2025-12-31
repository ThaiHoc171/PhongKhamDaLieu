using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

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
		public (bool Success, string Message) ThemNhanVien(NhanVienCreateDTO nv)
		{
			if(Helper.EmailHelper.IsEmail(nv.EmailLienHe) == false)
				return (false, "Email không hợp lệ");
			bool result = _repo.ThemNhanVien(nv);
			if (result)
				return (true, "Thêm nhân viên thành công.");
			return (false, "Thêm nhân viên thất bại.");
		}
		public (bool Success, string Message) CapNhatNhanVien(NhanVienUpdateDTO nv)
		{
			if(Helper.EmailHelper.IsEmail(nv.EmailLienHe) == false)
				return (false, "Email không hợp lệ");
			bool result = _repo.CapNhatNhanVien(nv);
			if (result)
				return (true, "Cập nhật nhân viên thành công.");
			return (false, "Cập nhật nhân viên thất bại.");
		}
		public bool XoaNhanVien(int nhanVienID)
		{
			return _repo.XoaNhanVien(nhanVienID);
		}
	}
}
