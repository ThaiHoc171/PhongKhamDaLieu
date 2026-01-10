using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class ChucVu
	{
		public int ChucVuID { get; private set; }
		public string TenChucVu { get; private set; }
		public string MoTa { get; private set; }
		public DateTime NgayTao { get; private set; }
		public string TrangThai { get; private set; }

		public ChucVu(string tenChucVu, string moTa)
		{
			TenChucVu = tenChucVu ?? throw new ArgumentNullException(nameof(tenChucVu));
			MoTa = moTa;
			NgayTao = DateTime.Now;
			TrangThai = "Hoạt động";
		}

		public ChucVu(int chucVuID, string tenChucVu, string moTa, DateTime ngayTao, string trangThai)
		{
			ChucVuID = chucVuID;
			TenChucVu = tenChucVu ?? throw new ArgumentNullException(nameof(tenChucVu));
			MoTa = moTa;
			NgayTao = ngayTao;
			TrangThai = trangThai;
		}

		public void CapNhat(string tenChucVu, string moTa)
		{
			if (!string.IsNullOrWhiteSpace(tenChucVu))
				TenChucVu = tenChucVu;
			MoTa = moTa;
		}

		public void CapNhatTrangThai(string trangThai)
		{
			if (!string.IsNullOrWhiteSpace(trangThai))
				TrangThai = trangThai;
		}
	}
}
