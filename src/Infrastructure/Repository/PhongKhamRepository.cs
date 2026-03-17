using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class PhongKhamRepository : IPhongKhamRepository
{
    private readonly string _connectionString;
    public PhongKhamRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseSelectLite =
        @"SELECT PhongKhamID, TenPhongKham, TrangThai";

    private const string BaseSelectDetail =
        @"SELECT PhongKhamID, TenPhongKham, GioiThieu, DiaChi,
                 Hotline, Email, Website, HinhAnhBanner,
                 TrangThai, NgayTao, NgayCapNhat";
    public async Task<PhongKham?> GetByIdAsync(int id)
    {
        const string sql = @"
        SELECT PhongKhamID, TenPhongKham, GioiThieu, DiaChi,
               Hotline, Email, Website, HinhAnhBanner,
               TrangThai, NgayTao, NgayCapNhat
        FROM PhongKham
        WHERE PhongKhamID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<(List<PhongKhamListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var sql = $@"
        {BaseSelectLite}
        FROM PhongKham
        ORDER BY NgayTao DESC
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

        SELECT COUNT(*) FROM PhongKham";
        var list = new List<PhongKhamListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@PageSize", size);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<PhongKhamReadModel?> GetDetailAsync(int id)
    {
        var sql = $@"
        {BaseSelectDetail}
        FROM PhongKham
        WHERE PhongKhamID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<int> AddAsync(PhongKham pk)
    {
        const string sql = @"
        INSERT INTO PhongKham
        (TenPhongKham, GioiThieu, DiaChi, Hotline, Email, Website, HinhAnhBanner, TrangThai)
        OUTPUT INSERTED.PhongKhamID
        VALUES (@Ten, @GioiThieu, @DiaChi, @Hotline, @Email, @Website, @Banner, @TrangThai)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ten", pk.TenPhongKham);
        cmd.Parameters.AddWithValue("@GioiThieu", (object?)pk.GioiThieu ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DiaChi", (object?)pk.DiaChi ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Hotline", (object?)pk.Hotline ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Email", (object?)pk.Email ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Website", (object?)pk.Website ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Banner", (object?)pk.HinhAnhBanner ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TrangThai", pk.TrangThai);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(PhongKham pk)
    {
        const string sql = @"
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
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ten", pk.TenPhongKham);
        cmd.Parameters.AddWithValue("@GioiThieu", (object?)pk.GioiThieu ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DiaChi", (object?)pk.DiaChi ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Hotline", (object?)pk.Hotline ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Email", (object?)pk.Email ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Website", (object?)pk.Website ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Banner", (object?)pk.HinhAnhBanner ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TrangThai", pk.TrangThai);
        cmd.Parameters.AddWithValue("@Id", pk.PhongKhamID);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static PhongKham MapToEntity(SqlDataReader r)
    {
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
        return new PhongKham(
            r.GetInt32(id),
            r.GetString(ten),
            r.IsDBNull(gioiThieu) ? null : r.GetString(gioiThieu),
            r.IsDBNull(diaChi) ? null : r.GetString(diaChi),
            r.IsDBNull(hotline) ? null : r.GetString(hotline),
            r.IsDBNull(email) ? null : r.GetString(email),
            r.IsDBNull(website) ? null : r.GetString(website),
            r.IsDBNull(banner) ? null : r.GetString(banner),
            r.GetString(trangThai),
            r.GetDateTime(ngayTao),
            r.GetDateTime(ngayCapNhat)
        );
    }
    private static PhongKhamListReadModel MapToLiteDTO(SqlDataReader r)
    {
        var id = r.GetOrdinal("PhongKhamID");
        var ten = r.GetOrdinal("TenPhongKham");
        var trangThai = r.GetOrdinal("TrangThai");
        return new PhongKhamListReadModel
        {
            PhongKhamID = r.GetInt32(id),
            TenPhongKham = r.GetString(ten),
            TrangThai = r.GetString(trangThai)
        };
    }
    private static PhongKhamReadModel MapToDetailDTO(SqlDataReader r)
    {
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
        return new PhongKhamReadModel
        {
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