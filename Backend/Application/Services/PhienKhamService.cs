using System.Collections.Generic;
using Domain.DTO;
using Domain.Entities;
using Domain.Repository;


namespace Services
{
	public class PhienKhamService
	{
		private readonly IPhienKhamRepositories _repo;
		public PhienKhamService(IPhienKhamRepositories repo)
		{
			_repo = repo;
		}
		public List<ListPhienKhamDTO> DanhSachPhienKham(int? Id)
		{
			return _repo.DanhSachPhienKham(Id??null);
		}
		public PhienKhamDetailDTO LayPhienKham(int Id)
		{
			return _repo.GetPhienKhambyID(Id);
		}
		public bool TaoPhienKham(TaoPhienKhamDTO pk)
		{
			return _repo.TaoPhienKham(pk);
		}
		public bool KetThucPhienKham(KetThucPhienKhamDTO pk)
		{
			return _repo.KetThucPhienKham(pk);
		}
		public bool HuyPhienKham(int PhienKhamID)
		{
			return _repo.HuyPhienKham(PhienKhamID);
		}
	}
}
