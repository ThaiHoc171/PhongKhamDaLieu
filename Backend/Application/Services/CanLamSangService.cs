using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Domain.DTO;
using Domain.Entities;
using Domain.Repository;

namespace Services
{
	public class CanLamSangService
	{
		private readonly ICanLamSangRepositories _repo;
		public CanLamSangService(ICanLamSangRepositories repo)
		{
			_repo = repo;
		}
		public List<CanLamSangListDTO> DanhSachCanLamSang()
		{
			return _repo.DanhSachCanLamSang();
		}
		public CanLamSangDetailDTO CanLamSang(int id)
		{
			return _repo.LayChiTietCanLamSang(id);
		}
		public bool ThemCanLamSang(ThemCanLamSangDTO cls)
		{
			return _repo.ThemCanLamSang(cls);
		}
		public bool CapNhatCLS(CapNhatCanLamSangDTO cls)
		{
			return _repo.CapNhatCanLamSang(cls);
		}
		public bool ChuyenTrangThai(Status stt)
		{
			return _repo.ChuyenTrangThai(stt);
		}
	}
}
