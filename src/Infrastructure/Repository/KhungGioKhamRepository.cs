using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
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
    private const string BaseFrom = @"FROM KhungGioKham";
    private const string BaseSelect = @"
        SELECT KhungGioID,
               CaLamViec,
               GioBatDau,
               GioKetThuc,
               TenKhung";
    public async Task<List<KhungGioKhamListReadModel>> GetAllAsync()
    {
        var sql = $@"
            {BaseSelect}
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
            {BaseSelect}
            {BaseFrom}
            WHERE KhungGioID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<KhungGioKhamReadModel?> GetDetailAsync(int id)
    {
        var sql = $@"
            {BaseSelect}
            {BaseFrom}
            WHERE KhungGioID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<int> CountKhungGioKhamAsync()
    {
        var sql = $@"SELECT COUNT(*) {BaseFrom}";
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
        cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = caLamViec;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(reader.GetInt32(0));
        return list;
    }
    public async Task<int> AddAsync(KhungGioKham kg)
    {
        const string sql = @"
            INSERT INTO KhungGioKham (CaLamViec, GioBatDau, GioKetThuc)
            OUTPUT INSERTED.KhungGioID
            VALUES (@CaLamViec, @GioBatDau, @GioKetThuc)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = kg.CaLamViec;
        cmd.Parameters.Add("@GioBatDau", SqlDbType.Time).Value = kg.GioBatDau;
        cmd.Parameters.Add("@GioKetThuc", SqlDbType.Time).Value = kg.GioKetThuc;
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(KhungGioKham kg)
    {
        const string sql = @"
            UPDATE KhungGioKham
            SET CaLamViec = @CaLamViec,
                GioBatDau = @GioBatDau,
                GioKetThuc = @GioKetThuc
            WHERE KhungGioID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = kg.KhungGioID;
        cmd.Parameters.Add("@CaLamViec", SqlDbType.Int).Value = kg.CaLamViec;
        cmd.Parameters.Add("@GioBatDau", SqlDbType.Time).Value = kg.GioBatDau;
        cmd.Parameters.Add("@GioKetThuc", SqlDbType.Time).Value = kg.GioKetThuc;
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task DeleteAsync(int id)
    {
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"DELETE FROM KhungGioKham WHERE KhungGioID=@Id";
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
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
                reader.GetInt32(0),
                reader.GetString(1)
            ));
        }
        return list;
    }
    private static KhungGioKham MapToEntity(SqlDataReader r)
    {
        return new KhungGioKham(
            r.GetInt32(r.GetOrdinal("KhungGioID")),
            r.GetInt32(r.GetOrdinal("CaLamViec")),
            r.GetTimeSpan(r.GetOrdinal("GioBatDau")),
            r.GetTimeSpan(r.GetOrdinal("GioKetThuc")),
            r.IsDBNull(r.GetOrdinal("TenKhung")) ? null : r.GetString(r.GetOrdinal("TenKhung"))
        );
    }
    private static KhungGioKhamListReadModel MapToListDTO(SqlDataReader r)
    {
        return new KhungGioKhamListReadModel
        {
            KhungGioID = r.GetInt32(r.GetOrdinal("KhungGioID")),
            CaLamViec = r.GetInt32(r.GetOrdinal("CaLamViec")),
            GioBatDau = r.GetTimeSpan(r.GetOrdinal("GioBatDau")),
            GioKetThuc = r.GetTimeSpan(r.GetOrdinal("GioKetThuc")),
            TenKhung = r.IsDBNull(r.GetOrdinal("TenKhung")) ? null : r.GetString(r.GetOrdinal("TenKhung"))
        };
    }
    private static KhungGioKhamReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new KhungGioKhamReadModel
        {
            KhungGioID = r.GetInt32(r.GetOrdinal("KhungGioID")),
            CaLamViec = r.GetInt32(r.GetOrdinal("CaLamViec")),
            GioBatDau = r.GetTimeSpan(r.GetOrdinal("GioBatDau")),
            GioKetThuc = r.GetTimeSpan(r.GetOrdinal("GioKetThuc")),
            TenKhung = r.IsDBNull(r.GetOrdinal("TenKhung")) ? null : r.GetString(r.GetOrdinal("TenKhung"))
        };
    }
}