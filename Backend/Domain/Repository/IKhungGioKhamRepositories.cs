using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IKhungGioKhamRepository
    {
        List<KhungGioKhamDTO> DanhSach();
        KhungGioKhamDTO LayKhungGioKhamByID(int khungGioID);
        List<KhungGioKhamDTO> LayKhungGioKhamByTen(string tenKhung);
        bool ThemKhungGioKham(ThemKhungGioKhamDTO kg);
        bool CapNhatKhungGioKham(KhungGioKhamDTO kg);
    }
}
