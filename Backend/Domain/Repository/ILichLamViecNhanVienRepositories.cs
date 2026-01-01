using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface ILichLamViecNhanVienRepository
    {
        List<LichLamViecNhanVienDTO> DanhSach();
        LichLamViecNhanVienDTO LayLichLamViecNhanVienByID(int LichLamViecID);
        List<LichLamViecNhanVienDTO> LayLichLamViecNhanVienByNhanVien(int NhanVienID);
        bool ThemLichLamViecNhanVien(ThemLichLamViecNhanVienDTO lich);
        bool CapNhatLichLamViecNhanVien(LichLamViecNhanVienDTO lich);
    }
}
