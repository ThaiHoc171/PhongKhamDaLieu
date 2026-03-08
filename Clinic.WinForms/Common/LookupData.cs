using System.Collections.Generic;

namespace Clinic.WinForms.Common
{
	public static class LookupData
	{
		public static readonly List<string> MucDoNghiemTrong = new List<string>
		{
			"nhẹ",
			"trung bình",
			"nặng"
		};

		public static readonly List<string> DoPhoBien = new List<string>
		{
			"phổ biến",
			"ít gặp",
			"hiếm"
		};

		public static readonly List<string> NhomBenh = new List<string>
		{
			"Viêm da",
			"Dị ứng",
			"Nhiễm trùng",
			"Virus",
			"Nấm",
			"Ký sinh trùng",
			"U lành tính",
			"Tự miễn",
			"Miễn dịch",
			"Ung thư da",
			"Viêm mạn",
			"Di truyền",
			"Tăng sừng",
			"Tiền ung thư",
			"Rối loạn chuyển hóa"
		};
		public static readonly List<string> LoaiCaKham = new List<string>
		{
			"Khám",
			"Điều trị"
		};
		public static readonly List<string> TrangThaiCaKham = new List<string>
		{
			"Trống",
			"Đã đặt",	
			"Đã xác nhận",
			"Đang khám",
			"Hoàn thành",
			"Đã hủy",
			"Không đến"
		};
		public static readonly List<string> TrangThaiPhienKham = new List<string>
		{
			"Hoàn thành",
			"Đã hủy"
		};
	}
}