namespace Domain.Entities;

public class LoaiBenh
{
	public int LoaiBenhID { get; private set; }
	public string TenBenh { get; private set; }
	public string TenKhoaHoc { get; private set; }
	public string NhomBenh { get; private set; }
	public string MoTa { get; private set; }
	public string DoPhoBien { get; private set; }
	public string MucDoNghiemTrong { get; private set; }
	public DateTime NgayTao { get; private set; }

	// Tạo mới
	public LoaiBenh(string tenBenh, string tenKhoaHoc, string nhomBenh, string moTa, string doPhoBien, string mucDoNghiemTrong)
	{
		Validate(tenBenh, tenKhoaHoc, nhomBenh, moTa, doPhoBien, mucDoNghiemTrong);

		TenBenh = tenBenh.Trim();
		TenKhoaHoc = tenKhoaHoc.Trim();
		NhomBenh = nhomBenh.Trim();
		MoTa = moTa.Trim();
		DoPhoBien = doPhoBien;
		MucDoNghiemTrong = mucDoNghiemTrong;
	}

	// Map DB
	public LoaiBenh(int loaiBenhID, string tenBenh, string tenKhoaHoc, string nhomBenh, string moTa, string doPhoBien, string mucDoNghiemTrong, DateTime ngayTao)
	{
		LoaiBenhID = loaiBenhID;
		TenBenh = tenBenh;
		TenKhoaHoc = tenKhoaHoc;
		NhomBenh = nhomBenh;
		MoTa = moTa;
		DoPhoBien = doPhoBien;
		MucDoNghiemTrong = mucDoNghiemTrong;
		NgayTao = ngayTao;
	}

	public void CapNhat(string tenBenh, string tenKhoaHoc, string nhomBenh, string moTa, string doPhoBien, string mucDoNghiemTrong)
	{
		Validate(tenBenh, tenKhoaHoc, nhomBenh, moTa, doPhoBien, mucDoNghiemTrong);

		TenBenh = tenBenh.Trim();
		TenKhoaHoc = tenKhoaHoc.Trim();
		NhomBenh = nhomBenh.Trim();
		MoTa = moTa.Trim();
		DoPhoBien = doPhoBien;
		MucDoNghiemTrong = mucDoNghiemTrong;
	}

	private void Validate(string tenBenh, string tenKhoaHoc, string nhomBenh, string moTa, string doPhoBien, string mucDoNghiemTrong)
	{
		if (string.IsNullOrWhiteSpace(tenBenh))
			throw new ArgumentException("Tên bệnh không hợp lệ");

		if (string.IsNullOrWhiteSpace(tenKhoaHoc))
			throw new ArgumentException("Tên khoa học không hợp lệ");

		if (string.IsNullOrWhiteSpace(nhomBenh))
			throw new ArgumentException("Nhóm bệnh không hợp lệ");

		if (string.IsNullOrWhiteSpace(moTa))
			throw new ArgumentException("Mô tả không hợp lệ");

		if (string.IsNullOrWhiteSpace(doPhoBien))
			throw new ArgumentException("Độ phổ biến không hợp lệ");

		if (string.IsNullOrWhiteSpace(mucDoNghiemTrong))
			throw new ArgumentException("Mức độ nghiêm trọng không hợp lệ");

		if (doPhoBien != "phổ biến" && doPhoBien != "ít gặp" && doPhoBien != "hiếm")
			throw new ArgumentException("Độ phổ biến không hợp lệ");

		if (mucDoNghiemTrong != "nhẹ" && mucDoNghiemTrong != "trung bình" && mucDoNghiemTrong != "nặng")
			throw new ArgumentException("Mức độ nghiêm trọng không hợp lệ");
	}
}