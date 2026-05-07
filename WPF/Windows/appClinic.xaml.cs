using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using HoanMyClinic.Common;
using HoanMyClinic.Windows.CaNhan;

namespace HoanMyClinic.Windows
{
	public partial class appClinic : Window
	{
		public appClinic()
		{
			InitializeComponent();
			SnackbarHelper.Init(MainSnackbar!);
			txtName.Text = Session.HoTen.Name;
			txtUserName.Text = Session.HoTen.Name;
		}
		public readonly NavigationHelper _nav = new NavigationHelper();
		public void OpenPage(Page page, string title)
		{
			txtHeader.Text = title;
			MainFrame.Navigate(page);
		}

		private void Expander_Expanded(object sender, RoutedEventArgs e)
		{
			var current = sender as Expander;

			foreach (var exp in new[]
			{
				expKham,
				expDieuTri,
				expBenhNhan,
				expCaKham,
				expLichLamViec,
				expNhanSu,
				expCSVC,
				expDanhMuc,
				expThongKe,
				expPublic
			})
			{
				if (exp != current)
					exp.IsExpanded = false;
			}
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// Phân quyền PhienKham
			if (Session.VaiTro == "Admin")
			{
				btnPhienKham.Visibility = Visibility.Visible;
				btnPhienKhamCaNhan.Visibility = Visibility.Collapsed;
			}
			else
			{
				btnPhienKham.Visibility = Visibility.Collapsed;
				btnPhienKhamCaNhan.Visibility = Visibility.Visible;
			}

			ApplyAuthorization();
			_nav.Navigate("Dashboard");
		}

		private void ApplyAuthorization()
		{
			bool isAdmin = Session.VaiTro == "Admin";
			bool isBacSiKham = Session.ChucVu == "Bác sĩ khám bệnh";
			bool isBacSiDieuTri = Session.ChucVu == "Bác sĩ điều trị";
			bool isYTa = Session.ChucVu == "Y tá";
			bool isKyThuatVien = Session.ChucVu == "Kỹ thuật viên";
			bool isLeTan = Session.ChucVu == "Lễ tân";

			// ── Nhân sự (chỉ Admin) ──────────────────────────────────────
			expNhanSu.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

			// ── Tài khoản (chỉ Admin) ────────────────────────────────────
			btnTaiKhoan.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

			// ── Danh mục (Admin + Bác sĩ) ───────────────────────────────
			bool canDanhMuc = isAdmin || isBacSiKham || isBacSiDieuTri;
			expDanhMuc.Visibility = canDanhMuc ? Visibility.Visible : Visibility.Collapsed;

			// ── Điều trị / Liệu trình (Admin + Bác sĩ điều trị) ─────────
			bool canDieuTri = isAdmin || isBacSiDieuTri;
			expDieuTri.Visibility = canDieuTri ? Visibility.Visible : Visibility.Collapsed;
			btnDieuTri.Visibility = canDieuTri ? Visibility.Visible : Visibility.Collapsed;

			// ── CLS (Admin + Kỹ thuật viên + Bác sĩ) ────────────────────
			bool canCls = isAdmin || isKyThuatVien || isBacSiKham || isBacSiDieuTri;
			btnCls.Visibility = canCls ? Visibility.Visible : Visibility.Collapsed;
			btnPkCls.Visibility = canCls ? Visibility.Visible : Visibility.Collapsed;

			// ── Lịch làm việc ────────────────────────────────────────────
			// Nhập lịch: chỉ Admin
			btnNhapLichLam.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
			// Xem lịch chung: Admin + mọi nhân viên
			// (giữ Visible mặc định — ai cũng xem được)

			// ── CSVC / Thiết bị (Admin + Kỹ thuật viên) ─────────────────
			bool canCsvc = isAdmin || isKyThuatVien;
			expCSVC.Visibility = canCsvc ? Visibility.Visible : Visibility.Collapsed;

			// ── Thống kê (chỉ Admin) ─────────────────────────────────────
			expThongKe.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;

			// ── Bệnh nhân / Khách (Lễ tân + Admin + Bác sĩ) ─────────────
			bool canBenhNhan = isAdmin || isLeTan || isBacSiKham;
			expBenhNhan.Visibility = canBenhNhan ? Visibility.Visible : Visibility.Collapsed;

			// ── Tái khám (Bác sĩ khám + Admin) ──────────────────────────
			bool canTaiKham = isAdmin || isBacSiKham;
			btnTaiKham.Visibility = canTaiKham ? Visibility.Visible : Visibility.Collapsed;

			// ── Ngày nghỉ (Admin) ─────────────────────────────────────────
			btnNgayNghi.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
		}
		private void Header_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				if (this.WindowState == WindowState.Maximized)
				{
					var mousePos = e.GetPosition(this);

					double percentX = mousePos.X / this.ActualWidth;

					this.WindowState = WindowState.Normal;
					iconMaximize.Kind = PackIconKind.WindowMaximize;
					this.Left = e.GetPosition(null).X - (this.Width * percentX);
					this.Top = e.GetPosition(null).Y - 10;
				}

