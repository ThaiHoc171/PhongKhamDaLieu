using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class ThietBiService
    {
        private readonly IThietBiRepository _repo;
        public ThietBiService(IThietBiRepository repo)
        {
            _repo = repo;
        }
        public List<ThietBiDTO> DanhSachThietBi()
        {
            return _repo.DanhSachThietBi();
        }
        public ThietBiDTO LayThietBiByID(int ThietBiID)
        {
            return _repo.LayThietBiByID(ThietBiID);
        }
        public List<ThietBiDTO> LayThietBiByTen(string tenTB)
        {
            return _repo.LayThietBiByTen(tenTB);
        }
        public bool ThemThietBi(ThemThietBiDTO tb)
        {
            return _repo.ThemThietBi(tb);
        }
        public bool CapNhatThietBi(ThietBiDTO tb)
        {
            return _repo.CapNhatThietBi(tb);
        }
    }
}
