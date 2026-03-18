using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class KhungGioKhamRepository : IKhungGioKhamRepository
{
    private readonly string _connectionString;

    public KhungGioKhamRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseFrom =
    @"FROM KhungGioKham";

    private const string BaseSelectList =
    @"SELECT KhungGioID,
             CaLamViec,
             GioBatDau,
             GioKetThuc,
             TenKhung";
    private const string BaseSelectEntity =
    @"SELECT KhungGioID,
             CaLamViec,
             GioBatDau,
             GioKetThuc,
             TenKhung";
    public async Task<List<KhungGioKhamListReadModel>> GetAllAsync()
    {
        var sql = $@"
        {BaseSelectList}
        {BaseFrom}
        ORDER BY CaLamViec, GioBatDau";
        var list = new List<KhungGioKhamListReadModel>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        return list;
    }
    public async Task<KhungGioKham?> GetByIdAsync(int id)
    {
        var sql = $@"
        {BaseSelectEntity}
        {BaseFrom}
        WHERE KhungGioID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync()
            ? MapToEntity(reader)
            : null;
    }
    public async Task<int> CountKhungGioKhamAsync()
    {
        var sql = $@"
        SELECT COUNT(*)
        {BaseFrom}";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task<List<int>> GetKhungGioIdsByCaLamViecAsync(int caLamViec)
    {
        var sql = $@"
        SELECT KhungGioID
        {BaseFrom}
        WHERE CaLamViec = @CaLamViec
        ORDER BY GioBatDau";
        var list = new List<int>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CaLamViec", caLamViec);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(reader.GetInt32(0));

        return list;
    }
    public async Task<KhungGioKhamReadModel?> GetDetailAsync(int id)
    {
        var sql = $@"
        {BaseSelectList}
        {BaseFrom}
        WHERE KhungGioID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync()
            ? MapToDetailDTO(reader)
            : null;
    }
    public async Task<int> AddAsync(KhungGioKham kg)
    {
        const string sql =
        @"INSERT INTO KhungGioKham
          (CaLamViec, GioBatDau, GioKetThuc, TenKhung)
          VALUES (@CaLamViec, @GioBatDau, @GioKetThuc, @TenKhung);
          SELECT SCOPE_IDENTITY();";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@CaLamViec", kg.CaLamViec);
        cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
        cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
        cmd.Parameters.AddWithValue("@TenKhung", (object?)kg.TenKhung ?? DBNull.Value);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(KhungGioKham kg)
    {
        const string sql =
        @"UPDATE KhungGioKham
          SET CaLamViec = @CaLamViec,
              GioBatDau = @GioBatDau,
              GioKetThuc = @GioKetThuc,
              TenKhung = @TenKhung
          WHERE KhungGioID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", kg.KhungGioID);
        cmd.Parameters.AddWithValue("@CaLamViec", kg.CaLamViec);
        cmd.Parameters.AddWithValue("@GioBatDau", kg.GioBatDau);
        cmd.Parameters.AddWithValue("@GioKetThuc", kg.GioKetThuc);
        cmd.Parameters.AddWithValue("@TenKhung", (object?)kg.TenKhung ?? DBNull.Value);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
    {
        var sql = $@"
        SELECT KhungGioID, TenKhung
        {BaseFrom}
        ORDER BY TenKhung";
        var list = new List<(int, string)>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add((
                reader.GetInt32(reader.GetOrdinal("KhungGioID")),
                reader.GetString(reader.GetOrdinal("TenKhung"))
            ));
        }
        return list;
    }
    private static KhungGioKham MapToEntity(SqlDataReader r)
    {
        var id = r.GetOrdinal("KhungGioID");
        var ca = r.GetOrdinal("CaLamViec");
        var batDau = r.GetOrdinal("GioBatDau");
        var ketThuc = r.GetOrdinal("GioKetThuc");
        var ten = r.GetOrdinal("TenKhung");
        return new KhungGioKham(
            r.GetInt32(id),
            r.GetInt32(ca),
            r.GetTimeSpan(batDau),
            r.GetTimeSpan(ketThuc),
            r.IsDBNull(ten) ? null : r.GetString(ten)
        );
    }
    private static KhungGioKhamListReadModel MapToListDTO(SqlDataReader r)
    {
        var khungGioID = r.GetOrdinal("KhungGioID");
        var caLamViec = r.GetOrdinal("CaLamViec");
        var gioBatDau = r.GetOrdinal("GioBatDau");
        var gioKetThuc = r.GetOrdinal("GioKetThuc");
        var tenKhung = r.GetOrdinal("TenKhung");
        return new KhungGioKhamListReadModel
        {
            KhungGioID = r.GetInt32(khungGioID),
            CaLamViec = r.GetInt32(caLamViec),
            GioBatDau = r.GetTimeSpan(gioBatDau),
            GioKetThuc = r.GetTimeSpan(gioKetThuc),
            TenKhung = r.IsDBNull(tenKhung) ? null : r.GetString(tenKhung)
        };
    }
    private static KhungGioKhamReadModel MapToDetailDTO(SqlDataReader r)
    {
        var khungGioID = r.GetOrdinal("KhungGioID");
        var caLamViec = r.GetOrdinal("CaLamViec");
        var gioBatDau = r.GetOrdinal("GioBatDau");
        var gioKetThuc = r.GetOrdinal("GioKetThuc");
        var tenKhung = r.GetOrdinal("TenKhung");

        return new KhungGioKhamReadModel
        {
            KhungGioID = r.GetInt32(khungGioID),
            CaLamViec = r.GetInt32(caLamViec),
            GioBatDau = r.GetTimeSpan(gioBatDau),
            GioKetThuc = r.GetTimeSpan(gioKetThuc),
            TenKhung = r.IsDBNull(tenKhung) ? null : r.GetString(tenKhung)
        };
    }
}