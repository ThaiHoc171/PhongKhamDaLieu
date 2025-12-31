using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class ToaThuocService
    {
        private readonly IToaThuocRepository _repo;

        public ToaThuocService(IToaThuocRepository repo)
        {
            _repo = repo;
        }

        public List<ToaThuocDTO> DanhSach()
        {
            return _repo.DanhSach();
        }

        public ToaThuocDTO LayToaThuocByID(int toaThuocID)
        {
            return _repo.LayToaThuocByID(toaThuocID);
        }

        public List<ToaThuocDTO> LayToaThuocByPhienKhamID(int phienKhamID)
        {
            return _repo.LayToaThuocByPhienKhamID(phienKhamID);
        }


        public bool ThemToaThuoc(ToaThuocCreateDTO toaThuoc)
        {
            return _repo.ThemToaThuoc(toaThuoc);
        }

        public bool CapNhatToaThuoc(ToaThuocDTO toaThuoc)
        {
            return _repo.CapNhatToaThuoc(toaThuoc);
        }
    }
}
