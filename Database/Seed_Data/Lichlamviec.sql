select * from KhungGioKham
select * from NgayNghiNhanVien
select * from LichLamViecNhanVien
select * from CaKham

INSERT INTO KhungGioKham (GioBatDau, GioKetThuc, TenKhung, MaxSlot) VALUES
('07:00', '07:30', N'Sáng 1', 5),
('07:30', '08:00', N'Sáng 2', 5),
('08:00', '08:30', N'Sáng 3', 5),
('08:30', '09:00', N'Sáng 4', 5),
('09:00', '09:30', N'Sáng 5', 5),
('09:30', '10:00', N'Sáng 6', 5),
('13:00', '13:30', N'Chiều 1', 5),
('13:30', '14:00', N'Chiều 2', 5),
('14:00', '14:30', N'Chiều 3', 5),
('14:30', '15:00', N'Chiều 4', 5),
('15:00', '15:30', N'Chiều 5', 5),
('15:30', '16:00', N'Chiều 6', 5);
GO


INSERT INTO NgayNghiNhanVien (NhanVienID, Ngay, LyDo) VALUES
-- Thứ 2
(5, '2025-12-30', N'Nghỉ hàng tuần'),
(8, '2025-12-30', N'Nghỉ hàng tuần'),
-- Thứ 3
(3, '2025-12-31', N'Nghỉ hàng tuần'),
(6, '2025-12-31', N'Nghỉ hàng tuần'),
-- Thứ 4
(1, '2026-01-01', N'Nghỉ hàng tuần'),
-- Thứ 5
(4, '2026-01-02', N'Nghỉ hàng tuần'),
(9, '2026-01-02', N'Nghỉ hàng tuần'),
-- Thứ 6
(2, '2026-01-03', N'Nghỉ hàng tuần'),
(7, '2026-01-03', N'Nghỉ hàng tuần');

-- ================================
-- LỊCH LÀM VIỆC NHÂN VIÊN TUẦN HIỆN TẠI
-- ================================
-- Thứ 2 - 2025-12-30
INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) VALUES
(1,'2025-12-30',1,N'Ca sáng'),
(2,'2025-12-30',2,N'Ca chiều'),
(3,'2025-12-30',1,N'Ca sáng'),
(4,'2025-12-30',2,N'Ca chiều'),
(6,'2025-12-30',1,N'Ca sáng'),
(7,'2025-12-30',2,N'Ca chiều'),
(9,'2025-12-30',1,N'Ca sáng');


-- Thứ 3 - 2025-12-31
INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) VALUES
(1,'2025-12-31',1,N'Ca sáng'),
(2,'2025-12-31',2,N'Ca chiều'),
(4,'2025-12-31',1,N'Ca sáng'),
(5,'2025-12-31',2,N'Ca chiều'),
(7,'2025-12-31',1,N'Ca sáng'),
(8,'2025-12-31',2,N'Ca chiều'),
(9,'2025-12-31',1,N'Ca sáng');


-- Thứ 4 - 2026-01-01
INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) VALUES
(2,'2026-01-01',1,N'Ca sáng'),
(3,'2026-01-01',2,N'Ca chiều'),
(4,'2026-01-01',1,N'Ca sáng'),
(5,'2026-01-01',2,N'Ca chiều'),
(6,'2026-01-01',1,N'Ca sáng'),
(7,'2026-01-01',2,N'Ca chiều'),
(8,'2026-01-01',1,N'Ca sáng'),
(9,'2026-01-01',2,N'Ca chiều');


-- Thứ 5 - 2026-01-02
INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) VALUES
(1,'2026-01-02',1,N'Ca sáng'),
(2,'2026-01-02',2,N'Ca chiều'),
(3,'2026-01-02',1,N'Ca sáng'),
(5,'2026-01-02',2,N'Ca chiều'),
(6,'2026-01-02',1,N'Ca sáng'),
(7,'2026-01-02',2,N'Ca chiều'),
(8,'2026-01-02',1,N'Ca sáng');


-- Thứ 6 - 2026-01-03
INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) VALUES
(1,'2026-01-03',1,N'Ca sáng'),
(3,'2026-01-03',2,N'Ca chiều'),
(4,'2026-01-03',1,N'Ca sáng'),
(5,'2026-01-03',2,N'Ca chiều'),
(6,'2026-01-03',1,N'Ca sáng'),
(8,'2026-01-03',2,N'Ca chiều'),
(9,'2026-01-03',1,N'Ca sáng');


