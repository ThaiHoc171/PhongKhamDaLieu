using Domain.Enums;

namespace Domain.Entities;

public class ChucVu
{
    public int ChucVuID { get; private set; }
    public string TenChucVu { get; private set; }
    public string MoTa { get; private set; }
    public DateTime NgayTao { get; private set; }
    public string TrangThai { get; private set; }
    public DateTime? NgayCapNhat { get; private set; }

    public ChucVu(string tenChucVu, string moTa, string trangThai)
    {
		Validate(tenChucVu, moTa, trangThai);

		TenChucVu = tenChucVu;
        MoTa = moTa;
		TrangThai = trangThai;
	}
    public ChucVu(int chucVuID, string tenChucVu, string moTa,string trangThai,  DateTime ngayTao, DateTime? ngayCapNhat)
    {
        ChucVuID = chucVuID;
        TenChucVu = tenChucVu;
        MoTa = moTa;
        NgayTao = ngayTao;
        NgayCapNhat = ngayCapNhat;
        TrangThai = trangThai;
    }
	public void CapNhat(string tenChucVu, string moTa, string trangThai)
	{
		Validate(tenChucVu, moTa, trangThai);

		TenChucVu = tenChucVu;
		MoTa = moTa;
		TrangThai = trangThai;
		NgayCapNhat = DateTime.UtcNow;
	}

	private void Validate(string tenChucVu, string moTa, string trangThai)
	{
		if (string.IsNullOrWhiteSpace(tenChucVu))
			throw new ArgumentException("Tên chức vụ không hợp lệ");
		if (string.IsNullOrWhiteSpace(moTa))
			throw new ArgumentException("Mô tả không hợp lệ");
		if (string.IsNullOrWhiteSpace(trangThai))
			throw new ArgumentException("Trạng thái không hợp lệ");
		TrangThaiSystemExtensions.FromDb(trangThai);
	}
}