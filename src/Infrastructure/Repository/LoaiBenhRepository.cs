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

    private const string BaseSelectLite =
    @"SELECT LoaiBenhID, TenBenh, NhomBenh, MucDoNghiemTrong";

    private const string BaseSelectDetail =
    @"SELECT LoaiBenhID, TenBenh, TenKhoaHoc, NhomBenh,
	         MoTa, DoPhoBien, MucDoNghiemTrong, NgayTao";
    public async Task<int> AddAsync(LoaiBenh lb)
    {
        const string sql =
        @"INSERT INTO LoaiBenh
		  (TenBenh,TenKhoaHoc,NhomBenh,MoTa,DoPhoBien,MucDoNghiemTrong)
		  OUTPUT INSERTED.LoaiBenhID
		  VALUES(@TenBenh,@TenKhoaHoc,@NhomBenh,@MoTa,@DoPhoBien,@MucDoNghiemTrong)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TenBenh", lb.TenBenh);
        cmd.Parameters.AddWithValue("@TenKhoaHoc", (object?)lb.TenKhoaHoc ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NhomBenh", (object?)lb.NhomBenh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@MoTa", (object?)lb.MoTa ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DoPhoBien", (object?)lb.DoPhoBien ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@MucDoNghiemTrong", (object?)lb.MucDoNghiemTrong ?? DBNull.Value);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(LoaiBenh lb)
    {
        const string sql =
        @"UPDATE LoaiBenh
		  SET TenBenh=@TenBenh,
		      TenKhoaHoc=@TenKhoaHoc,
		      NhomBenh=@NhomBenh,
		      MoTa=@MoTa,
		      DoPhoBien=@DoPhoBien,
		      MucDoNghiemTrong=@MucDoNghiemTrong
		  WHERE LoaiBenhID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", lb.LoaiBenhID);
        cmd.Parameters.AddWithValue("@TenBenh", lb.TenBenh);
        cmd.Parameters.AddWithValue("@TenKhoaHoc", (object?)lb.TenKhoaHoc ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NhomBenh", (object?)lb.NhomBenh ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@MoTa", (object?)lb.MoTa ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@DoPhoBien", (object?)lb.DoPhoBien ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@MucDoNghiemTrong", (object?)lb.MucDoNghiemTrong ?? DBNull.Value);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<LoaiBenh?> GetByIdAsync(int id)
    {
        const string sql =
        @"SELECT LoaiBenhID,TenBenh,TenKhoaHoc,NhomBenh,
		         MoTa,DoPhoBien,MucDoNghiemTrong,NgayTao
		  FROM LoaiBenh
		  WHERE LoaiBenhID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<LoaiBenhReadModel?> GetDetailAsync(int id)
    {
        var sql =
        $@"{BaseSelectDetail}
		   FROM LoaiBenh
		   WHERE LoaiBenhID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<(List<LoaiBenhListReadModel>, int)> GetPagedAsync(int page, int size)
    {
        var sql =
        $@"{BaseSelectLite}
		   FROM LoaiBenh
		   ORDER BY TenBenh
		   OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
		   SELECT COUNT(*) FROM LoaiBenh";
        var list = new List<LoaiBenhListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@PageSize", size);
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
        var sql =
        $@"{BaseSelectLite}
		   FROM LoaiBenh
		   WHERE TenBenh LIKE @Keyword OR TenKhoaHoc LIKE @Keyword
		   ORDER BY TenBenh
		   OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
		   SELECT COUNT(*)
		   FROM LoaiBenh
		   WHERE TenBenh LIKE @Keyword OR TenKhoaHoc LIKE @Keyword";
        var list = new List<LoaiBenhListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
        cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
        cmd.Parameters.AddWithValue("@PageSize", size);
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
        const string sql =
        @"SELECT LoaiBenhID,TenBenh
		  FROM LoaiBenh
		  ORDER BY TenBenh";
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
        const string sql =
        @"SELECT TenBenh
		  FROM LoaiBenh
		  WHERE LoaiBenhID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        return await cmd.ExecuteScalarAsync() as string;
    }
    public async Task<IEnumerable<LoaiBenh>> GetAllAsync()
    {
        const string sql =
        $@"{BaseSelectDetail}
        FROM LoaiBenh";
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
        var loaiBenhID = r.GetOrdinal("LoaiBenhID");
        var tenBenh = r.GetOrdinal("TenBenh");
        var tenKhoaHoc = r.GetOrdinal("TenKhoaHoc");
        var nhomBenh = r.GetOrdinal("NhomBenh");
        var moTa = r.GetOrdinal("MoTa");
        var doPhoBien = r.GetOrdinal("DoPhoBien");
        var mucDoNghiemTrong = r.GetOrdinal("MucDoNghiemTrong");
        var ngayTao = r.GetOrdinal("NgayTao");

        return new LoaiBenh(
            r.GetInt32(loaiBenhID),
            r.GetString(tenBenh),
            r.IsDBNull(tenKhoaHoc) ? null : r.GetString(tenKhoaHoc),
            r.IsDBNull(nhomBenh) ? null : r.GetString(nhomBenh),
            r.IsDBNull(moTa) ? null : r.GetString(moTa),
            r.IsDBNull(doPhoBien) ? null : r.GetString(doPhoBien),
            r.IsDBNull(mucDoNghiemTrong) ? null : r.GetString(mucDoNghiemTrong),
            r.GetDateTime(ngayTao)
        );
    }
    private static LoaiBenhListReadModel MapToListDTO(SqlDataReader r)
    {
        var loaiBenhID = r.GetOrdinal("LoaiBenhID");
        var tenBenh = r.GetOrdinal("TenBenh");
        var nhomBenh = r.GetOrdinal("NhomBenh");
        var mucDoNghiemTrong = r.GetOrdinal("MucDoNghiemTrong");

        return new LoaiBenhListReadModel
        {
            LoaiBenhID = r.GetInt32(loaiBenhID),
            TenBenh = r.GetString(tenBenh),
            NhomBenh = r.IsDBNull(nhomBenh) ? null : r.GetString(nhomBenh),
            MucDoNghiemTrong = r.IsDBNull(mucDoNghiemTrong) ? null : r.GetString(mucDoNghiemTrong)
        };
    }
    private static LoaiBenhReadModel MapToDetailDTO(SqlDataReader r)
    {
        var loaiBenhID = r.GetOrdinal("LoaiBenhID");
        var tenBenh = r.GetOrdinal("TenBenh");
        var tenKhoaHoc = r.GetOrdinal("TenKhoaHoc");
        var nhomBenh = r.GetOrdinal("NhomBenh");
        var moTa = r.GetOrdinal("MoTa");
        var doPhoBien = r.GetOrdinal("DoPhoBien");
        var mucDoNghiemTrong = r.GetOrdinal("MucDoNghiemTrong");
        var ngayTao = r.GetOrdinal("NgayTao");

        return new LoaiBenhReadModel
        {
            LoaiBenhID = r.GetInt32(loaiBenhID),
            TenBenh = r.GetString(tenBenh),
            TenKhoaHoc = r.IsDBNull(tenKhoaHoc) ? null : r.GetString(tenKhoaHoc),
            NhomBenh = r.IsDBNull(nhomBenh) ? null : r.GetString(nhomBenh),
            MoTa = r.IsDBNull(moTa) ? null : r.GetString(moTa),
            DoPhoBien = r.IsDBNull(doPhoBien) ? null : r.GetString(doPhoBien),
            MucDoNghiemTrong = r.IsDBNull(mucDoNghiemTrong) ? null : r.GetString(mucDoNghiemTrong),
            NgayTao = r.GetDateTime(ngayTao)
        };
    }
}