using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class BaiVietRepository : IBaiVietRepository
{
    private readonly string _connectionString;

    public BaiVietRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<int> AddAsync(BaiViet bv)
    {
        const string sql = @"
            INSERT INTO BaiViet (TieuDe, TomTat, NoiDung, HinhAnh, TacGiaID, LoaiBenhID, LuotXem, NgayDang, NgayCapNhat)
            OUTPUT INSERTED.BaiVietID
            VALUES (@TieuDe, @TomTat, @NoiDung, @HinhAnh, @TacGiaID, @LoaiBenhID, @LuotXem, @NgayDang, @NgayCapNhat)";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TieuDe", bv.TieuDe);
        cmd.Parameters.AddWithValue("@TomTat", bv.TomTat);
        cmd.Parameters.AddWithValue("@NoiDung", bv.NoiDung);
        cmd.Parameters.AddWithValue("@HinhAnh", bv.HinhAnh ?? "");
        cmd.Parameters.AddWithValue("@TacGiaID", bv.TacGiaID);
        cmd.Parameters.AddWithValue("@LoaiBenhID", bv.LoaiBenhID);
        cmd.Parameters.AddWithValue("@LuotXem", bv.LuotXem);
        cmd.Parameters.AddWithValue("@NgayDang", bv.NgayDang);
        cmd.Parameters.AddWithValue("@NgayCapNhat", bv.NgayCapNhat);

        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task<BaiViet?> GetByIdAsync(int id)
    {
        const string sql = @"SELECT BaiVietID, TieuDe, TomTat, NoiDung, HinhAnh, TacGiaID, LoaiBenhID, LuotXem, NgayDang, NgayCapNhat 
                            FROM BaiViet WHERE BaiVietID = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? Map(reader) : null;
    }

    public async Task<List<BaiViet>> GetAllAsync()
    {
        const string sql = @"SELECT BaiVietID, TieuDe, TomTat, NoiDung, HinhAnh, TacGiaID, LoaiBenhID, LuotXem, NgayDang, NgayCapNhat 
                            FROM BaiViet ORDER BY NgayDang DESC";
        var list = new List<BaiViet>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            list.Add(Map(reader));

        return list;
    }
    public async Task<List<BaiViet>> GetByLuotXemAsync()
    {
        const string sql = @"SELECT BaiVietID, TieuDe, TomTat, NoiDung, HinhAnh, TacGiaID, LoaiBenhID, LuotXem, NgayDang, NgayCapNhat 
                            FROM BaiViet ORDER BY LuotXem DESC";
        var list = new List<BaiViet>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            list.Add(Map(reader));

        return list;
    }
    public async Task<List<BaiViet>> GetByLoaiBenhAsync(int loaiBenhID)
    {
        const string sql = "SELECT * FROM BaiViet WHERE LoaiBenhID = @loaiBenhID ORDER BY LuotXem DESC";
        var list = new List<BaiViet>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@loaiBenhID", loaiBenhID);
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            list.Add(Map(reader));

        return list;
    }
    public async Task UpdateAsync(BaiViet bv)
    {
        const string sql = @"
            UPDATE BaiViet
            SET TieuDe=@TieuDe, TomTat=@TomTat, NoiDung=@NoiDung, HinhAnh=@HinhAnh,
                LoaiBenhID=@LoaiBenhID, LuotXem=@LuotXem, NgayCapNhat=@NgayCapNhat
            WHERE BaiVietID=@Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@Id", bv.BaiVietID);
        cmd.Parameters.AddWithValue("@TieuDe", bv.TieuDe);
        cmd.Parameters.AddWithValue("@TomTat", bv.TomTat);
        cmd.Parameters.AddWithValue("@NoiDung", bv.NoiDung);
        cmd.Parameters.AddWithValue("@HinhAnh", bv.HinhAnh ?? "");
        cmd.Parameters.AddWithValue("@LoaiBenhID", bv.LoaiBenhID);
        cmd.Parameters.AddWithValue("@LuotXem", bv.LuotXem);
        cmd.Parameters.AddWithValue("@NgayCapNhat", bv.NgayCapNhat);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static BaiViet Map(SqlDataReader r)
    {
        return new BaiViet(
            r.GetInt32(0),
            r.GetString(1),
            r.GetString(2),
            r.GetString(3),
            r.IsDBNull(4) ? "" : r.GetString(4),
            r.GetInt32(5),
            r.GetInt32(6),
            r.GetInt32(7),
            r.GetDateTime(8),
            r.GetDateTime(9)
        );
    }
}
