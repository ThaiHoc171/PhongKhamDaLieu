INSERT INTO PhongKham 
(TenPhongKham, GioiThieu, DiaChi, Hotline, Email, Website)
VALUES
(
    N'Phong kham Da Lieu Aura Care',
    N'Phong kham da lieu chuyen sau ve dieu tri mun, tri nam, tham, tre hoa da va cac benh ly da man tinh. Chung toi cung cap cac dich vu kham chua uy tin, su dung cong nghe hien dai va doi ngu bac si nhieu nam kinh nghiem.',
    N'123 Nguyen Dinh Chieu, Phuong 6, Quan 3, TP. Ho Chi Minh',
    N'0901 234 567',
    N'contact@auracareclinic.vn',
    N'https://auracareclinic.vn'
);
GO
select * from phongkham

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



INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID)
VALUES
-- PHÒNG KHÁM BỆNH 
(1, 1), -- Dermatoscope
(1, 2), -- Wood’s lamp
(1, 3), -- Máy soi da
(1, 17), -- Camera phân cực

-- PHÒNG ĐIỀU TRỊ 
(2, 12), -- RF Microneedling
(2, 13), -- HIFU
(2, 16), -- Lăn kim

-- PHÒNG XÉT NGHIỆM 
(3, 4), -- Kính hiển vi soi nấm
(3, 5), -- Máy đo đường
(3, 6), -- Máy CRP
(3, 7), -- Máy HbA1c
(3, 8), -- Máy test nhanh

-- PHÒNG CHẨN ĐOÁN 
(4, 1), -- Dermatoscope
(4, 3), -- Máy soi da
(4, 4), -- Kính hiển vi
(4, 17), -- Camera phân cực

-- PHÒNG THỦ THUẬT 
(5, 14), -- Máy đốt điện
(5, 15), -- Máy đông lạnh nitơ
(5, 16), -- Lăn kim

-- PHÒNG LASER
(6, 9),  -- Laser CO2
(6, 10), -- Laser YAG
(6, 11); -- IPL
GO


