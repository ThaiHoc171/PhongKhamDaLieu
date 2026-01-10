using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class ChiTietToaThuocService
    {
        private readonly IChiTietToaThuocRepository _repo;

        public ChiTietToaThuocService(IChiTietToaThuocRepository repo)
        {
            _repo = repo;
        }

        public List<ChiTietToaThuocDTO> DanhSachChiTietToaThuoc()
        {
            return _repo.DanhSachChiTietToaThuoc();
        }

        public ChiTietToaThuocDTO LayChiTietToaThuocByID(int id)
        {
            return _repo.LayChiTietToaThuocByID(id);
        }

        public List<ChiTietToaThuocDTO> LayChiTietToaThuocByToaThuocID(int toaThuocID)
        {
            return _repo.LayChiTietToaThuocByToaThuocID(toaThuocID);
        }

        public bool ThemChiTietToaThuoc(ChiTietToaThuocCreateDTO ct)
        {
            return _repo.ThemChiTietToaThuoc(ct);
        }

        public bool CapNhatChiTietToaThuoc(ChiTietToaThuocDTO ct)
        {
            return _repo.CapNhatChiTietToaThuoc(ct);
        }
    }
}
