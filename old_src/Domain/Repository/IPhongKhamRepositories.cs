using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IPhongKhamRepository
    {
        List<PhongKhamDTO> DanhSachPhongKham();
        bool CapNhatPhongKham(PhongKhamDTO phongKham);
    }
}
