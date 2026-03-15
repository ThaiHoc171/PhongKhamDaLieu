using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class ToaThuocRepository : IToaThuocRepository
{
	private readonly string _connectionString;
	public ToaThuocRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string not found.");
	}
	private SqlConnection CreateConnection() => new(_connectionString);
	public async Task<bool> ExistsByPhienKhamAsync(int phienKhamID)
	{
		const string sql =
		@"SELECT 1 FROM ToaThuoc WHERE PhienKhamID=@PhienKhamID";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result != null;
	}
	public async Task<int> AddAsync(ToaThuoc entity)
	{
		const string sql =
		@"INSERT INTO ToaThuoc (PhienKhamID,NhanVienKeDonID,GhiChu)
		  OUTPUT INSERTED.ToaThuocID
		  VALUES (@PhienKhamID,@NhanVienKeDonID,@GhiChu)";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = entity.PhienKhamID;
		cmd.Parameters.Add("@NhanVienKeDonID", SqlDbType.Int).Value = entity.NhanVienKeDonID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar).Value =
			(object?)entity.GhiChu ?? DBNull.Value;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task<ToaThuoc?> GetByIdAsync(int toaThuocID)
	{
		const string sql =
		@"SELECT ToaThuocID,PhienKhamID,NhanVienKeDonID,NgayLap,GhiChu
		  FROM ToaThuoc
		  WHERE ToaThuocID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = toaThuocID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapToEntity(reader);
	}
	public async Task<ToaThuocReadModel?> GetByPhienKhamAsync(int phienKhamID)
	{
		const string sql =
		@"SELECT t.ToaThuocID,t.PhienKhamID,t.NgayLap,t.GhiChu,nv.NhanVienID,tt.HoTen
		  FROM ToaThuoc t
		  JOIN NhanVien nv ON t.NhanVienKeDonID=nv.NhanVienID
		  JOIN ThongTinCaNhan tt ON nv.ThongTinID=tt.ThongTinID
		  WHERE t.PhienKhamID=@PhienKhamID";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@PhienKhamID", SqlDbType.Int).Value = phienKhamID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapToDetailDTO(reader);
	}
	public async Task<(List<ToaThuocListReadModel>, int)> GetPagedAsync(int page, int size)
	{
		var sql =
		@"SELECT t.ToaThuocID,t.NgayLap,t.GhiChu,tt.HoTen
		  FROM ToaThuoc t
		  JOIN NhanVien nv ON t.NhanVienKeDonID=nv.NhanVienID
		  JOIN ThongTinCaNhan tt ON nv.ThongTinID=tt.ThongTinID
		  ORDER BY t.NgayLap DESC
		  OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
		  SELECT COUNT(*) FROM ToaThuoc";
		var list = new List<ToaThuocListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = (page - 1) * size;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = size;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));
		if (await reader.NextResultAsync() && await reader.ReadAsync())
			total = reader.GetInt32(0);
		return (list, total);
	}
	public async Task DeleteAsync(int toaThuocID)
	{
		const string sql =
		@"DELETE FROM ToaThuoc WHERE ToaThuocID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = toaThuocID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	private static ToaThuoc MapToEntity(SqlDataReader r)
	{
		return new ToaThuoc(
			r.GetInt32(r.GetOrdinal("ToaThuocID")),
			r.GetInt32(r.GetOrdinal("PhienKhamID")),
			r.GetInt32(r.GetOrdinal("NhanVienKeDonID")),
			r.GetDateTime(r.GetOrdinal("NgayLap")),
			r.IsDBNull(r.GetOrdinal("GhiChu")) ? null : r.GetString(r.GetOrdinal("GhiChu"))
		);
	}
	private static ToaThuocListReadModel MapToListDTO(SqlDataReader r)
	{
		return new ToaThuocListReadModel
		{
			ToaThuocID = r.GetInt32(0),
			NgayLap = r.GetDateTime(1),
			GhiChu = r.IsDBNull(2) ? null : r.GetString(2),
			NguoiLap = r.GetString(3)
		};
	}
	private static ToaThuocReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new ToaThuocReadModel
		{
			ToaThuocID = r.GetInt32(0),
			PhienKhamID = r.GetInt32(1),
			NgayLap = r.GetDateTime(2),
			GhiChu = r.IsDBNull(3) ? null : r.GetString(3),
			NguoiLap = new NameResponseDTO
			{
				Id = r.GetInt32(4),
				Name = r.GetString(5)
			}
		};
	}
}