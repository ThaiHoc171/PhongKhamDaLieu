using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
namespace Infrastructure.Repositories;
public class BenhNhanRepository : IBenhNhanRepository
{
	private readonly string _connectionString;
	public BenhNhanRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<bool> ExistsByThongTinIdAsync(int thongTinId)
	{
		const string sql = @"
			SELECT COUNT(1) 
			FROM BenhNhan 
			WHERE ThongTinID=@ThongTinID
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = thongTinId;
		await conn.OpenAsync();
		var result = Convert.ToInt32(await cmd.ExecuteScalarAsync());
		return result > 0;
	}
	public async Task<BenhNhan?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT BenhNhanID,ThongTinID,GhiChu,NgayTao,NgayCapNhat 
			FROM BenhNhan 
			WHERE BenhNhanID=@Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync()) return null;
		return new BenhNhan(
			reader.GetInt32(0), 
			reader.GetInt32(1), 
			reader.IsDBNull(2) ? "" : reader.GetString(2), 
			reader.GetDateTime(3), 
			reader.IsDBNull(4) ? reader.GetDateTime(3) : reader.GetDateTime(4)
		);
	}
	public async Task<(List<BenhNhanReadModel> Data, int TotalCount)> SearchAsync(string? keyword, int pageNumber, int pageSize)
	{
		const string sql = @"
			SELECT bn.BenhNhanID,bn.ThongTinID,tt.HoTen,tt.SDT,tt.EmailLienHe,bn.GhiChu
			FROM BenhNhan bn
			JOIN ThongTinCaNhan tt ON bn.ThongTinID=tt.ThongTinID
			WHERE (@Keyword IS NULL OR tt.HoTen LIKE @Keyword)
			ORDER BY bn.BenhNhanID
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
			SELECT COUNT(*)
			FROM BenhNhan bn
			JOIN ThongTinCaNhan tt ON bn.ThongTinID=tt.ThongTinID
			WHERE (@Keyword IS NULL OR tt.HoTen LIKE @Keyword)
		";
		var list = new List<BenhNhanReadModel>();
		int totalCount = 0;
		int offset = (pageNumber - 1) * pageSize;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Keyword", SqlDbType.NVarChar, 255).Value = string.IsNullOrWhiteSpace(keyword) ? DBNull.Value : $"%{keyword}%";
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new BenhNhanReadModel
			{
				BenhNhanID = reader.GetInt32(0),
				ThongTinID = reader.GetInt32(1),
				HoTen = reader.IsDBNull(2) ? null : reader.GetString(2),
				SDT = reader.IsDBNull(3) ? null : reader.GetString(3),
				EmailLienHe = reader.IsDBNull(4) ? null : reader.GetString(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5)
			});
		}
		if (await reader.NextResultAsync() && await reader.ReadAsync())
		{
			totalCount = reader.GetInt32(0);
		}
		return (list, totalCount);
	}
	public async Task<(List<BenhNhanReadModel> Data, int TotalCount)> GetPagedAsync(int pageNumber, int pageSize)
	{
		const string sql = @"
			SELECT bn.BenhNhanID,bn.ThongTinID,tt.HoTen,tt.SDT,tt.EmailLienHe,bn.GhiChu 
			FROM BenhNhan bn 
			JOIN ThongTinCaNhan tt ON bn.ThongTinID=tt.ThongTinID 
			ORDER BY bn.BenhNhanID 
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY; 
			SELECT COUNT(*) FROM BenhNhan
		";
		var list = new List<BenhNhanReadModel>();
		int totalCount = 0;
		int offset = (pageNumber - 1) * pageSize;
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new BenhNhanReadModel
			{
				BenhNhanID = reader.GetInt32(0),
				ThongTinID = reader.GetInt32(1),
				HoTen = reader.IsDBNull(2) ? null : reader.GetString(2),
				SDT = reader.IsDBNull(3) ? null : reader.GetString(3),
				EmailLienHe = reader.IsDBNull(4) ? null : reader.GetString(4),
				GhiChu = reader.IsDBNull(5) ? null : reader.GetString(5)
			});
		}
		if (await reader.NextResultAsync() && await reader.ReadAsync())
		{
			totalCount = reader.GetInt32(0);
		}
		return (list, totalCount);
	}
	public async Task<BenhNhanDetailReadModel?> GetDetailAsync(int id)
	{
		const string sql = @"
			SELECT bn.BenhNhanID,bn.ThongTinID,tt.HoTen,tt.NgaySinh,tt.GioiTinh,
				   tt.SDT,tt.EmailLienHe,tt.DiaChi,tt.Avatar,
				   bn.GhiChu,bn.NgayTao,bn.NgayCapNhat
			FROM BenhNhan bn
			JOIN ThongTinCaNhan tt ON bn.ThongTinID=tt.ThongTinID
			WHERE bn.BenhNhanID=@Id
		";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		if (!await reader.ReadAsync()) return null;
		return new BenhNhanDetailReadModel
		{
			BenhNhanID = reader.GetInt32(0),
			ThongTinID = reader.GetInt32(1),
			HoTen = reader.IsDBNull(2) ? null : reader.GetString(2),
			NgaySinh = reader.IsDBNull(3) ? null : reader.GetDateTime(3),
			GioiTinh = reader.IsDBNull(4) ? null : reader.GetString(4),
			SDT = reader.IsDBNull(5) ? null : reader.GetString(5),
			EmailLienHe = reader.IsDBNull(6) ? null : reader.GetString(6),
			DiaChi = reader.IsDBNull(7) ? null : reader.GetString(7),
			Avatar = reader.IsDBNull(8) ? null : reader.GetString(8),
			GhiChu = reader.IsDBNull(9) ? null : reader.GetString(9),
			NgayTao = reader.GetDateTime(10),
			NgayCapNhat = reader.IsDBNull(11) ? reader.GetDateTime(10) : reader.GetDateTime(11)
		};
	}
	public async Task<int> AddAsync(BenhNhan benhNhan)
	{
		const string sql = @"INSERT INTO BenhNhan(ThongTinID,GhiChu) 
			OUTPUT INSERTED.BenhNhanID VALUES(@ThongTinID,@GhiChu)";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = benhNhan.ThongTinID;
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1).Value = benhNhan.GhiChu ?? "";
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}
	public async Task UpdateAsync(BenhNhan benhNhan)
	{
		const string sql = "UPDATE BenhNhan SET GhiChu=@GhiChu,NgayCapNhat=GETDATE() WHERE BenhNhanID=@Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@GhiChu", SqlDbType.NVarChar, -1).Value = benhNhan.GhiChu ?? "";
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = benhNhan.BenhNhanID;
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		const string sql = @"
			SELECT bn.BenhNhanID,tt.HoTen 
			FROM BenhNhan bn 
			JOIN ThongTinCaNhan tt ON bn.ThongTinID=tt.ThongTinID 
			ORDER BY tt.HoTen
		";
		var list = new List<NameResponseDTO>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add( new NameResponseDTO
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1)
			});
		}
		return list;
	}
}