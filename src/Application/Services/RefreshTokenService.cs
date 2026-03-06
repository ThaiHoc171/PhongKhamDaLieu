using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class RefreshTokenService
{
    private readonly IRefreshTokenRepository _repo;

    public RefreshTokenService(IRefreshTokenRepository repo)
    {
        _repo = repo;
    }
    public async Task RevokeAsync(string tokenHash)
    {
        if (string.IsNullOrEmpty(tokenHash))
        {
            throw new ArgumentException("TokenHash không hợp lệ");
        }
        await _repo.RevokeAsync(tokenHash);
    }
}
