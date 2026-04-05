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
            INSERT INTO OTP (TaiKhoanID, MaOTP, ThoiHanHetHan, ConHieuLuc)
            OUTPUT INSERTED.OtpID
            VALUES (@TaiKhoanID, @MaOTP, @ThoiHanHetHan, @ConHieuLuc)";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = otp.TaiKhoanID;
        cmd.Parameters.Add("@MaOTP", SqlDbType.NVarChar, 6).Value = otp.MaOTP;
        cmd.Parameters.Add("@ThoiHanHetHan", SqlDbType.DateTime).Value = otp.ThoiHanHetHan;
        cmd.Parameters.Add("@ConHieuLuc", SqlDbType.Bit).Value = otp.ConHieuLuc;

        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }

    public async Task<Otp?> GetValidOtpAsync(int taiKhoanID, string maOTP)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = @"
            SELECT TOP 1 OtpID, TaiKhoanID, MaOTP, ThoiHanHetHan, ConHieuLuc
            FROM OTP
            WHERE TaiKhoanID = @TaiKhoanID
              AND MaOTP = @MaOTP
              AND ConHieuLuc = 1
              AND ThoiHanHetHan > GETUTCDATE()
            ORDER BY OtpID DESC";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = taiKhoanID;
        cmd.Parameters.Add("@MaOTP", SqlDbType.NVarChar, 6).Value = maOTP;

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync())
            return MapToEntity(reader);

        return null;
    }

    public async Task InvalidateAllAsync(int taiKhoanID)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();

        var sql = "UPDATE OTP SET ConHieuLuc = 0 WHERE TaiKhoanID = @TaiKhoanID AND ConHieuLuc = 1";

        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TaiKhoanID", SqlDbType.Int).Value = taiKhoanID;

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
        r.GetInt32(r.GetOrdinal("TaiKhoanID")),
        r.GetString(r.GetOrdinal("MaOTP")),
        r.GetDateTime(r.GetOrdinal("ThoiHanHetHan")),
        r.GetBoolean(r.GetOrdinal("ConHieuLuc"))
    );
}