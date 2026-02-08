using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class LieuTrinhDieuTriRepository : ILieuTrinhDieuTriRepository
{
    private readonly string _connectionString;

    public LieuTrinhDieuTriRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<LieuTrinhDieuTri?> GetByIdAsync(int lieuTrinhID)
    {
        const string sql = @"
            SELECT LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
                   TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
            FROM LieuTrinhDieuTri
            WHERE LieuTrinhID = @LieuTrinhID";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@LieuTrinhID", lieuTrinhID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }

    public async Task<LieuTrinhDieuTri?> GetByBenhNhanIdAsync(int benhNhanID)
    {
        const string sql = @"
            SELECT TOP 1 LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
                   TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
            FROM LieuTrinhDieuTri
            WHERE BenhNhanID = @BenhNhanID
            ORDER BY NgayBatDau DESC";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? MapToEntity(reader) : null;
    }

    public async Task<int?> GetIdByBenhNhanIdAsync(int benhNhanID)
    {
        const string sql = @"
            SELECT TOP 1 LieuTrinhID
            FROM LieuTrinhDieuTri
            WHERE BenhNhanID = @BenhNhanID
            ORDER BY NgayBatDau DESC";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanID);

        await conn.OpenAsync();
        var result = await cmd.ExecuteScalarAsync();

        return result == null ? null : (int)result;
    }

    public async Task<List<LieuTrinhDieuTri>> GetAllAsync()
    {
        const string sql = @"
            SELECT LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
                   TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
            FROM LieuTrinhDieuTri";

        var list = new List<LieuTrinhDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }

        return list;
    }

    public async Task<List<LieuTrinhDieuTri>> LocBatDauAsync(DateTime ngay, string trangThai)
    {
        const string sql = @"
            SELECT LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
                   TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
            FROM LieuTrinhDieuTri
            WHERE NgayBatDau = @Ngay
              AND TrangThai = @TrangThai";

        var list = new List<LieuTrinhDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ngay", ngay);
        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }

        return list;
    }
    public async Task<List<LieuTrinhDieuTri>> LocKetThucAsync(DateTime ngay, string trangThai)
    {
        const string sql = @"
            SELECT LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
                   TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
            FROM LieuTrinhDieuTri
            WHERE NgayKetThuc = @Ngay
              AND TrangThai = @TrangThai";

        var list = new List<LieuTrinhDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ngay", ngay);
        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }

        return list;
    }
    public async Task<List<LieuTrinhDieuTri>> GetListByBenhNhanAsync(int benhNhanID)
    {
        const string sql = @"
            SELECT LieuTrinhID, BenhNhanID, PhienKhamID, TenLieuTrinh,
                   TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc
            FROM LieuTrinhDieuTri
            WHERE BenhNhanID = @BenhNhanID";

        var list = new List<LieuTrinhDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BenhNhanID", benhNhanID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(MapToEntity(reader));
        }

        return list;
    }

    public async Task<int> AddAsync(LieuTrinhDieuTri lieuTrinh)
    {
        const string sql = @"
            INSERT INTO LieuTrinhDieuTri
            (BenhNhanID, PhienKhamID, TenLieuTrinh, TongSoBuoi, GhiChu, NgayBatDau, NgayKetThuc)
            OUTPUT INSERTED.LieuTrinhID
            VALUES
            (@BenhNhanID, @PhienKhamID, @TenLieuTrinh, @TongSoBuoi, @GhiChu, @NgayBatDau, @NgayKetThuc)";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@BenhNhanID", lieuTrinh.BenhNhanID);
        cmd.Parameters.AddWithValue("@PhienKhamID", lieuTrinh.PhienKhamID);
        cmd.Parameters.AddWithValue("@TenLieuTrinh", lieuTrinh.TenLieuTrinh);
        cmd.Parameters.AddWithValue("@TongSoBuoi", lieuTrinh.TongSoBuoi);
        cmd.Parameters.AddWithValue("@GhiChu", (object?)lieuTrinh.GhiChu ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NgayBatDau", lieuTrinh.NgayBatDau);
        cmd.Parameters.AddWithValue("@NgayKetThuc", lieuTrinh.NgayKetThuc);

        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }
    public async Task UpdateAsync(LieuTrinhDieuTri lieuTrinh)
    {
        const string sql = @"
            UPDATE LieuTrinhDieuTri
            SET TenLieuTrinh = @TenLieuTrinh,
                TongSoBuoi = @TongSoBuoi,
                NgayBatDau = @NgayBatDau,
                NgayKetThuc = @NgayKetThuc
            WHERE LieuTrinhID = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@TenLieuTrinh", lieuTrinh.TenLieuTrinh);
        cmd.Parameters.AddWithValue("@TongSoBuoi", lieuTrinh.TongSoBuoi);
        cmd.Parameters.AddWithValue("@NgayBatDau", lieuTrinh.NgayBatDau);
        cmd.Parameters.AddWithValue("@NgayKetThuc", lieuTrinh.NgayKetThuc);
        cmd.Parameters.AddWithValue("@Id", lieuTrinh.LieuTrinhID);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }
    public async Task UpdateTrangThaiAsync(LieuTrinhDieuTri lieuTrinh)
    {
        const string sql = @"
            UPDATE LieuTrinhDieuTri
            SET TrangThai = @TrangThai,
                GhiChu = @GhiChu
            WHERE LieuTrinhID = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TrangThai", lieuTrinh.TrangThai ?? "");
        cmd.Parameters.AddWithValue("@GhiChu", (object?)lieuTrinh.GhiChu ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", lieuTrinh.LieuTrinhID);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static LieuTrinhDieuTri MapToEntity(SqlDataReader reader)
    {
        return new LieuTrinhDieuTri(
            lieuTrinhID: reader.GetInt32(0),
            benhNhanID: reader.GetInt32(1),
            phienKhamID: reader.GetInt32(2),
            tenLieuTrinh: reader.GetString(3),
            tongSoBuoi: reader.GetInt32(4),
            trangThai: reader.IsDBNull(5) ? null : reader.GetString(5),
            ghiChu: reader.IsDBNull(6) ? null : reader.GetString(6),
            ngayBatDau: reader.GetDateTime(7),
            ngayKetThuc: reader.GetDateTime(8)
        );
    }
}
