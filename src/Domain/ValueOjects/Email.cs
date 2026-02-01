using System.Text.RegularExpressions;

namespace Domain.ValueObjects;

public sealed class Email
{
	public string Value { get; }

	private Email(string value)
	{
		Value = value;
	}

	public static Email Create(string email)
	{
		if (string.IsNullOrWhiteSpace(email))
			throw new ArgumentException("Email không được rỗng");
		if (!IsValid(email))
			throw new ArgumentException("Email không hợp lệ");

		return new Email(email.Trim());
	}

	private static bool IsValid(string email)
	{
		return Regex.IsMatch(
			email,
			@"^[^@\s]+@[^@\s]+\.[^@\s]+$",
			RegexOptions.IgnoreCase
		);
	}

	public override string ToString() => Value;
}
