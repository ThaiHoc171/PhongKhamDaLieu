using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.WinForms.DTOs
{
	public class NameHelper
	{
		public int Id { get; set; }
		public string Name { get; set; } = string.Empty;
	}
	public class WeekItem
	{
		public int Page { get; set; }
		public string Display { get; set; } = "";
	}
	public class ModeItem
	{
		public string Text { get; set; } = "";
		public string Value { get; set; } = "";
	}
}
