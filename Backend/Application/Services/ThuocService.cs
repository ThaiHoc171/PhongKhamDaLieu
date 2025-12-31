using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class ThuocService
    {
        private readonly IThuocRepository _repo;
        public ThuocService(IThuocRepository repo)
        {
            _repo = repo;
        }
        public List<ThuocDTO> DanhSach()
        {
            return _repo.DanhSach();
        }
        public List<ThuocDTO> DanhSachThuocTheoKeywords(string kw)
        {
            return _repo.DanhSachThuocTheoKeywords(kw);
        }
        public ThuocDTO LayThuocTheoID(int thuocid)
        {
            return _repo.LayThuocTheoID(thuocid);
        }
        public bool ThemThuoc(ThuocCreateDTO thuoc)
        {
            return _repo.ThemThuoc(thuoc);
        }
        public bool CapNhatThuoc(ThuocDTO thuoc)
        {
            return _repo.CapNhatThuoc(thuoc);
        }
    }
}
