using System.Windows.Controls;
using System.Windows.Data;
using WPF.Common;
using WPF.ViewModels;

namespace WPF.Pages;

public partial class TaiKhoanPage : Page
{
    public TaiKhoanPage()
    {
        InitializeComponent();

        var vm = new TaiKhoanViewModel();
        DataContext = vm;

        Loaded += async (_, __) => await vm.Init();

        SetupDataGrid.ApplyStyle(GridContent);
        SetupColumns(vm);
    }

    private void SetupColumns(TaiKhoanViewModel vm)
    {
        GridContent.Columns.Clear();

        GridContent.Columns.Add(new DataGridTextColumn
        {
            Header = "Mã",
            Visibility = System.Windows.Visibility.Collapsed,
            Binding = new Binding("Id")
        });

        GridContent.Columns.Add(new DataGridTextColumn
        {
            Header = "Email",
            Binding = new Binding("Email"),
            Width = new DataGridLength(3, DataGridLengthUnitType.Star)
        });

        GridContent.Columns.Add(new DataGridTextColumn
        {
            Header = "Vai trò",
            Binding = new Binding("VaiTro"),
            Width = new DataGridLength(1, DataGridLengthUnitType.Star)
        });

        GridContent.Columns.Add(new DataGridTextColumn
        {
            Header = "Trạng thái",
            Binding = new Binding("TrangThai"),
            Width = new DataGridLength(1, DataGridLengthUnitType.Star)
        });

        // BUTTON (MVVM)
        GridContent.Columns.Add(
            SetupDataGrid.CreateIconButtonColumnVer2("Sync", "ResetCommand", "Reset mật khẩu"));

        GridContent.Columns.Add(
            SetupDataGrid.CreateIconButtonColumnVer2("Power", "ToggleStatusCommand", "Đổi trạng thái"));
    }
}