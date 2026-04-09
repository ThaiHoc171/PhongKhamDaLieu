using Application.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class ReminderBackgroundService : BackgroundService
{
    private readonly IFcmService _fcmService;
    private readonly string _connectionString;

    public ReminderBackgroundService(
        IFcmService fcmService,
        IConfiguration config)
    {
        _fcmService = fcmService;
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string not found.");
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddDays(1).AddHours(6);
            if (now.Hour < 8)
                nextRun = now.Date.AddHours(6);
            var delay = nextRun - now;
            await Task.Delay(delay, stoppingToken);
            await SendRemindersAsync();
        }
    }

    private async Task SendRemindersAsync()
    {
        const string sql = @"
            SELECT ck.CaKhamID, ck.NgayKham, tk.FCMToken
            FROM CaKham ck
            INNER JOIN BenhNhan bn ON ck.BenhNhanID = bn.BenhNhanID
            INNER JOIN ThongTinCaNhan ttcn ON bn.ThongTinID = ttcn.ThongTinID
            INNER JOIN TaiKhoan tk ON ttcn.TaiKhoanID = tk.TaiKhoanID
            WHERE ck.NgayKham = CAST(DATEADD(DAY, 1, GETDATE()) AS DATE)
              AND ck.TrangThai = N'Đã xác nhận'
              AND tk.FCMToken IS NOT NULL";

        try
        {
            await using var conn = new SqlConnection(_connectionString);
            await using var cmd = new SqlCommand(sql, conn);
            await conn.OpenAsync();
            await using var reader = await cmd.ExecuteReaderAsync();

            var records = new List<(int CaKhamId, DateTime NgayKham, string FCMToken)>();
            while (await reader.ReadAsync())
            {
                records.Add((
                    reader.GetInt32(reader.GetOrdinal("CaKhamID")),
                    reader.GetDateTime(reader.GetOrdinal("NgayKham")),
                    reader.GetString(reader.GetOrdinal("FCMToken"))
                ));
            }
            foreach (var (caKhamId, ngayKham, fcmToken) in records)
            {
                await _fcmService.SendAsync(
                    fcmToken,
                    title: "Nhắc nhở lịch khám ngày mai",
                    body: $"Bạn có lịch khám vào ngày {ngayKham:dd/MM/yyyy}. Đừng quên nhé!",
                    data: new Dictionary<string, string>
                    {
                        { "type", "nhac_nho" },
                        { "caKhamId", caKhamId.ToString() }
                    }
                );
            }
        }
        catch (Exception ex)
        {
        }
    }
}