using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class PhongChucNangRepository : IPhongChucNangRepository
{
	private readonly string _connectionString;
	public PhongChucNangRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new InvalidOperationException("Connection string not found.");
	}
	private SqlConnection CreateConnection() => new(_connectionString);
	private const string BaseSelectLite = @"SELECT PhongChucNangID, TenPhong, TrangThai";
	private const string BaseSelectDetail =@"SELECT PhongChucNangID, TenPhong, MoTa, TrangThai, NgayTao, NgayCapNhat";
	public async Task<PhongChucNang?> GetByIdAsync(int id)
	{
		const string sql =
		@"SELECT PhongChucNangID, TenPhong, MoTa, TrangThai, NgayTao, NgayCapNhat
		FROM PhongChucNang
		WHERE PhongChucNangID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<(List<PhongChucNangListReadModel>, int)>
	GetPagedAsync(int page, int size, string? trangThai)
	{
		var sql =$@"
			{BaseSelectLite}
			FROM PhongChucNang
			WHERE (@TrangThai IS NULL OR TrangThai=@TrangThai)
			ORDER BY PhongChucNangID DESC
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM PhongChucNang
			WHERE (@TrangThai IS NULL OR TrangThai=@TrangThai)
		";
		var list = new List<PhongChucNangListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar).Value = string.IsNullOrWhiteSpace(trangThai) 
			? DBNull.Value : TinhTrangExtensions.ToDbValue(Enum.Parse<Domain.Enums.TinhTrang>(trangThai));
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
	public async Task<(List<PhongChucNangListReadModel>, int)>
	SearchPagedAsync(string? keyword, int page, int size)
	{
		var sql =$@"
			{BaseSelectLite}
			FROM PhongChucNang
			WHERE (@Keyword IS NULL OR TenPhong LIKE @Keyword)
			ORDER BY PhongChucNangID DESC
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM PhongChucNang
			WHERE (@Keyword IS NULL OR TenPhong LIKE @Keyword)
		";
		var list = new List<PhongChucNangListReadModel>();
		int total = 0;
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value =
			string.IsNullOrWhiteSpace(keyword)
				? DBNull.Value
				: $"%{keyword}%";
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
	public async Task<PhongChucNangReadModel?> GetDetailAsync(int id)
	{
		var sql =
		$@"{BaseSelectDetail}
		FROM PhongChucNang
		WHERE PhongChucNangID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToDetailDTO(reader) : null;
	}
	public async Task<int> AddAsync(PhongChucNang phong)
	{
		const string sql =
		@"INSERT INTO PhongChucNang (TenPhong,MoTa)
		OUTPUT INSERTED.PhongChucNangID
		VALUES (@TenPhong,@MoTa)";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenPhong", SqlDbType.NVarChar, 200).Value = phong.TenPhong;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value =
			(object?)phong.MoTa ?? DBNull.Value;
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task UpdateAsync(PhongChucNang phong)
	{
		const string sql =
		@"UPDATE PhongChucNang
		SET TenPhong=@TenPhong, MoTa=@MoTa, TrangThai=@TrangThai, NgayCapNhat=GETDATE()
		WHERE PhongChucNangID=@Id";
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = phong.PhongChucNangID;
		cmd.Parameters.Add("@TenPhong", SqlDbType.NVarChar, 200).Value = phong.TenPhong;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar).Value =
			(object?)phong.MoTa ?? DBNull.Value;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = TinhTrangExtensions.ToDbValue(phong.TrangThai);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<List<(int Id, string Ten)>> GetComboboxAsync()
	{
		const string sql =
		@"SELECT PhongChucNangID, TenPhong FROM PhongChucNang WHERE TrangThai=N'Hoạt động' ORDER BY PhongChucNangID";
		var list = new List<(int, string)>();
		await using var conn = CreateConnection();
		await using var cmd = new SqlCommand(sql, conn);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add((
				reader.GetInt32(reader.GetOrdinal("PhongChucNangID")),
				reader.GetString(reader.GetOrdinal("TenPhong"))
			));
		}
		return list;
	}
	private static PhongChucNang MapToEntity(SqlDataReader r)
	{
		var ngayCapNhat = r.GetOrdinal("NgayCapNhat");
		return new PhongChucNang(
			r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			r.GetString(r.GetOrdinal("TenPhong")),
			r["MoTa"] as string,
			r.GetString(r.GetOrdinal("TrangThai")),
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.IsDBNull(ngayCapNhat) ? null : r.GetDateTime(ngayCapNhat)
		);
	}
	private static PhongChucNangListReadModel MapToListDTO(SqlDataReader r)
	{
		return new PhongChucNangListReadModel
		{
			PhongChucNangID = r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			TenPhong = r.GetString(r.GetOrdinal("TenPhong")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
		};
	}
	private static PhongChucNangReadModel MapToDetailDTO(SqlDataReader r)
	{
		var ngayCapNhat = r.GetOrdinal("NgayCapNhat");
		return new PhongChucNangReadModel
		{
			PhongChucNangID = r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			TenPhong = r.GetString(r.GetOrdinal("TenPhong")),
			MoTa = r["MoTa"] as string,
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(ngayCapNhat) ? null : r.GetDateTime(ngayCapNhat)
		};
	}
}