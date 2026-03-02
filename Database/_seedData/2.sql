/* ================================
   PHÒNG KHÁM
================================ */
INSERT INTO PhongKham 
(TenPhongKham, GioiThieu, DiaChi, Hotline, Email, Website)
VALUES
(
    N'Phòng khám Da Liễu Aura Care',
    N'Phòng khám da liễu chuyên sâu điều trị mụn, nám, thâm, trẻ hóa da và bệnh lý da mạn tính. Ứng dụng công nghệ hiện đại và đội ngũ bác sĩ giàu kinh nghiệm.',
    N'123 Nguyễn Đình Chiểu, Phường 6, Quận 3, TP.HCM',
    N'0901234567',
    N'contact@auracareclinic.vn',
    N'https://auracareclinic.vn'
);
GO


/* ================================
   THIẾT BỊ
================================ */
INSERT INTO ThietBi (TenTB, LoaiTB)
VALUES
-- Cận lâm sàng
(N'Dermatoscope', N'Thiết bị cận lâm sàng'),
(N'Đèn Wood', N'Thiết bị cận lâm sàng'),
(N'Máy soi da', N'Thiết bị cận lâm sàng'),
(N'Camera phân cực da', N'Thiết bị cận lâm sàng'),
(N'Kính hiển vi soi nấm (KOH)', N'Thiết bị cận lâm sàng'),

-- Xét nghiệm
(N'Máy đo đường huyết', N'Thiết bị xét nghiệm'),
(N'Máy CRP', N'Thiết bị xét nghiệm'),
(N'Máy HbA1c', N'Thiết bị xét nghiệm'),
(N'Máy test nhanh bệnh lý da', N'Thiết bị xét nghiệm'),

-- Điều trị
(N'RF Microneedling', N'Thiết bị điều trị'),
(N'HIFU', N'Thiết bị điều trị'),
(N'Máy đốt điện cao tần', N'Thiết bị điều trị'),
(N'Máy đông lạnh Nitơ lỏng', N'Thiết bị điều trị'),
(N'Lăn kim / Phi kim', N'Thiết bị điều trị'),

-- Laser
(N'Laser CO2 Fractional', N'Thiết bị laser'),
(N'Laser Nd:YAG', N'Thiết bị laser'),
(N'IPL', N'Thiết bị laser');
GO


/* ================================
   GÁN THIẾT BỊ VÀO PHÒNG
================================ */

INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, TongSoLuong)
SELECT p.PhongChucNangID, t.ThietBiID, 2
FROM PhongChucNang p
JOIN ThietBi t ON t.TenTB IN 
(N'Dermatoscope', N'Đèn Wood', N'Máy soi da', N'Camera phân cực da')
WHERE p.TenPhong = N'Phòng khám bệnh';


INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, TongSoLuong)
SELECT p.PhongChucNangID, t.ThietBiID, 3
FROM PhongChucNang p
JOIN ThietBi t ON t.TenTB IN 
(N'RF Microneedling', N'HIFU', N'Lăn kim / Phi kim')
WHERE p.TenPhong = N'Phòng điều trị';


INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, TongSoLuong)
SELECT p.PhongChucNangID, t.ThietBiID, 2
FROM PhongChucNang p
JOIN ThietBi t ON t.TenTB IN 
(N'Máy đo đường huyết', N'Máy CRP', N'Máy HbA1c', N'Máy test nhanh bệnh lý da')
WHERE p.TenPhong = N'Phòng xét nghiệm';


INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, TongSoLuong)
SELECT p.PhongChucNangID, t.ThietBiID, 2
FROM PhongChucNang p
JOIN ThietBi t ON t.TenTB IN 
(N'Dermatoscope', N'Máy soi da', N'Kính hiển vi soi nấm (KOH)', N'Camera phân cực da')
WHERE p.TenPhong = N'Phòng chẩn đoán da liễu';


INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, TongSoLuong)
SELECT p.PhongChucNangID, t.ThietBiID, 2
FROM PhongChucNang p
JOIN ThietBi t ON t.TenTB IN 
(N'Máy đốt điện cao tần', N'Máy đông lạnh Nitơ lỏng', N'Lăn kim / Phi kim')
WHERE p.TenPhong = N'Phòng thủ thuật';


INSERT INTO PhongChucNang_ThietBi (PhongChucNangID, ThietBiID, TongSoLuong)
SELECT p.PhongChucNangID, t.ThietBiID, 2
FROM PhongChucNang p
JOIN ThietBi t ON t.TenTB IN 
(N'Laser CO2 Fractional', N'Laser Nd:YAG', N'IPL')
WHERE p.TenPhong = N'Phòng laser';


/* ================================
   SINH MÃ TÀI SẢN
================================ */
INSERT INTO ChiTiet_PCNTB (PCN_TB_ID, MaTaiSan)
SELECT 
    pc.PCN_TB_ID,
    CONCAT(
        'TB-', 
        pc.PCN_TB_ID, '-', 
        ROW_NUMBER() OVER (
            PARTITION BY pc.PCN_TB_ID 
            ORDER BY (SELECT NULL)
        )
    )
FROM PhongChucNang_ThietBi pc
CROSS APPLY (
    SELECT TOP (pc.TongSoLuong) 1 AS x
    FROM sys.objects
) gen;
GO


select * from PhongKham;
select * from ThietBi;
select * from PhongChucNang_ThietBi;
select * from ChiTiet_PCNTB;