using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class TaiKhamRepository : ITaiKhamRepository
{
    private readonly string _connectionString;

    public TaiKhamRepository(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new ArgumentNullException("Connection string not found");
    }
    public async Task<List<TaiKham>> GetAllAsync()
    {
        const string sql = "SELECT TaiKhamId, PhienKhamId, BenhNhanId, NgayDuKien, LyDo, TrangThai, CaKhamId, NgayTao FROM TaiKham";
        var list = new List<TaiKham>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        
        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            list.Add(Map(reader));
        return list;
    }
    public async Task<int> AddAsync(TaiKham taiKham)
    {
        const string sql = @"
            INSERT INTO TaiKham
            (PhienKhamId, BenhNhanId, NgayDuKien, LyDo, TrangThai, CaKhamId, NgayTao)
            OUTPUT INSERTED.TaiKhamId
            VALUES
            (@PhienKhamId, @BenhNhanId, @NgayDuKien, @LyDo, @TrangThai, @CaKhamId, @NgayTao)";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@PhienKhamId", taiKham.PhienKhamId);
        cmd.Parameters.AddWithValue("@BenhNhanId", taiKham.BenhNhanId);
        cmd.Parameters.AddWithValue("@NgayDuKien", taiKham.NgayDuKien);
        cmd.Parameters.AddWithValue("@LyDo", taiKham.LyDo);
        cmd.Parameters.AddWithValue("@TrangThai", taiKham.TrangThai);
        cmd.Parameters.AddWithValue("@CaKhamId", (object?)taiKham.CaKhamId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@NgayTao", taiKham.NgayTao);

        await conn.OpenAsync();
        return (int)await cmd.ExecuteScalarAsync();
    }

    public async Task<TaiKham?> GetByIdAsync(int id)
    {
        const string sql = "SELECT * FROM TaiKham WHERE TaiKhamId = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@Id", id);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        return await reader.ReadAsync() ? Map(reader) : null;
    }

    public async Task<List<TaiKham>> GetByBenhNhanAsync(int benhNhanId)
    {
        const string sql = "SELECT * FROM TaiKham WHERE BenhNhanId = @BenhNhanId";

        var list = new List<TaiKham>();
        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@BenhNhanId", benhNhanId);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            list.Add(Map(reader));

        return list;
    }

    public async Task UpdateAsync(TaiKham taiKham)
    {
        const string sql = @"
            UPDATE TaiKham
            SET NgayDuKien = @NgayDuKien,
                LyDo = @LyDo,
                TrangThai = @TrangThai,
                CaKhamId = @CaKhamId
            WHERE TaiKhamId = @Id";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@NgayDuKien", taiKham.NgayDuKien);
        cmd.Parameters.AddWithValue("@LyDo", taiKham.LyDo);
        cmd.Parameters.AddWithValue("@TrangThai", taiKham.TrangThai);
        cmd.Parameters.AddWithValue("@CaKhamId", (object?)taiKham.CaKhamId ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@Id", taiKham.TaiKhamId);

        await conn.OpenAsync();
        await cmd.ExecuteNonQueryAsync();
    }

    private static TaiKham Map(SqlDataReader reader)
    {
        return new TaiKham(
            taiKhamId: reader.GetInt32(reader.GetOrdinal("TaiKhamId")),
            phienKhamId: reader.GetInt32(reader.GetOrdinal("PhienKhamId")),
            benhNhanId: reader.GetInt32(reader.GetOrdinal("BenhNhanId")),
            ngayDuKien: reader.GetDateTime(reader.GetOrdinal("NgayDuKien")),
            lyDo: reader.GetString(reader.GetOrdinal("LyDo")),
            trangThai: reader.GetString(reader.GetOrdinal("TrangThai")),
            caKhamId: reader.IsDBNull(reader.GetOrdinal("CaKhamId"))
                ? null
                : reader.GetInt32(reader.GetOrdinal("CaKhamId")),
            ngayTao: reader.GetDateTime(reader.GetOrdinal("NgayTao"))
        );
    }
}
