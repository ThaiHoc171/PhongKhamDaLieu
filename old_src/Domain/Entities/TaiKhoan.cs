using Domain.Enums;

namespace Domain.Entities
{
	public class TaiKhoan
	{
		public int Id { get; private set; }
		public string Email { get; private set; }
		public string MatKhau { get; private set; }
		public string VaiTro { get; private set; }
		public string TrangThai { get; private set; }
		public DateTime NgayTao { get; private set; }

		// Constructor dùng khi tạo mới (Đăng ký)
		public TaiKhoan(string email, string matKhau, VaiTroEnum vaiTro)
		{
			if (string.IsNullOrWhiteSpace(email))
				throw new ArgumentException("Email không hợp lệ");

			if (string.IsNullOrWhiteSpace(matKhau))
				throw new ArgumentException("Mật khẩu không hợp lệ");

			Email = email;
			MatKhau = matKhau;
			VaiTro = vaiTro.ToDbValue();
			TrangThai = "Hoạt động";
			NgayTao = DateTime.UtcNow;
		}

		// Constructor dùng khi map từ DB
		public TaiKhoan(
			int id,
			string email,
			string matKhau,
			string vaiTro,
			string trangThai,
			DateTime ngayTao)
		{
			Id = id;
			Email = email;
			MatKhau = matKhau;
			VaiTro = vaiTro;
			TrangThai = trangThai;
			NgayTao = ngayTao;
		}

		// Nghiệp vụ đổi mật khẩu
		public void DoiMatKhau(string matKhauMoi)
		{
			if (string.IsNullOrWhiteSpace(matKhauMoi))
				throw new ArgumentException("Mật khẩu mới không hợp lệ");

			MatKhau = matKhauMoi;
		}

		// Nghiệp vụ cập nhật trạng thái
		public void CapNhatTrangThai(string trangThaiMoi)
		{
			if (string.IsNullOrWhiteSpace(trangThaiMoi))
				throw new ArgumentException("Trạng thái không hợp lệ");

			TrangThai = trangThaiMoi;
		}
		public VaiTroEnum LayVaiTro()
			=> VaiTroExtensions.ToEnum(VaiTro);	
	}
}
