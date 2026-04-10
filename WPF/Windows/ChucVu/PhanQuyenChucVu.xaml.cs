using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using WPF.Client;
using WPF.Common;
using WPF.Models;

namespace WPF.Windows.ChucVu;

public partial class PhanQuyenChucVu : Window
{
	public PhanQuyenChucVu(int id)
	{
		InitializeComponent();
		_id = id;
		DataContext = this;
	}
	private readonly int _id;
	private readonly ChucVuClient _client = new ChucVuClient();
	private readonly ChucVuQuyenClient _quyen = new ChucVuQuyenClient();
	public ObservableCollection<ModuleGroup> Modules { get; set; } = new();
	private async void PhanQuyenChucVu_Loaded(object sender, RoutedEventArgs e)
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
	private async void btnLuu_Click(object sender, RoutedEventArgs e)
	{
		var selectedIds = Modules
			.SelectMany(m => m.QuyenList)
			.Where(x => x.Checked)
			.Select(x => x.QuyenID)
			.ToList();

		var dto = new ChucVuQuyenDTO
		{
			ChucVuID = _id,
			QuyenIDs = selectedIds
		};

		var res = await _quyen.Update(dto);

		if (res.Success)
		{
			SnackbarHelper.ShowSuccess("Lưu thành công");
			this.Close();
		}
		else
		{
			SnackbarHelper.ShowError(res.Message);
		}
	}
	private void btnHuy_Click(object sender, RoutedEventArgs e)
	{
		this.Close();
	}
}
