using Domain.Enums;

namespace Domain.Entities;
public class ThietBi
{
    public int ThietBiID { get; private set; }
    public string TenTB { get; private set; } = default!;
    public string LoaiTB { get; private set; }
    public string TrangThai {get; private set; }
	public DateTime NgayTao { get; private set; }
	public DateTime? NgayCapNhat { get; private set; }
	// Tạo mới
	public ThietBi(string tenTB, string loaiTB, string trangThai)
    {
		Validate(tenTB, loaiTB, trangThai);
		TenTB = tenTB;
        LoaiTB = loaiTB;
        TrangThai = trangThai;
    }
    // Map DB
    public ThietBi(int thietBiID, string tenTB, string loaiTB, string trangThai, DateTime ngayTao, DateTime? ngayCapNhat)
    {
        ThietBiID = thietBiID;
        TenTB = tenTB;
        LoaiTB = loaiTB;
        TrangThai = trangThai;
        NgayTao = ngayTao;
        NgayCapNhat = ngayCapNhat;
    }
    public void CapNhat(string tenTB, string loaiTB, string trangThai)
    {
        Validate(tenTB, loaiTB, trangThai);
        TenTB = tenTB;
        LoaiTB = loaiTB;
        TrangThai = trangThai;
        NgayCapNhat = DateTime.UtcNow;
    }
	private void Validate(string tenTB, string loaiTB, string trangThai)
	{
		if (string.IsNullOrWhiteSpace(tenTB))
			throw new ArgumentException("Tên thiết bị không hợp lệ");
		if (string.IsNullOrWhiteSpace(loaiTB))
			throw new ArgumentException("Loại thiết bị không hợp lệ");
		if (string.IsNullOrWhiteSpace(trangThai))
			throw new ArgumentException("Trạng thái không hợp lệ");
		TrangThaiSystemExtensions.FromDb(trangThai);
	}
}