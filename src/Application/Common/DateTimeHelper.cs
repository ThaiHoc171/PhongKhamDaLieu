
namespace Application.Common;

public class DateTimeHelper
{
	// Lấy khoảng tuần (Thứ 2 → Chủ nhật) chứa ngày truyền vào
	// Hàm trả về 2 giá trị: Ngày bắt đầu (Thứ 2 00:00:00) và ngày kết thúc (Chủ nhật 23:59:59)
	// DayofWeek: Sunday = 0, Monday = 1, ..., Saturday = 6
	public static (DateTime Start, DateTime End) GetWeekRange(DateTime date)
	{
		int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
		var start = date.Date.AddDays(-diff);
		var end = start.AddDays(7).AddTicks(-1); // CN 23:59:59

		return (start, end);
	}

	/// Lấy tuần theo page (0 = tuần hiện tại, +1 tuần sau, -1 tuần trước)
	public static (DateTime Start, DateTime End) GetWeekByPage(int page)
	{
		var baseDate = DateTime.Today.AddDays(page * 7);
		return GetWeekRange(baseDate);
	}
}
