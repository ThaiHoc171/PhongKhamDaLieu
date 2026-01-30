INSERT INTO CanLamSang
(TenCLS, MoTa, LoaiXetNghiem, Ghichu)
VALUES
(N'Xét nghiệm máu', N'Đánh giá tế bào máu', N'Huyết học', N'Xét nghiệm cơ bản'),
(N'Xét nghiệm đường huyết', N'Kiểm tra đường huyết', N'Hóa sinh', N'Test nhanh'),
(N'Xét nghiệm CRP', N'Đánh giá viêm', N'Hóa sinh', N'Chỉ số viêm'),
(N'Xét nghiệm HbA1c', N'Thăm dò tiểu đường', N'Hóa sinh', N'Đánh giá 3 tháng'),
(N'Test nhanh viêm gan', N'Tìm viêm gan', N'Sinh học phân tử', N'Test nhanh'),
(N'Xét nghiệm nước tiểu', N'Kiểm tra chức năng thận', N'Hóa sinh', N'Tổng quát'),
(N'Xét nghiệm lipid', N'Đánh giá mỡ máu', N'Hóa sinh', N'Cholesterol'),
(N'Xét nghiệm chức năng gan', N'Kiểm tra gan', N'Hóa sinh', N'AST, ALT'),
(N'Xét nghiệm nội tiết', N'Kiểm tra hormone', N'Hóa sinh', N'Nội tiết'),
(N'Xét nghiệm kháng thể', N'Tìm kháng thể tự miễn', N'Hóa sinh', N'Tự miễn'),
(N'Xét nghiệm vi sinh', N'Nuôi cấy vi khuẩn', N'Vi sinh', N'Nuôi cấy'),
(N'Xét nghiệm PCR', N'Phát hiện virus', N'Sinh học phân tử', N'Độ chính xác cao'),
(N'Xét nghiệm điện giải', N'Đánh giá cân bằng điện giải', N'Hóa sinh', N'Na, K, Cl'),
(N'Xét nghiệm đông máu', N'Đánh giá đông máu', N'Huyết học', N'INR, PT'),
(N'Xét nghiệm ESR', N'Đánh giá viêm', N'Huyết học', N'Tốc độ lắng máu'),
(N'Xét nghiệm men gan', N'Đánh giá gan', N'Hóa sinh', N'Men gan'),
(N'Xét nghiệm vi khuẩn da', N'Nuôi cấy vi sinh da', N'Vi sinh', N'Da liễu'),
(N'Xét nghiệm tủy xương', N'Đánh giá tủy', N'Huyết học', N'Chuyên sâu'),
(N'Xét nghiệm nước tiểu tổng quát', N'Tổng quát', N'Hóa sinh', N'Tổng hợp'),
(N'Xét nghiệm dị ứng', N'Test dị ứng', N'Dị ứng', N'Mỹ phẩm – thuốc');
GO


INSERT INTO PhienKham
(CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID,
 TrieuChung, GhiChu, HinhAnhJSON, ChuanDoanCuoi, NgayKham, TrangThai)
SELECT
    ck.CaKhamID,
    ck.BenhNhanID,
    llv.NhanVienID,
    ck.PhongChucNangID,
    N'Mụn viêm, ngứa, đỏ da',
    N'Bệnh nhân hợp tác tốt',
    N'["anh1.jpg","anh2.jpg"]',
    CASE ck.LoaiCaKham
        WHEN N'Khám' THEN N'Mụn trứng cá mức độ trung bình'
        WHEN N'Tái khám' THEN N'Cải thiện tốt, giảm viêm'
        ELSE N'Đang điều trị theo phác đồ'
    END,
    DATEADD(MINUTE, -30, ck.NgayDat),
    N'Hoàn thành'
FROM CaKham ck
JOIN LichLamViecNhanVien llv ON ck.LichLamViecID = llv.LichLamViecID
WHERE ck.TrangThai = N'Hoàn thành';
GO
INSERT INTO PhienKham_ThietBi
(PhienKhamID, ThietBiID, SoLuong, GhiChu)
SELECT
    pk.PhienKhamID,
    tb.ThietBiID,
    1,
    N'Sử dụng trong phiên khám'
