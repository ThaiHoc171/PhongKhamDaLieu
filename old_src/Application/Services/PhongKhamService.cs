using System;
using System.Collections.Generic;
using Domain.DTO;
using Domain.Repository;

namespace Services
{
    public class PhongKhamService
    {
        private readonly IPhongKhamRepository _repo;

        public PhongKhamService(IPhongKhamRepository repo)
        {
            _repo = repo;
        }

        public List<PhongKhamDTO> DanhSachPhongKham()
        {
            return _repo.DanhSachPhongKham();
        }

        public bool CapNhatPhongKham(PhongKhamDTO phongKham)
        {
            return _repo.CapNhatPhongKham(phongKham);
        }
    }
}