				DragMove();
			}
		}
		private void BtnMinimize_Click(object sender, RoutedEventArgs e)
		{
			this.WindowState = WindowState.Minimized;
		}

		private void BtnMaximize_Click(object sender, RoutedEventArgs e)
		{
			if (this.WindowState == WindowState.Maximized)
			{
				this.WindowState = WindowState.Normal;
				iconMaximize.Kind = PackIconKind.WindowMaximize;
			}
			else
			{
				this.WindowState = WindowState.Maximized;
				iconMaximize.Kind = PackIconKind.WindowRestore;
			}
		}

		private void BtnClose_Click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void btnAvatar_Click(object sender, RoutedEventArgs e)
		{
			AvatarPopup.IsOpen = !AvatarPopup.IsOpen;
		}
		private void MainFrame_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
		{

		}
		private void btnDashboard_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("Dashboard");
		}
		private void BtnChucVu_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("ChucVu");
		}

		private void btnCls_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("CanLamSang");
		}

		private void btnThietBi_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("ThietBi");
		}

		private void CaKhamTrong_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("CaKhamTrong");
		}

		private void btnXemLichCaNhan_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("XemLichCaNhan");
		}

		private void btnXemLichChung_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("XemLichChung");
		}

		private void btnNhapLichLam_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("NhapLichLam");
		}

		private void btnBenhNhan_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("BenhNhan");
		}

		private void btnKhach_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("Khach");
		}

		private void btnTaiKhoan_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("TaiKhoan");
		}

		private void btnPhong_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("Phong");
		}

		private void btnDangXuat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btnNhanVien_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("NhanVien");
		}

		private void CaKhamCho_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("CaKhamCho");
		}

		private void CaKhamDaXacNhan_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("CaKhamDaXacNhan");
        }
		private void LichSuCaKham_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("LichSuCaKham");
		}

		private void btnThuoc_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("Thuoc");
		}

		private void btnLoaiBenh_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("LoaiBenh");
		}

		private void btnPhienKham_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("PhienKham");
		}

		private void btnPhienKhamCaNhan_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("PhienKhamCaNhan");
		}

		private void btnPkCls_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("PhienKhamCLS");
		}

		private void btnTaiKham_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("TaiKham");
        }

		private void btnDieuTri_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("LieuTrinh");
		}
		private void btnBacSi_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("BacSi");
        }

		private void btnBaiViet_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("BaiViet");
		}

		private void btnThongKe_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("ThongKe");
		}
		private void btnNgayNghi_Click(object sender, RoutedEventArgs e)
		{
			_nav.Navigate("NgayNghi");
		}
		private async void HoSo_Click(object sender, RoutedEventArgs e)
		{
			//if (Session.VaiTro == "Admin")
			//{
			//	await MessageHelper.ShowMessage("Bạn đang sử dụng tài khoản admin");
			//	return;
			//}

			var overlay = OverlayHelper.GetOverlay(this);
			OverlayHelper.Show(overlay);

			try
			{
				await DialogHelper.OpenDialogAsync(
					new HoSoCaNhan { Owner = this },
					() => Task.CompletedTask
				);
			}
			finally
			{
				OverlayHelper.Hide(overlay);
			}
		}

		private async void Password_Click(object sender, RoutedEventArgs e)
		{
			var overlay = OverlayHelper.GetOverlay(this);
			OverlayHelper.Show(overlay);

			try
			{
				await DialogHelper.OpenDialogAsync(
					new DoiMatKhau { Owner = this },
					() => Task.CompletedTask
				);
			}
			finally
			{
				OverlayHelper.Hide(overlay);
			}
		}


    }
}