INSERT INTO NgayNghiNhanVien (NhanVienID, Ngay, LyDo)
VALUES
-- Bác sĩ khám
(2, '2026-01-06', N'Nghỉ cá nhân'),
(2, '2026-01-18', N'Nghỉ phép'),
(3, '2026-01-12', N'Nghỉ cá nhân'),
(3, '2026-01-24', N'Nghỉ phép'),

-- Bác sĩ điều trị
(4, '2026-01-08', N'Nghỉ phép'),
(4, '2026-01-20', N'Nghỉ cá nhân'),
(5, '2026-01-10', N'Nghỉ phép'),
(5, '2026-01-26', N'Nghỉ cá nhân'),

-- Y tá
(6, '2026-01-05', N'Nghỉ phép'),
(7, '2026-01-07', N'Nghỉ cá nhân'),
(8, '2026-01-15', N'Nghỉ phép'),
(9, '2026-01-22', N'Nghỉ cá nhân'),

-- KTV
(10, '2026-01-09', N'Nghỉ phép'),
(11, '2026-01-21', N'Nghỉ cá nhân'),

-- Lễ tân
(13, '2026-01-11', N'Nghỉ phép'),
(14, '2026-01-27', N'Nghỉ cá nhân');
GO
---======================================================================---
-- Bác sĩ khám
DECLARE @BacSiKham TABLE (NhanVienID INT, STT INT);
INSERT INTO @BacSiKham VALUES (2,1),(3,2);

-- Bác sĩ điều trị
DECLARE @BacSiDieuTri TABLE (NhanVienID INT, STT INT);
INSERT INTO @BacSiDieuTri VALUES (4,1),(5,2);

-- Y tá
DECLARE @YTa TABLE (NhanVienID INT, STT INT);
INSERT INTO @YTa VALUES (6,1),(7,2),(8,3),(9,4);

-- Kỹ thuật viên
DECLARE @KTV TABLE (NhanVienID INT, STT INT);
INSERT INTO @KTV VALUES (10,1),(11,2);

-- Lễ tân
DECLARE @LeTan TABLE (NhanVienID INT, STT INT);
INSERT INTO @LeTan VALUES (13,1),(14,2);

DECLARE @Ngay DATE = '2026-01-01';

WHILE @Ngay <= '2026-01-30'
BEGIN
    DECLARE @Ca INT = CASE WHEN DAY(@Ngay) % 2 = 0 THEN 1 ELSE 2 END;
    DECLARE @Index INT = DAY(@Ngay);

    /* ================= BS KHÁM ================= */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT b.NhanVienID, @Ngay, @Ca, N'BS khám'
    FROM @BacSiKham b
    WHERE b.STT = ((@Index - 1) % 2) + 1
      AND NOT EXISTS (
          SELECT 1 FROM NgayNghiNhanVien n
          WHERE n.NhanVienID = b.NhanVienID AND n.Ngay = @Ngay
      );

    /* ================= BS ĐIỀU TRỊ ================= */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT b.NhanVienID, @Ngay, @Ca, N'BS điều trị'
    FROM @BacSiDieuTri b
    WHERE b.STT = ((@Index - 1) % 2) + 1
      AND NOT EXISTS (
          SELECT 1 FROM NgayNghiNhanVien n
          WHERE n.NhanVienID = b.NhanVienID AND n.Ngay = @Ngay
      );

    /* ================= Y TÁ (2 NGƯỜI) ================= */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT y.NhanVienID, @Ngay, @Ca, N'Y tá'
    FROM @YTa y
    WHERE y.STT IN (
        ((@Index - 1) % 4) + 1,
        ((@Index) % 4) + 1
    )
    AND NOT EXISTS (
        SELECT 1 FROM NgayNghiNhanVien n
        WHERE n.NhanVienID = y.NhanVienID AND n.Ngay = @Ngay
    );

    /* ================= KTV (2 NGƯỜI) ================= */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT k.NhanVienID, @Ngay, @Ca, N'KTV'
    FROM @KTV k
    WHERE NOT EXISTS (
        SELECT 1 FROM NgayNghiNhanVien n
        WHERE n.NhanVienID = k.NhanVienID AND n.Ngay = @Ngay
    );

    /* ================= LỄ TÂN ================= */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT l.NhanVienID, @Ngay, @Ca, N'Lễ tân'
    FROM @LeTan l
    WHERE l.STT = ((@Index - 1) % 2) + 1
      AND NOT EXISTS (
          SELECT 1 FROM NgayNghiNhanVien n
          WHERE n.NhanVienID = l.NhanVienID AND n.Ngay = @Ngay
      );

    SET @Ngay = DATEADD(DAY, 1, @Ngay);
END;
GO
-- Không ai làm 2 ca/ngày
SELECT NhanVienID, Ngay, COUNT(*) SoCa
FROM LichLamViecNhanVien
GROUP BY NhanVienID, Ngay
HAVING COUNT(*) > 1;

-- Mỗi ca đủ người
SELECT Ngay, CaLamViec, COUNT(*) TongNguoi
FROM LichLamViecNhanVien
GROUP BY Ngay, CaLamViec
ORDER BY Ngay, CaLamViec;

INSERT INTO KhungGioKham
(CaLamViec, GioBatDau, GioKetThuc, TenKhung, MaxSlot)
VALUES
-- ===== CA SÁNG =====
(1, '07:00', '07:30', N'Sáng 1', 5),
(1, '07:30', '08:00', N'Sáng 2', 5),
(1, '08:00', '08:30', N'Sáng 3', 5),
(1, '08:30', '09:00', N'Sáng 4', 5),
(1, '09:00', '09:30', N'Sáng 5', 5),
(1, '09:30', '10:00', N'Sáng 6', 5),

-- ===== CA CHIỀU =====
(2, '13:00', '13:30', N'Chiều 1', 5),
(2, '13:30', '14:00', N'Chiều 2', 5),
(2, '14:00', '14:30', N'Chiều 3', 5),
(2, '14:30', '15:00', N'Chiều 4', 5),
(2, '15:00', '15:30', N'Chiều 5', 5),
(2, '15:30', '16:00', N'Chiều 6', 5);
GO
select * from LichLamViecNhanVien