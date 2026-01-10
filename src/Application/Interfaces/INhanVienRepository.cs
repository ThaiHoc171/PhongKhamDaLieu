using Domain.Entities;
using System.Collections.Generic;

namespace Application.Interfaces
{
	public interface INhanVienRepository
	{
		void Add(NhanVien nhanVien);
		void Update(NhanVien nhanVien);
		void UpdateTrangThai(int nhanVienID, string trangThai);
		NhanVien GetById(int nhanVienID);
		List<NhanVien> GetAll();
		List<NhanVien> GetNhanViens(string keyword);
	}
}