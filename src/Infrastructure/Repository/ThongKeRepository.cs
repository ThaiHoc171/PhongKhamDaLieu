using Application.DTOs.ThongKe;
using Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Infrastructure.Repositories;

public class ThongKeRepository : IThongKeRepository
{
    private readonly string _connectionString;

    public ThongKeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")!;
    }

    public async Task<TongQuanBenhNhanReadModel> GetTongQuanBenhNhanAsync(DateTime tuNgay, DateTime denNgay)
    {
        var sql = @"
        -- Tổng bệnh nhân trong khoảng (dựa theo ngày tạo BenhNhan)
        SELECT COUNT(*) FROM BenhNhan
        WHERE CAST(NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay;

        -- Bệnh nhân mới (lần đầu xuất hiện, không có phiên khám nào trước khoảng)
        SELECT COUNT(*) FROM BenhNhan bn
        WHERE CAST(bn.NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay
          AND NOT EXISTS (
              SELECT 1 FROM PhienKham pk
              WHERE pk.BenhNhanID = bn.BenhNhanID
                AND CAST(pk.NgayKham AS DATE) < @TuNgay
          );

        -- Bệnh nhân tái khám (có ít nhất 1 TaiKham hoàn thành trong khoảng)
        SELECT COUNT(DISTINCT tk.BenhNhanID) FROM TaiKham tk
        WHERE tk.TrangThai = N'Đã khám'
          AND EXISTS (
              SELECT 1 FROM PhienKham pk
              WHERE pk.BenhNhanID = tk.BenhNhanID
                AND CAST(pk.NgayKham AS DATE) BETWEEN @TuNgay AND @DenNgay
          );

        -- Bệnh nhân có tài khoản (ThongTinCaNhan.TaiKhoanID không null)
        SELECT COUNT(*) FROM BenhNhan bn
        INNER JOIN ThongTinCaNhan ttcn ON bn.ThongTinID = ttcn.ThongTinID
        WHERE ttcn.TaiKhoanID IS NOT NULL
          AND CAST(bn.NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay;";

        var model = new TongQuanBenhNhanReadModel();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync()) model.TongBenhNhan       = r.GetInt32(0);
        await r.NextResultAsync();
        if (await r.ReadAsync()) model.BenhNhanMoi        = r.GetInt32(0);
        await r.NextResultAsync();
        if (await r.ReadAsync()) model.BenhNhanTaiKham    = r.GetInt32(0);
        await r.NextResultAsync();
        if (await r.ReadAsync()) model.BenhNhanCoTaiKhoan = r.GetInt32(0);
        return model;
    }

    public async Task<List<BenhNhanTheoNgayReadModel>> GetBenhNhanTheoNgayAsync(DateTime tuNgay, DateTime denNgay)
    {
        var list = new List<BenhNhanTheoNgayReadModel>();
        var sql = @"
        SELECT CAST(NgayTao AS DATE) AS Ngay, COUNT(*) AS SoBenhNhanMoi
        FROM BenhNhan
        WHERE CAST(NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY CAST(NgayTao AS DATE)
        ORDER BY Ngay;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new BenhNhanTheoNgayReadModel
            {
                Ngay           = r.GetDateTime(r.GetOrdinal("Ngay")),
                SoBenhNhanMoi  = r.GetInt32(r.GetOrdinal("SoBenhNhanMoi"))
            });
        return list;
    }

    public async Task<List<BenhNhanTheoGioiTinhReadModel>> GetBenhNhanTheoGioiTinhAsync(DateTime tuNgay, DateTime denNgay)
    {
        var list = new List<BenhNhanTheoGioiTinhReadModel>();
        var sql = @"
        SELECT ttcn.GioiTinh, COUNT(*) AS SoLuong
        FROM BenhNhan bn
        INNER JOIN ThongTinCaNhan ttcn ON bn.ThongTinID = ttcn.ThongTinID
        WHERE CAST(bn.NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY ttcn.GioiTinh;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new BenhNhanTheoGioiTinhReadModel
            {
                GioiTinh = r.GetString(r.GetOrdinal("GioiTinh")),
                SoLuong  = r.GetInt32(r.GetOrdinal("SoLuong"))
            });
        return list;
    }

    public async Task<List<BenhNhanTheoDoTuoiReadModel>> GetBenhNhanTheoDoTuoiAsync(DateTime tuNgay, DateTime denNgay)
    {
        var list = new List<BenhNhanTheoDoTuoiReadModel>();
        var sql = @"
        SELECT
            CASE
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 18  THEN N'Dưới 18'
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 30  THEN N'18 – 29'
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 45  THEN N'30 – 44'
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 60  THEN N'45 – 59'
                ELSE N'60+'
            END AS NhomTuoi,
            COUNT(*) AS SoLuong
        FROM BenhNhan bn
        INNER JOIN ThongTinCaNhan ttcn ON bn.ThongTinID = ttcn.ThongTinID
        WHERE CAST(bn.NgayTao AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY
            CASE
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 18  THEN N'Dưới 18'
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 30  THEN N'18 – 29'
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 45  THEN N'30 – 44'
                WHEN DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()) < 60  THEN N'45 – 59'
                ELSE N'60+'
            END
        ORDER BY MIN(DATEDIFF(YEAR, ttcn.NgaySinh, GETDATE()));";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new BenhNhanTheoDoTuoiReadModel
            {
                NhomTuoi = r.GetString(r.GetOrdinal("NhomTuoi")),
                SoLuong  = r.GetInt32(r.GetOrdinal("SoLuong"))
            });
        return list;
    }


    public async Task<TongQuanCaKhamReadModel> GetTongQuanCaKhamAsync(DateTime tuNgay, DateTime denNgay)
    {
        var sql = @"
        SELECT
            COUNT(*)                                                                   AS TongCaKham,
            SUM(CASE WHEN TrangThai = N'Hoàn thành'  THEN 1 ELSE 0 END)               AS HoanThanh,
            SUM(CASE WHEN TrangThai = N'Đã hủy'      THEN 1 ELSE 0 END)               AS DaHuy,
            SUM(CASE WHEN TrangThai = N'Không đến'   THEN 1 ELSE 0 END)               AS KhongDen,
            SUM(CASE WHEN TrangThai = N'Đang khám'   THEN 1 ELSE 0 END)               AS DangKham
        FROM CaKham
        WHERE NgayKham BETWEEN @TuNgay AND @DenNgay
          AND TrangThai <> N'Trống';";

        var model = new TongQuanCaKhamReadModel();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            model.TongCaKham  = r.GetInt32(r.GetOrdinal("TongCaKham"));
            model.HoanThanh   = r.GetInt32(r.GetOrdinal("HoanThanh"));
            model.DaHuy       = r.GetInt32(r.GetOrdinal("DaHuy"));
            model.KhongDen    = r.GetInt32(r.GetOrdinal("KhongDen"));
            model.DangKham    = r.GetInt32(r.GetOrdinal("DangKham"));
        }
        return model;
    }

    public async Task<List<CaKhamTheoKhoangReadModel>> GetCaKhamTheoKhoangAsync(
        DateTime tuNgay, DateTime denNgay, string loaiKhoang)
    {
        var list = new List<CaKhamTheoKhoangReadModel>();

        var (groupExpr, labelExpr) = BuildGroupExpr("NgayKham", loaiKhoang);

        var sql = $@"
        SELECT
            {labelExpr}                                                       AS NhanX,
            MIN(NgayKham)                                                     AS TuNgay,
            SUM(CASE WHEN LoaiCaKham = N'Khám'       THEN 1 ELSE 0 END)      AS SoKham,
            SUM(CASE WHEN LoaiCaKham = N'Điều trị'   THEN 1 ELSE 0 END)      AS SoDieuTri
        FROM CaKham
        WHERE NgayKham BETWEEN @TuNgay AND @DenNgay
          AND TrangThai <> N'Trống'
        GROUP BY {groupExpr}
        ORDER BY MIN(NgayKham);";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new CaKhamTheoKhoangReadModel
            {
                NhanX     = r.GetString(r.GetOrdinal("NhanX")),
                TuNgay    = r.GetDateTime(r.GetOrdinal("TuNgay")),
                SoKham    = r.GetInt32(r.GetOrdinal("SoKham")),
                SoDieuTri = r.GetInt32(r.GetOrdinal("SoDieuTri"))
            });
        return list;
    }

    public async Task<TongQuanPhienKhamReadModel> GetTongQuanPhienKhamAsync(DateTime tuNgay, DateTime denNgay)
    {
        var sql = @"
        SELECT
            COUNT(*)                                                              AS TongPhienKham,
            SUM(CASE WHEN TrangThai = N'Hoàn thành'  THEN 1 ELSE 0 END)         AS HoanThanh,
            SUM(CASE WHEN TrangThai = N'Đang khám'   THEN 1 ELSE 0 END)         AS DangKham,
            SUM(CASE WHEN TrangThai = N'Đang chờ'    THEN 1 ELSE 0 END)         AS DangCho,
            SUM(CASE WHEN TrangThai = N'Đã hủy'      THEN 1 ELSE 0 END)         AS DaHuy
        FROM PhienKham
        WHERE CAST(NgayKham AS DATE) BETWEEN @TuNgay AND @DenNgay;";

        var model = new TongQuanPhienKhamReadModel();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            model.TongPhienKham = r.GetInt32(r.GetOrdinal("TongPhienKham"));
            model.HoanThanh     = r.GetInt32(r.GetOrdinal("HoanThanh"));
            model.DangKham      = r.GetInt32(r.GetOrdinal("DangKham"));
            model.DangCho       = r.GetInt32(r.GetOrdinal("DangCho"));
            model.DaHuy         = r.GetInt32(r.GetOrdinal("DaHuy"));
        }
        return model;
    }

    public async Task<List<PhienKhamTheoNgayReadModel>> GetPhienKhamTheoNgayAsync(DateTime tuNgay, DateTime denNgay)
    {
        var list = new List<PhienKhamTheoNgayReadModel>();
        var sql = @"
        SELECT
            CAST(NgayKham AS DATE)                                              AS Ngay,
            SUM(CASE WHEN TrangThai = N'Hoàn thành' THEN 1 ELSE 0 END)         AS SoHoanThanh,
            SUM(CASE WHEN TrangThai = N'Đang khám'  THEN 1 ELSE 0 END)         AS SoDangKham,
            SUM(CASE WHEN TrangThai = N'Đang chờ'   THEN 1 ELSE 0 END)         AS SoDangCho,
            SUM(CASE WHEN TrangThai = N'Đã hủy'     THEN 1 ELSE 0 END)         AS SoDaHuy
        FROM PhienKham
        WHERE CAST(NgayKham AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY CAST(NgayKham AS DATE)
        ORDER BY Ngay;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new PhienKhamTheoNgayReadModel
            {
                Ngay          = r.GetDateTime(r.GetOrdinal("Ngay")),
                SoHoanThanh   = r.GetInt32(r.GetOrdinal("SoHoanThanh")),
                SoDangKham    = r.GetInt32(r.GetOrdinal("SoDangKham")),
                SoDangCho     = r.GetInt32(r.GetOrdinal("SoDangCho")),
                SoDaHuy       = r.GetInt32(r.GetOrdinal("SoDaHuy"))
            });
        return list;
    }

    public async Task<List<PhienKhamTheoPhongReadModel>> GetPhienKhamTheoPhongAsync(DateTime tuNgay, DateTime denNgay)
    {
        var list = new List<PhienKhamTheoPhongReadModel>();
        var sql = @"
        SELECT pcn.TenPhong, COUNT(*) AS SoPhienKham
        FROM PhienKham pk
        INNER JOIN PhongChucNang pcn ON pk.PhongChucNangID = pcn.PhongChucNangID
        WHERE CAST(pk.NgayKham AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY pcn.TenPhong
        ORDER BY SoPhienKham DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new PhienKhamTheoPhongReadModel
            {
                TenPhong    = r.GetString(r.GetOrdinal("TenPhong")),
                SoPhienKham = r.GetInt32(r.GetOrdinal("SoPhienKham"))
            });
        return list;
    }

    public async Task<List<PhienKhamTheoLoaiBenhReadModel>> GetPhienKhamTheoLoaiBenhAsync(
        DateTime tuNgay, DateTime denNgay, int top = 10)
    {
        var list = new List<PhienKhamTheoLoaiBenhReadModel>();
        var sql = @"
        SELECT TOP (@Top)
            lb.TenBenh,
            ISNULL(lb.NhomBenh, N'Khác') AS NhomBenh,
            COUNT(*) AS SoLuong
        FROM PhienKham_Benh pb
        INNER JOIN LoaiBenh lb  ON pb.LoaiBenhID  = lb.LoaiBenhID
        INNER JOIN PhienKham pk ON pb.PhienKhamID = pk.PhienKhamID
        WHERE CAST(pk.NgayKham AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY lb.TenBenh, lb.NhomBenh
        ORDER BY SoLuong DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new PhienKhamTheoLoaiBenhReadModel
            {
                TenBenh  = r.GetString(r.GetOrdinal("TenBenh")),
                NhomBenh = r.GetString(r.GetOrdinal("NhomBenh")),
                SoLuong  = r.GetInt32(r.GetOrdinal("SoLuong"))
            });
        return list;
    }


    public async Task<TongQuanToaThuocReadModel> GetTongQuanToaThuocAsync(DateTime tuNgay, DateTime denNgay)
    {
        var sql = @"
        SELECT
            COUNT(DISTINCT tt.ToaThuocID)  AS TongToaThuoc,
            ISNULL(SUM(ct.SoLuong), 0)     AS TongLuotThuoc
        FROM ToaThuoc tt
        LEFT JOIN ChiTietToaThuoc ct ON tt.ToaThuocID = ct.ToaThuocID
        WHERE CAST(tt.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay;";

        var model = new TongQuanToaThuocReadModel();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            model.TongToaThuoc  = r.GetInt32(r.GetOrdinal("TongToaThuoc"));
            model.TongLuotThuoc = r.GetInt32(r.GetOrdinal("TongLuotThuoc"));
            model.TrungBinhThuocPerToa = model.TongToaThuoc > 0
                ? model.TongLuotThuoc / model.TongToaThuoc : 0;
        }
        return model;
    }

    public async Task<List<ToaThuocTheoKhoangReadModel>> GetToaThuocTheoKhoangAsync(
        DateTime tuNgay, DateTime denNgay, string loaiKhoang)
    {
        var list = new List<ToaThuocTheoKhoangReadModel>();
        var (groupExpr, labelExpr) = BuildGroupExpr("tt.NgayLap", loaiKhoang);

        var sql = $@"
        SELECT
            {labelExpr}                         AS NhanX,
            MIN(tt.NgayLap)                     AS TuNgay,
            COUNT(DISTINCT tt.ToaThuocID)       AS SoToaThuoc,
            ISNULL(SUM(ct.SoLuong), 0)          AS SoLuotThuoc
        FROM ToaThuoc tt
        LEFT JOIN ChiTietToaThuoc ct ON tt.ToaThuocID = ct.ToaThuocID
        WHERE CAST(tt.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY {groupExpr}
        ORDER BY MIN(tt.NgayLap);";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new ToaThuocTheoKhoangReadModel
            {
                NhanX       = r.GetString(r.GetOrdinal("NhanX")),
                TuNgay      = r.GetDateTime(r.GetOrdinal("TuNgay")),
                SoToaThuoc  = r.GetInt32(r.GetOrdinal("SoToaThuoc")),
                SoLuotThuoc = r.GetInt32(r.GetOrdinal("SoLuotThuoc"))
            });
        return list;
    }

    public async Task<List<TopThuocReadModel>> GetTopThuocAsync(DateTime tuNgay, DateTime denNgay, int top = 10)
    {
        var list = new List<TopThuocReadModel>();
        var sql = @"
        SELECT TOP (@Top)
            t.ThuocID,
            t.TenThuoc,
            ISNULL(t.HoatChat, N'') AS HoatChat,
            COUNT(*)                AS TongSoLan,
            SUM(ct.SoLuong)         AS TongSoLuong
        FROM ChiTietToaThuoc ct
        INNER JOIN Thuoc t      ON ct.ThuocID     = t.ThuocID
        INNER JOIN ToaThuoc tt  ON ct.ToaThuocID  = tt.ToaThuocID
        WHERE CAST(tt.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY t.ThuocID, t.TenThuoc, t.HoatChat
        ORDER BY TongSoLan DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new TopThuocReadModel
            {
                ThuocID      = r.GetInt32(r.GetOrdinal("ThuocID")),
                TenThuoc     = r.GetString(r.GetOrdinal("TenThuoc")),
                HoatChat     = r.GetString(r.GetOrdinal("HoatChat")),
                TongSoLan    = r.GetInt32(r.GetOrdinal("TongSoLan")),
                TongSoLuong  = r.GetInt32(r.GetOrdinal("TongSoLuong"))
            });
        return list;
    }

    public async Task<List<TopBacSiKeDonReadModel>> GetTopBacSiKeDonAsync(DateTime tuNgay, DateTime denNgay, int top = 5)
    {
        var list = new List<TopBacSiKeDonReadModel>();
        var sql = @"
        SELECT TOP (@Top)
            ttcn.HoTen,
            COUNT(*) AS SoToaThuoc
        FROM ToaThuoc tt
        INNER JOIN NhanVien nv         ON tt.NhanVienKeDonID = nv.NhanVienID
        INNER JOIN ThongTinCaNhan ttcn  ON nv.ThongTinID      = ttcn.ThongTinID
        WHERE CAST(tt.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
        GROUP BY ttcn.HoTen
        ORDER BY SoToaThuoc DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        cmd.Parameters.Add("@Top", SqlDbType.Int).Value = top;
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new TopBacSiKeDonReadModel
            {
                HoTen      = r.GetString(r.GetOrdinal("HoTen")),
                SoToaThuoc = r.GetInt32(r.GetOrdinal("SoToaThuoc"))
            });
        return list;
    }


    public async Task<TongQuanNhanVienReadModel> GetTongQuanNhanVienAsync()
    {
        var sql = @"
        SELECT
            COUNT(*)                                                              AS TongNhanVien,
            SUM(CASE WHEN TrangThai = N'Đang làm việc' THEN 1 ELSE 0 END)        AS DangLamViec,
            SUM(CASE WHEN TrangThai = N'Nghỉ việc'     THEN 1 ELSE 0 END)        AS NghiViec
        FROM NhanVien;";

        var model = new TongQuanNhanVienReadModel();
        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);

        using var r = await cmd.ExecuteReaderAsync();
        if (await r.ReadAsync())
        {
            model.TongNhanVien = r.GetInt32(r.GetOrdinal("TongNhanVien"));
            model.DangLamViec  = r.GetInt32(r.GetOrdinal("DangLamViec"));
            model.NghiViec     = r.GetInt32(r.GetOrdinal("NghiViec"));
        }
        return model;
    }

    public async Task<List<NhanVienTheoChucVuReadModel>> GetNhanVienTheoChucVuAsync()
    {
        var list = new List<NhanVienTheoChucVuReadModel>();
        var sql = @"
        SELECT cv.TenChucVu, COUNT(*) AS SoLuong
        FROM NhanVien nv
        INNER JOIN ChucVu cv ON nv.ChucVuID = cv.ChucVuID
        WHERE nv.TrangThai = N'Đang làm việc'
        GROUP BY cv.TenChucVu
        ORDER BY SoLuong DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new NhanVienTheoChucVuReadModel
            {
                TenChucVu = r.GetString(r.GetOrdinal("TenChucVu")),
                SoLuong   = r.GetInt32(r.GetOrdinal("SoLuong"))
            });
        return list;
    }

    public async Task<List<NhanVienTheoPhongReadModel>> GetNhanVienTheoPhongAsync()
    {
        var list = new List<NhanVienTheoPhongReadModel>();
        var sql = @"
        SELECT pcn.TenPhong, COUNT(*) AS SoLuong
        FROM NhanVien nv
        INNER JOIN PhongChucNang pcn ON nv.PhongChucNangID = pcn.PhongChucNangID
        WHERE nv.TrangThai = N'Đang làm việc'
        GROUP BY pcn.TenPhong
        ORDER BY SoLuong DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new NhanVienTheoPhongReadModel
            {
                TenPhong = r.GetString(r.GetOrdinal("TenPhong")),
                SoLuong  = r.GetInt32(r.GetOrdinal("SoLuong"))
            });
        return list;
    }

    public async Task<List<HieuSuatBacSiReadModel>> GetHieuSuatBacSiAsync(DateTime tuNgay, DateTime denNgay)
    {
        var list = new List<HieuSuatBacSiReadModel>();
        var sql = @"
        SELECT
            nv.NhanVienID,
            ttcn.HoTen,
            cv.TenChucVu,
            COUNT(DISTINCT pk.PhienKhamID)                                         AS SoPhienKham,
            SUM(CASE WHEN pk.TrangThai = N'Hoàn thành' THEN 1 ELSE 0 END)         AS SoHoanThanh,
            COUNT(DISTINCT tt.ToaThuocID)                                          AS SoToaThuoc
        FROM NhanVien nv
        INNER JOIN ThongTinCaNhan ttcn ON nv.ThongTinID  = ttcn.ThongTinID
        INNER JOIN ChucVu cv           ON nv.ChucVuID    = cv.ChucVuID
        LEFT  JOIN PhienKham pk        ON pk.NhanVienID  = nv.NhanVienID
            AND CAST(pk.NgayKham AS DATE) BETWEEN @TuNgay AND @DenNgay
        LEFT  JOIN ToaThuoc tt         ON tt.NhanVienKeDonID = nv.NhanVienID
            AND CAST(tt.NgayLap AS DATE) BETWEEN @TuNgay AND @DenNgay
        WHERE nv.TrangThai = N'Đang làm việc'
        GROUP BY nv.NhanVienID, ttcn.HoTen, cv.TenChucVu
        ORDER BY SoPhienKham DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new HieuSuatBacSiReadModel
            {
                NhanVienID  = r.GetInt32(r.GetOrdinal("NhanVienID")),
                HoTen       = r.GetString(r.GetOrdinal("HoTen")),
                TenChucVu   = r.GetString(r.GetOrdinal("TenChucVu")),
                SoPhienKham = r.GetInt32(r.GetOrdinal("SoPhienKham")),
                SoHoanThanh = r.GetInt32(r.GetOrdinal("SoHoanThanh")),
                SoToaThuoc  = r.GetInt32(r.GetOrdinal("SoToaThuoc"))
            });
        return list;
    }

    public async Task<List<NgayNghiNhanVienReadModel>> GetNgayNghiNhanVienAsync(DateTime tuNgay, DateTime denNgay)
    {
        var list = new List<NgayNghiNhanVienReadModel>();
        var sql = @"
        SELECT ttcn.HoTen, COUNT(*) AS SoNgayNghi
        FROM NgayNghiNhanVien nn
        INNER JOIN NhanVien nv         ON nn.NhanVienID = nv.NhanVienID
        INNER JOIN ThongTinCaNhan ttcn  ON nv.ThongTinID = ttcn.ThongTinID
        WHERE nn.Ngay BETWEEN @TuNgay AND @DenNgay
        GROUP BY ttcn.HoTen
        ORDER BY SoNgayNghi DESC;";

        using var conn = new SqlConnection(_connectionString);
        await conn.OpenAsync();
        using var cmd = new SqlCommand(sql, conn);
        AddDateParams(cmd, tuNgay, denNgay);

        using var r = await cmd.ExecuteReaderAsync();
        while (await r.ReadAsync())
            list.Add(new NgayNghiNhanVienReadModel
            {
                HoTen       = r.GetString(r.GetOrdinal("HoTen")),
                SoNgayNghi  = r.GetInt32(r.GetOrdinal("SoNgayNghi"))
            });
        return list;
    }


    private static void AddDateParams(SqlCommand cmd, DateTime tuNgay, DateTime denNgay)
    {
        cmd.Parameters.Add("@TuNgay",  SqlDbType.Date).Value = tuNgay.Date;
        cmd.Parameters.Add("@DenNgay", SqlDbType.Date).Value = denNgay.Date;
    }

    private static (string groupExpr, string labelExpr) BuildGroupExpr(string col, string loaiKhoang)
    {
        return loaiKhoang.ToLower() switch
        {
            "day"   => ($"CAST({col} AS DATE)",
                        $"CONVERT(NVARCHAR, CAST({col} AS DATE), 103)"),        // dd/MM/yyyy

            "week"  => ($"YEAR({col}), DATEPART(ISO_WEEK, {col})",
                        $"N'T' + CAST(DATEPART(ISO_WEEK, {col}) AS NVARCHAR) + N'/' + CAST(YEAR({col}) AS NVARCHAR)"),

            "month" => ($"YEAR({col}), MONTH({col})",
                        $"N'Tháng ' + CAST(MONTH({col}) AS NVARCHAR) + N'/' + CAST(YEAR({col}) AS NVARCHAR)"),

            _       => ($"YEAR({col})",                                          // year
                        $"CAST(YEAR({col}) AS NVARCHAR)")
        };
    }
}
