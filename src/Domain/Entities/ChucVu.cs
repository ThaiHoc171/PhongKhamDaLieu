namespace Domain.Entities;

public class ChucVu
{
    public int ChucVuID { get; private set; }
    public string TenChucVu { get; private set; }
    public string? MoTa { get; private set; }
    public DateTime NgayTao { get; private set; }
    public string TrangThai { get; private set; }

    public ChucVu(string tenChucVu, string? moTa, string trangThai)
    {
        if (string.IsNullOrWhiteSpace(tenChucVu))
            throw new ArgumentException("Tên chức vụ không hợp lệ");
        TenChucVu = tenChucVu;
        MoTa = moTa;
        NgayTao = DateTime.UtcNow;
		TrangThai = trangThai;
	}
    public ChucVu(int chucVuID, string tenChucVu, string? moTa, DateTime ngayTao, string trangThai)
    {
        ChucVuID = chucVuID;
        TenChucVu = tenChucVu;
        MoTa = moTa;
        NgayTao = ngayTao;
        TrangThai = trangThai;
    }
    public void CapNhat(string tenChucVu, string? moTa, string trangThai)
    {
        if (string.IsNullOrWhiteSpace(tenChucVu))
            throw new ArgumentException("Tên chức vụ không hợp lệ");
        TenChucVu = tenChucVu;
        MoTa = moTa;
		TrangThai = trangThai;
	}
}