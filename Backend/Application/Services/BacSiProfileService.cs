using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class BacSiProfileService
    {
        private readonly IBacSiProfileRepository _repo;

        public BacSiProfileService(IBacSiProfileRepository repo)
        {
            _repo = repo;
        }

        public List<BacSiProfileDTO> DanhSachBacSiProfile()
        {
            return _repo.DanhSachBacSiProfile();
        }

        public BacSiProfileDTO LayBacSiProfileByID(int id)
        {
            return _repo.LayBacSiProfileByID(id);
        }

        public BacSiProfileDTO LayBacSiProfileByNhanVienID(int nhanVienID)
        {
            return _repo.LayBacSiProfileByNhanVienID(nhanVienID);
        }

        public bool ThemBacSiProfile(ThemBacSiProfileDTO bacSi)
        {
            return _repo.ThemBacSiProfile(bacSi);
        }

        public bool CapNhatBacSiProfile(BacSiProfileDTO bacSi)
        {
            return _repo.CapNhatBacSiProfile(bacSi);
        }
    }
}
