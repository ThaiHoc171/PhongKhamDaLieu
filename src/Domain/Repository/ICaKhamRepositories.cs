using Domain.DTO;
using System;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface ICaKhamRepository
    {
        List<CaKhamDTO> DanhSach();
        CaKhamDTO GetByID(int id);
        List<CaKhamDTO> TimKiem(string danhmuc, string keyword);

        bool ThemCaKham(ThemCaKhamDTO ck);
        bool CapNhatCaKham(CaKhamDTO ck);
    }
}
