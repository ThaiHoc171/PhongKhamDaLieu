using Domain.DTO;
using Domain.Entities;
using System.Collections.Generic;


namespace Domain.Repository
{
	public interface ICanLamSangRepositories
	{
		List<CanLamSangListDTO> DanhSachCanLamSang();
		CanLamSangDetailDTO LayChiTietCanLamSang(int CanLamSangID);
		bool ThemCanLamSang(ThemCanLamSangDTO cls);
		bool CapNhatCanLamSang(CapNhatCanLamSangDTO cls);
		bool ChuyenTrangThai(Status stt);
	}
}
