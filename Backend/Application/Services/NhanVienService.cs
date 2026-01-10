using System;
using System.Collections.Generic;
using Domain.Entities;
using Application.DTOs;
using Application.Interfaces;

namespace Services
{
	public class NhanVienService
	{
		private readonly INhanVienRepository _nhanVienRepo;
		private readonly IThongTinCaNhanRepository _thongTinRepo;
		public NhanVienService(INhanVienRepository nhanVienRepo,IThongTinCaNhanRepository thongTinRepo)
		{
			_nhanVienRepo = nhanVienRepo;
			_thongTinRepo = thongTinRepo;
		}
		public void ThemNhanVien(ThemNhanVienDTO dto)
		{
			var thongTin = new ThongTinCaNhan(
				hoTen: dto.HoTen,
				ngaySinh: dto.NgaySinh,
				gioiTinh: dto.GioiTinh,
				sdt: dto.SDT,
				emailLienHe: dto.EmailLienHe,
				diaChi: dto.DiaChi,
				avatar: dto.Avatar,
				loai: "Nhân viên"
			);

			int thongTinID = _thongTinRepo.Add(thongTin);

			var nhanVien = new NhanVien(
				thongTinID: thongTinID,
				chucVuID: dto.ChucVuID,
				ngayVaoLam: dto.NgayVaoLam,
				bangCap: dto.BangCap,
				kinhNghiem: dto.KinhNghiem
			);

			_nhanVienRepo.Add(nhanVien);
		}
		public bool CapNhatNhanVien(int nhanVienID, ThemNhanVienDTO dto)
		{
			var nv = _nhanVienRepo.GetById(nhanVienID);
			if (nv == null) return false;

			nv.CapNhat(
				chucVuID: dto.ChucVuID,
				ngayVaoLam: dto.NgayVaoLam,
				bangCap: dto.BangCap,
				kinhNghiem: dto.KinhNghiem
			);

			_nhanVienRepo.Update(nv);
			return true;
		}
		public List<ListNhanVienDTO> DanhSachNhanVien()
		{
			var list = _nhanVienRepo.GetAll();
			var result = new List<ListNhanVienDTO>();

			foreach (var nv in list)
			{
				result.Add(new ListNhanVienDTO
				{
					NhanVienID = nv.NhanVienID,
					HoTen = nv.ThongTinCaNhan.HoTen,
					SDT = nv.ThongTinCaNhan.SDT,
					EmailLienHe = nv.ThongTinCaNhan.EmailLienHe,
					TenChucVu = nv.TenChucVu,
					TrangThai = nv.TrangThai
				});
			}

			return result;
		}
		public List<ListNhanVienDTO> TimKiemNhanVien(string keyword)
		{
			var list = _nhanVienRepo.GetNhanViens(keyword);
			var result = new List<ListNhanVienDTO>();
			foreach (var nv in list)
			{
				result.Add(new ListNhanVienDTO
				{
					NhanVienID = nv.NhanVienID,
					HoTen = nv.ThongTinCaNhan.HoTen,
					SDT = nv.ThongTinCaNhan.SDT,
					EmailLienHe = nv.ThongTinCaNhan.EmailLienHe,
					TenChucVu = nv.TenChucVu,
					TrangThai = nv.TrangThai
				});
			}
			return result;
		}
		public bool CapNhatTrangThai(int nhanVienID)
		{
			var nv = _nhanVienRepo.GetById(nhanVienID);
			if (nv == null) return false;
			string trangThai = "Nghỉ việc";
			nv.CapNhatTrangThai(trangThai);
			_nhanVienRepo.UpdateTrangThai(nhanVienID, trangThai);
			return true;
		}
		public NhanVien ChiTietNhanVien(int nhanVienID)
		{
			return _nhanVienRepo.GetById(nhanVienID);
		}
	}
}
