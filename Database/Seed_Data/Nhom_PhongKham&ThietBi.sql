INSERT INTO PhongKham 
(TenPhongKham, GioiThieu, DiaChi, Hotline, Email, Website, GioMoCua)
VALUES
(
    N'Phong kham Da Lieu Aura Care',
    N'Phong kham da lieu chuyen sau ve dieu tri mun, tri nam, tham, tre hoa da va cac benh ly da man tinh. Chung toi cung cap cac dich vu kham chua uy tin, su dung cong nghe hien dai va doi ngu bac si nhieu nam kinh nghiem.',
    N'123 Nguyen Dinh Chieu, Phuong 6, Quan 3, TP. Ho Chi Minh',
    N'0901 234 567',
    N'contact@auracareclinic.vn',
    N'https://auracareclinic.vn',
    N'Thu 2 – Chu nhat: 08:00 – 20:00'
);
GO

--- ThietBi ---
INSERT INTO ThietBi (TenTB, LoaiTB) ---Thiếu TinhTrang, NgayNhap
VALUES
-- Thiết bị khám & cận lâm sàng
(N'Dermatoscope', N'Thiết bị cận lâm sàng'),
(N'Wood’s lamp', N'Thiết bị cận lâm sàng'),
(N'Máy soi da', N'Thiết bị cận lâm sàng'),
(N'Kính hiển vi soi nấm (KOH)', N'Thiết bị cận lâm sàng'),

-- Thiết bị xét nghiệm
(N'Máy đo đường', N'Thiết bị xét nghiệm'),
(N'Máy CRP', N'Thiết bị xét nghiệm'),
(N'Máy HbA1c', N'Thiết bị xét nghiệm'),
(N'Máy test nhanh bệnh lý', N'Thiết bị xét nghiệm'),

-- Thiết bị thủ thuật & điều trị
(N'Laser CO2', N'Thiết bị thủ thuật'),
(N'Laser YAG', N'Thiết bị laser'),
(N'IPL', N'Thiết bị laser'),
(N'RF Microneedling', N'Thiết bị thủ thuật'),
(N'HIFU', N'Thiết bị thủ thuật'),
(N'Máy đốt điện', N'Thiết bị thủ thuật'),
(N'Máy đông lạnh nitơ', N'Thiết bị thủ thuật'),
(N'Lăn kim / Phi kim', N'Thiết bị thủ thuật'),
(N'Camera phân cực', N'Thiết bị cận lâm sàng');

--- PhongChucNang ---
INSERT INTO PhongChucNang (TenPhong, LoaiPhong, MoTa) ---Thiếu TrangThai, NgayTao
VALUES
(N'Phòng khám bệnh', N'Khám lâm sàng', N'Phòng khám da liễu tổng quát, đánh giá tình trạng da, chẩn đoán ban đầu.'),
(N'Phòng xét nghiệm', N'Xét nghiệm nhanh', N'Thực hiện các xét nghiệm cơ bản như đường huyết, CRP, HbA1c, test nhanh.'),
(N'Phòng chẩn đoán da liễu', N'Chẩn đoán cận lâm sàng', N'Soi da, soi nấm, kiểm tra sắc tố, tổn thương da.'),
(N'Phòng thủ thuật', N'Thủ thuật da liễu', N'Thực hiện thủ thuật nhỏ: đốt điện, lạnh nitơ, lăn kim, RF.'),
(N'Phòng laser', N'Điều trị laser', N'Sử dụng công nghệ laser CO2, YAG, IPL để điều trị chuyên sâu.');



-- Phòng khám bệnh (ID = 1)
INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
VALUES
(1, 1), -- Dermatoscope
(1, 2), -- Wood’s lamp
(1, 3); -- Máy soi da

-- Phòng xét nghiệm (ID = 2)
INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
VALUES
(2, 5), -- Máy đo đường
(2, 6), -- Máy CRP
(2, 7), -- Máy HbA1c
(2, 8); -- Máy test nhanh

-- Phòng chẩn đoán da liễu (ID = 3)
INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
VALUES
(3, 1),  -- Dermatoscope
(3, 2),  -- Wood’s lamp
(3, 3),  -- Máy soi da
(3, 4),  -- Kính hiển vi soi nấm
(3, 17); -- Camera phân cực

-- Phòng thủ thuật (ID = 4)
INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
VALUES
(4, 11), -- RF Microneedling
(4, 12), -- HIFU
(4, 13), -- Máy đốt điện
(4, 14), -- Máy đông lạnh nitơ
(4, 15); -- Lăn kim / Phi kim

-- Phòng laser (ID = 5)
INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
VALUES
(5, 9),  -- Laser CO2
(5, 10), -- Laser YAG
(5, 11); -- IPL


-- Dermatoscope (PCN_TB_ID = 1)
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, GhiChu)
VALUES
(1, N'DERM-001', N'Dermatoscope chính'),
(1, N'DERM-002', N'Dermatoscope dự phòng');

-- Wood’s lamp (PCN_TB_ID = 2)
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, GhiChu)
VALUES
(2, N'WOOD-001', NULL);

-- Máy soi da (PCN_TB_ID = 3)
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, GhiChu)
VALUES
(3, N'SOI-DA-001', N'Máy soi da phòng khám');
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, GhiChu)
VALUES
(4, N'DUONG-001', N'Máy đo đường chính'),
(4, N'DUONG-002', N'Máy đo đường dự phòng'),

(5, N'CRP-001', NULL),

(6, N'HBA1C-001', NULL),

(7, N'TEST-001', N'Máy test nhanh tổng hợp'),
(7, N'TEST-002', N'Máy test nhanh backup');
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, TinhTrang, GhiChu)
VALUES
(8,  N'DERM-CHD-001', N'Hoạt động', NULL),
(8,  N'DERM-CHD-002', N'Bảo trì', N'Đang gửi bảo dưỡng'),

(9,  N'WOOD-CHD-001', N'Hoạt động', NULL),

(10, N'SOI-DA-CHD-001', N'Hoạt động', NULL),

(11, N'KOH-001', N'Hoạt động', N'Kính hiển vi soi nấm'),

(12, N'CAM-001', N'Hoạt động', N'Camera phân cực');
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, GhiChu)
VALUES
(13, N'RF-001', NULL),
(13, N'RF-002', NULL),

(14, N'HIFU-001', N'Máy HIFU chính'),

(15, N'DOTDIEN-001', NULL),

(16, N'NITO-001', N'Máy đông lạnh nitơ'),

(17, N'LAN-KIM-001', NULL),
(17, N'LAN-KIM-002', NULL);
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan, TinhTrang, GhiChu)
VALUES
(18, N'CO2-001', N'Hoạt động', N'Laser CO2 chính'),
(18, N'CO2-002', N'Bảo trì', N'Đang bảo trì định kỳ'),

(19, N'YAG-001', N'Hoạt động', NULL),

(20, N'IPL-001', N'Hoạt động', NULL),
(20, N'IPL-002', N'Hỏng', N'Chờ sửa chữa');


UPDATE p
SET TongSoLuong = (
    SELECT COUNT(*)
    FROM ChiTiet_PCNTB c
    WHERE c.PCN_TB_ID = p.PCN_TB_ID
)
FROM PhongChucNang_ThietBi p;
