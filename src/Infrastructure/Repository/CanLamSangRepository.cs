using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class CanLamSangRepository : ICanLamSangRepository
{
	private readonly string _connectionString;

	public CanLamSangRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")!;
	}

	public async Task<List<CanLamSang>> GetAllAsync()
	{
		const string sql = @"SELECT CanLamSangID, TenCLS, MoTa, LoaiXetNghiem, NgayTao, TrangThai FROM CanLamSang";

		var list = new List<CanLamSang>();

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

	public async Task<CanLamSang?> GetByIdAsync(int id)
	{
		const string sql = @"SELECT CanLamSangID, TenCLS, MoTa, LoaiXetNghiem, NgayTao,TrangThai
							FROM CanLamSang
							WHERE CanLamSangID = @id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task AddAsync(CanLamSang cls)
	{
		const string sql = @"INSERT INTO CanLamSang (TenCLS, MoTa, LoaiXetNghiem)
							 VALUES (@TenCLS, @MoTa, @LoaiXetNghiem)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TenCLS", cls.TenCLS);
		cmd.Parameters.AddWithValue("@MoTa", (object?)cls.MoTa ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@LoaiXetNghiem", cls.LoaiXetNghiem);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(CanLamSang cls)
	{
		const string sql = @"UPDATE CanLamSang 
							 SET TenCLS=@TenCLS, MoTa=@MoTa, LoaiXetNghiem=@LoaiXetNghiem, TrangThai=@TrangThai
							 WHERE CanLamSangID=@Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", cls.CanLamSangID);
		cmd.Parameters.AddWithValue("@TenCLS", cls.TenCLS);
		cmd.Parameters.AddWithValue("@MoTa", (object?)cls.MoTa ?? DBNull.Value);
		cmd.Parameters.AddWithValue("@LoaiXetNghiem", cls.LoaiXetNghiem);
		cmd.Parameters.AddWithValue("@TrangThai", cls.TrangThai);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
    public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
    {
        const string sql = @"
			SELECT CanLamSangID, TenCLS
			FROM CanLamSang
			ORDER BY TenCLS";

        var list = new List<(int, string)>();

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add((
                reader.GetInt32(reader.GetOrdinal("CanLamSangID")),
                reader.GetString(reader.GetOrdinal("TenCLS"))
            ));
        }

        return list;
    }
    private static CanLamSang MapToEntity(SqlDataReader r)
	{
		return new CanLamSang(
			id: r.GetInt32(0),
			tenCLS: r.GetString(1),
			moTa: r.IsDBNull(2) ? null : r.GetString(2),
			loaiXetNghiem: r.GetString(3),
			ngayTao: r.GetDateTime(4),
			trangThai: r.GetString(5)
		);
	}
}
