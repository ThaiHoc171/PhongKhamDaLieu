using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;
using Domain.Entities;


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
		public (bool Success, string Message) ThemThongTinBenhNhan(HoSoBenhNhanDTO hs)
		{
			if(Helper.EmailHelper.IsEmail(hs.EmailLienHe) == false)
			{
				return (false, "Email không hợp lệ.");
			}
			bool result = _repo.ThemThongTinBenhNhan(hs);
			if(result)
			{
				return (true, "Thêm hồ sơ bệnh nhân thành công.");
			}
			return (false, "Thêm hồ sơ bệnh nhân thất bại.");
		}
		public bool ThemBenhNhan(ThemBenhNhanDTO bn)
		{
			return _repo.ThemBenhNhan(bn);
		}
		public (bool Success, string Message) CapNhatBenhNhan(BenhNhanUpdateDTO bn)
		{
			if (Helper.EmailHelper.IsEmail(bn.EmailLienHe) == false)
			{
				return (false, "Email không hợp lệ.");
			}
			bool result = _repo.CapNhatBenhNhan(bn);
			if (result)
			{
				return (true, "Cập nhật hồ sơ bệnh nhân thành công.");
			}
			return (false, "Cập nhật hồ sơ bệnh nhân thất bại.");
		}
		public bool ChuyenTrangThai(Status stt)
		{
			return _repo.ChuyenTrangThai(stt);
		}
	}
}
