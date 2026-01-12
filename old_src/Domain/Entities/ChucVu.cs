using System;

namespace Domain.Entities
{
	public class ChucVu
	{
		public int ChucVuID { get; private set; }
		public string TenChucVu { get; private set; }
		public string? MoTa { get; private set; }
		public DateTime NgayTao { get; private set; }
		public string TrangThai { get; private set; }

		// Tạo mới
		public ChucVu(string tenChucVu, string? moTa)
		{
			if (string.IsNullOrWhiteSpace(tenChucVu))
				throw new ArgumentException("Tên chức vụ không hợp lệ");

			TenChucVu = tenChucVu;
			MoTa = moTa;
			NgayTao = DateTime.UtcNow;
			TrangThai = "Hoạt động";
		}

		// Map từ DB
		public ChucVu(
			int chucVuID,
			string tenChucVu,
			string? moTa,
			DateTime ngayTao,
			string trangThai)
		{
			ChucVuID = chucVuID;
			TenChucVu = tenChucVu;
			MoTa = moTa;
			NgayTao = ngayTao;
			TrangThai = trangThai;
		}

		public void CapNhat(string tenChucVu, string? moTa)
		{
			if (string.IsNullOrWhiteSpace(tenChucVu))
				throw new ArgumentException("Tên chức vụ không hợp lệ");

			TenChucVu = tenChucVu;
			MoTa = moTa;
		}

		public void CapNhatTrangThai(string trangThai)
		{
			if (string.IsNullOrWhiteSpace(trangThai))
				throw new ArgumentException("Trạng thái không hợp lệ");

			TrangThai = trangThai;
		}
	}
}