-- Thứ 7 - 2026-01-04
INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) VALUES
(1,'2026-01-04',1,N'Ca sáng'),
(2,'2026-01-04',2,N'Ca chiều'),
(3,'2026-01-04',1,N'Ca sáng'),
(4,'2026-01-04',2,N'Ca chiều'),
(5,'2026-01-04',1,N'Ca sáng'),
(6,'2026-01-04',2,N'Ca chiều'),
(7,'2026-01-04',1,N'Ca sáng'),
(8,'2026-01-04',2,N'Ca chiều'),
(9,'2026-01-04',1,N'Ca sáng');

-- Chủ nhật - 2026-01-05
INSERT INTO LichLamViecNhanVien (NhanVienID, Ngay, CaLamViec, GhiChu) VALUES
(1,'2026-01-05',1,N'Ca sáng'),
(2,'2026-01-05',2,N'Ca chiều'),
(3,'2026-01-05',1,N'Ca sáng'),
(4,'2026-01-05',2,N'Ca chiều'),
(5,'2026-01-05',1,N'Ca sáng'),
(6,'2026-01-05',2,N'Ca chiều'),
(7,'2026-01-05',1,N'Ca sáng'),
(8,'2026-01-05',2,N'Ca chiều'),
(9,'2026-01-05',1,N'Ca sáng');



-- ===============================
-- CA KHÁM ĐẦY ĐỦ HƠN
-- ===============================
-- Ngày 2025-12-30
INSERT INTO CaKham (LichLamViecID, PhongChucNangID, NgayKham, KhungGioID, BenhNhanID, LyDoKham, TrangThai, GhiChu)
VALUES
(1, 1, '2025-12-30', 1, 1, N'Khám da liễu tổng quát', N'Đã đặt', NULL),
(1, 1, '2025-12-30', 2, 2, N'Khám dị ứng da', N'Đã đặt', NULL),
(1, 1, '2025-12-30', 3, NULL, NULL, N'Trống', NULL),
-- Ngày 2025-12-31

(2, 1, '2025-12-31', 1, 3, N'Khám vảy nến', N'Đã xác nhận', NULL),
(2, 1, '2025-12-31', 2, NULL, NULL, N'Trống', NULL),
(2, 1, '2025-12-31', 3, 4, N'Khám mụn bọc', N'Đã đặt', NULL),

-- Ngày 2026-01-01

(3, 2, '2026-01-01', 1, 5, N'Tái khám nám da', N'Đã xác nhận', NULL),
(3, 2, '2026-01-01', 2, NULL, NULL, N'Trống', NULL),
(3, 2, '2026-01-01', 3, 6, N'Điều trị sẹo rỗ', N'Hoàn thành', NULL),

-- Ngày 2026-01-02

(4, 2, '2026-01-02', 1, 7, N'Khám mụn ẩn', N'Đã đặt', NULL),
(4, 2, '2026-01-02', 2, NULL, NULL, N'Trống', NULL),
(4, 2, '2026-01-02', 3, 8, N'Tái khám da khô', N'Đã xác nhận', NULL),

-- Ngày 2026-01-03

(5, 3, '2026-01-03', 1, 9, N'Khám da nhạy cảm', N'Đã đặt', NULL),
(5, 3, '2026-01-03', 2, NULL, NULL, N'Trống', NULL),
(5, 3, '2026-01-03', 3, NULL, NULL, N'Trống', NULL),

-- Ngày 2026-01-04

(6, 3, '2026-01-04', 1, 1, N'Khám da dầu', N'Đã đặt', NULL),
(6, 3, '2026-01-04', 2, NULL, NULL, N'Trống', NULL),
(6, 3, '2026-01-04', 3, 2, N'Khám dị ứng mỹ phẩm', N'Đã xác nhận', NULL),

-- Ngày 2026-01-05

(7, 2, '2026-01-05', 1, 3, N'Khám mụn trứng cá', N'Đã đặt', NULL),
(7, 2, '2026-01-05', 2, NULL, NULL, N'Trống', NULL),
(7, 2, '2026-01-05', 3, 4, N'Khám rụng tóc', N'Hoàn thành', NULL);
