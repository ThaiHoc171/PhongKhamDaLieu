using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Repositories;
public class LoaiBenhRepository : ILoaiBenhRepository
{
    private readonly string _connectionString;
    public LoaiBenhRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseFrom = "FROM LoaiBenh";
    private const string BaseSelectLite =
    @"SELECT LoaiBenhID, TenBenh, NhomBenh, MucDoNghiemTrong";
    private const string BaseSelectDetail =
    @"SELECT LoaiBenhID, TenBenh, TenKhoaHoc, NhomBenh,
             MoTa, DoPhoBien, MucDoNghiemTrong, NgayTao";
    public async Task<int> AddAsync(LoaiBenh lb)
    {
        const string sql =
        @"INSERT INTO LoaiBenh
          (TenBenh, TenKhoaHoc, NhomBenh, MoTa, DoPhoBien, MucDoNghiemTrong)
          OUTPUT INSERTED.LoaiBenhID
          VALUES (@TenBenh, @TenKhoaHoc, @NhomBenh, @MoTa, @DoPhoBien, @MucDoNghiemTrong)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TenBenh", System.Data.SqlDbType.NVarChar).Value = lb.TenBenh;
        cmd.Parameters.Add("@TenKhoaHoc", System.Data.SqlDbType.NVarChar).Value = (object?)lb.TenKhoaHoc ?? DBNull.Value;
        cmd.Parameters.Add("@NhomBenh", System.Data.SqlDbType.NVarChar).Value = (object?)lb.NhomBenh ?? DBNull.Value;
        cmd.Parameters.Add("@MoTa", System.Data.SqlDbType.NVarChar).Value = (object?)lb.MoTa ?? DBNull.Value;
        cmd.Parameters.Add("@DoPhoBien", System.Data.SqlDbType.NVarChar).Value = (object?)lb.DoPhoBien ?? DBNull.Value;
        cmd.Parameters.Add("@MucDoNghiemTrong", System.Data.SqlDbType.NVarChar).Value = (object?)lb.MucDoNghiemTrong ?? DBNull.Value;
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(LoaiBenh lb)
    {
        const string sql =
        @"UPDATE LoaiBenh
          SET TenBenh = @TenBenh,
              TenKhoaHoc = @TenKhoaHoc,
              NhomBenh = @NhomBenh,
              MoTa = @MoTa,
              DoPhoBien = @DoPhoBien,
              MucDoNghiemTrong = @MucDoNghiemTrong
          WHERE LoaiBenhID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = lb.LoaiBenhID;
        cmd.Parameters.Add("@TenBenh", System.Data.SqlDbType.NVarChar).Value = lb.TenBenh;
        cmd.Parameters.Add("@TenKhoaHoc", System.Data.SqlDbType.NVarChar).Value = (object?)lb.TenKhoaHoc ?? DBNull.Value;
        cmd.Parameters.Add("@NhomBenh", System.Data.SqlDbType.NVarChar).Value = (object?)lb.NhomBenh ?? DBNull.Value;
        cmd.Parameters.Add("@MoTa", System.Data.SqlDbType.NVarChar).Value = (object?)lb.MoTa ?? DBNull.Value;
        cmd.Parameters.Add("@DoPhoBien", System.Data.SqlDbType.NVarChar).Value = (object?)lb.DoPhoBien ?? DBNull.Value;
        cmd.Parameters.Add("@MucDoNghiemTrong", System.Data.SqlDbType.NVarChar).Value = (object?)lb.MucDoNghiemTrong ?? DBNull.Value;
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<LoaiBenh?> GetByIdAsync(int id)
    {
        var sql = $@"{BaseSelectDetail} {BaseFrom} WHERE LoaiBenhID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<LoaiBenhReadModel?> GetDetailAsync(int id)
    {
        var sql = $@"{BaseSelectDetail} {BaseFrom} WHERE LoaiBenhID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<(List<LoaiBenhListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var sql = $@"
        {BaseSelectLite}
        {BaseFrom}
        ORDER BY TenBenh
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*) {BaseFrom}";
        var list = new List<LoaiBenhListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Offset", System.Data.SqlDbType.Int).Value = (page - 1) * size;
        cmd.Parameters.Add("@Size", System.Data.SqlDbType.Int).Value = size;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<(List<LoaiBenhListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
    {
        var sql = $@"
        {BaseSelectLite}
        {BaseFrom}
        WHERE (@Keyword IS NULL OR TenBenh LIKE @Keyword OR TenKhoaHoc LIKE @Keyword)
        ORDER BY TenBenh
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
        SELECT COUNT(*)
        {BaseFrom}
        WHERE (@Keyword IS NULL OR TenBenh LIKE @Keyword OR TenKhoaHoc LIKE @Keyword)";
        var list = new List<LoaiBenhListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Keyword", System.Data.SqlDbType.NVarChar)
            .Value = string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
        cmd.Parameters.Add("@Offset", System.Data.SqlDbType.Int).Value = (page - 1) * size;
        cmd.Parameters.Add("@Size", System.Data.SqlDbType.Int).Value = size;
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToListDTO(reader));
        if (await reader.NextResultAsync() && await reader.ReadAsync())
            total = reader.GetInt32(0);
        return (list, total);
    }
    public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
    {
        const string sql = @"SELECT LoaiBenhID, TenBenh FROM LoaiBenh ORDER BY TenBenh";
        var list = new List<(int, string)>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add((reader.GetInt32(0), reader.GetString(1)));
        return list;
    }
    public async Task<string?> GetTenBenhByIdAsync(int id)
    {
        const string sql = @"SELECT TenBenh FROM LoaiBenh WHERE LoaiBenhID = @Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
        await conn.OpenAsync();
        return await cmd.ExecuteScalarAsync() as string;
    }
    public async Task<IEnumerable<LoaiBenh>> GetAllAsync()
    {
        var sql = $@"{BaseSelectDetail} {BaseFrom}";
        var list = new List<LoaiBenh>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToEntity(reader));
        return list;
    }
    private static LoaiBenh MapToEntity(SqlDataReader r)
    {
        return new LoaiBenh(
            r.GetInt32(r.GetOrdinal("LoaiBenhID")),
            r.GetString(r.GetOrdinal("TenBenh")),
            r["TenKhoaHoc"] as string,
            r["NhomBenh"] as string,
            r["MoTa"] as string,
            r["DoPhoBien"] as string,
            r["MucDoNghiemTrong"] as string,
            r.GetDateTime(r.GetOrdinal("NgayTao"))
        );
    }
    private static LoaiBenhListReadModel MapToListDTO(SqlDataReader r)
    {
        return new LoaiBenhListReadModel
        {
            LoaiBenhID = r.GetInt32(r.GetOrdinal("LoaiBenhID")),
            TenBenh = r.GetString(r.GetOrdinal("TenBenh")),
            NhomBenh = r["NhomBenh"] as string,
            MucDoNghiemTrong = r["MucDoNghiemTrong"] as string
        };
    }
    private static LoaiBenhReadModel MapToDetailDTO(SqlDataReader r)
    {
        return new LoaiBenhReadModel
        {
            LoaiBenhID = r.GetInt32(r.GetOrdinal("LoaiBenhID")),
            TenBenh = r.GetString(r.GetOrdinal("TenBenh")),
            TenKhoaHoc = r["TenKhoaHoc"] as string,
            NhomBenh = r["NhomBenh"] as string,
            MoTa = r["MoTa"] as string,
            DoPhoBien = r["DoPhoBien"] as string,
            MucDoNghiemTrong = r["MucDoNghiemTrong"] as string,
            NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao"))
        };
    }
}