using System.Windows.Controls;
using WPF.Pages;
using WPF.Pages.CaKham;
using WPF.Pages.LichLamViec;
using WPF.Pages.PhienKham;
using WPF.Windows;

namespace WPF.Common;

public static class NavigationRoutes
{
	public static readonly Dictionary<string, Func<Page>> Routes = new()
	{
		{ "Dashboard", () => new Dashboard() },
		{ "ChucVu", () => new ChucVuPage() },
		{ "CanLamSang", () => new CanLamSangPage() },
		{ "ThietBi", () => new ThietBiPage() },
		{ "BenhNhan", () => new BenhNhanPage() },
		{ "NhanVien", () => new NhanVienPage() },
		{ "Khach", () => new KhachPage() },
		{ "TaiKhoan", () => new TaiKhoanPage() },
		{ "Phong", () => new PhongChucNangPage() },

		{ "CaKhamTrong", () => new Blank() },
		{ "CaKhamCho", () => new WaitPage() },
		{ "CaKhamDaXacNhan", () => new Accepted() },
		{ "LichSuCaKham", () => new History() },

		{ "XemLichCaNhan", () => new Personal() },
		{ "XemLichChung", () => new Shared() },
		{ "NhapLichLam", () => new ImportLich() },

		{ "Thuoc", () => new ThuocPage() },
		{ "LoaiBenh", () => new LoaiBenhPage() },

		{ "PhienKham", () => new SharedPage() },
		{ "PhienKhamCaNhan", () => new PersonalPage() },
		{ "PhienKhamCLS", () => new PhienKhamCLSPage() },

		{ "TaiKham", () => new TaiKhamPage() },
		{ "LieuTrinh", () => new LieuTrinhPage() }
	};
	public static readonly Dictionary<string, string> Permissions = new()
	{
		{ "Dashboard", "" },
		{ "ChucVu", "NHANSU_READ" },
		{ "CanLamSang", "CSVC_READ" },
		{ "ThietBi", "CSVC_READ" },
		{ "BenhNhan", "BENHNHAN_READ" },
		{ "NhanVien", "NHANSU_READ" },
		{ "Khach", "USER_READ" },
		{ "TaiKhoan", "USER_READ" },
		{ "Phong", "CSVC_READ" },

		{ "CaKhamTrong", "LICH_READ" },
		{ "CaKhamCho", "LICH_READ" },
		{ "CaKhamDaXacNhan", "LICH_READ" },
		{ "LichSuCaKham", "LICH_READ" },

		{ "XemLichCaNhan", "LICH_READ" },
		{ "XemLichChung", "LICH_READ" },
		{ "NhapLichLam", "LICH_WRITE" },

		{ "Thuoc", "HETHONG_READ" },
		{ "LoaiBenh", "HETHONG_READ" },

		{ "PhienKham", "KHAMBENH_READ" },
		{ "PhienKhamCaNhan", "KHAMBENH_READ" },
		{ "PhienKhamCLS", "KHAMBENH_READ" },

		{ "TaiKham", "KHAMBENH_READ" },
		{ "LieuTrinh", "KHAMBENH_READ" }
	};

	public static readonly Dictionary<string, string> Titles = new()
	{
		{ "Dashboard", "Dashboard" },
		{ "ChucVu", "Quản lý chức vụ" },
		{ "CanLamSang", "Quản lý cận lâm sàng" },
		{ "ThietBi", "Quản lý thiết bị" },
		{ "BenhNhan", "Quản lý bệnh nhân" },
		{ "NhanVien", "Quản lý nhân viên" },
		{ "Khach", "Quản lý khách" },
		{ "TaiKhoan", "Quản lý tài khoản" },
		{ "Phong", "Quản lý phòng chức năng" },

		{ "CaKhamTrong", "Ca khám còn trống" },
		{ "CaKhamCho", "Ca khám chờ xác nhận" },
		{ "CaKhamDaXacNhan", "Ca khám đang chờ khám" },
		{ "LichSuCaKham", "Lịch sử ca khám" },

		{ "XemLichCaNhan", "Lịch cá nhân" },
		{ "XemLichChung", "Lịch phòng khám" },
		{ "NhapLichLam", "Nhập lịch làm việc" },

		{ "Thuoc", "Quản lý thuốc" },
		{ "LoaiBenh", "Quản lý loại bệnh" },

		{ "PhienKham", "Phiên khám" },
		{ "PhienKhamCaNhan", "Phiên khám cá nhân" },
		{ "PhienKhamCLS", "Phiên khám CLS" },

		{ "TaiKham", "Quản lý tái khám" },
		{ "LieuTrinh", "Quản lý liệu trình điều trị" }
	};
}