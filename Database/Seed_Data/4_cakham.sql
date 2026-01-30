;WITH DanhSachNgay AS (
    SELECT LichLamViecID, Ngay, CaLamViec
    FROM LichLamViecNhanVien
    WHERE Ngay BETWEEN '2026-01-01' AND '2026-01-15'
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
        k.KhungGioID,
        d.Ngay,
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
    BenhNhanID,
    LyDoKham,
    TrangThai,
    NgayDat,
    GhiChu
)
SELECT
    CASE 
        WHEN SlotThuTu = 1 THEN N'Khám'
        WHEN SlotThuTu = 2 THEN N'Tái khám'
        ELSE N'Điều trị'
    END AS LoaiCaKham,

    LichLamViecID,
    KhungGioID,

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
    END AS PhongChucNangID,

    BenhNhanID,

    N'Khám da liễu theo lịch hẹn',

    N'Hoàn thành',

    DATEADD(DAY, -ABS(CHECKSUM(NEWID())) % 20, GETDATE()),

    N'Dữ liệu seed quá khứ'
FROM CaSinh;
GO
SELECT 
    Ngay,
    KhungGioID,
    COUNT(*) AS SoCa
FROM CaKham ck
JOIN LichLamViecNhanVien llv ON ck.LichLamViecID = llv.LichLamViecID
GROUP BY Ngay, KhungGioID
HAVING COUNT(*) <> 5;
