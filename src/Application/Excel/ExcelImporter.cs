using OfficeOpenXml;
using System.Reflection;
public static class ExcelImporter
{
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
					var value = sheet.Cells[r, col].Text.Trim();
					if (!string.IsNullOrWhiteSpace(value))
					{
						hasData = true;
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
}