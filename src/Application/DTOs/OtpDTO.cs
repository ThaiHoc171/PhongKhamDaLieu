namespace Application.DTOs;

public class TaoOtpRequestDTO
{
    public string Email { get; set; } = "";
}

public class XacThucOtpRequestDTO
{
    public string Email { get; set; } = "";
    public string MaOTP { get; set; } = "";
}