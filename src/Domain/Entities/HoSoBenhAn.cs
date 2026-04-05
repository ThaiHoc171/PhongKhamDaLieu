namespace Domain.Entities;

public class HoSoBenhAn
{
	public int HoSoBenhAnID { get; private set; }
	public int BenhNhanID { get; private set; }
	public string? BenhNen { get; private set; }
	public string? DiUng { get; private set; }
	public string? TienSuBenh { get; private set; }
	public string? TienSuGiaDinh { get; private set; }
	public string? ThoiQuenSong { get; private set; }
	public string? ThongTinKhac { get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime NgayCapNhat { get; private set; }

	// Tạo mới
	public HoSoBenhAn(int benhNhanID, string? benhNen, string? diUng, string? tienSuBenh, 
		string? tienSuGiaDinh, string? thoiQuenSong, string? thongTinKhac)
	{
		if (benhNhanID <= 0)
			throw new ArgumentException("Bệnh nhân không hợp lệ");

		BenhNhanID = benhNhanID;
		BenhNen = benhNen?.Trim();
		DiUng = diUng?.Trim();
		TienSuBenh = tienSuBenh?.Trim();
		TienSuGiaDinh = tienSuGiaDinh?.Trim();
		ThoiQuenSong = thoiQuenSong?.Trim();
		ThongTinKhac = thongTinKhac?.Trim();
	}

	// Map DB
	public HoSoBenhAn(int hoSoBenhAnID, int benhNhanID, string? benhNen, string? diUng, string? tienSuBenh,
		string? tienSuGiaDinh, string? thoiQuenSong, string? thongTinKhac, DateTime ngayTao, DateTime ngayCapNhat)
	{
		HoSoBenhAnID = hoSoBenhAnID;
		BenhNhanID = benhNhanID;
		BenhNen = benhNen;
		DiUng = diUng;
		TienSuBenh = tienSuBenh;
		TienSuGiaDinh = tienSuGiaDinh;
		ThoiQuenSong = thoiQuenSong;
		ThongTinKhac = thongTinKhac;
		NgayTao = ngayTao;
		NgayCapNhat = ngayCapNhat;
	}

	public void CapNhatThongTin(string? benhNen, string? diUng, string? tienSuBenh, 
		string? tienSuGiaDinh, string? thoiQuenSong, string? thongTinKhac)
	{
		BenhNen = benhNen?.Trim();
		DiUng = diUng?.Trim();
		TienSuBenh = tienSuBenh?.Trim();
		TienSuGiaDinh = tienSuGiaDinh?.Trim();
		ThoiQuenSong = thoiQuenSong?.Trim();
		ThongTinKhac = thongTinKhac?.Trim();
	}
}