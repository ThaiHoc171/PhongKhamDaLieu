/* ================= NGÀY NGHỈ ================= */
INSERT INTO NgayNghiNhanVien (NhanVienID, Ngay, LyDo)
VALUES
(1,'2026-03-06',N'Nghỉ cá nhân'),
(2,'2026-03-18',N'Nghỉ phép'),
(3,'2026-03-12',N'Nghỉ cá nhân'),
(3,'2026-03-24',N'Nghỉ phép'),
(4,'2026-03-08',N'Nghỉ phép'),
(4,'2026-03-20',N'Nghỉ cá nhân'),
(5,'2026-03-10',N'Nghỉ phép'),
(5,'2026-03-26',N'Nghỉ cá nhân'),
(6,'2026-03-05',N'Nghỉ phép'),
(7,'2026-03-07',N'Nghỉ cá nhân'),
(8,'2026-03-15',N'Nghỉ phép'),
(9,'2026-03-22',N'Nghỉ cá nhân'),
(10,'2026-03-09',N'Nghỉ phép'),
(11,'2026-03-21',N'Nghỉ cá nhân'),
(13,'2026-03-11',N'Nghỉ phép'),
(13,'2026-03-27',N'Nghỉ cá nhân');
GO


/* ================= DANH SÁCH XOAY CA ================= */
DECLARE @BacSiKham TABLE (NhanVienID INT, STT INT);
INSERT INTO @BacSiKham VALUES (1,1),(2,2);

DECLARE @BacSiDieuTri TABLE (NhanVienID INT, STT INT);
INSERT INTO @BacSiDieuTri VALUES (3,1),(4,2);

DECLARE @YTa TABLE (NhanVienID INT, STT INT);
INSERT INTO @YTa VALUES (5,1),(6,2),(7,3),(8,4);

DECLARE @KTV TABLE (NhanVienID INT, STT INT);
INSERT INTO @KTV VALUES (9,1),(10,2),(11,3);

DECLARE @LeTan TABLE (NhanVienID INT, STT INT);
INSERT INTO @LeTan VALUES (12,1),(13,2);


/* ================= SINH LỊCH ================= */
DECLARE @Ngay DATE = '2026-03-02';

WHILE @Ngay <= '2026-04-02'
BEGIN
    DECLARE @Ca INT = CASE WHEN DAY(@Ngay) % 2 = 0 THEN 1 ELSE 2 END;
    DECLARE @Index INT = DAY(@Ngay);

    INSERT INTO LichLamViecNhanVien
    (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT b.NhanVienID, @Ngay, @Ca, NULL
    FROM @BacSiKham b
    WHERE b.STT = ((@Index - 1) % 2) + 1
    AND NOT EXISTS (
        SELECT 1 FROM NgayNghiNhanVien n
        WHERE n.NhanVienID = b.NhanVienID AND n.Ngay = @Ngay
    )
    AND NOT EXISTS (
        SELECT 1 FROM LichLamViecNhanVien l
        WHERE l.NhanVienID=b.NhanVienID AND l.Ngay=@Ngay AND l.CaLamViec=@Ca
    );

    /* ===== BS ĐIỀU TRỊ ===== */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT b.NhanVienID, @Ngay, @Ca, NULL
    FROM @BacSiDieuTri b
    WHERE b.STT = ((@Index - 1) % 2) + 1
    AND NOT EXISTS (
        SELECT 1 FROM NgayNghiNhanVien n
        WHERE n.NhanVienID=b.NhanVienID AND n.Ngay=@Ngay
    )
    AND NOT EXISTS (
        SELECT 1 FROM LichLamViecNhanVien l
        WHERE l.NhanVienID=b.NhanVienID AND l.Ngay=@Ngay AND l.CaLamViec=@Ca
    );

    /* ===== Y TÁ (4 NGƯỜI) ===== */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT y.NhanVienID, @Ngay, @Ca, NULL
    FROM @YTa y
    WHERE y.STT IN (
        ((@Index - 1) % 4) + 1,
        ((@Index) % 4) + 1
    )
    AND NOT EXISTS (
        SELECT 1 FROM NgayNghiNhanVien n
        WHERE n.NhanVienID=y.NhanVienID AND n.Ngay=@Ngay
    )
    AND NOT EXISTS (
        SELECT 1 FROM LichLamViecNhanVien l
        WHERE l.NhanVienID=y.NhanVienID AND l.Ngay=@Ngay AND l.CaLamViec=@Ca
    );
    /* ===== KTV 3 ng ===== */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT k.NhanVienID, @Ngay, @Ca, NULL
    FROM @KTV k
    WHERE k.STT = ((@Index - 1) % 2) + 1
    AND NOT EXISTS (
        SELECT 1 FROM NgayNghiNhanVien n
        WHERE n.NhanVienID=k.NhanVienID AND n.Ngay=@Ngay
    )
    AND NOT EXISTS (
        SELECT 1 FROM LichLamViecNhanVien l
        WHERE l.NhanVienID=k.NhanVienID AND l.Ngay=@Ngay AND l.CaLamViec=@Ca
    );

    /* ===== LỄ TÂN 2 ng ===== */
    INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu)
    SELECT l.NhanVienID, @Ngay, @Ca, NULL
    FROM @LeTan l
    WHERE l.STT = ((@Index - 1) % 2) + 1
    AND NOT EXISTS (
        SELECT 1 FROM NgayNghiNhanVien n
        WHERE n.NhanVienID=l.NhanVienID AND n.Ngay=@Ngay
    )
    AND NOT EXISTS (
        SELECT 1 FROM LichLamViecNhanVien lv
        WHERE lv.NhanVienID=l.NhanVienID AND lv.Ngay=@Ngay AND lv.CaLamViec=@Ca
    );
    SET @Ngay = DATEADD(DAY,1,@Ngay);
END;
GO


/* ================= KHUNG GIỜ ================= */
IF NOT EXISTS (SELECT 1 FROM KhungGioKham)
BEGIN
    INSERT INTO KhungGioKham
    (CaLamViec,GioBatDau,GioKetThuc,TenKhung)
    VALUES
    (1,'07:00','07:30',N'Sáng 1'),
    (1,'07:30','08:00',N'Sáng 2'),
    (1,'08:00','08:30',N'Sáng 3'),
    (1,'08:30','09:00',N'Sáng 4'),
    (1,'09:00','09:30',N'Sáng 5'),
    (1,'09:30','10:00',N'Sáng 6'),
    (2,'13:00','13:30',N'Chiều 1'),
    (2,'13:30','14:00',N'Chiều 2'),
    (2,'14:00','14:30',N'Chiều 3'),
    (2,'14:30','15:00',N'Chiều 4'),
    (2,'15:00','15:30',N'Chiều 5'),
    (2,'15:30','16:00',N'Chiều 6');
END;
GO

select * from NgayNghiNhanVien;
select * from LichLamViecNhanVien;
select * from KhungGioKham;