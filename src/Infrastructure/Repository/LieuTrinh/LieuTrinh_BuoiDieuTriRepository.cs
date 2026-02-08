using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repository;

public class LieuTrinh_BuoiDieuTriRepository : ILieuTrinh_BuoiDieuTriRepository
{
    private readonly string _connectionString;

    public LieuTrinh_BuoiDieuTriRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }

    public async Task<LieuTrinh_BuoiDieuTri?> GetByIdAsync(int buoiDieuTriID)
    {
        const string sql = @"SELECT BuoiDieuTriID, LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien, NgayThucHien, NhanVienID, TrangThai, GhiChu, HinhAnhJSON FROM LieuTrinh_BuoiDieuTri WHERE BuoiDieuTriID = @id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", buoiDieuTriID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        return await reader.ReadAsync() ? Map(reader) : null;
    }
    public async Task<List<LieuTrinh_BuoiDieuTri>> GetAllAsync()
    {
        const string sql = @"
            SELECT BuoiDieuTriID, LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien, NgayThucHien, NhanVienID, TrangThai, GhiChu, HinhAnhJSON FROM LieuTrinh_BuoiDieuTri";

        var list = new List<LieuTrinh_BuoiDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(Map(reader));
        }

        return list;
    }
    public async Task<List<LieuTrinh_BuoiDieuTri>> GetByLieuTrinhAsync(int lieuTrinhID)
    {
        const string sql = @"SELECT BuoiDieuTriID, LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien, NgayThucHien, NhanVienID, TrangThai, GhiChu, HinhAnhJSON FROM LieuTrinh_BuoiDieuTri WHERE LieuTrinhID = @id";
        var list = new List<LieuTrinh_BuoiDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", lieuTrinhID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
            list.Add(Map(reader));

        return list;
    }
    public async Task<List<LieuTrinh_BuoiDieuTri>> LocDuKienAsync(DateTime ngay, string trangThai)
    {
        const string sql = @"
            SELECT BuoiDieuTriID, LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien, NgayThucHien, NhanVienID, TrangThai, GhiChu, HinhAnhJSON
            FROM LieuTrinh_BuoiDieuTri
            WHERE NgayDuKien = @Ngay
              AND TrangThai = @TrangThai";

        var list = new List<LieuTrinh_BuoiDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ngay", ngay);
        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(Map(reader));
        }

        return list;
    }
    public async Task<List<LieuTrinh_BuoiDieuTri>> LocBatDauAsync(DateTime ngay, string trangThai)
    {
        const string sql = @"
            SELECT BuoiDieuTriID, LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien, NgayThucHien, NhanVienID, TrangThai, GhiChu, HinhAnhJSON
            FROM LieuTrinh_BuoiDieuTri
            WHERE NgayThucHien = @Ngay
              AND TrangThai = @TrangThai";

        var list = new List<LieuTrinh_BuoiDieuTri>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Ngay", ngay);
        cmd.Parameters.AddWithValue("@TrangThai", trangThai);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(Map(reader));
        }

        return list;
    }
    public async Task<bool> ExistsByCaKhamAsync(int caKhamID)
    {
        const string sql = @"SELECT 1 FROM LieuTrinh_BuoiDieuTri WHERE CaKhamID = @id";
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", caKhamID);

        await conn.OpenAsync();
        return await cmd.ExecuteScalarAsync() != null;
    }
    public async Task<int> CountBySoBuoiAsync(int lieuTrinhID)
    {
        const string sql = @"SELECT COUNT(SoBuoi) FROM LieuTrinh_BuoiDieuTri WHERE LieuTrinhID = @id";
        var list = new List<CaKham>();
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", lieuTrinhID);
        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }
    public async Task<int> AddAsync(LieuTrinh_BuoiDieuTri buoi)
    {
        const string sql = @"
            INSERT INTO LieuTrinh_BuoiDieuTri
            (LieuTrinhID, CaKhamID, SoBuoi, NgayDuKien)
            OUTPUT INSERTED.BuoiDieuTriID
            VALUES (@LieuTrinhID, @CaKhamID, @SoBuoi, @NgayDuKien)";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@LieuTrinhID", buoi.LieuTrinhID);
        cmd.Parameters.AddWithValue("@CaKhamID", buoi.CaKhamID);
        cmd.Parameters.AddWithValue("@SoBuoi", buoi.SoBuoi);
        cmd.Parameters.AddWithValue("@NgayDuKien", (object?)buoi.NgayDuKien ?? DBNull.Value);

        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task UpdateTrangThaiAsync(LieuTrinh_BuoiDieuTri buoi)
    {
        const string sql = @"
            UPDATE LieuTrinh_BuoiDieuTri
            SET TrangThai = @TrangThai,
                NhanVienID = @NhanVienID,
                NgayThucHien = @NgayThucHien,
                GhiChu = @GhiChu
            WHERE BuoiDieuTriID = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TrangThai", buoi.TrangThai);
        cmd.Parameters.AddWithValue("@NhanVienID", (object?)buoi.NhanVienID ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NgayThucHien", (object?)buoi.NgayThucHien ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@GhiChu", (object?)buoi.GhiChu ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", buoi.BuoiDieuTriID);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static LieuTrinh_BuoiDieuTri Map(SqlDataReader r)
    {
        return new LieuTrinh_BuoiDieuTri(
            buoiDieuTriID: r.GetInt32(0),
            lieuTrinhID: r.GetInt32(1),
            caKhamID: r.GetInt32(2),
            soBuoi: r.GetInt32(3),
            ngayDuKien: r.IsDBNull(4) ? null : r.GetDateTime(4),
            ngayThucHien: r.IsDBNull(5) ? null : r.GetDateTime(5),
            nhanVienID: r.IsDBNull(6) ? null : r.GetInt32(6),
            trangThai: r.GetString(7),
            ghiChu: r.IsDBNull(8) ? null : r.GetString(8),
            hinhAnhJSON: r.IsDBNull(9) ? null : r.GetString(9)
        );
    }
}
