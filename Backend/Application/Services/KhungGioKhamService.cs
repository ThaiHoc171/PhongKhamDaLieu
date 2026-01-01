using Domain.DTO;
using Domain.Repository;
using System.Collections.Generic;

namespace Services
{
    public class KhungGioKhamService
    {
        private readonly IKhungGioKhamRepository _repo;

        public KhungGioKhamService(IKhungGioKhamRepository repo)
        {
            _repo = repo;
        }

        public List<KhungGioKhamDTO> DanhSach()
        {
            return _repo.DanhSach();
        }

        public KhungGioKhamDTO LayKhungGioKhamByID(int id)
        {
            return _repo.LayKhungGioKhamByID(id);
        }

        public List<KhungGioKhamDTO> LayKhungGioKhamByTen(string tenKhung)
        {
            return _repo.LayKhungGioKhamByTen(tenKhung);
        }

        public bool ThemKhungGioKham(ThemKhungGioKhamDTO kg)
        {
            return _repo.ThemKhungGioKham(kg);
        }

        public bool CapNhatKhungGioKham(KhungGioKhamDTO kg)
        {
            return _repo.CapNhatKhungGioKham(kg);
        }
    }
}
