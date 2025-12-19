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
(1, '2025-01-08', N'Nghỉ hàng tuần'),
(2, '2025-01-10', N'Nghỉ hàng tuần'),
(3, '2025-01-07', N'Nghỉ hàng tuần'),
(4, '2025-01-09', N'Nghỉ hàng tuần'),

(5, '2025-01-06', N'Nghỉ hàng tuần'),
(6, '2025-01-07', N'Nghỉ hàng tuần'),
(7, '2025-01-10', N'Nghỉ hàng tuần'),

(8, '2025-01-06', N'Nghỉ hàng tuần'),
(9, '2025-01-09', N'Nghỉ hàng tuần'),
(10, '2025-01-11', N'Nghỉ hàng tuần'),

(11, '2025-01-08', N'Nghỉ hàng tuần'),
(12, '2025-01-12', N'Nghỉ hàng tuần');
GO
-- ================================
-- LỊCH LÀM VIỆC TUẦN HOÀN CHỈNH
-- ================================

-- THỨ 2 (2025-01-06) - Nghỉ: NV5, NV8
INSERT INTO LichLamViecNhanVien (NhanVienID, PhongChucNangID, Ngay, CaLamViec) VALUES
(1,1,'2025-01-06',1),
(2,1,'2025-01-06',2),
(3,1,'2025-01-06',1),
(4,1,'2025-01-06',2),
(6,1,'2025-01-06',1),
(7,1,'2025-01-06',2),
(9,1,'2025-01-06',1),
(10,1,'2025-01-06',2),
(11,1,'2025-01-06',1),
(12,1,'2025-01-06',2);

-- THỨ 3 (2025-01-07) - Nghỉ: NV3, NV6
INSERT INTO LichLamViecNhanVien (NhanVienID, PhongChucNangID, Ngay, CaLamViec) VALUES
(1,1,'2025-01-07',1),
(2,1,'2025-01-07',2),
(4,1,'2025-01-07',1),
(5,1,'2025-01-07',2),
(7,1,'2025-01-07',1),
(8,1,'2025-01-07',2),
(9,1,'2025-01-07',1),
(10,1,'2025-01-07',2),
(11,1,'2025-01-07',1),
(12,1,'2025-01-07',2);

-- THỨ 4 (2025-01-08) - Nghỉ: NV1, NV11
INSERT INTO LichLamViecNhanVien (NhanVienID, PhongChucNangID, Ngay, CaLamViec) VALUES
(2,1,'2025-01-08',1),
(3,1,'2025-01-08',2),
(4,1,'2025-01-08',1),
(5,1,'2025-01-08',2),
(6,1,'2025-01-08',1),
(7,1,'2025-01-08',2),
(8,1,'2025-01-08',1),
(9,1,'2025-01-08',2),
(10,1,'2025-01-08',1),
(12,1,'2025-01-08',2);

-- THỨ 5 (2025-01-09) - Nghỉ: NV4, NV9
INSERT INTO LichLamViecNhanVien (NhanVienID, PhongChucNangID, Ngay, CaLamViec) VALUES
(1,1,'2025-01-09',1),
(2,1,'2025-01-09',2),
(3,1,'2025-01-09',1),
(5,1,'2025-01-09',2),
(6,1,'2025-01-09',1),
(7,1,'2025-01-09',2),
(8,1,'2025-01-09',1),
(10,1,'2025-01-09',2),
(11,1,'2025-01-09',1),
(12,1,'2025-01-09',2);

-- THỨ 6 (2025-01-10) - Nghỉ: NV2, NV7
INSERT INTO LichLamViecNhanVien (NhanVienID, PhongChucNangID, Ngay, CaLamViec) VALUES
(1,1,'2025-01-10',1),
(3,1,'2025-01-10',2),
(4,1,'2025-01-10',1),
(5,1,'2025-01-10',2),
(6,1,'2025-01-10',1),
(8,1,'2025-01-10',2),
(9,1,'2025-01-10',1),
(10,1,'2025-01-10',2),
(11,1,'2025-01-10',1),
(12,1,'2025-01-10',2);

