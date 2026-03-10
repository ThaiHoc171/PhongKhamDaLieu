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
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException("Connection string not found");
	}
	public async Task<bool> ExistsByThongTinIdAsync(int thongTinId)
	{
		const string sql = @"
		SELECT COUNT(1)
		FROM BenhNhan
		WHERE ThongTinID = @ThongTinID";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@ThongTinID", thongTinId);

		await conn.OpenAsync();

		var result = Convert.ToInt32(await cmd.ExecuteScalarAsync());

		return result > 0;
	}
	public async Task<BenhNhan?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT BenhNhanID, ThongTinID, GhiChu, NgayTao, NgayCapNhat
			FROM BenhNhan WHERE BenhNhanID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}
	public async Task<int> GetIdByThongTinAsync(int id)
	{
		const string sql = @"
			SELECT BenhNhanID
			FROM BenhNhan 
			WHERE ThongTinID = @id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@id", id);
		await conn.OpenAsync();
		var result = await cmd.ExecuteScalarAsync();
		return result == null ? 0 : Convert.ToInt32(result);
	}
	public async Task<List<BenhNhan>> GetBenhNhans(string keyword)
	{
		const string sql = @"
				SELECT bn.BenhNhanID, bn.GhiChu,tt.ThongTinID,tt.HoTen,tt.SDT,tt.EmailLienHe
				FROM BenhNhan bn
				INNER JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
				WHERE tt.HoTen LIKE @Keyword
				";
		var list = new List<BenhNhan>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(MapToListEntity(reader));
		}
		return list;
	}
	public async Task<(List<BenhNhan> Data, int TotalCount)>GetPagedAsync(int pageNumber, int pageSize)
	{
		const string sql = @"
			SELECT 
				bn.BenhNhanID, bn.GhiChu,
				tt.ThongTinID,tt.HoTen,tt.SDT,tt.EmailLienHe

			FROM BenhNhan bn
			JOIN ThongTinCaNhan tt
				ON bn.ThongTinID = tt.ThongTinID

			ORDER BY bn.BenhNhanID
			OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

			SELECT COUNT(*) FROM BenhNhan;
		";

		var list = new List<BenhNhan>();
		int totalCount = 0;

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		int offset = (pageNumber - 1) * pageSize;

		cmd.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;
		cmd.Parameters.Add("@PageSize", SqlDbType.Int).Value = pageSize;

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		// Result 1
		while (await reader.ReadAsync())
		{
			list.Add(MapToListEntity(reader));
		}

		// Result 2
		if (await reader.NextResultAsync())
		{
			if (await reader.ReadAsync())
			{
				totalCount = reader.GetInt32(0);
			}
		}

		return (list, totalCount);
	}
	public async Task<string?> GetNameByIdAsync(int id)
	{
		const string sql = @"
			SELECT tt.HoTen as TenBenhNhan
			FROM BenhNhan bn
			INNER JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
			WHERE bn.BenhNhanID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() as string;
	}
    public async Task<BenhNhan?> GetForAuthAsync(int taiKhoanID)
    {
        const string sql = @"
        SELECT bn.BenhNhanID, bn.ThongTinID
        FROM BenhNhan bn
        INNER JOIN ThongTinCaNhan tt ON bn.ThongTinID = tt.ThongTinID
        WHERE tt.TaiKhoanID = @TaiKhoanID";

        await using var conn = new SqlConnection(_connectionString);
        await using var cmd = new SqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("@TaiKhoanID", taiKhoanID);

        await conn.OpenAsync();
        await using var reader = await cmd.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
            return null;

        return new BenhNhan(
            benhNhanID: (int)reader["BenhNhanID"],
            thongTinID: (int)reader["ThongTinID"],
            ghiChu: null,
            ngayTao: DateTime.Now,
            ngayCapNhat: DateTime.Now
        );
    }
    public async Task<int> AddAsync(BenhNhan benhNhan)
	{
		const string sql = @"
			INSERT INTO BenhNhan (ThongTinID, GhiChu) 
			OUTPUT INSERTED.BenhNhanID
			VALUES (@ThongTinID, @GhiChu)";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@ThongTinID", benhNhan.ThongTinID);
		cmd.Parameters.AddWithValue("@GhiChu", benhNhan.GhiChu ?? "");
		await conn.OpenAsync();
		return Convert.ToInt32(await cmd.ExecuteScalarAsync());
	}

	public async Task UpdateAsync(BenhNhan benhNhan)
	{
		const string sql = @"
			UPDATE BenhNhan 
			SET GhiChu = @GhiChu, NgayCapNhat = GETDATE()
            WHERE BenhNhanID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@GhiChu", benhNhan.GhiChu ?? "");
		cmd.Parameters.AddWithValue("@Id", benhNhan.BenhNhanID);
		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}
	public async Task<List<(int Id, string Ten)>> GetIdAndNameAsync()
	{
		const string sql = @"
			SELECT bn.BenhNhanID, tt.HoTen
			FROM BenhNhan bn
			JOIN ThongTinCaNhan tt 
				ON bn.ThongTinID = tt.ThongTinID
			ORDER BY tt.HoTen";

		var list = new List<(int Id, string Ten)>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add((
				Id: reader.GetInt32(0),
				Ten: reader.GetString(1)
			));
		}

		return list;
	}


	private static BenhNhan MapToEntity(SqlDataReader reader)
	{
		return new BenhNhan(
			benhNhanID: reader.GetInt32(0),
			thongTinID: reader.GetInt32(1),
			ghiChu: reader.IsDBNull(2) ? "" : reader.GetString(2),
			ngayTao: reader.GetDateTime(3),
			ngayCapNhat: reader.IsDBNull(4) ? reader.GetDateTime(3) : reader.GetDateTime(4)
		);
	}

	private static BenhNhan MapToListEntity(SqlDataReader reader)
	{
		var thongTin = new ThongTinCaNhan(
			thongTinID: reader.GetInt32(2),
			taiKhoanID: null,
			hoTen: reader.GetString(3),
			ngaySinh: null,
			gioiTinh: null,
			sdt: reader.IsDBNull(4) ? "" : reader.GetString(4),
			emailLienHe: reader.IsDBNull(5) ? "" : reader.GetString(5),
			diaChi: null,
			avatar: null,
			loai: "Bệnh nhân",
			ngayTao: DateTime.MinValue,
			ngayCapNhat: null
		);

		return new BenhNhan(
			benhNhanID: reader.GetInt32(0),
			ghiChu: reader.IsDBNull(1) ? "" : reader.GetString(1),
			thongTinCaNhan: thongTin
		);
	}
}

