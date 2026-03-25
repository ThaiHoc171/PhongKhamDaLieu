[AttributeUsage(AttributeTargets.Property)]
public class ExcelColumnAttribute : Attribute
{
	public string Name { get; }

	public ExcelColumnAttribute(string name)
	{
		Name = name;
	}
}