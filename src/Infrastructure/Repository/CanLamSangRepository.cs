using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace Infrastructure.Repository;
public class CanLamSangRepository : ICanLamSangRepository
{
    private readonly string _connectionString;
    public CanLamSangRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }
    private SqlConnection CreateConnection() => new(_connectionString);
    private const string BaseSelectLite =
    @"SELECT CanLamSangID, TenCLS, LoaiXetNghiem, TrangThai, NgayTao
      FROM CanLamSang";
    private const string BaseSelectDetail =
    @"SELECT CanLamSangID, TenCLS, MoTa, LoaiXetNghiem, TrangThai, NgayTao
      FROM CanLamSang";
    public async Task<CanLamSang?> GetByIdAsync(int id)
    {
        var sql = BaseSelectDetail + " WHERE CanLamSangID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }
    public async Task<(List<CanLamSangListReadModel>, int)>GetPagedAsync(int page, int size, string? loaiXetNghiem, string? trangThai)
    {
        var sql =
        $@"{BaseSelectLite}
           WHERE (@Loai IS NULL OR LoaiXetNghiem=@Loai)
             AND (@TrangThai IS NULL OR TrangThai=@TrangThai)
           ORDER BY NgayTao DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*)
           FROM CanLamSang
           WHERE (@Loai IS NULL OR LoaiXetNghiem=@Loai)
             AND (@TrangThai IS NULL OR TrangThai=@TrangThai)";
        var list = new List<CanLamSangListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Loai", (object?)loaiXetNghiem ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@TrangThai", (object?)trangThai ?? DBNull.Value);
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
    public async Task<(List<CanLamSangListReadModel>, int)>SearchPagedAsync(string keyword, int page, int size)
    {
        var sql =
        $@"{BaseSelectLite}
           WHERE TenCLS LIKE @Keyword
           ORDER BY NgayTao DESC
           OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
           SELECT COUNT(*)
           FROM CanLamSang
           WHERE TenCLS LIKE @Keyword";
        var list = new List<CanLamSangListReadModel>();
        int total = 0;
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
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
    public async Task<List<CanLamSangListReadModel>>GetByLoaiXetNghiemAsync(string loaiXetNghiem)
    {
        var sql =
        $@"{BaseSelectLite}
           WHERE LoaiXetNghiem=@Loai
           ORDER BY TenCLS";
        var list = new List<CanLamSangListReadModel>();
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Loai", loaiXetNghiem);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(MapToLiteDTO(reader));
        return list;
    }
    public async Task<CanLamSangReadModel?> GetDetailAsync(int id)
    {
        var sql = BaseSelectDetail + " WHERE CanLamSangID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
    }
    public async Task<int> AddAsync(CanLamSang cls)
    {
        const string sql =
        @"INSERT INTO CanLamSang
          (TenCLS, MoTa, LoaiXetNghiem, TrangThai)
          OUTPUT INSERTED.CanLamSangID
          VALUES (@TenCLS, @MoTa, @LoaiXetNghiem, @TrangThai)";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TenCLS", cls.TenCLS);
        cmd.Parameters.AddWithValue("@MoTa", (object?)cls.MoTa ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@LoaiXetNghiem", cls.LoaiXetNghiem);
        cmd.Parameters.AddWithValue("@TrangThai", cls.TrangThai);
        await conn.OpenAsync();
        return Convert.ToInt32(await cmd.ExecuteScalarAsync());
    }
    public async Task UpdateAsync(CanLamSang cls)
    {
        const string sql =
        @"UPDATE CanLamSang
          SET TenCLS=@TenCLS,
              MoTa=@MoTa,
              LoaiXetNghiem=@LoaiXetNghiem,
              TrangThai=@TrangThai
          WHERE CanLamSangID=@Id";
        await using var conn = CreateConnection();
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TenCLS", cls.TenCLS);
        cmd.Parameters.AddWithValue("@MoTa", (object?)cls.MoTa ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@LoaiXetNghiem", cls.LoaiXetNghiem);
        cmd.Parameters.AddWithValue("@TrangThai", cls.TrangThai);
        cmd.Parameters.AddWithValue("@Id", cls.CanLamSangID);
        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
    {
        var list = new List<(int Id, string Ten)>();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        var sql = @"SELECT CanLamSangID, TenCLS
                    FROM CanLamSang
                    ORDER BY TenCLS";
        using var cmd = new SqlCommand(sql, conn);
        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add((reader.GetInt32(0), reader.GetString(1)));
        }
        return list;
    }
    private static CanLamSang MapToEntity(SqlDataReader r)
    {
        var id = r.GetOrdinal("CanLamSangID");
        var ten = r.GetOrdinal("TenCLS");
        var mota = r.GetOrdinal("MoTa");
        var loai = r.GetOrdinal("LoaiXetNghiem");
        var trangThai = r.GetOrdinal("TrangThai");
        var ngay = r.GetOrdinal("NgayTao");
        return new CanLamSang(
            r.GetInt32(id),
            r.GetString(ten),
            r.IsDBNull(mota) ? null : r.GetString(mota),
            r.GetString(loai),
            r.GetDateTime(ngay),
            r.GetString(trangThai)
        );
    }
    private static CanLamSangListReadModel MapToLiteDTO(SqlDataReader r)
    {
        var id = r.GetOrdinal("CanLamSangID");
        var ten = r.GetOrdinal("TenCLS");
        var loai = r.GetOrdinal("LoaiXetNghiem");
        var trangThai = r.GetOrdinal("TrangThai");
        var ngay = r.GetOrdinal("NgayTao");
        return new CanLamSangListReadModel
        {
            CanLamSangID = r.GetInt32(id),
            TenCLS = r.GetString(ten),
            LoaiXetNghiem = r.GetString(loai),
            TrangThai = r.GetString(trangThai)
        };
    }
    private static CanLamSangReadModel MapToDetailDTO(SqlDataReader r)
    {
        var id = r.GetOrdinal("CanLamSangID");
        var ten = r.GetOrdinal("TenCLS");
        var mota = r.GetOrdinal("MoTa");
        var loai = r.GetOrdinal("LoaiXetNghiem");
        var trangThai = r.GetOrdinal("TrangThai");
        var ngay = r.GetOrdinal("NgayTao");
        return new CanLamSangReadModel
        {
            CanLamSangID = r.GetInt32(id),
            TenCLS = r.GetString(ten),
            MoTa = r.IsDBNull(mota) ? null : r.GetString(mota),
            LoaiXetNghiem = r.GetString(loai),
            TrangThai = r.GetString(trangThai),
            NgayTao = r.GetDateTime(ngay)
        };
    }
}