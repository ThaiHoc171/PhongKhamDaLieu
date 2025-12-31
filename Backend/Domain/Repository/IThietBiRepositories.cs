using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IThietBiRepository
    {
        List<ThietBiDTO> DanhSachThietBi();
        ThietBiDTO LayThietBiByID(int thietBiID);
        List<ThietBiDTO> LayThietBiByTen(string tenTB);
        bool ThemThietBi(ThemThietBiDTO tb);
        bool CapNhatThietBi(ThietBiDTO tb);
    }
}
