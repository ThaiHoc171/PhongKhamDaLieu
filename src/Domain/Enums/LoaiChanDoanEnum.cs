using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Domain.Enums;
public enum LoaiChanDoanEnum
{	
	ChanDoanChinh,
	ChanDoanPhatSinh
}
public static class LoaiChanDoanExtensions
{
	public static string ToDbValue(this LoaiChanDoanEnum val)
	{
		return val switch
		{
			LoaiChanDoanEnum.ChanDoanChinh => "Chẩn đoán chính",
			LoaiChanDoanEnum.ChanDoanPhatSinh => "Chẩn đoán phát sinh",
			_ => throw new ArgumentOutOfRangeException(nameof(val), val, null)
		};
	}
	public static LoaiChanDoanEnum FromDb(string description)
	{
		return description switch
		{
			"Chẩn đoán chính" => LoaiChanDoanEnum.ChanDoanChinh,
			"Chẩn đoán phát sinh" => LoaiChanDoanEnum.ChanDoanPhatSinh,
			_ => throw new ArgumentException($"Unknown description: {description}")
		};
	}
}