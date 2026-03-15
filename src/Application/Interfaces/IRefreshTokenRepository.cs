using Domain.Entities;
namespace Application.Interfaces;
public interface IRefreshTokenRepository
{
    Task SaveAsync(RefreshToken token);
    Task<RefreshToken?> GetAsync(string tokenHash);
    Task RevokeAsync(string tokenHash);
    Task RevokeAllAsync(int taiKhoanId);
}
