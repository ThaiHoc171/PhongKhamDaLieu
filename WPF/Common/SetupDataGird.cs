using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF.Common;

public static class SetupDataGrid
{
	public static void ApplyStyle(DataGrid grid)
	{
		grid.AutoGenerateColumns = false;
		grid.CanUserAddRows = false;
		grid.CanUserResizeColumns = false;
		grid.CanUserReorderColumns = false;
		grid.IsReadOnly = true;

		grid.SelectionMode = DataGridSelectionMode.Single;
		grid.SelectionUnit = DataGridSelectionUnit.FullRow;

		grid.RowHeight = 45;
		grid.FontSize = 16;
		grid.ColumnWidth = new DataGridLength(1, DataGridLengthUnitType.Star);

		grid.Background = Brushes.Transparent;
		grid.BorderThickness = new Thickness(0);
		grid.GridLinesVisibility = DataGridGridLinesVisibility.None;

		grid.RowBackground = Brushes.White;
		grid.AlternatingRowBackground = new SolidColorBrush(Color.FromRgb(245, 247, 250));
		// Material Design padding
		DataGridAssist.SetCellPadding(grid, new Thickness(10));
		DataGridAssist.SetColumnHeaderPadding(grid, new Thickness(10));

		grid.ColumnHeaderStyle = CreateHeaderStyle();
		grid.CellStyle = CreateCellStyle();
	}

