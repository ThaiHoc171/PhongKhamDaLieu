using Domain.DTO;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface INgayNghiNhanVienRepository
    {
        List<NgayNghiNhanVienDTO> DanhSach();
        NgayNghiNhanVienDTO LayNgayNghiByID(int ngayNghiID);
        List<NgayNghiNhanVienDTO> LayNgayNghiByNhanVien(int nhanVienID);
        bool ThemNgayNghi(ThemNgayNghiNhanVienDTO nn);
        bool CapNhatNgayNghi(NgayNghiNhanVienDTO nn);
    }
}
