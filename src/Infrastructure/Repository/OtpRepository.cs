using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class OtpRepository : IOtpRepository
{
    private readonly string _connectionString;

    public OtpRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<int> AddAsync(Otp otp)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        INSERT INTO OTP (TaiKhoanID, Email, MaOTP, ThoiHanHetHan, ConHieuLuc)
        OUTPUT INSERTED.OtpID
        VALUES (@TaiKhoanID, @Email, @MaOTP, @ThoiHanHetHan, @ConHieuLuc)";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = (object?)otp.TaiKhoanID ?? DBNull.Value;
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = (object?)otp.Email ?? DBNull.Value;
        cmd.Parameters.Add("@MaOTP", SqlDbType.NVarChar, 6).Value = otp.MaOTP;
        cmd.Parameters.Add("@ThoiHanHetHan", SqlDbType.DateTime).Value = otp.ThoiHanHetHan;
        cmd.Parameters.Add("@ConHieuLuc", SqlDbType.Bit).Value = otp.ConHieuLuc;
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task<Otp?> GetValidOtpByTaiKhoanAsync(int taiKhoanID, string maOTP)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        SELECT TOP 1 OtpID, TaiKhoanID, Email, MaOTP, ThoiHanHetHan, ConHieuLuc
        FROM OTP
        WHERE TaiKhoanID = @TaiKhoanID AND MaOTP = @MaOTP
          AND ConHieuLuc = 1 AND ThoiHanHetHan > GETUTCDATE()
        ORDER BY OtpID DESC";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = taiKhoanID;
        cmd.Parameters.Add("@MaOTP", SqlDbType.NVarChar, 6).Value = maOTP;
        using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }

    public async Task<Otp?> GetValidOtpByEmailAsync(string email, string maOTP)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        SELECT TOP 1 OtpID, TaiKhoanID, Email, MaOTP, ThoiHanHetHan, ConHieuLuc
        FROM OTP
        WHERE Email = @Email AND MaOTP = @MaOTP
          AND ConHieuLuc = 1 AND ThoiHanHetHan > GETUTCDATE()
        ORDER BY OtpID DESC";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = email.Trim().ToLower();
        cmd.Parameters.Add("@MaOTP", SqlDbType.NVarChar, 6).Value = maOTP;
        using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }

    public async Task InvalidateAllByTaiKhoanAsync(int taiKhoanID)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = "UPDATE OTP SET ConHieuLuc = 0 WHERE TaiKhoanID = @TaiKhoanID AND ConHieuLuc = 1";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = taiKhoanID;
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task InvalidateAllByEmailAsync(string email)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = "UPDATE OTP SET ConHieuLuc = 0 WHERE Email = @Email AND ConHieuLuc = 1";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 255).Value = email.Trim().ToLower();
        await cmd.ExecuteNonQueryAsync();
    }

    public async Task InvalidateAsync(int otpID)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = "UPDATE OTP SET ConHieuLuc = 0 WHERE OtpID = @OtpID";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@OtpID", SqlDbType.Int).Value = otpID;
        await cmd.ExecuteNonQueryAsync();
    }

    private Otp MapToEntity(SqlDataReader r) => new Otp(
        r.GetInt32(r.GetOrdinal("OtpID")),
        r.IsDBNull(r.GetOrdinal("TaiKhoanID")) ? null : r.GetInt32(r.GetOrdinal("TaiKhoanID")),
        r.IsDBNull(r.GetOrdinal("Email")) ? null : r.GetString(r.GetOrdinal("Email")),
        r.GetString(r.GetOrdinal("MaOTP")),
        r.GetDateTime(r.GetOrdinal("ThoiHanHetHan")),
        r.GetBoolean(r.GetOrdinal("ConHieuLuc"))
    );
}