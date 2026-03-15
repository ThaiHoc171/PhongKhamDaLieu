namespace Domain.Entities;
public class RefreshToken
{
    public int RefreshTokenId { get; private set; }
    public int TaiKhoanId { get; private set; }
    public string TokenHash { get; private set; } = null!;
    public DateTime ExpiryDate { get; private set; }
    public bool IsRevoked { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public RefreshToken(
        int taiKhoanId,
        string tokenHash,
        DateTime expiryDate)    
    {
        TaiKhoanId = taiKhoanId;
        TokenHash = tokenHash;
        ExpiryDate = expiryDate;
        CreatedAt = DateTime.UtcNow;
        IsRevoked = false;
    }
    public RefreshToken(
        int refreshTokenId,
        int taiKhoanId,
        string tokenHash,
        DateTime expiryDate,
        DateTime createAt,
        bool isRevoked)
    {
        RefreshTokenId = refreshTokenId;
        TaiKhoanId = taiKhoanId;
        TokenHash = tokenHash;
        ExpiryDate = expiryDate;
        CreatedAt = createAt;
        IsRevoked = isRevoked;
    }
    public void Revoke()
    {
        IsRevoked = true;
    }
    public bool IsActive()
        => !IsRevoked && ExpiryDate > DateTime.UtcNow;
}