	// ================= HEADER =================
	private static Style CreateHeaderStyle()
	{
		var style = new Style(typeof(DataGridColumnHeader));

		style.Setters.Add(new Setter(Control.BackgroundProperty, new SolidColorBrush(Color.FromRgb(153, 204, 255))));
		style.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
		style.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.SemiBold));
		style.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));
		style.Setters.Add(new Setter(Control.PaddingProperty, new Thickness(10)));
		style.Setters.Add(new Setter(Control.FocusVisualStyleProperty, null));

		return style;
	}

	// ================= CELL =================
	private static Style CreateCellStyle()
	{
		var style = new Style(typeof(DataGridCell));
		
		style.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0)));
		style.Setters.Add(new Setter(Control.BackgroundProperty, Brushes.Transparent));
		style.Setters.Add(new Setter(Control.ForegroundProperty, Brushes.Black));
		style.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Left));
		style.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center));
		style.Setters.Add(new Setter(Control.FocusVisualStyleProperty, null));
		style.Setters.Add(new Setter(Control.IsTabStopProperty, false));
		style.Setters.Add(new Setter(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Left));
		style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));
		return style;
	}
	// ================= BUTTON ICON (Material) =================
	public static DataGridTemplateColumn CreateIconButtonColumn(string iconKind, RoutedEventHandler click, string toolTip = "", string? status = null)
	{
		var col = new DataGridTemplateColumn
		{
			Width = DataGridLength.Auto,
			MinWidth = 40
		};

		var buttonFactory = new FrameworkElementFactory(typeof(Button));
		buttonFactory.SetValue(Control.WidthProperty, 28.0);
		buttonFactory.SetValue(Control.HeightProperty, 28.0);
		buttonFactory.SetValue(Control.PaddingProperty, new Thickness(1));
		buttonFactory.SetValue(Button.MarginProperty, new Thickness(5));
		buttonFactory.SetValue(Control.MinWidthProperty, 0.0);
		buttonFactory.SetValue(Control.MinHeightProperty, 0.0);
		buttonFactory.SetValue(Button.ToolTipProperty, toolTip);

		// Style Material
		buttonFactory.SetValue(Button.StyleProperty, Application.Current.FindResource("MaterialDesignOutlinedButton"));
		buttonFactory.SetValue(ButtonAssist.CornerRadiusProperty, new CornerRadius(50));
		buttonFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
		buttonFactory.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
		buttonFactory.SetValue(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
		buttonFactory.SetValue(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center);

		// Bind Tag
		//buttonFactory.SetBinding(Button.TagProperty, new System.Windows.Data.Binding());
		buttonFactory.SetBinding(Button.TagProperty, new Binding("."));

		buttonFactory.AddHandler(Button.ClickEvent, click);

		// ICON
		var iconFactory = new FrameworkElementFactory(typeof(PackIcon));
		iconFactory.SetValue(PackIcon.KindProperty, Enum.Parse(typeof(PackIconKind), iconKind));
		iconFactory.SetValue(PackIcon.WidthProperty, 20.0);
		iconFactory.SetValue(PackIcon.HeightProperty, 20.0);
		iconFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty,HorizontalAlignment.Center);
		iconFactory.SetValue(FrameworkElement.VerticalAlignmentProperty,VerticalAlignment.Center);
		buttonFactory.AppendChild(iconFactory);

		if (!string.IsNullOrEmpty(status))
		{
			var baseStyle = (Style)Application.Current
				.FindResource("MaterialDesignOutlinedButton");

			var style = new Style(typeof(Button), baseStyle);

			style.Setters.Add(new Setter(UIElement.IsEnabledProperty, false));

			var statuses = status.Split(',');

			foreach (var st in statuses)
			{
				var trigger = new DataTrigger
				{
					Binding = new Binding("TrangThai"),
					Value = st.Trim()
				};

				trigger.Setters.Add(new Setter(UIElement.IsEnabledProperty, true));

				style.Triggers.Add(trigger);
			}

			buttonFactory.SetValue(Button.StyleProperty, style);
		}
		else
		{
			buttonFactory.SetValue(Button.StyleProperty,
				Application.Current.FindResource("MaterialDesignOutlinedButton"));
		}

		col.CellTemplate = new DataTemplate
		{
			VisualTree = buttonFactory
		};

		return col;
	}
	public static DataGridTemplateColumn CreateIconButtonColumnVer2(string iconKind, string commandBinding, string toolTip = "", string? status = null)
	{
		var col = new DataGridTemplateColumn
		{
			Width = DataGridLength.Auto,
			MinWidth = 40
		};

		var buttonFactory = new FrameworkElementFactory(typeof(Button));
		buttonFactory.SetValue(Control.WidthProperty, 28.0);
		buttonFactory.SetValue(Control.HeightProperty, 28.0);
		buttonFactory.SetValue(Control.PaddingProperty, new Thickness(1));
		buttonFactory.SetValue(Button.MarginProperty, new Thickness(5));
		buttonFactory.SetValue(Control.MinWidthProperty, 0.0);
		buttonFactory.SetValue(Control.MinHeightProperty, 0.0);
		buttonFactory.SetValue(Button.ToolTipProperty, toolTip);

		// Style Material
		buttonFactory.SetValue(Button.StyleProperty, Application.Current.FindResource("MaterialDesignOutlinedButton"));
		buttonFactory.SetValue(ButtonAssist.CornerRadiusProperty, new CornerRadius(50));
		buttonFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
		buttonFactory.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
		buttonFactory.SetValue(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
		buttonFactory.SetValue(Control.VerticalContentAlignmentProperty, VerticalAlignment.Center);

		// Bind Tag
		//buttonFactory.SetBinding(Button.TagProperty, new System.Windows.Data.Binding());
		buttonFactory.SetBinding(Button.TagProperty, new Binding("."));
		buttonFactory.SetBinding(Button.CommandProperty,
			new Binding(commandBinding)
			{
				RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGrid), 1),
				Path = new PropertyPath($"DataContext.{commandBinding}")
			});
		buttonFactory.SetBinding(Button.CommandParameterProperty, new Binding("."));
		// ICON
		var iconFactory = new FrameworkElementFactory(typeof(PackIcon));
		iconFactory.SetValue(PackIcon.KindProperty, Enum.Parse(typeof(PackIconKind), iconKind));
		iconFactory.SetValue(PackIcon.WidthProperty, 20.0);
		iconFactory.SetValue(PackIcon.HeightProperty, 20.0);
		iconFactory.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
		iconFactory.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);
		buttonFactory.AppendChild(iconFactory);

		if (!string.IsNullOrEmpty(status))
		{
			var baseStyle = (Style)Application.Current
				.FindResource("MaterialDesignOutlinedButton");

			var style = new Style(typeof(Button), baseStyle);

			style.Setters.Add(new Setter(UIElement.IsEnabledProperty, false));

			var statuses = status.Split(',');

			foreach (var st in statuses)
			{
				var trigger = new DataTrigger
				{
					Binding = new Binding("TrangThai"),
					Value = st.Trim()
				};

				trigger.Setters.Add(new Setter(UIElement.IsEnabledProperty, true));

				style.Triggers.Add(trigger);
			}

			buttonFactory.SetValue(Button.StyleProperty, style);
		}
		else
		{
			buttonFactory.SetValue(Button.StyleProperty,
				Application.Current.FindResource("MaterialDesignOutlinedButton"));
		}

		col.CellTemplate = new DataTemplate
		{
			VisualTree = buttonFactory
		};

		return col;
	}
}