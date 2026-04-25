using Application.DTOs.Dashboard;
using Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly string _connectionString;

    public DashboardRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
    public async Task<DashboardKpiReadModel> GetKpiAsync(DateTime today)
    {
        var sql = @"
        DECLARE @Today DATE = @TodayParam;

        -- 1. Bệnh nhân có ca khám hôm nay (đã xác nhận hoặc đang khám hoặc hoàn thành)
        SELECT COUNT(DISTINCT ck.ThongTinID)
        FROM CaKham ck
        WHERE ck.NgayKham = @Today
          AND ck.TrangThai IN (N'Đã xác nhận', N'Đang khám', N'Hoàn thành');

        -- 2. Ca khám còn lại hôm nay (chưa bắt đầu)
        SELECT COUNT(*)
        FROM CaKham
        WHERE NgayKham = @Today
          AND TrangThai IN (N'Đã đặt', N'Đã xác nhận');

        -- 3. Liệu trình đang chạy (toàn hệ thống)
        SELECT COUNT(*)
        FROM LieuTrinhDieuTri
        WHERE TrangThai = N'Đang điều trị';

        -- 4. Xét nghiệm cận lâm sàng chờ kết quả
        SELECT COUNT(*)
        FROM PhienKham_CanLamSang pcls
        INNER JOIN PhienKham pk ON pcls.PhienKhamID = pk.PhienKhamID
        WHERE pcls.TrangThai IN (N'Đang chờ', N'Đang thực hiện')
          AND CAST(pk.NgayKham AS DATE) = @Today;

        -- 5. Toa thuốc được kê hôm nay
        SELECT COUNT(*)
        FROM ToaThuoc tt
        WHERE CAST(tt.NgayLap AS DATE) = @Today;

        -- 6. Độ chính xác AI hôm nay (100 - AVG ErrorScore của các feedback hôm nay)
        SELECT ISNULL(ROUND(100 - AVG(CAST(ErrorScore AS FLOAT)), 1), 0)
        FROM AI_TrainingFeedback
        WHERE CAST(CreatedAt AS DATE) = @Today
          AND ErrorScore IS NOT NULL;";

        var kpi = new DashboardKpiReadModel();

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@TodayParam", SqlDbType.Date).Value = today.Date;

        using var reader = await cmd.ExecuteReaderAsync();

        if (await reader.ReadAsync()) kpi.BenhNhanHomNay = reader.GetInt32(0);
        await reader.NextResultAsync();
        if (await reader.ReadAsync()) kpi.CaKhamConLai = reader.GetInt32(0);
        await reader.NextResultAsync();
        if (await reader.ReadAsync()) kpi.LieuTrinhDangChay = reader.GetInt32(0);
        await reader.NextResultAsync();
        if (await reader.ReadAsync()) kpi.XetNghiemChoKetQua = reader.GetInt32(0);
        await reader.NextResultAsync();
        if (await reader.ReadAsync()) kpi.ToaThuocHomNay = reader.GetInt32(0);
        await reader.NextResultAsync();
        if (await reader.ReadAsync()) kpi.DoChinhXacAI = reader.GetDouble(0);

        return kpi;
    }
    public async Task<List<CaKhamTheoNgayReadModel>> GetCaKhamTheoTuanAsync(DateTime endDate)
    {
        var list = new List<CaKhamTheoNgayReadModel>();

        var sql = @"
        SELECT
            CAST(NgayKham AS DATE)          AS Ngay,
            SUM(CASE WHEN LoaiCaKham = N'Khám'        THEN 1 ELSE 0 END) AS SoKham,
            SUM(CASE WHEN LoaiCaKham = N'Điều trị'   THEN 1 ELSE 0 END) AS SoDieuTri
        FROM CaKham
        WHERE NgayKham >= @StartDate
          AND NgayKham <= @EndDate
          AND TrangThai <> N'Trống'
        GROUP BY CAST(NgayKham AS DATE)
        ORDER BY Ngay;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@StartDate", SqlDbType.Date).Value = endDate.Date.AddDays(-6);
        cmd.Parameters.Add("@EndDate",   SqlDbType.Date).Value = endDate.Date;

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new CaKhamTheoNgayReadModel
            {
                Ngay      = reader.GetDateTime(reader.GetOrdinal("Ngay")),
                SoKham    = reader.GetInt32(reader.GetOrdinal("SoKham")),
                SoDieuTri = reader.GetInt32(reader.GetOrdinal("SoDieuTri"))
            });
        }
        return list;
    }
    public async Task<List<TrangThaiCaKhamReadModel>> GetTrangThaiCaKhamAsync(int year, int month)
    {
        var list = new List<TrangThaiCaKhamReadModel>();

        var sql = @"
        SELECT TrangThai, COUNT(*) AS SoLuong
        FROM CaKham
        WHERE YEAR(NgayKham) = @Year
          AND MONTH(NgayKham) = @Month
          AND TrangThai <> N'Trống'
        GROUP BY TrangThai;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Year",  SqlDbType.Int).Value = year;
        cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new TrangThaiCaKhamReadModel
            {
                TrangThai = reader.GetString(reader.GetOrdinal("TrangThai")),
                SoLuong   = reader.GetInt32(reader.GetOrdinal("SoLuong"))
            });
        }
        return list;
    }

    public async Task<List<TopBenhReadModel>> GetTopBenhAsync(int year, int month, int top = 5)
    {
        var list = new List<TopBenhReadModel>();

        var sql = @"
        SELECT TOP (@Top)
            lb.TenBenh,
            COUNT(*) AS SoLuong
        FROM PhienKham_Benh pb
        INNER JOIN LoaiBenh lb ON pb.LoaiBenhID = lb.LoaiBenhID
        INNER JOIN PhienKham pk ON pb.PhienKhamID = pk.PhienKhamID
        WHERE YEAR(pk.NgayKham)  = @Year
          AND MONTH(pk.NgayKham) = @Month
          AND pb.LoaiChanDoan    = N'Chẩn đoán chính'
        GROUP BY lb.TenBenh
        ORDER BY SoLuong DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Top",   SqlDbType.Int).Value = top;
        cmd.Parameters.Add("@Year",  SqlDbType.Int).Value = year;
        cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new TopBenhReadModel
            {
                TenBenh  = reader.GetString(reader.GetOrdinal("TenBenh")),
                SoLuong  = reader.GetInt32(reader.GetOrdinal("SoLuong"))
            });
        }
        return list;
    }
    public async Task<List<TopBacSiReadModel>> GetTopBacSiAsync(int year, int month, int top = 4)
    {
        var list = new List<TopBacSiReadModel>();

        var sql = @"
        SELECT TOP (@Top)
            ttcn.HoTen,
            cv.TenChucVu,
            COUNT(*) AS SoPhienKham
        FROM PhienKham pk
        INNER JOIN NhanVien nv    ON pk.NhanVienID   = nv.NhanVienID
        INNER JOIN ThongTinCaNhan ttcn ON nv.ThongTinID = ttcn.ThongTinID
        INNER JOIN ChucVu cv      ON nv.ChucVuID     = cv.ChucVuID
        WHERE YEAR(pk.NgayKham)  = @Year
          AND MONTH(pk.NgayKham) = @Month
          AND pk.TrangThai = N'Hoàn thành'
        GROUP BY ttcn.HoTen, cv.TenChucVu
        ORDER BY SoPhienKham DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Top",   SqlDbType.Int).Value = top;
        cmd.Parameters.Add("@Year",  SqlDbType.Int).Value = year;
        cmd.Parameters.Add("@Month", SqlDbType.Int).Value = month;

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new TopBacSiReadModel
            {
                HoTen        = reader.GetString(reader.GetOrdinal("HoTen")),
                TenChucVu    = reader.GetString(reader.GetOrdinal("TenChucVu")),
                SoPhienKham  = reader.GetInt32(reader.GetOrdinal("SoPhienKham"))
            });
        }
        return list;
    }

    public async Task<List<LieuTrinhProgressReadModel>> GetLieuTrinhDangDieuTriAsync(int top = 4)
    {
        var list = new List<LieuTrinhProgressReadModel>();

        var sql = @"
            SELECT TOP (@Top)
                lt.LieuTrinhID,
                lt.TenLieuTrinh,
                ttcn.HoTen AS TenBenhNhan,
                lt.TongSoBuoi,
                COUNT(CASE WHEN lb.TrangThai = N'Hoàn thành' THEN 1 END)  AS SoBuoiHoanThanh
            FROM LieuTrinhDieuTri lt
            INNER JOIN BenhNhan         bn    ON lt.BenhNhanID = bn.BenhNhanID
            INNER JOIN ThongTinCaNhan   ttcn  ON bn.ThongTinID = ttcn.ThongTinID
            LEFT  JOIN LieuTrinh_BuoiDieuTri lb ON lt.LieuTrinhID = lb.LieuTrinhID
            WHERE lt.TrangThai = N'Đang điều trị'
            GROUP BY
                lt.LieuTrinhID,
                lt.TenLieuTrinh,
                ttcn.HoTen,
                lt.TongSoBuoi,
                lt.NgayBatDau
            ORDER BY lt.NgayBatDau DESC";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new LieuTrinhProgressReadModel
            {
                LieuTrinhID       = reader.GetInt32(reader.GetOrdinal("LieuTrinhID")),
                TenLieuTrinh      = reader.GetString(reader.GetOrdinal("TenLieuTrinh")),
                TenBenhNhan       = reader.GetString(reader.GetOrdinal("TenBenhNhan")),
                TongSoBuoi        = reader.GetInt32(reader.GetOrdinal("TongSoBuoi")),
                SoBuoiHoanThanh   = reader.GetInt32(reader.GetOrdinal("SoBuoiHoanThanh"))
            });
        }
        return list;
    }

    public async Task<List<HoatDongReadModel>> GetHoatDongGanDayAsync(int take = 6)
    {
        var list = new List<HoatDongReadModel>();

        var sql = @"
        SELECT TOP (@Take) *
        FROM (
            -- Phiên khám hoàn thành
            SELECT
                pk.NgayKham     AS ThoiGian,
                N'Phiên khám'   AS LoaiSuKien,
                N'Hoàn thành phiên khám - ' + ttcn.HoTen AS MoTa
            FROM PhienKham pk
            INNER JOIN BenhNhan bn        ON pk.BenhNhanID = bn.BenhNhanID
            INNER JOIN ThongTinCaNhan ttcn ON bn.ThongTinID = ttcn.ThongTinID
            WHERE pk.TrangThai = N'Hoàn thành'

            UNION ALL

            -- Toa thuốc vừa kê
            SELECT
                tt.NgayLap,
                N'Toa thuốc',
                N'Toa thuốc #' + CAST(tt.ToaThuocID AS NVARCHAR) +
                    N' kê bởi ' + ttcn.HoTen
            FROM ToaThuoc tt
            INNER JOIN NhanVien nv         ON tt.NhanVienKeDonID = nv.NhanVienID
            INNER JOIN ThongTinCaNhan ttcn  ON nv.ThongTinID      = ttcn.ThongTinID

            UNION ALL

            -- Xét nghiệm có kết quả
            SELECT
                pcls.NgayThucHien,
                N'Xét nghiệm',
                N'Kết quả xét nghiệm: ' + cls.TenCLS
            FROM PhienKham_CanLamSang pcls
            INNER JOIN CanLamSang cls ON pcls.CanLamSangID = cls.CanLamSangID
            WHERE pcls.TrangThai = N'Hoàn thành'
              AND pcls.KetQua IS NOT NULL

            UNION ALL

            -- Tái khám vừa được tạo
            SELECT
                tk.NgayTao,
                N'Tái khám',
                N'Chỉ định tái khám cho ' + ttcn.HoTen +
                    N' ngày ' + CONVERT(NVARCHAR, tk.NgayDuKien, 103)
            FROM TaiKham tk
            INNER JOIN BenhNhan bn        ON tk.BenhNhanID = bn.BenhNhanID
            INNER JOIN ThongTinCaNhan ttcn ON bn.ThongTinID = ttcn.ThongTinID
        ) AS Combined
        ORDER BY ThoiGian DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Take", SqlDbType.Int).Value = take;

        using var reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            list.Add(new HoatDongReadModel
            {
                ThoiGian    = reader.GetDateTime(reader.GetOrdinal("ThoiGian")),
                LoaiSuKien  = reader.GetString(reader.GetOrdinal("LoaiSuKien")),
                MoTa        = reader.GetString(reader.GetOrdinal("MoTa"))
            });
        }
        return list;
    }
}
