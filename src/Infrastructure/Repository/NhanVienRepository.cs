using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class NhanVienRepository : INhanVienRepository
{
	private readonly string _connectionString;

	public NhanVienRepository(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection")!;
	}

	#region Queries

	private const string BaseSelectList = @"
        SELECT nv.NhanVienID, tt.HoTen, tt.EmailLienHe, cv.TenChucVu, nv.TrangThai
        FROM NhanVien nv
        JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
        JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID";

	private const string BaseSelectDetail = @"
        SELECT nv.NhanVienID, nv.ThongTinID, nv.NgayVaoLam, nv.BangCap, nv.KinhNghiem, nv.TrangThai, nv.NgayTao, nv.NgayCapNhat,               tt.HoTen, tt.NgaySinh, tt.GioiTinh, tt.SDT, tt.EmailLienHe, tt.DiaChi, tt.Avatar,
               cv.ChucVuID, cv.TenChucVu,
               pcn.PhongChucNangID, pcn.TenPhong
        FROM NhanVien nv
        JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
        JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
        JOIN PhongChucNang pcn ON nv.PhongChucNangID = pcn.PhongChucNangID";

	#endregion


	public async Task<(List<NhanVienReadListModel>, int)> GetPagedAsync(int page, int size)
	{
		var list = new List<NhanVienReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
            {BaseSelectList}
            ORDER BY nv.NhanVienID
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

            SELECT COUNT(*) FROM NhanVien";

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


	public async Task<(List<NhanVienReadListModel>, int)> SearchAsync(string keyword, int page, int size)
	{
		var list = new List<NhanVienReadListModel>();
		int total = 0;

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		int offset = (page - 1) * size;

		var sql = $@"
            {BaseSelectList}
            WHERE tt.HoTen LIKE @Keyword OR tt.EmailLienHe LIKE @Keyword
            ORDER BY nv.NhanVienID
            OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY;

            SELECT COUNT(*)
            FROM NhanVien nv
            JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
            WHERE tt.HoTen LIKE @Keyword OR tt.EmailLienHe LIKE @Keyword";

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


	public async Task<NhanVienReadModel?> GetDetailAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = BaseSelectDetail + " WHERE nv.NhanVienID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToDetailDTO(reader);

		return null;
	}


	public async Task<NhanVien?> GetByIdAsync(int id)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		const string sql = @"
            SELECT NhanVienID,ThongTinID,ChucVuID,PhongChucNangID,
                   NgayVaoLam,BangCap,KinhNghiem,TrangThai,
                   NgayTao,NgayCapNhat
            FROM NhanVien
            WHERE NhanVienID=@Id";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = id;

		using var reader = await cmd.ExecuteReaderAsync();

		if (await reader.ReadAsync())
			return MapToEntity(reader);

		return null;
	}


	public async Task<int> AddAsync(NhanVien nv)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"INSERT INTO NhanVien
                    (ThongTinID,ChucVuID,PhongChucNangID,NgayVaoLam,BangCap,KinhNghiem,TrangThai)
                    VALUES
                    (@ThongTinID,@ChucVuID,@PhongChucNangID,@NgayVaoLam,@BangCap,@KinhNghiem,@TrangThai)";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@ThongTinID", SqlDbType.Int).Value = nv.ThongTinID;
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = nv.ChucVuID;
		cmd.Parameters.Add("@PhongChucNangID", SqlDbType.Int).Value = nv.PhongChucNangID;
		cmd.Parameters.Add("@NgayVaoLam", SqlDbType.DateTime).Value = nv.NgayVaoLam;
		cmd.Parameters.Add("@BangCap", SqlDbType.NVarChar, -1).Value = nv.BangCap;
		cmd.Parameters.Add("@KinhNghiem", SqlDbType.NVarChar, -1).Value = nv.KinhNghiem;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = nv.TrangThai;

		return await cmd.ExecuteNonQueryAsync();
	}

	public async Task<int> UpdateAsync(NhanVien nv)
	{
		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"UPDATE NhanVien
                    SET ChucVuID=@ChucVuID,
                        PhongChucNangID=@PhongChucNangID,
                        NgayVaoLam=@NgayVaoLam,
                        BangCap=@BangCap,
                        KinhNghiem=@KinhNghiem,
                        TrangThai=@TrangThai,
                        NgayCapNhat=@NgayCapNhat
                    WHERE NhanVienID=@Id";

		using var cmd = new SqlCommand(sql, conn);

		cmd.Parameters.Add("@Id", SqlDbType.Int).Value = nv.NhanVienID;
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = nv.ChucVuID;
		cmd.Parameters.Add("@PhongChucNangID", SqlDbType.Int).Value = nv.PhongChucNangID;
		cmd.Parameters.Add("@NgayVaoLam", SqlDbType.DateTime).Value = nv.NgayVaoLam;
		cmd.Parameters.Add("@BangCap", SqlDbType.NVarChar, -1).Value = nv.BangCap;
		cmd.Parameters.Add("@KinhNghiem", SqlDbType.NVarChar, -1).Value = nv.KinhNghiem;
		cmd.Parameters.Add("@TrangThai", SqlDbType.NVarChar, 50).Value = nv.TrangThai;
		cmd.Parameters.Add("@NgayCapNhat", SqlDbType.DateTime).Value = nv.NgayCapNhat;

		return await cmd.ExecuteNonQueryAsync();
	}


	public async Task<List<NameResponseDTO>> GetComboboxAsync(int chucVuId)
	{
		var list = new List<NameResponseDTO>();

		using var conn = new SqlConnection(_connectionString);
		await conn.OpenAsync();

		var sql = @"
            SELECT nv.NhanVienID,tt.HoTen
            FROM NhanVien nv
            JOIN ThongTinCaNhan tt ON nv.ThongTinID = tt.ThongTinID
            WHERE nv.ChucVuID=@ChucVuID
            AND nv.TrangThai=N'Đang làm việc'
            ORDER BY tt.HoTen";

		using var cmd = new SqlCommand(sql, conn);
		cmd.Parameters.Add("@ChucVuID", SqlDbType.Int).Value = chucVuId;

		using var reader = await cmd.ExecuteReaderAsync();

		while (await reader.ReadAsync())
		{
			list.Add(new NameResponseDTO
			{
				Id = reader.GetInt32(reader.GetOrdinal("NhanVienID")),
				Name = reader.GetString(reader.GetOrdinal("HoTen"))
			});
		}

		return list;
	}


	#region Mapping

	private NhanVien MapToEntity(SqlDataReader r)
	{
		return new NhanVien(
			r.GetInt32(r.GetOrdinal("NhanVienID")),
			r.GetInt32(r.GetOrdinal("ThongTinID")),
			r.GetInt32(r.GetOrdinal("ChucVuID")),
			r.GetInt32(r.GetOrdinal("PhongChucNangID")),
			r.GetDateTime(r.GetOrdinal("NgayVaoLam")),
			r.GetString(r.GetOrdinal("BangCap")),
			r.GetString(r.GetOrdinal("KinhNghiem")),
			r.GetString(r.GetOrdinal("TrangThai")),
			r.GetDateTime(r.GetOrdinal("NgayTao")),
			r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		);
	}

	private NhanVienReadListModel MapToListDTO(SqlDataReader r)
	{
		return new NhanVienReadListModel
		{
			NhanVienID = r.GetInt32(r.GetOrdinal("NhanVienID")),
			HoTen = r.GetString(r.GetOrdinal("HoTen")),
			Email = r.GetString(r.GetOrdinal("EmailLienHe")),
			TenChucVu = r.GetString(r.GetOrdinal("TenChucVu")),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai"))
		};
	}

	private NhanVienReadModel MapToDetailDTO(SqlDataReader r)
	{
		return new NhanVienReadModel
		{
			NhanVienID = r.GetInt32(r.GetOrdinal("NhanVienID")),
			ThongTinID = r.GetInt32(r.GetOrdinal("ThongTinID")),

			ChucVu = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("ChucVuID")),
				Name = r.GetString(r.GetOrdinal("TenChucVu"))
			},

			PhongChucNang = new NameResponseDTO
			{
				Id = r.GetInt32(r.GetOrdinal("PhongChucNangID")),
				Name = r.GetString(r.GetOrdinal("TenPhong"))
			},

			HoTen = r.GetString(r.GetOrdinal("HoTen")),
			NgaySinh = r.GetDateTime(r.GetOrdinal("NgaySinh")),
			GioiTinh = r["GioiTinh"]?.ToString(),
			SDT = r["SDT"]?.ToString(),
			EmailLienHe = r.GetString(r.GetOrdinal("EmailLienHe")),
			DiaChi = r["DiaChi"]?.ToString(),
			Avatar = r["Avatar"]?.ToString(),
			NgayVaoLam = r.GetDateTime(r.GetOrdinal("NgayVaoLam")),
			BangCap = r["BangCap"]?.ToString(),
			KinhNghiem = r["KinhNghiem"]?.ToString(),
			TrangThai = r.GetString(r.GetOrdinal("TrangThai")),
			NgayTao = r.GetDateTime(r.GetOrdinal("NgayTao")),
			NgayCapNhat = r.IsDBNull(r.GetOrdinal("NgayCapNhat")) ? null : r.GetDateTime(r.GetOrdinal("NgayCapNhat"))
		};
	}

	#endregion
}