using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Common;
using WPF.Windows.CaNhan;

namespace WPF.Windows
{
	public partial class appClinic : Window
	{
		public appClinic()
		{
			InitializeComponent();
			SnackbarHelper.Init(MainSnackbar!);
			txtName.Text = Session.HoTen.Name;
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
			_nav.Navigate("Dashboard");
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

		private async void HoSo_Click(object sender, RoutedEventArgs e)
		{
			if (Session.VaiTro == "Admin")
			{
				SnackbarHelper.ShowWarning("Bạn đang sử dụng tài khoản admin");
				return;
			}

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