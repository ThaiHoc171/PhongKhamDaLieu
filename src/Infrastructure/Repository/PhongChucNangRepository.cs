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
 	public PhongChucNangRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}
 	#region Queries
 	private const string BaseSelectList = @"
		SELECT PhongChucNangID, TenPhong, TrangThai
		FROM PhongChucNang";
 	private const string BaseSelectDetail = @"
		SELECT PhongChucNangID, TenPhong, MoTa, TrangThai, NgayTao, NgayCapNhat
		FROM PhongChucNang";
 	#endregion
 	public async Task<(List<PhongChucNangReadListModel>, int)> GetPagedAsync(int page, int size, string? trangThai)
	{
		var list = new List<PhongChucNangReadListModel>();
		int total = 0;
 		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
 		int offset = (page - 1) * size;
 		var sql = $@"
		{BaseSelectList}
		WHERE (@TrangThai IS NULL OR TrangThai=@TrangThai)
		ORDER BY PhongChucNangID ASC
		OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
 		SELECT COUNT(*)
		FROM PhongChucNang
		WHERE (@TrangThai IS NULL OR TrangThai=@TrangThai)";
 		using var cmd = new SqlCommand(sql, conn);
 		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value =
			string.IsNullOrWhiteSpace(trangThai)
			? DBNull.Value
			: TinhTrangExtensions.ToDbValue(Enum.Parse<TinhTrang>(trangThai));
 		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;
 		using var reader = await cmd.ExecuteReaderAsync();
 		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));
 		await reader.NextResultAsync();
 		if (await reader.ReadAsync())
			total = reader.GetInt32(0);
 		return (list, total);
	}
 	public async Task<(List<PhongChucNangReadListModel>, int)> SearchPagedAsync(string? keyword, int page, int size)
	{
		var list = new List<PhongChucNangReadListModel>();
		int total = 0;
 		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
 		int offset = (page - 1) * size;
 		var sql = $@"
		{BaseSelectList}
		WHERE TenPhong LIKE @Keyword
		ORDER BY PhongChucNangID ASC
		OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;
 		SELECT COUNT(*)
		FROM PhongChucNang
		WHERE TenPhong LIKE @Keyword";
 		using var cmd = new SqlCommand(sql, conn);
 		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 200).Value = $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@Size", SqlDbType.Int).Value = size;
 		using var reader = await cmd.ExecuteReaderAsync();
 		while (await reader.ReadAsync())
			list.Add(MapToListDTO(reader));
 		await reader.NextResultAsync();
 		if (await reader.ReadAsync())
			total = reader.GetInt32(0);
 		return (list, total);
	}
 	public async Task<PhongChucNangReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
 		var sql = BaseSelectDetail + " WHERE PhongChucNangID=@Id";
 		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
 		using var reader = await cmd.ExecuteReaderAsync();
 		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);
 		return null;
	}
 	public async Task<PhongChucNang?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
 		var sql = BaseSelectDetail + " WHERE PhongChucNangID=@Id";
 		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
 		using var reader = await cmd.ExecuteReaderAsync();
 		if (await reader.ReadAsync())
			return MapToEntity(reader);
 		return null;
	}
 	public async Task<int> AddAsync(PhongChucNang phong)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
 		var sql = @"INSERT INTO PhongChucNang(TenPhong,MoTa)
					VALUES(@TenPhong,@MoTa)";
 		using var cmd = new SqlCommand(sql, conn);
 		cmd.Parameters.Add("@TenPhong", SqlDbType.NVarChar, 200).Value = phong.TenPhong;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, -1).Value =
			(object?)phong.MoTa ?? DBNull.Value;
 		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}
 	public async Task<int> UpdateAsync(PhongChucNang phong)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
 		var sql = @"UPDATE PhongChucNang
					SET TenPhong=@TenPhong,
						MoTa=@MoTa,
						TrangThai=@TrangThai,
						NgayCapNhat=@NgayCapNhat
					WHERE PhongChucNangID=@Id";
 		using var cmd = new SqlCommand(sql, conn);
 		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = phong.PhongChucNangID;
		cmd.Parameters.Add("@TenPhong", SqlDbType.NVarChar, 200).Value = phong.TenPhong;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, -1).Value =
			(object?)phong.MoTa ?? DBNull.Value;
 		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50)
			.Value = TinhTrangExtensions.ToDbValue(phong.TrangThai);
 		cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime)
			.Value = phong.NgayCapNhat;
 		int row = await cmd.ExecuteNonQueryAsync();
		return row;
	}
 	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		var list = new List<NameResponseDTO>();
 		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
 		var sql = @"SELECT PhongChucNangID,TenPhong
					FROM PhongChucNang
					WHERE TrangThai = N'Hoạt động'
					ORDER BY TenPhong";
 		using var cmd = new SqlCommand(sql, conn);
		using var reader = await cmd.ExecuteReaderAsync();
 		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("PhongChucNangID")),
				Name = reader.GetString(reader.GetOrdinal("TenPhong"))
			});
		}
		return list;
	}
 	#region Mapping
 	private PhongChucNang MapToEntity(SqlDataReader r)
	{
		return new PhongChucNang(
			r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			r.GetString(r.GetOrdinal("TenPhong")),
			r["MoTa"] as string,
			r.GetString(r.GetOrdinal("TrangThai")),
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.IsDBNull(r.GetOrdinal("NgayCapNhat"))
				? null
				: r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		);
	}
 	private PhongChucNangReadListModel MapToListDTO(SqlDataReader r)
	{
		return new PhongChucNangReadListModel
		{
			PhongChucNangID = r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			TenPhong = r.GetString(r.GetOrdinal("TenPhong")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
		};
	}
 	private PhongChucNangReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new PhongChucNangReadModel
		{
			PhongChucNangID = r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			TenPhong = r.GetString(r.GetOrdinal("TenPhong")),
			MoTa = r["MoTa"] as string,
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat"))
				? null
				: r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}
 	#endregion
}