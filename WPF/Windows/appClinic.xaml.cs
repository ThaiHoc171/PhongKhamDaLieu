using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF.Common;
using WPF.Pages;
using WPF.Pages.CaKham;
using WPF.Pages.LichLamViec;
using WPF.Pages.PhienKham;

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
				expBenhNhan,
				expCaKham,
				expLichLamViec,
				expNhanSu,
				expDanhMuc,
				expCSVC,
				expKham
			})
			{
				if (exp != current)
					exp.IsExpanded = false;
			}
		}
		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			OpenPage(new Dashboard(), "Dashboard");
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

		private void BtnChucVu_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new ChucVuPage(), "Quản lý chức vụ");
		}

		private void btnCls_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new CanLamSangPage(), "Quản lý cận lâm sàng");
        }

		private void btnThietBi_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new ThietBiPage(), "Quản lý danh mục thiết bị");
		}

		private void CaKhamTrong_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new CaKhamTrongPage(), "Danh sách ca khám còn trống");
		}

		private void btnXemLichCaNhan_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new XemLichCaNhan(), "Lịch làm việc cá nhân");
		}

		private void btnXemLichChung_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new XemLichChung(), "Lịch làm việc phòng khám");
		}

		private void btnNhapLichLam_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new NhapLichLamViec(), "Nhập làm việc phòng khám từ Excel");
		}

		private void btnBenhNhan_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new BenhNhanPage(), "Quản lý bệnh nhân");
		}

		private void btnKhach_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new KhachPage(), "Quản lý khách");
		}

		private void btnTaiKhoan_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new TaiKhoanPage(), "Quản lý tài khoản");
		}

		private void btnPhong_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new PhongChucNangPage(), "Quản lý phòng chức năng");
		}

		private void btnDangXuat_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void btnNhanVien_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new NhanVienPage(), "Quản lý nhân viên");
		}

		private void CaKhamCho_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new CaKhamChoPage(), "Danh sách ca khám chờ xác nhận");
		}

		private void CaKhamDaXacNhan_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new CaKhamDaXacNhan(), "Danh sách ca khám đang chờ khám");
        }
		private void LichSuCaKham_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new LichSuCaKhamPage(), "Lịch sử ca khám");
		}

		private void btnThuoc_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new ThuocPage(), "Quản lý thuốc");
		}

		private void btnLoaiBenh_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new LoaiBenhPage(), "Quản lý loại bệnh");
		}	

		private void btnPhienKham_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new PhienKhamPage(), "Quản lý phiên khám");
		}

		private void btnPhienKhamCaNhan_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new PhienKhamCaNhanPage(), "Quản lý phiên khám cá nhân");
		}

		private void btnPkCls_Click(object sender, RoutedEventArgs e)
		{
			OpenPage(new PhienKhamCLSPage(), "Quản lý phiên khám cận lâm sàng");
		}

		private void btnTaiKham_Click(object sender, RoutedEventArgs e)
		{
			SnackbarHelper.ShowWarning("Chức năng đang phát triển");
        }

    }
}