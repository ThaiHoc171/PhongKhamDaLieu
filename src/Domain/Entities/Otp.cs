public class Otp
{
    public int OtpID { get; private set; }
    public int? TaiKhoanID { get; private set; }  
    public string? Email { get; private set; }   
    public string MaOTP { get; private set; }
    public DateTime ThoiHanHetHan { get; private set; }
    public bool ConHieuLuc { get; private set; }

    public Otp(int taiKhoanID, string maOTP)
    {
        if (taiKhoanID <= 0)
            throw new ArgumentException("Mã tài khoản không hợp lệ");
        if (string.IsNullOrWhiteSpace(maOTP))
            throw new ArgumentException("Mã OTP không hợp lệ");
        TaiKhoanID = taiKhoanID;
        MaOTP = maOTP;
        ThoiHanHetHan = DateTime.UtcNow.AddMinutes(2);
        ConHieuLuc = true;
    }

    public Otp(string email, string maOTP)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email không hợp lệ");
        if (string.IsNullOrWhiteSpace(maOTP))
            throw new ArgumentException("Mã OTP không hợp lệ");
        Email = email.Trim().ToLower();
        MaOTP = maOTP;
        ThoiHanHetHan = DateTime.UtcNow.AddMinutes(2);
        ConHieuLuc = true;
    }
    public Otp(int otpID, int? taiKhoanID, string? email, string maOTP, DateTime thoiHanHetHan, bool conHieuLuc)
    {
        OtpID = otpID;
        TaiKhoanID = taiKhoanID;
        Email = email;
        MaOTP = maOTP;
        ThoiHanHetHan = thoiHanHetHan;
        ConHieuLuc = conHieuLuc;
    }

    public bool IsValid() => ConHieuLuc && ThoiHanHetHan > DateTime.UtcNow;
    public void Invalidate()
    {
        if (!ConHieuLuc)
            throw new InvalidOperationException("OTP đã được sử dụng hoặc hết hạn");
        ConHieuLuc = false;
    }
}