using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class PhongChucNang_ThietBiService
    {
        private readonly IPhongChucNang_ThietBiRepository _repo;
        public PhongChucNang_ThietBiService(IPhongChucNang_ThietBiRepository repo)
        {
            _repo = repo;
        }
        public List<PhongChucNang_ThietBiDTO> DanhSach()
        {
            return _repo.DanhSach();
        }
        public PhongChucNang_ThietBiDTO LayPhongChucNang_ThietBiByID(int PCN_TB_ID)
        {
            return _repo.LayPhongChucNang_ThietBiByID(PCN_TB_ID);
        }
        public List<PhongChucNang_ThietBiDTO> LayPhongChucNang_ThietBiByPhong(int PhongChucNangID)
        {
            return _repo.LayPhongChucNang_ThietBiByPhong(PhongChucNangID);
        }
        public bool ThemPhongChucNang_ThietBi(ThemPhongChucNang_ThietBiDTO pcn_tb)
        {
            return _repo.ThemPhongChucNang_ThietBi(pcn_tb);
        }
        public bool CapNhatPhongChucNang_ThietBi(PhongChucNang_ThietBiDTO pcn_tb)
        {
            return _repo.CapNhatPhongChucNang_ThietBi(pcn_tb);
        }
    }
}
