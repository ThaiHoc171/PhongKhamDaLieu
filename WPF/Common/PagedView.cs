using System.Windows.Input;
using System.Runtime.CompilerServices;
namespace HoanMyClinic.Common;

public abstract class PagedViewModel : BaseViewModel
{
	private bool _isLoadingInternal;

	private async Task SafeLoadData()
	{
		if (_isLoadingInternal) return;

		try
		{
			_isLoadingInternal = true;
			await LoadData();
		}
		finally
		{
			_isLoadingInternal = false;
		}
	}

	#region PAGING

	private int _page = 1;
	public int Page
	{
		get => _page;
		set
		{
			if (_page == value) return;
			_page = value;
			OnPropertyChanged();
			_ = SafeLoadData();
		}
	}

	private int _sizePage = 15;
	public int SizePage
	{
		get => _sizePage;
		set
		{
			if (_sizePage == value) return;
			_sizePage = value;
			OnPropertyChanged();
		}
	}

	private int _totalPages;
	public int TotalPages
	{
		get => _totalPages;
		set
		{
			if (_totalPages == value) return;
			_totalPages = value;
			OnPropertyChanged();
		}
	}

	public string PageDisplay => $"{Page} / {TotalPages}";
	public bool CanGoPrev => Page > 1;
	public bool CanGoNext => Page < TotalPages;

	#endregion

	protected abstract Task LoadData();

	public async Task Init() => await SafeLoadData();

	#region COMMANDS

	private ICommand? _nextCommand;
	public ICommand NextCommand => _nextCommand ??= new RelayCommand(
		() => Page++,
		() => CanGoNext
	);

	private ICommand? _prevCommand;
	public ICommand PrevCommand => _prevCommand ??= new RelayCommand(
		() => Page--,
		() => CanGoPrev
	);

	private ICommand? _firstCommand;
	public ICommand FirstCommand => _firstCommand ??= new RelayCommand(
		() => Page = 1,
		() => CanGoPrev
	);

	private ICommand? _lastCommand;
	public ICommand LastCommand => _lastCommand ??= new RelayCommand(
		() => Page = TotalPages,
		() => CanGoNext
	);

	#endregion

	protected override void OnPropertyChanged([CallerMemberName] string name = "")
	{
		base.OnPropertyChanged(name);

		if (name == nameof(Page) || name == nameof(TotalPages))
		{
			base.OnPropertyChanged(nameof(PageDisplay));
			base.OnPropertyChanged(nameof(CanGoNext));
			base.OnPropertyChanged(nameof(CanGoPrev));

			(_nextCommand as RelayCommand)?.RaiseCanExecuteChanged();
			(_prevCommand as RelayCommand)?.RaiseCanExecuteChanged();
			(_firstCommand as RelayCommand)?.RaiseCanExecuteChanged();
			(_lastCommand as RelayCommand)?.RaiseCanExecuteChanged();
		}
	}
}