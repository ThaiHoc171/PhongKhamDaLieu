using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class PhongKhamRepository : IPhongKhamRepository
{
    private readonly string _connectionString;

    public PhongKhamRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    private const string BaseSelectDetail = @"
        SELECT PhongKhamID, TenPhongKham, GioiThieu, DiaChi,
               Hotline, Email, Website, HinhAnhBanner,
               TrangThai, NgayTao, NgayCapNhat
        FROM PhongKham";
    public async Task<PhongKham?> GetByIdAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail + " WHERE PhongKhamID = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToEntity(reader);
        return null;
    }

    public async Task<PhongKhamReadModel?> GetDetailAsync()
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = BaseSelectDetail;
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
            return MapToDetailDTO(reader);
        return null;
    }
    public async Task UpdateAsync(PhongKham pk)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"
        UPDATE PhongKham
        SET TenPhongKham = @Ten,
            GioiThieu = @GioiThieu,
            DiaChi = @DiaChi,
            Hotline = @Hotline,
            Email = @Email,
            Website = @Website,
            HinhAnhBanner = @Banner,
            TrangThai = @TrangThai,
            NgayCapNhat = GETDATE()
        WHERE PhongKhamID = @Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = pk.PhongKhamID;
        cmd.Parameters.Add("@Ten", SqlDbType.NVarChar).Value = pk.TenPhongKham;
        cmd.Parameters.Add("@GioiThieu", SqlDbType.NVarChar).Value = (object?)pk.GioiThieu ?? DBNull.Value;
        cmd.Parameters.Add("@DiaChi", SqlDbType.NVarChar).Value = (object?)pk.DiaChi ?? DBNull.Value;
        cmd.Parameters.Add("@Hotline", SqlDbType.NVarChar).Value = (object?)pk.Hotline ?? DBNull.Value;
        cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = (object?)pk.Email ?? DBNull.Value;
        cmd.Parameters.Add("@Website", SqlDbType.NVarChar).Value = (object?)pk.Website ?? DBNull.Value;
        cmd.Parameters.Add("@Banner", SqlDbType.NVarChar).Value = (object?)pk.HinhAnhBanner ?? DBNull.Value;
        cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = pk.TrangThai;
        await cmd.ExecuteNonQueryAsync();
    }
    private PhongKham MapToEntity(SqlDataReader r)
    {
        return new PhongKham(
            (int)r["PhongKhamID"],
            (string)r["TenPhongKham"],
            r["GioiThieu"] as string,
            r["DiaChi"] as string,
            r["Hotline"] as string,
            r["Email"] as string,
            r["Website"] as string,
            r["HinhAnhBanner"] as string,
            (string)r["TrangThai"],
            (DateTime)r["NgayTao"],
            (DateTime)r["NgayCapNhat"]
        );
    }
    private static PhongKhamReadModel MapToDetailDTO(SqlDataReader r) { 
        var id = r.GetOrdinal("PhongKhamID"); 
        var ten = r.GetOrdinal("TenPhongKham");
        var gioiThieu = r.GetOrdinal("GioiThieu"); 
        var diaChi = r.GetOrdinal("DiaChi"); 
        var hotline = r.GetOrdinal("Hotline"); 
        var email = r.GetOrdinal("Email"); 
        var website = r.GetOrdinal("Website"); 
        var banner = r.GetOrdinal("HinhAnhBanner"); 
        var trangThai = r.GetOrdinal("TrangThai"); 
        var ngayTao = r.GetOrdinal("NgayTao"); 
        var ngayCapNhat = r.GetOrdinal("NgayCapNhat"); 
        return new PhongKhamReadModel { 
            PhongKhamID = r.GetInt32(id), 
            TenPhongKham = r.GetString(ten), 
            GioiThieu = r.IsDBNull(gioiThieu) ? null : r.GetString(gioiThieu), 
            DiaChi = r.IsDBNull(diaChi) ? null : r.GetString(diaChi), 
            Hotline = r.IsDBNull(hotline) ? null : r.GetString(hotline), 
            Email = r.IsDBNull(email) ? null : r.GetString(email), 
            Website = r.IsDBNull(website) ? null : r.GetString(website), 
            HinhAnhBanner = r.IsDBNull(banner) ? null : r.GetString(banner), 
            TrangThai = r.GetString(trangThai), 
            NgayTao = r.GetDateTime(ngayTao),
            NgayCapNhat = r.GetDateTime(ngayCapNhat) 
        }; 
    }
}