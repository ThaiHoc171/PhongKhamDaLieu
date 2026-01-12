using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class CaKhamService
    {
        private readonly ICaKhamRepository _repo;
        public CaKhamService(ICaKhamRepository repo)
        {
            _repo = repo;
        }
        public List<CaKhamDTO> DanhSach()
        {
            return _repo.DanhSach();
        }
        public CaKhamDTO GetByID(int id)
        { 
            return _repo.GetByID(id); 
        }
        public List<CaKhamDTO> TimKiem(string danhmuc, string keyword)
        {
            return _repo.TimKiem(danhmuc, keyword);
        }
        public bool ThemCaKham(ThemCaKhamDTO ck)
        {
            return _repo.ThemCaKham(ck);
        }
        public bool CapNhatCaKham(CaKhamDTO ck)
        {
            return _repo.CapNhatCaKham(ck);
        }
    }
}
