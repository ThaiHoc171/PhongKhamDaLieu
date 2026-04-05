using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class LoaiBenhRepository : ILoaiBenhRepository
{
	private readonly string _connectionString;

	public LoaiBenhRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
        SELECT LoaiBenhID,TenBenh,NhomBenh,MucDoNghiemTrong
        FROM LoaiBenh";

	private const string BaseSelectDetail = @"
        SELECT LoaiBenhID,TenBenh,TenKhoaHoc,NhomBenh,MoTa,DoPhoBien,MucDoNghiemTrong,NgayTao
        FROM LoaiBenh";

	#endregion

	public async Task<(List<LoaiBenhListReadModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<LoaiBenhListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        ORDER BY TenBenh
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*) FROM LoaiBenh";

		using var cmd = new SqlCommand(sql, conn);
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

	public async Task<(List<LoaiBenhListReadModel>, int)> SearchPagedAsync(string keyword, int page, int size)
	{
		var list = new List<LoaiBenhListReadModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
        {BaseSelectList}
        WHERE TenBenh LIKE @Keyword OR TenKhoaHoc LIKE @Keyword
        ORDER BY TenBenh
        OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

        SELECT COUNT(*)
        FROM LoaiBenh
        WHERE TenBenh LIKE @Keyword OR TenKhoaHoc LIKE @Keyword";

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

	public async Task<LoaiBenhReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE LoaiBenhID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}

	public async Task<LoaiBenh?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE LoaiBenhID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}

	public async Task<int> AddAsync(LoaiBenh loaiBenh)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO LoaiBenh
					(TenBenh,TenKhoaHoc,NhomBenh,MoTa,DoPhoBien,MucDoNghiemTrong)
					VALUES(@TenBenh,@TenKhoaHoc,@NhomBenh,@MoTa,@DoPhoBien,@MucDoNghiemTrong)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@TenBenh", SqlDbType.NVarChar, 200).Value = loaiBenh.TenBenh;
		cmd.Parameters.Add("@TenKhoaHoc", SqlDbType.NVarChar, 200).Value = loaiBenh.TenKhoaHoc;
		cmd.Parameters.Add("@NhomBenh", SqlDbType.NVarChar, 100).Value = loaiBenh.NhomBenh;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, -1).Value = loaiBenh.MoTa;
		cmd.Parameters.Add("@DoPhoBien", SqlDbType.NVarChar, 50).Value = loaiBenh.DoPhoBien;
		cmd.Parameters.Add("@MucDoNghiemTrong", SqlDbType.NVarChar, 50).Value = loaiBenh.MucDoNghiemTrong;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}

	public async Task<int> UpdateAsync(LoaiBenh loaiBenh)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE LoaiBenh
					SET TenBenh=@TenBenh,
						TenKhoaHoc=@TenKhoaHoc,
						NhomBenh=@NhomBenh,
						MoTa=@MoTa,
						DoPhoBien=@DoPhoBien,
						MucDoNghiemTrong=@MucDoNghiemTrong
					WHERE LoaiBenhID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = loaiBenh.LoaiBenhID;
		cmd.Parameters.Add("@TenBenh", SqlDbType.NVarChar, 200).Value = loaiBenh.TenBenh;
		cmd.Parameters.Add("@TenKhoaHoc", SqlDbType.NVarChar, 200).Value = loaiBenh.TenKhoaHoc;
		cmd.Parameters.Add("@NhomBenh", SqlDbType.NVarChar, 100).Value = loaiBenh.NhomBenh;
		cmd.Parameters.Add("@MoTa", SqlDbType.NVarChar, -1).Value = loaiBenh.MoTa;
		cmd.Parameters.Add("@DoPhoBien", SqlDbType.NVarChar, 50).Value = loaiBenh.DoPhoBien;
		cmd.Parameters.Add("@MucDoNghiemTrong", SqlDbType.NVarChar, 50).Value = loaiBenh.MucDoNghiemTrong;

		int row = await cmd.ExecuteNonQueryAsync();

		return row;
	}
	public async Task BulkInsertAsync(List<LoaiBenh> list)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var table = new DataTable();

		table.Columns.Add("TenBenh");
		table.Columns.Add("TenKhoaHoc");
		table.Columns.Add("NhomBenh");
		table.Columns.Add("MoTa");
		table.Columns.Add("DoPhoBien");
		table.Columns.Add("MucDoNghiemTrong");

		foreach (var item in list)
		{
			table.Rows.Add(
				item.TenBenh,
				item.TenKhoaHoc,
				item.NhomBenh,
				item.MoTa,
				item.DoPhoBien,
				item.MucDoNghiemTrong
			);
		}

		using var bulk = new SqlBulkCopy(conn);

		bulk.DestinationTableName = "LoaiBenh";

		bulk.ColumnMappings.Add("TenBenh", "TenBenh");
		bulk.ColumnMappings.Add("TenKhoaHoc", "TenKhoaHoc");
		bulk.ColumnMappings.Add("NhomBenh", "NhomBenh");
		bulk.ColumnMappings.Add("MoTa", "MoTa");
		bulk.ColumnMappings.Add("DoPhoBien", "DoPhoBien");
		bulk.ColumnMappings.Add("MucDoNghiemTrong", "MucDoNghiemTrong");

		await bulk.WriteToServerAsync(table);
	}
	public async Task<List<NameResponseDTO>> GetComboboxAsync()
	{
		var list = new List<NameResponseDTO>();
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();
		var sql = @"SELECT LoaiBenhID,TenBenh
                    FROM LoaiBenh
                    ORDER BY TenBenh ASC";
		using var cmd = new SqlCommand(sql, conn);
		using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("LoaiBenhID")),
				Name = reader.GetString(reader.GetOrdinal("TenBenh"))
			});
		}
		return list;
	}
	public async Task<string?> GetTenBenhByIdAsync(int id)
	{
		const string sql = @"SELECT TenBenh FROM LoaiBenh WHERE LoaiBenhID = @Id";
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = id;
		await conn.OpenAsync();
		return await cmd.ExecuteScalarAsync() as string;
	}
	public async Task<bool> ExistsTenBenhAsync(string tenBenh)
	{
		const string sql = @"
		SELECT 1 
		FROM LoaiBenh
		WHERE TenBenh = @TenBenh";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenBenh", SqlDbType.NVarChar, 200).Value = tenBenh;

		var result = await cmd.ExecuteScalarAsync();

		return result != null;
	}

	public async Task<bool> ExistsTenKhoaHocAsync(string tenKhoaHoc)
	{
		const string sql = @"
		SELECT 1
		FROM LoaiBenh
		WHERE TenKhoaHoc = @TenKhoaHoc";

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@TenKhoaHoc", SqlDbType.NVarChar, 200).Value = tenKhoaHoc;

		var result = await cmd.ExecuteScalarAsync();

		return result != null;
	}
	#region Mapping

	private LoaiBenh MapToEntity(SqlDataReader r)
	{
		return new LoaiBenh(
			r.GetInt32(r.GetOrdinal("LoaiBenhID")),
			r.GetString(r.GetOrdinal("TenBenh")),
			r.GetString(r.GetOrdinal("TenKhoaHoc")),
			r.GetString(r.GetOrdinal("NhomBenh")),
			r.GetString(r.GetOrdinal("MoTa")),
			r.GetString(r.GetOrdinal("DoPhoBien")),
			r.GetString(r.GetOrdinal("MucDoNghiemTrong")),
			r.GetDateTime(r.GetOrdinal("NgayTao"))
		);
	}

	private LoaiBenhListReadModel MapToListDTO(SqlDataReader r)
	{
		return new LoaiBenhListReadModel
		{
			LoaiBenhID = r.GetInt32(r.GetOrdinal("LoaiBenhID")),
			TenBenh = r.GetString(r.GetOrdinal("TenBenh")),
			NhomBenh = r.GetString(r.GetOrdinal("NhomBenh")),
			MucDoNghiemTrong = r.GetString(r.GetOrdinal("MucDoNghiemTrong"))
		};
	}

	private LoaiBenhReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new LoaiBenhReadModel
		{
			LoaiBenhID = r.GetInt32(r.GetOrdinal("LoaiBenhID")),
			TenBenh = r.GetString(r.GetOrdinal("TenBenh")),
			TenKhoaHoc = r.GetString(r.GetOrdinal("TenKhoaHoc")),
			NhomBenh = r.GetString(r.GetOrdinal("NhomBenh")),
			MoTa = r.GetString(r.GetOrdinal("MoTa")),
			DoPhoBien = r.GetString(r.GetOrdinal("DoPhoBien")),
			MucDoNghiemTrong = r.GetString(r.GetOrdinal("MucDoNghiemTrong")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao"))
		};
	}

	#endregion
}