-- THỨ 7 (2025-01-11) - Nghỉ: NV10
INSERT INTO LichLamViecNhanVien (NhanVienID, PhongChucNangID, Ngay, CaLamViec) VALUES
(1,1,'2025-01-11',1),
(2,1,'2025-01-11',2),
(3,1,'2025-01-11',1),
(4,1,'2025-01-11',2),
(5,1,'2025-01-11',1),
(6,1,'2025-01-11',2),
(7,1,'2025-01-11',1),
(8,1,'2025-01-11',2),
(9,1,'2025-01-11',1),
(11,1,'2025-01-11',2),
(12,1,'2025-01-11',1);

-- CHỦ NHẬT (2025-01-12) - Nghỉ: NV12
INSERT INTO LichLamViecNhanVien (NhanVienID, PhongChucNangID, Ngay, CaLamViec) VALUES
(1,1,'2025-01-12',1),
(2,1,'2025-01-12',2),
(3,1,'2025-01-12',1),
(4,1,'2025-01-12',2),
(5,1,'2025-01-12',1),
(6,1,'2025-01-12',2),
(7,1,'2025-01-12',1),
(8,1,'2025-01-12',2),
(9,1,'2025-01-12',1),
(10,1,'2025-01-12',2),
(11,1,'2025-01-12',1);

INSERT INTO CaKham (LichLamViecID, PhongChucNangID, NgayKham, KhungGioID, BenhNhanID, LyDoKham, TrangThai, GhiChu)
VALUES
(1, 1, '2025-01-03', 1, 1, N'Khám da liễu tổng quát', 'booked', NULL),
(1, 1, '2025-01-03', 2, NULL, NULL, 'available', NULL),
(1, 1, '2025-01-03', 3, 2, N'Nổi mẩn đỏ', 'confirmed', NULL),

(2, 1, '2025-01-04', 1, NULL, NULL, 'available', NULL),
(2, 1, '2025-01-04', 2, 3, N'Mụn trứng cá nặng', 'booked', NULL),
(2, 1, '2025-01-04', 3, 4, N'Rụng tóc', 'completed', NULL),

(3, 2, '2025-01-05', 1, 5, N'Soi da & điều trị laser', 'confirmed', NULL),
(3, 2, '2025-01-05', 2, NULL, NULL, 'available', NULL),
(3, 2, '2025-01-05', 3, 6, N'Nám da', 'booked', NULL),

(4, 2, '2025-01-06', 1, 7, N'Điều trị sẹo rỗ', 'confirmed', NULL),
(4, 2, '2025-01-06', 2, NULL, NULL, 'available', NULL),
(4, 2, '2025-01-06', 3, 8, N'Giãn mao mạch', 'completed', NULL),

(5, 3, '2025-01-07', 1, NULL, NULL, 'available', NULL),
(5, 3, '2025-01-07', 2, 9, N'Theo dõi điều trị', 'booked', NULL),
(5, 3, '2025-01-07', 3, 10, N'Tái khám', 'confirmed', NULL),

(6, 3, '2025-01-08', 1, 2, N'Xét nghiệm da liễu', 'booked', NULL),
(6, 3, '2025-01-08', 2, NULL, NULL, 'available', NULL),
(6, 3, '2025-01-08', 3, 3, N'Khám dị ứng', 'completed', NULL),

(7, 3, '2025-01-09', 1, NULL, NULL, 'available', NULL),
(7, 3, '2025-01-09', 2, 4, N'Khám ban đỏ', 'booked', NULL),
(7, 3, '2025-01-09', 3, 5, N'Nấm da đầu', 'confirmed', NULL),

(8, 2, '2025-01-10', 1, 6, N'Điều trị laser nám', 'completed', NULL),
(8, 2, '2025-01-10', 2, NULL, NULL, 'available', NULL),
(8, 2, '2025-01-10', 3, 7, N'Xóa xăm', 'booked', NULL),

(9, 2, '2025-01-11', 1, 8, N'Chăm sóc da chuyên sâu', 'confirmed', NULL),
(9, 2, '2025-01-11', 2, NULL, NULL, 'available', NULL),
(9, 2, '2025-01-11', 3, 9, N'Điều trị mụn', 'booked', NULL),

(10, 3, '2025-01-12', 1, 10, N'Theo dõi kết quả điều trị', 'confirmed', NULL),
(10, 3, '2025-01-12', 2, NULL, NULL, 'available', NULL),
(10, 3, '2025-01-12', 3, 1, N'Khám định kỳ', 'booked', NULL);
