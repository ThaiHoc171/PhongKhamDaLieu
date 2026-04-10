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
/* =====================================================
   PHIÊN KHÁM
===================================================== */



INSERT INTO PhienKham 
(
    CaKhamID,
    BenhNhanID,
    NhanVienID,
    PhongChucNangID,
    TrieuChung,
    GhiChu,
    NgayKham,
    TrangThai
)
SELECT
    ck.CaKhamID,
    bn.BenhNhanID,
    llv.NhanVienID,
    ck.PhongChucNangID,
    N'Mụn viêm, ngứa, đỏ da',
    N'Bệnh nhân hợp tác tốt',
    DATEADD(MINUTE,5,CAST(ck.NgayKham AS DATETIME)),
    N'Hoàn thành'
FROM CaKham ck
JOIN LichLamViecNhanVien llv 
     ON ck.LichLamViecID = llv.LichLamViecID
JOIN BenhNhan bn 
     ON ck.ThongTinID = bn.ThongTinID
WHERE ck.TrangThai = N'Hoàn thành'
AND NOT EXISTS (
    SELECT 1 
    FROM PhienKham pk 
    WHERE pk.CaKhamID = ck.CaKhamID
);
/* =====================================================
   THIẾT BỊ SỬ DỤNG (ASSET LEVEL)
===================================================== */


INSERT INTO PhienKham_ThietBi 
(
    PhienKhamID,
    ChiTietID,
    GhiChu
)
SELECT
    pk.PhienKhamID,
    ct.ChiTietID,
    N'Sử dụng trong phiên khám'
FROM PhienKham pk
CROSS APPLY
(
    SELECT TOP 2 ChiTietID
    FROM ChiTiet_PCNTB
    ORDER BY NEWID()
) ct
WHERE NOT EXISTS
(
    SELECT 1
    FROM PhienKham_ThietBi tb
    WHERE tb.PhienKhamID = pk.PhienKhamID
);


/* =====================================================
   CẬN LÂM SÀNG CHO PHIÊN KHÁM
===================================================== */

INSERT INTO PhienKham_CanLamSang
(
    PhienKhamID,
    CanLamSangID,
    TrangThai,
    KetQua,
    NhanVienChiDinhID,
    NhanVienThucHienID,
    GhiChu
)
SELECT
    pk.PhienKhamID,
    cls.CanLamSangID,
    N'Hoàn thành',
    N'Kết quả trong giới hạn cho phép',
    pk.NhanVienID,
    pk.NhanVienID,
    N'Thực hiện đúng quy trình'
FROM PhienKham pk
CROSS APPLY (
    SELECT TOP 1 CanLamSangID
    FROM CanLamSang
    ORDER BY NEWID()
) cls;
GO


/* =====================================================
   TẠO TOA THUỐC
===================================================== */

INSERT INTO ToaThuoc (PhienKhamID, NhanVienKeDonID, GhiChu)
SELECT 
    pk.PhienKhamID,
    pk.NhanVienID,
    N'Kê đơn điều trị da liễu'
FROM PhienKham pk
WHERE NOT EXISTS (
    SELECT 1 
    FROM ToaThuoc tt
    WHERE tt.PhienKhamID = pk.PhienKhamID
);
GO


/* =====================================================
   CHI TIẾT TOA THUỐC
===================================================== */

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
    END,
    (ABS(CHECKSUM(NEWID())) % 10) + 5
FROM RandomThuoc rt
WHERE rt.rn <= 3;
GO


/* =====================================================
   CHẨN ĐOÁN BỆNH
===================================================== */

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
    N'Bệnh chính xác định trong phiên khám'
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
);
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
    N'Chẩn đoán phát sinh',
    N'Bệnh kèm theo'
FROM PhienKham pk
CROSS APPLY (
    SELECT TOP 1 LoaiBenhID
    FROM LoaiBenh
    ORDER BY NEWID()
) lb
WHERE ABS(CHECKSUM(NEWID())) % 10 < 3;

select * from CanLamSang;
select * from PhienKham;
select top 1 * from PhienKham_Benh;
select top 1 * from PhienKham_CanLamSang;
select top 1 * from PhienKham_ThietBi;
select top 1 * from ToaThuoc;
select top 1 * from ChiTietToaThuoc;