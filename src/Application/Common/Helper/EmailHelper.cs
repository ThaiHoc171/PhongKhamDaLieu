using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Helper
{
	public static class EmailHelper
	{
		public static bool IsEmail(string email)
		{
			return Regex.IsMatch(email,
				@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
				RegexOptions.IgnoreCase);
		}
	}
}
