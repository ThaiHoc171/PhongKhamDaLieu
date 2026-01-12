using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class LichLamViecNhanVienService
    {
        private readonly ILichLamViecNhanVienRepository _repo;
        public LichLamViecNhanVienService(ILichLamViecNhanVienRepository repo)
        {
            _repo = repo;
        }
        public List<LichLamViecNhanVienDTO> DanhSach()
        {
            return _repo.DanhSach();
        }
        public LichLamViecNhanVienDTO LayLichLamViecNhanVienByID(int LichLamViecID)
        {
            return _repo.LayLichLamViecNhanVienByID(LichLamViecID);
        }
        public List<LichLamViecNhanVienDTO> LayLichLamViecNhanVienByNhanVien(int NhanVienID)
        {
            return _repo.LayLichLamViecNhanVienByNhanVien(NhanVienID);
        }
        public bool ThemLichLamViecNhanVien(ThemLichLamViecNhanVienDTO lich)
        {
            return _repo.ThemLichLamViecNhanVien(lich);
        }
        public bool CapNhatLichLamViecNhanVien(LichLamViecNhanVienDTO lich)
        {
            return _repo.CapNhatLichLamViecNhanVien(lich);
        }
    }
}
