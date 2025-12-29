using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
	public class PhongChucNangService
	{
		private readonly IPhongChucNangRepository _repo;
		public PhongChucNangService(IPhongChucNangRepository repo)
		{
			_repo = repo;
		}
		public List<PhongChucNangDTO> DanhSachPhongChucNang()
		{
			return _repo.DanhSachPhongChucNang();
		}
		public PhongChucNangDTO LayPhongChucNangTheoID(int phongChucNangID)
		{
			return _repo.GetPhongByID(phongChucNangID);
		}
		public List<PhongChucNangDTO> TimKiem(string keyword)
		{
			return _repo.TimKiem(keyword);
		}
		public bool ThemPhongChucNang(PhongChucNangCreateDTO pcn)
		{
			return _repo.ThemPhongChucNang(pcn);
		}
		public bool CapNhatPhongChucNang(PhongChucNangDTO pcn)
		{
			return _repo.CapNhatPhongChucNang(pcn);
		}
		public bool XoaPhongChucNang(int phongChucNangID)
		{
			return _repo.XoaPhongChucNang(phongChucNangID);
		}
	}
}
