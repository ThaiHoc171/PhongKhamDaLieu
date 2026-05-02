using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using HoanMyClinic.Common;
using HoanMyClinic.ViewModels;

namespace HoanMyClinic.Pages;

public partial class ThongKePage : Page
{
	private ThongKeViewModel _vm = null!;

	public ThongKePage()
	{
		InitializeComponent();
		_vm = new ThongKeViewModel();
		DataContext = _vm;
		Loaded += async (_, __) =>
		{
			SetupAllGrids();
			await _vm.Init();
		};

		_vm.TabChanged += SetupAllGrids;
	}

	private void SetupAllGrids()
	{
		SetupBenhNhan();
		SetupCaKham();
		SetupPhienKham();
		SetupToaThuoc();
		SetupNhanVien();
	}

	// ── Tab: Bệnh nhân ──────────────────────────────────────────────────────

	private void SetupBenhNhan()
	{
		SetupDataGrid.ApplyStyle(GridBenhNhanTheoNgay);
		GridBenhNhanTheoNgay.Columns.Clear();
		GridBenhNhanTheoNgay.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày",
			Binding = new Binding("Ngay") { StringFormat = "dd/MM/yyyy" },
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridBenhNhanTheoNgay.Columns.Add(new DataGridTextColumn
		{
			Header = "BN mới",
			Binding = new Binding("SoBenhNhanMoi"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridGioiTinh);
		GridGioiTinh.Columns.Clear();
		GridGioiTinh.Columns.Add(new DataGridTextColumn
		{
			Header = "Giới tính",
			Binding = new Binding("GioiTinh"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridGioiTinh.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("SoLuong"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridDoTuoi);
		GridDoTuoi.Columns.Clear();
		GridDoTuoi.Columns.Add(new DataGridTextColumn
		{
			Header = "Nhóm tuổi",
			Binding = new Binding("NhomTuoi"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridDoTuoi.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("SoLuong"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
	}

	// ── Tab: Ca khám ────────────────────────────────────────────────────────

	private void SetupCaKham()
	{
		SetupDataGrid.ApplyStyle(GridCaKhamTheoKhoang);
		GridCaKhamTheoKhoang.Columns.Clear();
		GridCaKhamTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Nhãn",
			Binding = new Binding("NhanX"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridCaKhamTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Từ ngày",
			Binding = new Binding("TuNgay") { StringFormat = "dd/MM/yyyy" },
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridCaKhamTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Số khám",
			Binding = new Binding("SoKham"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridCaKhamTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Điều trị",
			Binding = new Binding("SoDieuTri"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
	}

	// ── Tab: Phiên khám ─────────────────────────────────────────────────────

	private void SetupPhienKham()
	{
		SetupDataGrid.ApplyStyle(GridPhienKhamTheoNgay);
		GridPhienKhamTheoNgay.Columns.Clear();
		GridPhienKhamTheoNgay.Columns.Add(new DataGridTextColumn
		{
			Header = "Ngày",
			Binding = new Binding("Ngay") { StringFormat = "dd/MM/yyyy" },
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridPhienKhamTheoNgay.Columns.Add(new DataGridTextColumn
		{
			Header = "Hoàn thành",
			Binding = new Binding("SoHoanThanh"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridPhienKhamTheoNgay.Columns.Add(new DataGridTextColumn
		{
			Header = "Đang khám",
			Binding = new Binding("SoDangKham"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridPhienKhamTheoNgay.Columns.Add(new DataGridTextColumn
		{
			Header = "Đang chờ",
			Binding = new Binding("SoDangCho"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridPhienKhamTheoNgay.Columns.Add(new DataGridTextColumn
		{
			Header = "Đã hủy",
			Binding = new Binding("SoDaHuy"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridPhienKhamTheoPhong);
		GridPhienKhamTheoPhong.Columns.Clear();
		GridPhienKhamTheoPhong.Columns.Add(new DataGridTextColumn
		{
			Header = "Phòng",
			Binding = new Binding("TenPhong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridPhienKhamTheoPhong.Columns.Add(new DataGridTextColumn
		{
			Header = "Số phiên",
			Binding = new Binding("SoPhienKham"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridPhienKhamTheoLoaiBenh);
		GridPhienKhamTheoLoaiBenh.Columns.Clear();
		GridPhienKhamTheoLoaiBenh.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên bệnh",
			Binding = new Binding("TenBenh"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridPhienKhamTheoLoaiBenh.Columns.Add(new DataGridTextColumn
		{
			Header = "Nhóm",
			Binding = new Binding("NhomBenh"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridPhienKhamTheoLoaiBenh.Columns.Add(new DataGridTextColumn
		{
			Header = "Số ca",
			Binding = new Binding("SoLuong"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
	}

	// ── Tab: Toa thuốc ──────────────────────────────────────────────────────

	private void SetupToaThuoc()
	{
		SetupDataGrid.ApplyStyle(GridToaThuocTheoKhoang);
		GridToaThuocTheoKhoang.Columns.Clear();
		GridToaThuocTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Nhãn",
			Binding = new Binding("NhanX"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridToaThuocTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Từ ngày",
			Binding = new Binding("TuNgay") { StringFormat = "dd/MM/yyyy" },
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridToaThuocTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Số toa",
			Binding = new Binding("SoToaThuoc"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridToaThuocTheoKhoang.Columns.Add(new DataGridTextColumn
		{
			Header = "Lượt thuốc",
			Binding = new Binding("SoLuotThuoc"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridTopThuoc);
		GridTopThuoc.Columns.Clear();
		GridTopThuoc.Columns.Add(new DataGridTextColumn
		{
			Header = "Tên thuốc",
			Binding = new Binding("TenThuoc"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridTopThuoc.Columns.Add(new DataGridTextColumn
		{
			Header = "Hoạt chất",
			Binding = new Binding("HoatChat"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridTopThuoc.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lần",
			Binding = new Binding("TongSoLan"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridTopThuoc.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("TongSoLuong"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridTopBacSiKeDon);
		GridTopBacSiKeDon.Columns.Clear();
		GridTopBacSiKeDon.Columns.Add(new DataGridTextColumn
		{
			Header = "Bác sĩ",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridTopBacSiKeDon.Columns.Add(new DataGridTextColumn
		{
			Header = "Số toa",
			Binding = new Binding("SoToaThuoc"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
	}

	// ── Tab: Nhân viên ──────────────────────────────────────────────────────

	private void SetupNhanVien()
	{
		SetupDataGrid.ApplyStyle(GridNhanVienTheoChucVu);
		GridNhanVienTheoChucVu.Columns.Clear();
		GridNhanVienTheoChucVu.Columns.Add(new DataGridTextColumn
		{
			Header = "Chức vụ",
			Binding = new Binding("TenChucVu"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridNhanVienTheoChucVu.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("SoLuong"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridNhanVienTheoPhong);
		GridNhanVienTheoPhong.Columns.Clear();
		GridNhanVienTheoPhong.Columns.Add(new DataGridTextColumn
		{
			Header = "Phòng",
			Binding = new Binding("TenPhong"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridNhanVienTheoPhong.Columns.Add(new DataGridTextColumn
		{
			Header = "Số lượng",
			Binding = new Binding("SoLuong"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridHieuSuatBacSi);
		GridHieuSuatBacSi.Columns.Clear();
		GridHieuSuatBacSi.Columns.Add(new DataGridTextColumn
		{
			Header = "Họ tên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridHieuSuatBacSi.Columns.Add(new DataGridTextColumn
		{
			Header = "Chức vụ",
			Binding = new Binding("TenChucVu"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridHieuSuatBacSi.Columns.Add(new DataGridTextColumn
		{
			Header = "Phiên khám",
			Binding = new Binding("SoPhienKham"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridHieuSuatBacSi.Columns.Add(new DataGridTextColumn
		{
			Header = "Hoàn thành",
			Binding = new Binding("SoHoanThanh"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridHieuSuatBacSi.Columns.Add(new DataGridTextColumn
		{
			Header = "Toa thuốc",
			Binding = new Binding("SoToaThuoc"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
		GridHieuSuatBacSi.Columns.Add(new DataGridTextColumn
		{
			Header = "Tỉ lệ HT",
			Binding = new Binding("TiLeHoanThanh") { StringFormat = "{0}%" },
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});

		SetupDataGrid.ApplyStyle(GridNgayNghiNhanVien);
		GridNgayNghiNhanVien.Columns.Clear();
		GridNgayNghiNhanVien.Columns.Add(new DataGridTextColumn
		{
			Header = "Nhân viên",
			Binding = new Binding("HoTen"),
			Width = new DataGridLength(2, DataGridLengthUnitType.Star)
		});
		GridNgayNghiNhanVien.Columns.Add(new DataGridTextColumn
		{
			Header = "Số ngày nghỉ",
			Binding = new Binding("SoNgayNghi"),
			Width = new DataGridLength(1, DataGridLengthUnitType.Star)
		});
	}
}