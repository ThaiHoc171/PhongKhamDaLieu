using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;


namespace Services
{
	public class BenhNhanService
	{
		private readonly IBenhNhanRepository _repo;
		public BenhNhanService(IBenhNhanRepository repo)
		{
			_repo = repo;
		}
		public List<BenhNhanListDTO> LayDanhSachBenhNhan()
		{
			return _repo.LayDanhSachBenhNhan();
		}
		public List<BenhNhanListDTO> LayBenhNhanByKeyWord(string keyword)
		{
			return _repo.LayBenhNhanByKeyWord(keyword);
		}
		public BenhNhanDetailDTO LayBenhNhanByID(int benhNhanID)
		{
			return _repo.LayBenhNhanByID(benhNhanID);
		}
		public bool ThemBenhNhan(BenhNhanCreateDTO bn)
		{
			return _repo.ThemBenhNhan(bn);
		}
		public bool CapNhatBenhNhan(BenhNhanCreateDTO bn)
		{
			return _repo.CapNhatBenhNhan(bn);
		}
		public bool XoaBenhNhan(int benhNhanID)
		{
			return _repo.XoaBenhNhan(benhNhanID);
		}
	}
}
