using Application.Common;
using OfficeOpenXml;
using System.Reflection;
public static class ExcelImporter
{
	public static ApiResponse<ExcelImportResult<T>> Preview<T>(Stream stream, string sheet,	Func<T, int, List<string>> validator) where T : new()
	{
		var result = Import<T>(stream, sheet);

		int row = 2;
		foreach (var item in result.Data)
		{
			var errors = validator(item, row);
			if (errors != null && errors.Any())
				result.Errors.AddRange(errors);
			row++;
		}

		if (result.Errors.Any())
			return ApiResponse<ExcelImportResult<T>>.SuccessResponse(result, "File Excel có dữ liệu không hợp lệ");

		return ApiResponse<ExcelImportResult<T>>.SuccessResponse(result);
	}
	public static ExcelImportResult<T> Import<T>(Stream stream, string sheetName) where T : new()
	{
		var result = new ExcelImportResult<T>();
		using var package = new ExcelPackage(stream);
		var sheet = package.Workbook.Worksheets[sheetName];
		if (sheet == null)
		{
			result.Errors.Add($"Sheet '{sheetName}' không tồn tại");
			return result;
		}
		if (sheet.Dimension == null)
		{
			result.Errors.Add("Sheet không có dữ liệu");
			return result;
		}
		int rows = sheet.Dimension.Rows;
		int cols = sheet.Dimension.Columns;
		result.TotalRows = rows - 1;
		var headers = new Dictionary<string, int>();
		for (int c = 1; c <= cols; c++)
		{
			var name = sheet.Cells[1, c].Text.Trim();
			if (!headers.ContainsKey(name))
				headers.Add(name, c);
		}
		var props = typeof(T).GetProperties();
		for (int r = 2; r <= rows; r++)
		{
			try
			{
				var obj = new T();
				bool hasData = false;
				foreach (var prop in props)
				{
					var attr = prop.GetCustomAttribute<ExcelColumnAttribute>();
					if (attr == null) continue;
					if (!headers.ContainsKey(attr.Name))
						continue;
					int col = headers[attr.Name];
					var valueText = sheet.Cells[r, col].Text.Trim();
					if (!string.IsNullOrWhiteSpace(valueText))
					{
						hasData = true;
						object? value = ConvertValue(valueText, prop.PropertyType);
						prop.SetValue(obj, value);
					}
				}
				if (!hasData)
					continue;
				result.Data.Add(obj);
			}
			catch (Exception ex)
			{
				result.Errors.Add($"Row {r}: {ex.Message}");
			}
		}
		result.SuccessRows = result.Data.Count;
		return result;
	}
	private static object? ConvertValue(string text, Type type)
	{
		try
		{
			if (type == typeof(string)) return text;
			if (type == typeof(int) || type == typeof(int?)) return int.TryParse(text, out var v) ? v : null;
			if (type == typeof(double) || type == typeof(double?)) return double.TryParse(text, out var v) ? v : null;
			if (type == typeof(decimal) || type == typeof(decimal?)) return decimal.TryParse(text, out var v) ? v : null;
			if (type == typeof(bool) || type == typeof(bool?))
				return text.Equals("true", StringComparison.OrdinalIgnoreCase)
					|| text.Equals("yes", StringComparison.OrdinalIgnoreCase)
					|| text.Equals("1");
			return text;
		}
		catch
		{
			return null;
		}
	}
}