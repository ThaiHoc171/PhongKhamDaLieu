using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
	public class Password
	{
		public static string PassWordHash(string password)
		{
			using (SHA256 sha = SHA256.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(password);
				byte[] hash = sha.ComputeHash(bytes);
				return Convert.ToBase64String(hash);
			}
		}
		public static bool VerifyPassword(string password, string passwordHash)
		{
			string hashedInput = PassWordHash(password);
			return hashedInput == passwordHash;
		}
	}
}
