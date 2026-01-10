using Domain.DTO;
using Domain.Repository;
using System.Collections.Generic;

namespace Services
{
    public class NgayNghiNhanVienService
    {
        private readonly INgayNghiNhanVienRepository _repo;

        public NgayNghiNhanVienService(INgayNghiNhanVienRepository repo)
        {
            _repo = repo;
        }

        public List<NgayNghiNhanVienDTO> DanhSach()
        {
            return _repo.DanhSach();
        }

        public NgayNghiNhanVienDTO LayNgayNghiByID(int ngayNghiID)
        {
            return _repo.LayNgayNghiByID(ngayNghiID);
        }

        public List<NgayNghiNhanVienDTO> LayNgayNghiByNhanVien(int nhanVienID)
        {
            return _repo.LayNgayNghiByNhanVien(nhanVienID);
        }

        public bool ThemNgayNghi(ThemNgayNghiNhanVienDTO nn)
        {
            return _repo.ThemNgayNghi(nn);
        }

        public bool CapNhatNgayNghi(NgayNghiNhanVienDTO nn)
        {
            return _repo.CapNhatNgayNghi(nn);
        }
    }
}
