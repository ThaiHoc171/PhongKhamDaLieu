using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class NgayNghiNhanVienRepository : INgayNghiNhanVienRepository
{
	private readonly string _connectionString;
	public NgayNghiNhanVienRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task AddAsync(NgayNghiNhanVien entity)
	{
		const string sql = @"
			INSERT INTO NgayNghiNhanVien (NhanVienID, Ngay, LyDo) 
			VALUES (@NhanVienID, @Ngay, @LyDo)
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = entity.NhanVienID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = entity.Ngay;
		cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500).Value = (object?)entity.LyDo ?? DBNull.Value;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task UpdateAsync(NgayNghiNhanVien entity)
	{
		const string sql = @"
			UPDATE NgayNghiNhanVien 
			SET Ngay = @Ngay, LyDo = @LyDo 
			WHERE NgayNghiID = @NgayNghiID
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NgayNghiID", SqlDbType.Int).Value = entity.NgayNghiID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = entity.Ngay;
		cmd.Parameters.Add("@LyDo", SqlDbType.NVarChar, 500).Value = (object?)entity.LyDo ?? DBNull.Value;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<NgayNghiNhanVien?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT NgayNghiID, NhanVienID, Ngay, LyDo 
			FROM NgayNghiNhanVien 
			WHERE NgayNghiID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapEntity(reader) : null;
	}
	public async Task<NgayNghiReadModel?> GetDetailAsync(int id)
	{
		const string sql = @"
			SELECT nn.NgayNghiID, nn.NhanVienID, ttc.HoTen, nn.Ngay, nn.LyDo 
			FROM NgayNghiNhanVien nn 
			JOIN NhanVien nv ON nv.NhanVienID = nn.NhanVienID 
			JOIN ThongTinCaNhan ttc ON ttc.ThongTinID = nv.ThongTinID 
			WHERE nn.NgayNghiID = @Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync())
			return null;
		return MapReadModel(reader);
	}
	public async Task<List<NgayNghiReadModel>> GetByNhanVienIdAsync(int nhanVienID)
	{
		const string sql = @"
			SELECT nn.NgayNghiID, nn.NhanVienID, ttc.HoTen, nn.Ngay, nn.LyDo 
			FROM NgayNghiNhanVien nn JOIN NhanVien nv ON nv.NhanVienID = nn.NhanVienID 
			JOIN ThongTinCaNhan ttc ON ttc.ThongTinID = nv.ThongTinID 
			WHERE nn.NhanVienID = @NhanVienID 
			ORDER BY nn.Ngay
		";
		var list = new List<NgayNghiReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienID;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapReadModel(reader));
		return list;
	}
	public async Task<List<NgayNghiReadModel>> GetByMonthAsync(int thang, int nam)
	{
		var tuNgay = new DateTime(nam, thang, 1);
		var denNgay = tuNgay.AddMonths(1);
		const string sql = @"
			SELECT nn.NgayNghiID, nn.NhanVienID, ttc.HoTen, nn.Ngay, nn.LyDo 
			FROM NgayNghiNhanVien nn JOIN NhanVien nv ON nv.NhanVienID = nn.NhanVienID 
			JOIN ThongTinCaNhan ttc ON ttc.ThongTinID = nv.ThongTinID 
			WHERE nn.Ngay >= @TuNgay AND nn.Ngay < @DenNgay 
			ORDER BY nn.Ngay
		";
		var list = new List<NgayNghiReadModel>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TuNgay", SqlDbType.Date).Value = tuNgay;
		cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
			list.Add(MapReadModel(reader));
		return list;
	}
	public async Task<bool> IsNgayNghiAsync(int nhanVienID, DateTime ngay)
	{
		const string sql = @"
			SELECT 1 
			FROM NgayNghiNhanVien 
			WHERE NhanVienID = @NhanVienID AND Ngay = @Ngay
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@NhanVienID", SqlDbType.Int).Value = nhanVienID;
		cmd.Parameters.Add("@Ngay", SqlDbType.Date).Value = ngay.Date;
		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() != null;
	}
	private static NgayNghiNhanVien MapEntity(SqlDataReader reader)
	{
		return new NgayNghiNhanVien(
			reader.GetInt32(0),
			reader.GetInt32(1),
			reader.GetDateTime(2),
			reader.IsDBNull(3) ? null : reader.GetString(3)
		);
	}
	private static NgayNghiReadModel MapReadModel(SqlDataReader reader)
	{
		return new NgayNghiReadModel
		{
			NgayNghiID = reader.GetInt32(0),
			NhanVien = new NameResponseDTO
			{
				Id = reader.GetInt32(1),
				Name = reader.GetString(2)
			},
			Ngay = reader.GetDateTime(3),
			LyDo = reader.IsDBNull(4) ? null : reader.GetString(4)
		};
	}
}