FROM PhienKham pk
CROSS APPLY (
    SELECT TOP 2 ThietBiID
    FROM ThietBi
    ORDER BY NEWID()
) tb;
GO
INSERT INTO PhienKham_CanLamSang
(PhienKhamID, CanLamSangID, TrangThai, KetQua,
 NhanVienChiDinhID, NhanVienThucHienID, GhiChu)
SELECT
    pk.PhienKhamID,
    cls.CanLamSangID,
    N'Hoàn thành',
    N'Kết quả trong giới hạn cho phép',
    pk.NhanVienID,
    nvkt.NhanVienID,
    N'Thực hiện đúng quy trình'
FROM PhienKham pk
CROSS APPLY (
    SELECT TOP 1 CanLamSangID
    FROM CanLamSang
    ORDER BY NEWID()
) cls
CROSS APPLY (
    SELECT TOP 1 NhanVienID
    FROM NhanVien
    ORDER BY NEWID()
) nvkt;
GO


INSERT INTO ToaThuoc (PhienKhamID, NhanVienKeDonID, GhiChu)
SELECT 
    pk.PhienKhamID,
    -- random bác sĩ kê đơn
    (ABS(CHECKSUM(NEWID())) % 5) + 1 AS NhanVienKeDonID,
    N'Kê đơn điều trị da liễu'
FROM PhienKham pk
WHERE NOT EXISTS (
    SELECT 1 FROM ToaThuoc tt
    WHERE tt.PhienKhamID = pk.PhienKhamID
);
;WITH RandomThuoc AS (
    SELECT 
        tt.ToaThuocID,
        t.ThuocID,
        ROW_NUMBER() OVER (
            PARTITION BY tt.ToaThuocID 
            ORDER BY NEWID()
        ) AS rn
    FROM ToaThuoc tt
    CROSS JOIN Thuoc t
)
INSERT INTO ChiTietToaThuoc
(ToaThuocID, ThuocID, LieuDung, SoLuong)
SELECT
    rt.ToaThuocID,
    rt.ThuocID,
    CASE 
        WHEN rt.rn = 1 THEN N'Bôi 1–2 lần/ngày'
        WHEN rt.rn = 2 THEN N'Uống sau ăn'
        WHEN rt.rn = 3 THEN N'Bôi buổi tối'
        ELSE N'Uống sáng'
    END AS LieuDung,
    (ABS(CHECKSUM(NEWID())) % 10) + 5 AS SoLuong
FROM RandomThuoc rt
WHERE rt.rn <= (ABS(CHECKSUM(NEWID())) % 3) + 2; -- 2–4 thuốc/toa


INSERT INTO PhienKham_Benh
(
    PhienKhamID,
    LoaiBenhID,
    LoaiChanDoan,
    GhiChu
)
SELECT
    pk.PhienKhamID,
    lb.LoaiBenhID,
    N'Chẩn đoán chính',
    N'Bệnh chính được xác định trong phiên khám'
FROM PhienKham pk
CROSS APPLY (
    SELECT TOP 1 LoaiBenhID
    FROM LoaiBenh
    ORDER BY NEWID()
) lb
WHERE NOT EXISTS (
    SELECT 1
    FROM PhienKham_Benh pb
    WHERE pb.PhienKhamID = pk.PhienKhamID
      AND pb.LoaiChanDoan = N'Chẩn đoán chính'
);
;WITH RandomPhatSinh AS (
    SELECT
        pk.PhienKhamID,
        lb.LoaiBenhID,
        ROW_NUMBER() OVER (
            PARTITION BY pk.PhienKhamID
            ORDER BY NEWID()
        ) AS rn
    FROM PhienKham pk
    CROSS JOIN LoaiBenh lb
    WHERE NOT EXISTS (
        SELECT 1
        FROM PhienKham_Benh pb
        WHERE pb.PhienKhamID = pk.PhienKhamID
          AND pb.LoaiBenhID = lb.LoaiBenhID
    )
)
INSERT INTO PhienKham_Benh
(
    PhienKhamID,
    LoaiBenhID,
    LoaiChanDoan,
    GhiChu
)
SELECT
    r.PhienKhamID,
    r.LoaiBenhID,
    N'Chẩn đoán phát sinh',
    N'Bệnh kèm theo trong quá trình khám'
FROM RandomPhatSinh r
WHERE r.rn <= (ABS(CHECKSUM(NEWID())) % 3); -- 0–2 bệnh phát sinh


select * from cakham