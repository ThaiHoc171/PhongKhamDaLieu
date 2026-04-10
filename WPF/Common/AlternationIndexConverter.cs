using System.Globalization;
using System.Windows.Data;

namespace WPF.Common;

public class AlternationIndexConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		if (value is int index)
			return index + 1;

		return 0;
	}
	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}