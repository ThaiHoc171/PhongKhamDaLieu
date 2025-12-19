INSERT INTO PhongKham 
(TenPhongKham, GioiThieu, DiaChi, Hotline, Email, Website, GioMoCua, HinhAnhBanner)
VALUES
(
    N'Phong kham Da Lieu Aura Care',
    N'Phong kham da lieu chuyen sau ve dieu tri mun, tri nam, tham, tre hoa da va cac benh ly da man tinh. Chung toi cung cap cac dich vu kham chua uy tin, su dung cong nghe hien dai va doi ngu bac si nhieu nam kinh nghiem.',
    N'123 Nguyen Dinh Chieu, Phuong 6, Quan 3, TP. Ho Chi Minh',
    N'0901 234 567',
    N'contact@auracareclinic.vn',
    N'https://auracareclinic.vn',
    N'Thu 2 – Chu nhat: 08:00 – 20:00',
    N'/images/banner/clinic_banner.jpg'
);
GO

--- ThietBi ---
INSERT INTO ThietBi (TenTB, LoaiTB)
VALUES
-- Thiết bị khám & cận lâm sàng
(N'Dermatoscope', N'Thiết bị cận lâm sàng'),
(N'Wood’s lamp', N'Thiết bị cận lâm sàng'),
(N'Máy soi da', N'Thiết bị cận lâm sàng'),
(N'Kính hiển vi soi nấm (KOH)', N'Thiết bị cận lâm sàng'),

-- Thiết bị xét nghiệm nhanh
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
INSERT INTO PhongChucNang (TenPhong, LoaiPhong, MoTa)
VALUES
(N'Phòng khám bệnh', N'Khám lâm sàng', N'Phòng khám da liễu tổng quát, đánh giá tình trạng da, chẩn đoán ban đầu.'),
(N'Phòng xét nghiệm', N'Xét nghiệm nhanh', N'Thực hiện các xét nghiệm cơ bản như đường huyết, CRP, HbA1c, test nhanh.'),
(N'Phòng chẩn đoán da liễu', N'Chẩn đoán cận lâm sàng', N'Soi da, soi nấm, kiểm tra sắc tố, tổn thương da.'),
(N'Phòng thủ thuật', N'Thủ thuật da liễu', N'Thực hiện thủ thuật nhỏ: đốt điện, lạnh nitơ, lăn kim, RF.'),
(N'Phòng laser', N'Điều trị laser', N'Sử dụng công nghệ laser CO2, YAG, IPL để điều trị chuyên sâu.');

--- Thiết Bị theo phòng --- 
INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, SoLuong)
VALUES
(1, 1, 1),
(1, 2, 1),
(1, 3, 1);

INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, SoLuong)
VALUES
(2, 5, 1),
(2, 6, 1),
(2, 7, 1),
(2, 8, 1);

INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, SoLuong)
VALUES
(3, 1, 1),
(3, 2, 1),
(3, 3, 1),
(3, 4, 1),
(3, 17, 1);

INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, SoLuong)
VALUES
(4, 11, 1),
(4, 12, 1),
(4, 13, 1),
(4, 14, 1),
(4, 15, 1);

INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, SoLuong)
VALUES
(5, 9, 1),
(5, 10, 1),
(5, 11, 1);


