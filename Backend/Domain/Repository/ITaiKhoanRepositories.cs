using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
	public interface ITaiKhoanRepository
	{
		TaiKhoanDTO LayTaiKhoanTheoEmail(string email);
		TaiKhoanDTO LayTaiKhoanTheoID(int taiKhoanID);
		List<TaiKhoanDTO> LayDanhSachTaiKhoan();
		bool TaoTaiKhoan(TaiKhoanCreateDTO taiKhoan);
		bool CapNhatMatKhau(int taiKhoanID, string newPasswordHash);
		bool VoHieuHoaTaiKhoan(int taiKhoanID);
	}
}
