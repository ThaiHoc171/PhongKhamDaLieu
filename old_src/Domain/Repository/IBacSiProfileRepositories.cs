using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IBacSiProfileRepository
    {
        List<BacSiProfileDTO> DanhSachBacSiProfile();
        BacSiProfileDTO LayBacSiProfileByID(int bacSiProfileID);
        BacSiProfileDTO LayBacSiProfileByNhanVienID(int nhanVienID);
        bool ThemBacSiProfile(ThemBacSiProfileDTO bacSi);
        bool CapNhatBacSiProfile(BacSiProfileDTO bacSi);
    }
}
