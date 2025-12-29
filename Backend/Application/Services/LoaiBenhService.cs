using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
   public class LoaiBenhService
    {
        private readonly ILoaiBenhRepository _repo;
        public LoaiBenhService(ILoaiBenhRepository repo)
        {
            _repo = repo;
        }
        public List<LoaiBenhDTO> DanhSachLoaiBenh()
        {
            return _repo.DanhSachLoaiBenh();
        }
        public LoaiBenhDTO LayLoaiBenhByID(int loaiBenhID)
        {
            return _repo.LayLoaiBenhByID(loaiBenhID);
        }
        public List<LoaiBenhDTO> LayLoaiBenhByTen(string tenbenh)
        {
            return _repo.LayLoaiBenhByTen(tenbenh);
        }
        public bool ThemLoaiBenh(LoaiBenhDTO lb)
        {
            return _repo.ThemLoaiBenh(lb);
        }
        public bool CapNhatLoaiBenh(LoaiBenhDTO lb)
        {
            return _repo.CapNhatLoaiBenh(lb);
        }
    }
}
