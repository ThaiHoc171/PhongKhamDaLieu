using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoanMyClinic.Models;

public class PhienKhamPdfDto
{
	public string BenhNhan { get; set; } = "";
	public string BacSi { get; set; } = "";
	public DateTime? NgayKham { get; set; }
	public string TrangThai { get; set; } = "";

	public string TrieuChung { get; set; } = "";
	public string ChanDoan { get; set; } = "";
	public string GhiChu { get; set; } = "";

	public List<PhienKhamBenhReadModel> BenhList { get; set; } = new();
	public List<PhienKhamClsReadListModel> CLSList { get; set; } = new();
	public List<PhienKhamThietBiReadModel> ThietBiList { get; set; } = new();
}