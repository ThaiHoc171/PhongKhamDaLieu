using Application.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Application.Interfaces;
namespace Infrastructure.Repositories;
public class QuyenRepository : IQuyenRepository
{
	private readonly string _connectionString;
	public QuyenRepository(IConfiguration config)
	{
		_connectionString = config.GetConnectionString("DefaultConnection")!;
	}
	public async Task<List<NameResponseDTO>> GetAllAsync()
	{
		const string sql = @"
            SELECT QuyenID, TenQuyen
            FROM Quyen
        ";
		var list = new List<NameResponseDTO>();
		await using var conn = new SqlConnection(_connectionString);
		await using var cmd = new SqlCommand(sql, conn);
		await conn.OpenAsync();
		await using var reader = await cmd.ExecuteReaderAsync();
		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(0),
				Name = reader.GetString(1)
			});
		}
		return list;
	}
}