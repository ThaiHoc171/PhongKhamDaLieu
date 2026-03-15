using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class TaiKhamRepository : ITaiKhamRepository
{
	private readonly string _connectionString;
	public TaiKhamRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<TaiKham?> GetByIdAsync(int taiKhamID)
	{
		const string sql = "SELECT TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao FROM TaiKham WHERE TaiKhamID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKhamID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapToEntity(reader);
	}
	public async Task<TaiKhamDetailReadModel?> GetDetailAsync(int taiKhamID)
	{
		const string sql = "SELECT tk.TaiKhamID, tk.PhienKhamID, bn.BenhNhanID, ttc.HoTen, tk.NgayDuKien, tk.LyDo, tk.TrangThai, tk.CaKhamID, tk.NgayTao FROM TaiKham tk JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID WHERE tk.TaiKhamID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKhamID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return new TaiKhamDetailReadModel
		{
			TaiKhamID = reader.GetInt32(0),
			PhienKhamID = reader.GetInt32(1),
			BenhNhan = new NameResponseDTO
			{
				Id = reader.GetInt32(2),
				Name = reader.GetString(3)
			},
			NgayDuKien = reader.GetDateTime(4),
			LyDo = reader.IsDBNull(5) ? null : reader.GetString(5),
			TrangThai = reader.IsDBNull(6) ? null : reader.GetString(6),
			CaKhamID = reader.IsDBNull(7) ? null : reader.GetInt32(7),
			NgayTao = reader.GetDateTime(8)
		};
	}
	public async Task<(List<TaiKhamReadModel>, int)> GetPagedAsync(int page, int size, string? trangThai)
	{
		const string sql =
		@"SELECT tk.TaiKhamID, bn.BenhNhanID, ttc.HoTen, tk.NgayDuKien, tk.LyDo, tk.TrangThai
          FROM TaiKham tk
          JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID
          JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID
          WHERE (@TrangThai IS NULL OR tk.TrangThai = @TrangThai)
          ORDER BY tk.NgayDuKien DESC
          OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
          SELECT COUNT(*)
          FROM TaiKham tk
          WHERE (@TrangThai IS NULL OR tk.TrangThai = @TrangThai)";
		var list = new List<TaiKhamReadModel>();
		int total = 0;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = (object?)trangThai ?? DBNull.Value;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToReadModel(reader));
		}
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<(List<TaiKhamReadModel>, int)> SearchAsync(string? keyword, int page, int size)
	{
		const string sql =
		@"SELECT tk.TaiKhamID, bn.BenhNhanID, ttc.HoTen, tk.NgayDuKien, tk.LyDo, tk.TrangThai
          FROM TaiKham tk
          JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID
          JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID
          WHERE (@Keyword IS NULL OR ttc.HoTen LIKE @Keyword)
          ORDER BY tk.NgayDuKien DESC
          OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
          SELECT COUNT(*)
          FROM TaiKham tk
          JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID
          JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID
          WHERE (@Keyword IS NULL OR ttc.HoTen LIKE @Keyword)";
		var list = new List<TaiKhamReadModel>();
		int total = 0;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value =
			string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToReadModel(reader));
		}
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<int> AddAsync(TaiKham taiKham)
	{
		const string sql = "INSERT INTO TaiKham (PhienKhamID, BenhNhanID, NgayDuKien, LyDo) OUTPUT INSERTED.TaiKhamID VALUES (@PhienKhamID, @BenhNhanID, @NgayDuKien, @LyDo)";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = taiKham.PhienKhamID;
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = taiKham.BenhNhanID;
		cmd.Parameters.Add("@NgayDuKien", SqlDbType.Date).Value = taiKham.NgayDuKien;
		cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500).Value = (object?)taiKham.LyDo ?? DBNull.Value;
		await conn.OpenAsync();
		return (int)await cmd.ExecuteScalarAsync();
	}
	public async Task UpdateAsync(TaiKham taiKham)
	{
		const string sql = "UPDATE TaiKham SET TrangThai = @TrangThai, CaKhamID = @CaKhamID WHERE TaiKhamID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = TaiKhamExtensions.ToDbValue(taiKham.TrangThai);
		cmd.Parameters.Add("@CaKhamID", SqlDbType.Int).Value = (object?)taiKham.CaKhamID ?? DBNull.Value;
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = taiKham.TaiKhamID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<bool> ExistsByPhienKhamAsync(int phienKhamID)
	{
		const string sql = "SELECT 1 FROM TaiKham WHERE PhienKhamID = @PhienKhamID";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<(List<TaiKhamReadModel>, int)> GetListByBenhNhanAsync(int benhNhanID, int page, int size)
	{
		const string sql =@"
			SELECT tk.TaiKhamID, bn.BenhNhanID, ttc.HoTen, tk.NgayDuKien, tk.LyDo, tk.TrangThai
			FROM TaiKham tk
			JOIN BenhNhan bn ON tk.BenhNhanID = bn.BenhNhanID
			JOIN ThongTinCaNhan ttc ON bn.ThongTinID = ttc.ThongTinID
			WHERE tk.BenhNhanID = @BenhNhanID
			ORDER BY tk.NgayDuKien DESC
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM TaiKham
			WHERE BenhNhanID = @BenhNhanID
		";
		var list = new List<TaiKhamReadModel>();
		int total = 0;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToReadModel(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task<TaiKham?> GetByBenhNhanIdAsync(int benhNhanID)
	{
		const string sql = "SELECT TOP 1 TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao FROM TaiKham WHERE BenhNhanID = @BenhNhanID ORDER BY NgayDuKien DESC";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapToEntity(reader);
	}
	public async Task<TaiKham?> GetTaiKhamDangChoAsync(int benhNhanID)
	{
		const string sql = "SELECT TOP 1 TaiKhamID, PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID, NgayTao FROM TaiKham WHERE BenhNhanID = @BenhNhanID AND TrangThai = N'Chờ khám' ORDER BY NgayDuKien DESC";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@BenhNhanID", SqlDbType.Int).Value = benhNhanID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapToEntity(reader);
	}
	private static TaiKham MapToEntity(SqlDataReader reader)
	{
		return new TaiKham(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetInt32(2),
			reader.GetDateTime(3),
			reader.IsDBNull(4) ? null : reader.GetString(4),
			reader.IsDBNull(5) ? null : reader.GetString(5),
			reader.IsDBNull(6) ? null : reader.GetInt32(6),
			reader.GetDateTime(7)
		);
	}
	private static TaiKhamReadModel MapToReadModel(SqlDataReader reader)
	{
		return new TaiKhamReadModel
		{
			TaiKhamID = reader.GetInt32(0),
			BenhNhan = new NameResponseDTO
			{
				Id = reader.GetInt32(1),
				Name = reader.GetString(2)
			},
			NgayDuKien = reader.GetDateTime(3),
			LyDo = reader.IsDBNull(4) ? null : reader.GetString(4),
			TrangThai = reader.IsDBNull(5) ? null : reader.GetString(5)
		};
	}
}