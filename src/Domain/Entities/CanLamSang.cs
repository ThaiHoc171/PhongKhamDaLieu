using Domain.Enums;

namespace Domain.Entities;

public class CanLamSang
{
    public int CanLamSangID { get; private set; }
    public string TenCLS { get; private set; }
    public string MoTa { get; private set; }
    public string LoaiXetNghiem { get; private set; }
    public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }
	public string TrangThai { get; private set; }

    public CanLamSang(string tenCLS, string moTa, string loaiXetNghiem, string trangThai)
    {
		Validate(tenCLS, moTa, loaiXetNghiem, trangThai);

		TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        TrangThai = trangThai;
    }
    public CanLamSang(int canLamSangID, string tenCLS, string moTa, string loaiXetNghiem, string trangThai, DateTime ngayTao, DateTime? ngayCapNhat)
    {
        CanLamSangID = canLamSangID;
        TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        TrangThai = trangThai;
        NgayTao = ngayTao;
        NgayCapNhat = ngayCapNhat;
    }
    public void CapNhat(string tenCLS, string moTa, string loaiXetNghiem, string trangThai)
    {
        Validate(tenCLS, moTa, loaiXetNghiem, trangThai);

        TenCLS = tenCLS;
        MoTa = moTa;
        LoaiXetNghiem = loaiXetNghiem;
        TrangThai = trangThai;
        NgayCapNhat = DateTime.UtcNow;
    }
	private void Validate(string tenCLS, string moTa,string loaiXetNghiem, string trangThai)
	{
		if (string.IsNullOrWhiteSpace(tenCLS))
			throw new ArgumentException("Tên cận lâm sàng không hợp lệ");
		if (string.IsNullOrWhiteSpace(moTa))
			throw new ArgumentException("Mô tả không hợp lệ");
		if (string.IsNullOrWhiteSpace(loaiXetNghiem))
			throw new ArgumentException("Loại xét nghiệm không hợp lệ");
		if (string.IsNullOrWhiteSpace(trangThai))
			throw new ArgumentException("Trạng thái không hợp lệ");
		TrangThaiSystemExtensions.FromDb(trangThai);
	}
}