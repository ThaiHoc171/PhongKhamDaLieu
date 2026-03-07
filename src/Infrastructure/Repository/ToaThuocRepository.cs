using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class ToaThuocRepository : IToaThuocRepository
{
	private readonly string _connectionString;

	public ToaThuocRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
	?? throw new ArgumentNullException("Connection string not found");
	}

	public async Task<int> AddAsync(ToaThuoc toaThuoc)
	{
		const string sql = @"
        INSERT INTO ToaThuoc (PhienKhamID, NhanVienKeDonID, GhiChu)
        OUTPUT INSERTED.ToaThuocID
        VALUES (@PhienKhamID, @NhanVienKeDonID, @GhiChu)";

		using var conn = new SqlConnection(_connectionString);
		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhienKhamID", toaThuoc.PhienKhamID);
		cmd.Parameters.AddWithValue("@NhanVienKeDonID", toaThuoc.NhanVienKeDonID);
		cmd.Parameters.AddWithValue("@GhiChu", (object?)toaThuoc.GhiChu ?? DBNull.Value);

		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}

	public async Task<ToaThuocReadModel?> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql = @"
        SELECT t.ToaThuocID, tt.HoTen, t.NgayLap, t.GhiChu
        FROM ToaThuoc t
		JOIN NhanVien nv ON t.NhanVienKeDonID = nv.NhanVienID
		JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
        WHERE t.PhienKhamID = @PhienKhamID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@PhienKhamID", phienKhamID);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		if (!await reader.ReadAsync())
			return null;

		return new ToaThuocReadModel
		{
			ToaThuocID = reader.GetInt32(0),
			NguoiLap = reader.GetString(1),
			NgayLap = reader.GetDateTime(2),
			GhiChu = reader["GhiChu"] as string
		};
	}
	public async Task<(List<ToaThuocReadModel>, int)> GetPagedAsync(int page, int size)
	{
		var sql =
		@"SELECT t.ToaThuocID, tt.HoTen, t.NgayLap, t.GhiChu
		FROM ToaThuoc t
		JOIN NhanVien nv ON t.NhanVienKeDonID = nv.NhanVienID
		JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
		ORDER BY t.NgayLap DESC
		OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

		SELECT COUNT(*) FROM ToaThuoc";

		var list = new List<ToaThuocReadModel>();
		int total = 0;

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Offset", (page - 1) * size);
		cmd.Parameters.AddWithValue("@PageSize", size);

		await conn.OpenAsync();

		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new ToaThuocReadModel
			{
				ToaThuocID = reader.GetInt32(0),
				NguoiLap = reader.GetString(1),
				NgayLap = reader.GetDateTime(2),
				GhiChu = reader["GhiChu"] as string
			});
		}

		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);

		return (list, total);
	}
}
