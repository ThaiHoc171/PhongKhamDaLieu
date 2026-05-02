using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.Windows.ChucVu;

public partial class ChucVuAuthorize : Window
{
	public ChucVuAuthorize(int id)
	{
		InitializeComponent();
		_id = id;
		DataContext = this;
	}
	private readonly int _id;
	private readonly ChucVuClient _client = new ChucVuClient();
	private readonly ChucVuQuyenClient _quyen = new ChucVuQuyenClient();
	public ObservableCollection<ModuleGroup> Modules { get; set; } = new();
	private async void ChucVuAuthorize_Loaded(object sender, RoutedEventArgs e)
	{
		var cv = await _client.Detail(_id);
		txtTenChucVu.Text = cv.Data!.TenChucVu.ToString();
		var res = await _quyen.GetChecklist(_id);


		if (!res.Success)
		{
			SnackbarHelper.ShowError(res.Message);
			this.Close();
			return;
		}

		var list = res.Data!.Select(x => new QuyenItemVM
		{
			QuyenID = x.QuyenID,
			TenQuyen = x.TenQuyen,
			Module = x.Module,
			Checked = x.Checked
		});

		Modules = new ObservableCollection<ModuleGroup>(
			list.GroupBy(x => x.Module)
				.Select(g => new ModuleGroup
				{
					Module = g.Key,
					QuyenList = new ObservableCollection<QuyenItemVM>(g)
				})
		);

		DataContext = null;
		DataContext = this;
	}
	private void Header_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
	{
		if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
		{
			this.DragMove();
		}
	}
	private void ToggleUI(bool isEnabled)
	{
		btnSave.IsEnabled = isEnabled;
		btnClose.IsEnabled = isEnabled;
	}
	private async void btnSave_Click(object sender, RoutedEventArgs e)
	{
		var selectedIds = Modules
			.SelectMany(m => m.QuyenList)
			.Where(x => x.Checked)
			.Select(x => x.QuyenID)
			.ToList();

		var req= new ChucVuQuyenDTO
		{
			ChucVuID = _id,
			QuyenIDs = selectedIds
		};

		try
		{
			ToggleUI(false);

			var res = await _quyen.Update(req);

			if (res.Success)
			{
				this.DialogResult = true;
				this.Close();
			}
			else
			{
				SnackbarHelper.ShowError(res.Message);
			}
		}
		catch (Exception)
		{
			SnackbarHelper.ShowError("Có lỗi xảy ra, vui lòng thử lại!");
		}
		finally
		{
			ToggleUI(true);
		}
	}
	private void btnClose_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
