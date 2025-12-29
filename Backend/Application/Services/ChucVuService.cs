using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;
using Domain.Repository;


namespace Services
{
	public class ChucVuService
	{
		private readonly IChucVuRepository _repo;
		public ChucVuService(IChucVuRepository repo)
		{
			_repo = repo;
		}
		public List<ChucVuDTO> DanhSachChucVu()
		{
			return _repo.DanhSachChucVu();
		}
		public ChucVuDTO LayChucVuByID(int chucvuID)
		{
			return _repo.LayChucVuByID(chucvuID);
		}
		public bool ThemChucVu(ThemChucVuDTO cv)
		{
			return _repo.ThemChucVu(cv);
		}
		public bool CapNhatChucVu(ChucVuDTO cv)
		{
			return _repo.CapNhatChucVu(cv);
		}
		public bool VoHieuHoaChucVu(int chucvuID)
		{
			return _repo.VoHieuHoaChucVu(chucvuID);
		}
	}
}
