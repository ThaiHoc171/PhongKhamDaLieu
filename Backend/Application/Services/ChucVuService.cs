using System.Collections.Generic;
using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;

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
			var list = _repo.GetAll();
			var result = new List<ChucVuDTO>();
			foreach (var item in list)
			{
				result.Add(new ChucVuDTO
				{
					ChucVuID = item.ChucVuID,
					TenChucVu = item.TenChucVu,
					MoTa = item.MoTa,
					NgayTao = item.NgayTao
				});
			}
			return result;
		}
		public ChucVuDTO LayChucVuTheoID(int id)
		{
			var cv = _repo.GetById(id);
			if (cv == null) return null;
			return new ChucVuDTO
			{
				ChucVuID = cv.ChucVuID,
				TenChucVu = cv.TenChucVu,
				MoTa = cv.MoTa,
				NgayTao = cv.NgayTao
			};
		}
		public bool ThemChucVu(ThemChucVuDTO dto)
		{
			var cv = new ChucVu(dto.TenChucVu, dto.MoTa);
			_repo.Add(cv);
			return true;
		}
		public bool CapNhatChucVu(int id, CapNhatChucVuDTO dto)
		{
			var cv = _repo.GetById(id);
			if (cv == null) return false;
			cv.CapNhat(dto.TenChucVu, dto.MoTa);
			_repo.Update(cv);
			return true;
		}
		public bool CapNhatTrangThaiChucVu(int id, string trangThaiMoi)
		{
			var cv = _repo.GetById(id);
			if (cv == null) return false;
			cv.CapNhatTrangThai(trangThaiMoi);
			_repo.Update(cv);
			return true;
		}
	}
}
