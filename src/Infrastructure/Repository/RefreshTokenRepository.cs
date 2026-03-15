using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Repository;
public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly string _connectionString;
    public RefreshTokenRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }
    public async Task SaveAsync(RefreshToken token)
    {
        const string sql = @"
        INSERT INTO RefreshTokens
        (TaiKhoanId, TokenHash, ExpiryDate, CreatedAt, IsRevoked)
        VALUES (@TaiKhoanId, @TokenHash, @ExpiryDate, GETUTCDATE(), 0)";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TaiKhoanId", token.TaiKhoanId);
        cmd.Parameters.AddWithValue("@TokenHash", token.TokenHash);
        cmd.Parameters.AddWithValue("@ExpiryDate", token.ExpiryDate);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<RefreshToken?> GetAsync(string tokenHash)
    {
        const string sql = @"SELECT Id, TaiKhoanId, TokenHash, ExpiryDate, CreatedAt, IsRevoked
                             FROM RefreshTokens
                             WHERE TokenHash = @TokenHash";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TokenHash", tokenHash);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync()) return null;
        return new RefreshToken(
            reader.GetInt32(0),
            reader.GetInt32(1),
            reader.GetString(2),
            reader.GetDateTime(3),
            reader.GetDateTime(4),
            reader.GetBoolean(5)
        );
    }
    public async Task RevokeAsync(string tokenHash)
    {
        const string sql = @"
        UPDATE RefreshTokens
        SET IsRevoked = 1
        WHERE TokenHash = @TokenHash";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TokenHash", tokenHash);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task RevokeAllAsync(int taiKhoanId)
    {
        const string sql = @"
        UPDATE RefreshTokens
        SET IsRevoked = 1
        WHERE TaiKhoanId = @TaiKhoanId";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TaiKhoanId", taiKhoanId);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
}
