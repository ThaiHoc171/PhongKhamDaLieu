;WITH DanhSachNgay AS (
    SELECT LichLamViecID, Ngay, CaLamViec
    FROM LichLamViecNhanVien
    WHERE Ngay BETWEEN '2026-03-01' AND '2026-03-15'
),
KhungTheoCa AS (
    SELECT KhungGioID, CaLamViec
    FROM KhungGioKham
),
BenhNhanXepSo AS (
    SELECT 
        BenhNhanID,
        ROW_NUMBER() OVER (ORDER BY BenhNhanID) AS STT
    FROM BenhNhan
),
CaSinh AS (
    SELECT
        d.LichLamViecID,
        d.Ngay,
        k.KhungGioID,
        ROW_NUMBER() OVER (
            PARTITION BY d.LichLamViecID, k.KhungGioID
            ORDER BY bn.BenhNhanID
        ) AS SlotThuTu,
        bn.BenhNhanID
    FROM DanhSachNgay d
    JOIN KhungTheoCa k
        ON d.CaLamViec = k.CaLamViec
    CROSS APPLY (
        SELECT TOP 5 *
        FROM BenhNhanXepSo
        ORDER BY NEWID()
    ) bn
)

INSERT INTO CaKham
(
    LoaiCaKham,
    LichLamViecID,
    KhungGioID,
    PhongChucNangID,
    ThongTinID,
    LyDoKham,
    TrangThai,
    NgayDat,
    NgayKham
)
SELECT
    CASE 
        WHEN SlotThuTu <= 2 THEN N'Khám'
        ELSE N'Điều trị'
    END,

    cs.LichLamViecID,
    cs.KhungGioID,

    CASE 
        WHEN SlotThuTu <= 2 THEN (
            SELECT TOP 1 PhongChucNangID 
            FROM PhongChucNang 
            WHERE TenPhong LIKE N'%khám%'
        )
        ELSE (
            SELECT TOP 1 PhongChucNangID 
            FROM PhongChucNang 
            WHERE TenPhong LIKE N'%điều trị%'
        )
    END,

    cs.BenhNhanID,

    N'Khám da liễu theo lịch hẹn',

    CASE ABS(CHECKSUM(NEWID())) % 10
        WHEN 0 THEN N'Đã hủy'
        WHEN 1 THEN N'Không đến'
        ELSE N'Hoàn thành'
    END,

    DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 20, GETDATE()),

    cs.Ngay
FROM CaSinh cs;
GO
SELECT TrangThai, COUNT(*) 
FROM CaKham
GROUP BY TrangThai;
select * from CaKham;