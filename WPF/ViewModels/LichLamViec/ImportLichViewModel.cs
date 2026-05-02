using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using HoanMyClinic.Client;
using HoanMyClinic.Common;
using HoanMyClinic.Models;

namespace HoanMyClinic.ViewModels;

public class ImportLichViewModel : BaseViewModel
{
	private readonly LichLamViecClient _client = new();
	private readonly ExcelClient _excel = new();

	public ObservableCollection<string> Sheets { get; set; } = new();
	public ObservableCollection<LichLamViecRequest> PreviewData { get; set; } = new();
	public ObservableCollection<string> Errors { get; set; } = new();

	#region PROPERTIES

	private string _filePath = "";
	public string FilePath
	{
		get => _filePath;
		set { _filePath = value; OnPropertyChanged(); }
	}

	private string _selectedSheet = "";
	public string SelectedSheet
	{
		get => _selectedSheet;
		set { _selectedSheet = value; OnPropertyChanged(); }
	}

	private string _summary = "";
	public string Summary
	{
		get => _summary;
		set { _summary = value; OnPropertyChanged(); }
	}

	private string _status = "";
	public string Status
	{
		get => _status;
		set { _status = value; OnPropertyChanged(); }
	}

	private bool _canSave;
	public bool CanSave
	{
		get => _canSave;
		set
		{
			if (_canSave == value) return;
			_canSave = value;
			OnPropertyChanged();

			(_saveCommand as RelayCommand)?.RaiseCanExecuteChanged();
		}
	}

	#endregion

	public async Task Init()
	{
		await Task.CompletedTask;
	}

	#region COMMANDS

	// ================= CHOOSE FILE =================
	public ICommand ChooseFileCommand => new RelayCommand(() => _ = ChooseFile());

	private async Task ChooseFile()
	{
		var dlg = new OpenFileDialog
		{
			Filter = "Excel (*.xlsx)|*.xlsx"
		};

		if (dlg.ShowDialog() != true) return;

		FilePath = dlg.FileName;

		// reset data
		PreviewData.Clear();
		Errors.Clear();
		Summary = "";
		Status = "";
		CanSave = false;

		SnackbarHelper.ShowSuccess($"Đã chọn file: {Path.GetFileName(FilePath)}");

		await LoadSheets(FilePath);
	}

	// ================= PREVIEW =================
	public ICommand PreviewCommand => new RelayCommand(() => _ = Preview());

	private async Task Preview()
	{
		try
		{
			IsLoading = true;

			var result = await _client.PreviewImport(FilePath, SelectedSheet);

			if (!result.Success || result.Data == null)
			{
				SnackbarHelper.ShowError(result.Message);
				return;
			}

			PreviewData.Clear();
			foreach (var item in result.Data.Data)
				PreviewData.Add(item);

			Errors.Clear();
			foreach (var e in result.Data.Errors)
				foreach (var err in e.Errors)
					Errors.Add(err);

			Summary = $"Total: {result.Data.TotalRows} | Valid: {result.Data.SuccessRows}";
			CanSave = false;
		}
		finally
		{
			IsLoading = false;
		}
	}

	// ================= VALIDATE =================
	public ICommand ValidateCommand => new RelayCommand(() => _ = Validate());

	private async Task Validate()
	{
		try
		{
			IsLoading = true;

			if (!PreviewData.Any())
			{
				SnackbarHelper.ShowError("Chưa có dữ liệu preview");
				return;
			}

			var result = await _client.ValidateImport(PreviewData.ToList());

			if (!result.Success)
			{
				SnackbarHelper.ShowError(result.Message);
				return;
			}

			Errors.Clear();

			var errors = result.Data?.Errors;

			if (errors != null && errors.Any())
			{
				foreach (var e in errors.SelectMany(x => x.Errors))
					Errors.Add(e);

				SnackbarHelper.ShowError("Có lỗi trong dữ liệu");
				CanSave = false;
				return;
			}

			CanSave = true;
			SnackbarHelper.ShowSuccess("Validate thành công!");
		}
		finally
		{
			IsLoading = false;
		}
	}

	// ================= SAVE =================
	private ICommand? _saveCommand;
	public ICommand SaveCommand => _saveCommand ??= new RelayCommand(() => _ = Save(), () => CanSave);

	private async Task Save()
	{
		try
		{
			IsLoading = true;

			if (!PreviewData.Any())
			{
				SnackbarHelper.ShowError("Chưa có dữ liệu");
				return;
			}

			var confirm = await MessageHelper.Confirm("Bạn có chắc muốn lưu?");
			if (!confirm) return;

			var res = await _client.ConfirmImport(PreviewData.ToList());

			if (res.Success)
			{
				SnackbarHelper.ShowSuccess("Thêm lịch thành công");
				Status = "Đã lưu dữ liệu";
			}
			else
			{
				SnackbarHelper.ShowError(res.Message);
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex);
			SnackbarHelper.ShowError("Lỗi khi lưu");
		}
		finally
		{
			IsLoading = false;
		}
	}

	#endregion

	#region PRIVATE

	private async Task LoadSheets(string file)
	{
		var result = await _excel.GetSheets(file);

		Sheets.Clear();

		if (!result.Success || result.Data == null || !result.Data.Any())
		{
			SnackbarHelper.ShowError("Không lấy được sheet");
			return;
		}

		foreach (var s in result.Data)
			Sheets.Add(s);

		SelectedSheet = Sheets.First();

		SnackbarHelper.ShowSuccess($"Đã load {Sheets.Count} sheet");
	}

	#endregion
}