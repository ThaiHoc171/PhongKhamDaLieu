INSERT INTO TaiKham
(
    PhienKhamID,
    BenhNhanID,
    NgayDuKien,
    LyDo,
    TrangThai,
    CaKhamID
)
VALUES
(1, 1, '2026-02-05', N'Tái khám đánh giá đáp ứng điều trị', N'Chờ xử lý', NULL),
(3, 2, '2026-02-07', N'Tái khám kiểm tra tổn thương da', N'Chờ xử lý', NULL),
(5, 3, '2026-02-10', N'Tái khám theo dõi tiến triển', N'Chờ xử lý', NULL);


INSERT INTO LieuTrinhDieuTri
(
    BenhNhanID,
    PhienKhamID,
    TenLieuTrinh,
    TongSoBuoi,
    TrangThai,
    GhiChu,
    NgayBatDau,
    NgayKetThuc
)
VALUES
(1, 1, N'Liệu trình điều trị mụn viêm', 6, N'Đang điều trị', N'Điều trị bằng thuốc + laser', '2026-01-29', NULL),
(2, 3, N'Liệu trình trẻ hóa da', 4, N'Đang điều trị', N'RF kết hợp chăm sóc da', '2026-01-30', NULL),
(3, 5, N'Liệu trình điều trị nám', 5, N'Đang điều trị', N'Laser + thuốc bôi', '2026-01-31', NULL);

INSERT INTO LieuTrinh_BuoiDieuTri
(
    LieuTrinhID,
    CaKhamID,
    SoBuoi,
    NgayDuKien,
    NgayThucHien,
    NhanVienID,
    TrangThai,
    GhiChu,
    HinhAnhJSON
)
VALUES
-- Liệu trình 1
(1, 10, 1, '2026-01-29', '2026-01-29', 4, N'Hoàn thành', N'Buổi đầu – đáp ứng tốt', NULL),
(1, 12, 2, '2026-02-04', '2026-02-04', 4, N'Hoàn thành', N'Da giảm viêm', NULL),
(1, 15, 3, '2026-02-09', NULL, 4, N'Chờ xử lý', NULL, NULL),
(1, 18, 4, '2026-02-14', NULL, 4, N'Chờ xử lý', NULL, NULL),
(1, 21, 5, '2026-02-19', NULL, 4, N'Chờ xử lý', NULL, NULL),
(1, 24, 6, '2026-02-24', NULL, 4, N'Chờ xử lý', NULL, NULL),

-- Liệu trình 2
(2, 30, 1, '2026-01-30', '2026-01-30', 5, N'Hoàn thành', N'Trẻ hóa lần 1', NULL),
(2, 33, 2, '2026-02-06', NULL, 5, N'Chờ xử lý', NULL, NULL),
(2, 36, 3, '2026-02-13', NULL, 5, N'Chờ xử lý', NULL, NULL),
(2, 39, 4, '2026-02-20', NULL, 5, N'Chờ xử lý', NULL, NULL),

-- Liệu trình 3
(3, 45, 1, '2026-01-31', '2026-01-31', 6, N'Hoàn thành', N'Buổi điều trị nám đầu tiên', NULL),
(3, 48, 2, '2026-02-07', NULL, 6, N'Chờ xử lý', NULL, NULL),
(3, 51, 3, '2026-02-14', NULL, 6, N'Chờ xử lý', NULL, NULL),
(3, 54, 4, '2026-02-21', NULL, 6, N'Chờ xử lý', NULL, NULL),
(3, 57, 5, '2026-02-28', NULL, 6, N'Chờ xử lý', NULL, NULL);


select top 1 * from TaiKham;
select top 1 * from LieuTrinhDieuTri;
select top 1 * from LieuTrinh_BuoiDieuTri;