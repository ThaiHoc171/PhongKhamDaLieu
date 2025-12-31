using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IPhongChucNang_ThietBiRepository
    {
        List<PhongChucNang_ThietBiDTO> DanhSach();
        PhongChucNang_ThietBiDTO LayPhongChucNang_ThietBiByID(int PCN_TB_ID);
        List<PhongChucNang_ThietBiDTO> LayPhongChucNang_ThietBiByPhong(int PhongChucNangID);
        bool ThemPhongChucNang_ThietBi(ThemPhongChucNang_ThietBiDTO pcn_tb);
        bool CapNhatPhongChucNang_ThietBi(PhongChucNang_ThietBiDTO pcn_tb);
    }
}
