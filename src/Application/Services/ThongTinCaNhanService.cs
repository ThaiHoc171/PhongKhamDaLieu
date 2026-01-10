using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Services
{
	public class ThongTinCaNhanService
	{
		private readonly IThongTinCaNhanRepository _repo;

		public ThongTinCaNhanService(IThongTinCaNhanRepository repo)
		{
			_repo = repo;
		}

		public int ThemThongTinNhanVien(ThemThongTinCaNhanDTO dto)
		{
			var entity = new ThongTinCaNhan(
				hoTen: dto.HoTen,
				ngaySinh: dto.NgaySinh,
				gioiTinh: dto.GioiTinh,
				sdt: dto.SDT,
				emailLienHe: dto.EmailLienHe,
				diaChi: dto.DiaChi,
				avatar: dto.Avatar,
				loai: "Nhân viên"
			);

			return _repo.Add(entity);
		}
		public int ThemThongTinBenhNhan(ThemThongTinCaNhanDTO dto)
		{
			var entity = new ThongTinCaNhan(
				hoTen: dto.HoTen,
				ngaySinh: dto.NgaySinh,
				gioiTinh: dto.GioiTinh,
				sdt: dto.SDT,
				emailLienHe: dto.EmailLienHe,
				diaChi: dto.DiaChi,
				avatar: dto.Avatar,
				loai: "Bệnh nhân"
			);

			return _repo.Add(entity);
		}

		public List<ThongTinCaNhanResponseDTO> DanhSachThongTinBenhNhan()
		{
			var list = _repo.GetAllByLoai("Bệnh nhân");
			return MapToResponseList(list);
		}

		public List<ThongTinCaNhanResponseDTO> DanhSachThongTinNhanVien()
		{
			var list = _repo.GetAllByLoai("Nhân viên");
			return MapToResponseList(list);
		}

		public ThongTinCaNhanResponseDTO ThongTin(int id)
		{
			var entity = _repo.GetById(id);
			if (entity == null) return null;

			return MapToResponse(entity);
		}
		public bool CapNhatThongTin(int thongTinID, CapNhatThongTinCaNhanDTO dto)
		{
			var entity = _repo.GetById(thongTinID);
			if (entity == null) return false;

			entity.CapNhat(
				hoTen: dto.HoTen,
				ngaySinh: dto.NgaySinh,
				gioiTinh: dto.GioiTinh,
				sdt: dto.SDT,
				emailLienHe: dto.EmailLienHe,
				diaChi: dto.DiaChi,
				avatar: dto.Avatar
			);

			_repo.Update(entity);
			return true;
		}

		private List<ThongTinCaNhanResponseDTO> MapToResponseList(List<ThongTinCaNhan> entities)
		{
			var result = new List<ThongTinCaNhanResponseDTO>();
			foreach (var e in entities)
			{
				result.Add(MapToResponse(e));
			}
			return result;
		}

		private ThongTinCaNhanResponseDTO MapToResponse(ThongTinCaNhan e)
		{
			return new ThongTinCaNhanResponseDTO
			{
				ThongTinID = e.ThongTinID,
				TaiKhoanID = e.TaiKhoanID,
				HoTen = e.HoTen,
				SDT = e.SDT,
				EmailLienHe = e.EmailLienHe,
				Avatar = e.Avatar,
				Loai = e.Loai
			};
		}
	}
}
