using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF.Models;
public class ChucVuQuyenDTO
{
	public int ChucVuID { get; set; }
	public List<int> QuyenIDs { get; set; } = new();
}
public class QuyenChecklistDTO
{
	public int QuyenID { get; set; }
	public string TenQuyen { get; set; } = "";
	public string Module { get; set; } = "";
	public bool Checked { get; set; }
}

public class QuyenItemVM : INotifyPropertyChanged
{
	public int QuyenID { get; set; }
	public string TenQuyen { get; set; } = "";
	public string Module { get; set; } = "";

	private bool _checked;
	public bool Checked
	{
		get => _checked;
		set
		{
			_checked = value;
			OnPropertyChanged();
		}
	}

	public event PropertyChangedEventHandler? PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string name = "")
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}

public class ModuleGroup
{
	public string Module { get; set; } = "";
	public ObservableCollection<QuyenItemVM> QuyenList { get; set; } = new();
}