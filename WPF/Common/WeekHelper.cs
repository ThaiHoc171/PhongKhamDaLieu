using System.Windows.Media;
using WPF.Models;

namespace WPF.Common
{
	public class WeekItem
	{
		public int Page { get; set; }
		public string Display { get; set; } = "";
	}
	public class DayShiftViewModel
	{
		public DateTime Date { get; set; }
		public string ShiftDisplay { get; set; } = "";
		public Brush Color { get; set; } = default!;
	}
	public class DayShiftViewsModel
	{
		public DateTime Date { get; set; }
		public string ShiftDisplay { get; set; } = "";
		public Brush Color { get; set; } = default!;
		public List<LichLamViecReadModel> NhanViens { get; set; } = new();
	}
	public static class WeekHelper
	{
		public static string GetWeekDisplay(int page)
		{
			var today = DateTime.Today;

			// Tính thứ 2 của tuần hiện tại
			int diff = (7 + (today.DayOfWeek - DayOfWeek.Monday)) % 7;
			var monday = today.AddDays(-diff).Date;

			// Tính thứ 2 của tuần mục tiêu
			var targetMonday = monday.AddDays(page * 7);
			var sunday = targetMonday.AddDays(6);

			return $"Tuần {targetMonday:dd/MM} - {sunday:dd/MM}";
		}
	}
}
