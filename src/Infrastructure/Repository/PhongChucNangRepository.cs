using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories;

public class PhongChucNangRepository : IPhongChucNangRepository
{
	private readonly string _connectionString;

	public PhongChucNangRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")
			?? throw new ArgumentNullException();
	}

	public async Task<List<PhongChucNang>> GetAllAsync()
	{
		const string sql = @"SELECT PhongChucNangID, TenPhong, LoaiPhong, MoTa, TrangThai, NgayTao FROM PhongChucNang";
		var list = new List<PhongChucNang>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntity(reader));
		return list;
	}

	public async Task<List<PhongChucNang>> SearchAsync(string keyword)
	{
		const string sql = @"
			SELECT PhongChucNangID, TenPhong, LoaiPhong, MoTa, TrangThai, NgayTao
			FROM PhongChucNang
			WHERE TenPhong LIKE @Keyword
			OR LoaiPhong LIKE @Keyword ";

		var list = new List<PhongChucNang>();

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Keyword", $"%{keyword}%");

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
			list.Add(MapToEntity(reader));

		return list;
	}

	public async Task<PhongChucNang?> GetByIdAsync(int id)
	{
		const string sql = @"
			SELECT PhongChucNangID, TenPhong, LoaiPhong, MoTa, TrangThai, NgayTao 
			FROM PhongChucNang 
			WHERE PhongChucNangID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.AddWithValue("@Id", id);

		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();

		return await reader.ReadAsync() ? MapToEntity(reader) : null;
	}

	public async Task AddAsync(PhongChucNang phong)
	{
		const string sql = @"
			INSERT INTO PhongChucNang (TenPhong, LoaiPhong, MoTa, TrangThai, NgayTao)
			VALUES (@TenPhong, @LoaiPhong, @MoTa, @TrangThai, @NgayTao)";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
		cmd.Parameters.AddWithValue("@LoaiPhong", phong.LoaiPhong ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@MoTa", phong.MoTa ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@TrangThai", phong.TrangThai);
		cmd.Parameters.AddWithValue("@NgayTao", phong.NgayTao);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	public async Task UpdateAsync(PhongChucNang phong)
	{
		const string sql = @"
			UPDATE PhongChucNang
			SET TenPhong = @TenPhong,
			    LoaiPhong = @LoaiPhong,
			    MoTa = @MoTa,
			    TrangThai = @TrangThai
			WHERE PhongChucNangID = @Id";

		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.AddWithValue("@Id", phong.Id);
		cmd.Parameters.AddWithValue("@TenPhong", phong.TenPhong);
		cmd.Parameters.AddWithValue("@LoaiPhong", phong.LoaiPhong ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@MoTa", phong.MoTa ?? (object)DBNull.Value);
		cmd.Parameters.AddWithValue("@TrangThai", phong.TrangThai);

		await conn.OpenAsync();
		await cmd.ExecuteNonQueryAsync();
	}

	private static PhongChucNang MapToEntity(SqlDataReader r)
		=> new(
			id: r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			tenPhong: r.GetString(r.GetOrdinal("TenPhong")),
			loaiPhong: r["LoaiPhong"] as string,
			moTa: r["MoTa"] as string,
			trangThai: r.GetString(r.GetOrdinal("TrangThai")),
			ngayTao: r.GetDateTime(r.GetOrdinal("NgayTao"))
		);
}
