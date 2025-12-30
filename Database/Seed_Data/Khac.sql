INSERT INTO HoSoBenhAn (BenhNhanID, BenhNen, DiUng, TienSuBenh, TienSuGiaDinh, ThoiQuenSong, ThongTinKhac)
VALUES
(1, N'Mụn trứng cá', N'Không', N'Không', N'Bố bị tiểu đường', N'Không hút thuốc', N''),
(2, N'Dị ứng da', N'Phấn hoa', N'Không', N'Không', N'Không', N''),
(3, N'Vảy nến', N'Không', N'Không', N'Mẹ bị vảy nến', N'Uống rượu ít', N''),
(4, N'Mụn trứng cá', N'Không', N'Không', N'Không', N'Không', N''),
(5, N'Da khô', N'Không', N'Không', N'Không', N'Thích tắm nước nóng', N''),
(6, N'Rụng tóc', N'Không', N'Không', N'Ông nội bị rụng tóc', N'Không', N''),
(7, N'Mụn trứng cá', N'Không', N'Không', N'Không', N'Ăn nhiều đồ ngọt', N''),
(8, N'Viêm da cơ địa', N'Không', N'Không', N'Không', N'Không', N''),
(9, N'Dị ứng mỹ phẩm', N'Hương liệu', N'Không', N'Không', N'Không', N''),
(10, N'Nám da', N'Không', N'Không', N'Mẹ bị nám da', N'Không', N''),
(11, N'Mụn trứng cá', N'Không', N'Không', N'Không', N'Không', N''),
(12, N'Da dầu', N'Không', N'Không', N'Không', N'Không', N''),
(13, N'Vảy nến', N'Không', N'Không', N'Không', N'Uống nhiều cà phê', N''),
(14, N'Viêm nang lông', N'Không', N'Không', N'Không', N'Không', N''),
(15, N'Rụng tóc', N'Không', N'Không', N'Ông bị rụng tóc', N'Không', N''),
(16, N'Mụn trứng cá', N'Không', N'Không', N'Không', N'Ăn đồ cay', N''),
(17, N'Da nhạy cảm', N'Hoa quả', N'Không', N'Không', N'Không', N''),
(18, N'Bỏng nắng', N'Không', N'Không', N'Không', N'Không', N''),
(19, N'Viêm da tiếp xúc', N'Hóa chất', N'Không', N'Không', N'Không', N''),
(20, N'Mụn ẩn', N'Không', N'Không', N'Không', N'Không', N''),
(21, N'Mụn đầu đen', N'Không', N'Không', N'Không', N'Không', N''),
(22, N'Viêm da tiết bã', N'Không', N'Không', N'Không', N'Thay đổi thời tiết', N''),
(23, N'Dị ứng da', N'Thực phẩm biển', N'Không', N'Không', N'Không', N''),
(24, N'Rối loạn sắc tố', N'Không', N'Không', N'Không', N'Tiếp xúc ánh nắng', N''),
(25, N'Mụn bọc', N'Không', N'Không', N'Không', N'Thức khuya', N''),
(26, N'Da nhờn', N'Không', N'Không', N'Không', N'Không', N''),
(27, N'Viêm da dị ứng', N'Mỹ phẩm', N'Không', N'Không', N'Sử dụng mỹ phẩm thường xuyên', N''),
(28, N'Rụng tóc từng mảng', N'Không', N'Không', N'Không', N'Căng thẳng kéo dài', N''),
(29, N'Chàm da', N'Không', N'Không', N'Không', N'Không', N''),
(30, N'Mụn nội tiết', N'Không', N'Không', N'Không', N'Rối loạn hormone', N''),
(31, N'Da hỗn hợp', N'Không', N'Không', N'Không', N'Không', N''),
(32, N'Viêm da quanh miệng', N'Không', N'Không', N'Không', N'Sử dụng kem corticoid', N''),
(33, N'Dị ứng thuốc', N'Kháng sinh', N'Không', N'Không', N'Không', N''),
(34, N'Rụng tóc sau sinh', N'Không', N'Không', N'Không', N'Không', N''),
(35, N'Mụn viêm', N'Không', N'Không', N'Không', N'Ăn nhiều đồ dầu mỡ', N''),
(36, N'Da sạm', N'Không', N'Không', N'Không', N'Tiếp xúc ánh nắng nhiều', N''),
(37, N'Viêm da cơ địa', N'Không', N'Không', N'Không', N'Không', N''),
(38, N'Da lão hóa sớm', N'Không', N'Không', N'Không', N'Không dùng kem chống nắng', N''),
(39, N'Mụn cám', N'Không', N'Không', N'Không', N'Không', N'');


INSERT INTO PhienKham (CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID, TrieuChung, GhiChu, ChuanDoanCuoi)
VALUES
(1, 1, 1, 1, N'Ngứa, mẩn đỏ', NULL, N'Khám da liễu tổng quát', N'Hoàn thành'),
(2, 2, 1, 1, N'Dị ứng, nổi mẩn', NULL, N'Khám dị ứng da', N'Hoàn thành'),
(4, 3, 1, 1, N'Vảy nến nhẹ', NULL, N'Khám vảy nến', N'Hoàn thành'),
(6, 4, 1, 1, N'Mụn bọc sưng đỏ', NULL, N'Khám mụn bọc', N'Hoàn thành'),
(7, 5, 2, 2, N'Nám da mặt', NULL, N'Tái khám nám da', N'Hoàn thành'),
(9, 6, 2, 2, N'Sẹo rỗ', NULL, N'Điều trị sẹo rỗ', N'Hoàn thành'),
(10, 7, 3, 2, N'Mụn ẩn', NULL, N'Khám mụn ẩn', N'Hoàn thành'),
(12, 8, 3, 2, N'Da khô', NULL, N'Tái khám da khô', N'Hoàn thành'),
(13, 9, 2, 3, N'Da nhạy cảm', NULL, N'Khám da nhạy cảm', N'Hoàn thành'),
(16, 1, 3, 3, N'Da dầu', NULL, N'Khám da dầu', N'Hoàn thành'),
(18, 2, 3, 3, N'Dị ứng mỹ phẩm', NULL, N'Khám dị ứng mỹ phẩm', N'Hoàn thành'),
(19, 3, 2, 2, N'Mụn trứng cá', NULL, N'Khám mụn trứng cá', N'Hoàn thành'),
(21, 4, 2, 2, N'Rụng tóc', NULL, N'Khám rụng tóc', N'Hoàn thành');


INSERT INTO PhienKham_ThietBi (PhienKhamID, ThietBiID, SoLuong, GhiChu)
VALUES
(1, 1, 1, NULL),
(2, 2, 1, NULL),
(3, 3, 1, NULL),
(4, 1, 1, NULL),
(5, 2, 1, NULL),
(6, 3, 1, NULL),
(7, 1, 1, NULL),
(8, 2, 1, NULL),
(9, 3, 1, NULL),
(10, 1, 1, NULL),
(11, 2, 1, NULL),
(12, 3, 1, NULL),
(13, 1, 1, NULL);


<<<<<<< HEAD
=======


>>>>>>> d4d8c8708a5009f37d5e1475d791376a70469453
INSERT INTO CanLamSang (TenCLS, MoTa, Gia, LoaiXetNghiem) ---Thiếu GhiChu, NgayTao
VALUES
(N'Xét nghiệm máu', N'Đánh giá tế bào máu', 200000, N'Huyết học'),
(N'Xét nghiệm đường huyết', N'Kiểm tra đường huyết', 150000, N'Hóa sinh'),
(N'Xét nghiệm CRP', N'Đánh giá viêm', 250000, N'Hóa sinh'),
(N'Xét nghiệm HbA1c', N'Thăm dò tiểu đường', 300000, N'Hóa sinh'),
(N'Test nhanh viêm gan', N'Tìm viêm gan', 180000, N'Sinh học phân tử'),
(N'Xét nghiệm nước tiểu', N'Kiểm tra chức năng thận', 120000, N'Hóa sinh'),
(N'Xét nghiệm lipid', N'Đánh giá mỡ máu', 200000, N'Hóa sinh'),
(N'Xét nghiệm chức năng gan', N'Kiểm tra gan', 220000, N'Hóa sinh'),
(N'Xét nghiệm nội tiết', N'Kiểm tra hormone', 250000, N'Hóa sinh'),
(N'Xét nghiệm kháng thể', N'Tìm kháng thể tự miễn', 300000, N'Hóa sinh'),
(N'Xét nghiệm vi sinh', N'Nuôi cấy vi khuẩn', 180000, N'Vi sinh'),
(N'Xét nghiệm PCR', N'Phát hiện virus', 350000, N'Sinh học phân tử'),
(N'Xét nghiệm điện giải', N'Đánh giá cân bằng điện giải', 200000, N'Hóa sinh'),
(N'Xét nghiệm đông máu', N'Đánh giá đông máu', 220000, N'Huyết học'),
(N'Xét nghiệm ESR', N'Đánh giá viêm', 150000, N'Huyết học'),
(N'Xét nghiệm men gan', N'Đánh giá gan', 200000, N'Hóa sinh'),
(N'Xét nghiệm vi khuẩn da', N'Nuôi cấy vi sinh da', 250000, N'Vi sinh'),
(N'Xét nghiệm tủy xương', N'Đánh giá tủy', 300000, N'Huyết học'),
(N'Xét nghiệm nước tiểu tổng quát', N'Tổng quát', 120000, N'Hóa sinh'),
(N'Xét nghiệm dị ứng', N'Test dị ứng', 200000, N'Dị ứng');


<<<<<<< HEAD
INSERT INTO PhienKham_CanLamSang (PhienKhamID, CanLamSangID, KetQua, NhanVienChiDinhID, NhanVienThucHienID) ---Thiếu FileDinhKem, NgayChiDinh, NgayThucHien, GhiChu
=======
INSERT INTO PhienKham_CanLamSang
(PhienKhamID, CanLamSangID, TrangThai, KetQua, FileDinhKem,
 NgayChiDinh, NgayThucHien, NhanVienChiDinhID, NhanVienThucHienID, GhiChu)
>>>>>>> d4d8c8708a5009f37d5e1475d791376a70469453
VALUES
(1, 1, N'Hoàn thành', N'Bình thường', N'/files/cls/1.pdf', '2025-01-01', '2025-01-01', 2, 3, NULL),
(2, 2, N'Hoàn thành', N'Tăng nhẹ', N'/files/cls/2.pdf', '2025-01-01', '2025-01-01', 3, 2, NULL),
(3, 3, N'Hoàn thành', N'Bình thường', N'/files/cls/3.pdf', '2025-01-02', '2025-01-02', 2, 3, NULL),
(4, 4, N'Hoàn thành', N'Tăng', N'/files/cls/4.pdf', '2025-01-02', '2025-01-02', 3, 2, NULL),
(5, 5, N'Hoàn thành', N'Âm tính', N'/files/cls/5.pdf', '2025-01-03', '2025-01-03', 2, 3, NULL),
(6, 6, N'Hoàn thành', N'Bình thường', N'/files/cls/6.pdf', '2025-01-03', '2025-01-03', 3, 2, NULL),
(7, 7, N'Hoàn thành', N'Bình thường', N'/files/cls/7.pdf', '2025-01-04', '2025-01-04', 2, 3, NULL),
(8, 8, N'Hoàn thành', N'Tăng nhẹ', N'/files/cls/8.pdf', '2025-01-04', '2025-01-04', 3, 2, NULL),
(9, 9, N'Hoàn thành', N'Bình thường', N'/files/cls/9.pdf', '2025-01-05', '2025-01-05', 2, 3, NULL),
(10, 10, N'Hoàn thành', N'Bình thường', N'/files/cls/10.pdf', '2025-01-05', '2025-01-05', 3, 2, NULL),

(11, 11, N'Hoàn thành', N'Tăng', N'/files/cls/11.pdf', '2025-01-06', '2025-01-06', 2, 3, NULL),
(12, 12, N'Hoàn thành', N'Âm tính', N'/files/cls/12.pdf', '2025-01-06', '2025-01-06', 3, 2, NULL),
(13, 13, N'Hoàn thành', N'Bình thường', N'/files/cls/13.pdf', '2025-01-07', '2025-01-07', 2, 3, NULL);


-- ToaThuoc
<<<<<<< HEAD
INSERT INTO ToaThuoc (PhienKhamID, NhanVienKeDonID, TongTien) ---Thiếu NgayLap, GhiChu, bỏ TongTien
=======
INSERT INTO ToaThuoc
(PhienKhamID, NhanVienKeDonID, NgayLap, GhiChu)
>>>>>>> d4d8c8708a5009f37d5e1475d791376a70469453
VALUES
(1,2,'2025-01-01',N'Điều trị mụn trứng cá'),
(2,3,'2025-01-01',N'Điều trị dị ứng da'),
(3,2,'2025-01-02',N'Điều trị vảy nến'),
(4,3,'2025-01-02',N'Điều trị mụn viêm'),
(5,2,'2025-01-03',N'Điều trị da khô'),
(6,3,'2025-01-03',N'Điều trị rụng tóc'),
(7,2,'2025-01-04',N'Điều trị mụn bọc'),
(8,3,'2025-01-04',N'Điều trị viêm da cơ địa'),
(9,2,'2025-01-05',N'Điều trị dị ứng mỹ phẩm'),
(10,3,'2025-01-05',N'Điều trị nám da'),

<<<<<<< HEAD
-- ChiTietToaThuoc
INSERT INTO ChiTietToaThuoc (ToaThuocID, ThuocID, LieuDung, SoLuong) ---Bỏ DonGia, ThanhTien
VALUES
(1,1,N'Sáng 1 viên',10),
(2,2,N'Sáng-Tối',8),
(3,3,N'Tối',9),
(4,4,N'Sáng 1 viên',7),
(5,5,N'Sáng-Tối',6),
(6,6,N'Tối',8),
(7,7,N'Sáng 1 viên',10),
(8,8,N'Sáng-Tối',9),
(9,9,N'Sáng 1 viên',10),
(10,10,N'Tối',6),
(11,11,N'Sáng 1 viên',8),
(12,12,N'Tối',9),
(13,13,N'Sáng-Tối',7);
=======
(11,2,'2025-01-06',N'Điều trị mụn trứng cá'),
(12,3,'2025-01-06',N'Điều trị da dầu'),
(13,2,'2025-01-07',N'Điều trị vảy nến');


INSERT INTO ChiTietToaThuoc
(ToaThuocID, ThuocID, LieuDung, SoLuong)
VALUES
(1,1,N'Sáng 1 viên',10),
(2,2,N'Sáng - Tối',8),
(3,3,N'Tối 1 viên',9),
(4,4,N'Sáng 1 viên',7),
(5,5,N'Sáng - Tối',6),
(6,6,N'Tối 1 viên',8),
(7,7,N'Sáng 1 viên',10),
(8,8,N'Sáng - Tối',9),
(9,9,N'Sáng 1 viên',10),
(10,10,N'Tối 1 viên',6),

(11,11,N'Sáng 1 viên',8),
(12,12,N'Tối 1 viên',9),
(13,13,N'Sáng - Tối',7);
>>>>>>> d4d8c8708a5009f37d5e1475d791376a70469453


INSERT INTO PhienKham_Benh
(PhienKhamID, LoaiBenhID, GhiChu)
VALUES
(1,1,N'Mụn trứng cá nặng'),
(2,2,N'Dị ứng da'),
(3,3,N'Vảy nến'),
(4,1,N'Mụn trứng cá nhẹ'),
(5,4,N'Da khô'),
(6,5,N'Rụng tóc'),
(7,1,N'Mụn bọc'),
(8,2,N'Viêm da cơ địa nhẹ'),
(9,3,N'Dị ứng mỹ phẩm'),
(10,4,N'Nám da'),

(11,1,N'Mụn trứng cá'),
(12,2,N'Da dầu'),
(13,3,N'Vảy nến mức trung bình');


<<<<<<< HEAD
INSERT INTO TaiKham (PhienKhamID, BenhNhanID, NgayDuKien, LyDo) ---Thiếu CaKhamID, NgayTao
=======
INSERT INTO TaiKham
(PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai, CaKhamID)
>>>>>>> d4d8c8708a5009f37d5e1475d791376a70469453
VALUES
(1,1,'2025-12-19',N'Tái khám mụn',N'Hoàn thành',1),
(2,2,'2025-12-20',N'Tái khám dị ứng',N'Hoàn thành',2),
(3,3,'2025-12-21',N'Tái khám vảy nến',N'Hoàn thành',3),
(4,4,'2025-12-22',N'Tái khám mụn',N'Hoàn thành',4),
(5,5,'2025-12-23',N'Tái khám da khô',N'Hoàn thành',5),
(6,6,'2025-12-24',N'Tái khám rụng tóc',N'Hoàn thành',6),
(7,7,'2025-12-25',N'Tái khám mụn',N'Hoàn thành',7),
(8,8,'2025-12-26',N'Tái khám viêm da cơ địa',N'Hoàn thành',8),
(9,9,'2025-12-27',N'Tái khám dị ứng mỹ phẩm',N'Hoàn thành',9),
(10,10,'2025-12-28',N'Tái khám nám da',N'Hoàn thành',10),

(11,11,'2025-12-29',N'Tái khám mụn',N'Hoàn thành',11),
(12,12,'2025-12-30',N'Tái khám da dầu',N'Hoàn thành',12),
(13,13,'2025-12-31',N'Tái khám vảy nến',N'Hoàn thành',13);


<<<<<<< HEAD
INSERT INTO LieuTrinhDieuTri (BenhNhanID, PhienKhamID, TenLieuTrinh, TongSoBuoi) ---Thiếu GhiChu, NgayBatDau, NgayKetThuc
=======
INSERT INTO LieuTrinhDieuTri
(BenhNhanID, PhienKhamID, TenLieuTrinh, TongSoBuoi, TrangThai, GhiChu, NgayBatDau, NgayKetThuc)
>>>>>>> d4d8c8708a5009f37d5e1475d791376a70469453
VALUES
(1,1,N'Liệu trình mụn',4,N'Đang điều trị',N'Điều trị mụn mức độ trung bình','2025-12-01',NULL),
(2,2,N'Liệu trình dị ứng',3,N'Đang điều trị',N'Giảm triệu chứng dị ứng','2025-12-02',NULL),
(3,3,N'Liệu trình vảy nến',5,N'Đang điều trị',N'Kiểm soát vảy nến','2025-12-03',NULL),
(4,4,N'Liệu trình mụn',4,N'Đang điều trị',N'Điều trị mụn viêm','2025-12-04',NULL),
(5,5,N'Liệu trình da khô',3,N'Đang điều trị',N'Cấp ẩm và phục hồi da','2025-12-05',NULL),
(6,6,N'Liệu trình rụng tóc',4,N'Đang điều trị',N'Kích thích mọc tóc','2025-12-06',NULL),
(7,7,N'Liệu trình mụn',4,N'Đang điều trị',N'Điều trị mụn bọc','2025-12-07',NULL),
(8,8,N'Liệu trình viêm da cơ địa',3,N'Đang điều trị',N'Giảm viêm da','2025-12-08',NULL),
(9,9,N'Liệu trình dị ứng mỹ phẩm',3,N'Đang điều trị',N'Ngưng tác nhân dị ứng','2025-12-09',NULL),
(10,10,N'Liệu trình nám da',5,N'Đang điều trị',N'Làm mờ sắc tố','2025-12-10',NULL),

(11,11,N'Liệu trình mụn',4,N'Đang điều trị',N'Ổn định tuyến bã nhờn','2025-12-11',NULL),
(12,12,N'Liệu trình da dầu',3,N'Đang điều trị',N'Giảm dầu thừa','2025-12-12',NULL),
(13,13,N'Liệu trình vảy nến',5,N'Đang điều trị',N'Kiểm soát tái phát','2025-12-13',NULL);


<<<<<<< HEAD
INSERT INTO LieuTrinh_BuoiDieuTri (LieuTrinhID, SoBuoi, NgayDuKien, NhanVienID)  ---Thiếu NgayThucHien, GhiChu, HinhAnhJSON
VALUES 
(1,1,'2025-12-20',1),(1,2,'2025-12-22',1),(1,3,'2025-12-24',1),(1,4,'2025-12-26',1),
(2,1,'2025-12-21',2),(2,2,'2025-12-23',2),(2,3,'2025-12-25',2),
(3,1,'2025-12-22',3),(3,2,'2025-12-24',3),(3,3,'2025-12-26',3),(3,4,'2025-12-28',3),(3,5,'2025-12-30',3),
(4,1,'2025-12-23',1),(4,2,'2025-12-25',1),(4,3,'2025-12-27',1),(4,4,'2025-12-29',1),
(5,1,'2025-12-24',2),(5,2,'2025-12-26',2),(5,3,'2025-12-28',2),
(6,1,'2025-12-25',3);


INSERT INTO BuoiDieuTri_CanLamSang (BuoiDieuTriID, CanLamSangID, KetQua) ---Sửa tên ID của BuoiDieuTri_CanLamSang 
=======

INSERT INTO LieuTrinh_BuoiDieuTri
(LieuTrinhID, SoBuoi, NgayDuKien, NgayThucHien, NhanVienID, TrangThai, GhiChu, HinhAnhJSON)
VALUES
-- Liệu trình 1 (4 buổi)
(1,1,'2025-12-20','2025-12-20',1,N'Hoàn thành',N'Buổi đầu điều trị',NULL),
(1,2,'2025-12-22','2025-12-22',1,N'Hoàn thành',N'Giảm viêm',NULL),
(1,3,'2025-12-24',NULL,1,N'Chờ xử lý',N'Tiếp tục liệu trình',NULL),
(1,4,'2025-12-26',NULL,1,N'Chờ xử lý',N'Buổi cuối',NULL),

-- Liệu trình 2 (3 buổi)
(2,1,'2025-12-21','2025-12-21',2,N'Hoàn thành',N'Ổn định da',NULL),
(2,2,'2025-12-23','2025-12-23',2,N'Hoàn thành',N'Giảm ngứa',NULL),
(2,3,'2025-12-25',NULL,2,N'Chờ xử lý',N'Theo dõi dị ứng',NULL),

-- Liệu trình 3 (5 buổi)
(3,1,'2025-12-22','2025-12-22',3,N'Hoàn thành',N'Điều trị nền',NULL),
(3,2,'2025-12-24','2025-12-24',3,N'Hoàn thành',N'Giảm bong tróc',NULL),
(3,3,'2025-12-26',NULL,3,N'Chờ xử lý',N'Tiếp tục theo dõi',NULL),
(3,4,'2025-12-28',NULL,3,N'Chờ xử lý',N'Ổn định triệu chứng',NULL),
(3,5,'2025-12-30',NULL,3,N'Chờ xử lý',N'Đánh giá kết quả',NULL),

-- Liệu trình 4 (4 buổi)
(4,1,'2025-12-23','2025-12-23',1,N'Hoàn thành',N'Làm sạch da',NULL),
(4,2,'2025-12-25','2025-12-25',1,N'Hoàn thành',N'Giảm mụn viêm',NULL),
(4,3,'2025-12-27',NULL,1,N'Chờ xử lý',N'Tiếp tục điều trị',NULL),
(4,4,'2025-12-29',NULL,1,N'Chờ xử lý',N'Hoàn tất liệu trình',NULL),

-- Liệu trình 5 (3 buổi)
(5,1,'2025-12-24','2025-12-24',2,N'Hoàn thành',N'Cấp ẩm da',NULL),
(5,2,'2025-12-26','2025-12-26',2,N'Hoàn thành',N'Phục hồi hàng rào da',NULL),
(5,3,'2025-12-28',NULL,2,N'Chờ xử lý',N'Duy trì kết quả',NULL),

-- Liệu trình 6 (4 buổi)
(6,1,'2025-12-25','2025-12-25',3,N'Hoàn thành',N'Kích thích mọc tóc',NULL),
(6,2,'2025-12-27','2025-12-27',3,N'Hoàn thành',N'Giảm rụng tóc',NULL),
(6,3,'2025-12-29',NULL,3,N'Chờ xử lý',N'Tiếp tục điều trị',NULL),
(6,4,'2025-12-31',NULL,3,N'Chờ xử lý',N'Đánh giá tiến triển',NULL),

-- Liệu trình 7 (4 buổi)
(7,1,'2026-01-01','2026-01-01',1,N'Hoàn thành',N'Điều trị mụn',NULL),
(7,2,'2026-01-03','2026-01-03',1,N'Hoàn thành',N'Giảm sưng viêm',NULL),
(7,3,'2026-01-05',NULL,1,N'Chờ xử lý',N'Tiếp tục liệu trình',NULL),
(7,4,'2026-01-07',NULL,1,N'Chờ xử lý',N'Buổi cuối',NULL),

-- Liệu trình 8 (3 buổi) → đủ 39 dòng
(8,1,'2026-01-02','2026-01-02',2,N'Hoàn thành',N'Giảm viêm da',NULL),
(8,2,'2026-01-04','2026-01-04',2,N'Hoàn thành',N'Ổn định da',NULL),
(8,3,'2026-01-06',NULL,2,N'Chờ xử lý',N'Theo dõi phản ứng',NULL),

-- LieuTrinh 9 (3 buổi)
(9,1,'2026-01-07','2026-01-07',3,N'Hoàn thành',N'Buổi đầu điều trị',NULL),
(9,2,'2026-01-09','2026-01-09',3,N'Hoàn thành',N'Tiếp tục liệu trình',NULL),
(9,3,'2026-01-11',NULL,3,N'Chờ xử lý',N'Buổi cuối',NULL),

-- LieuTrinh 10 (4 buổi)
(10,1,'2026-01-08','2026-01-08',1,N'Hoàn thành',N'Buổi 1',NULL),
(10,2,'2026-01-10','2026-01-10',1,N'Hoàn thành',N'Buổi 2',NULL),
(10,3,'2026-01-12',NULL,1,N'Chờ xử lý',N'Buổi 3',NULL),
(10,4,'2026-01-14',NULL,1,N'Chờ xử lý',N'Buổi 4',NULL),

-- LieuTrinh 11 (3 buổi)
(11,1,'2026-01-09','2026-01-09',2,N'Hoàn thành',N'Buổi 1',NULL),
(11,2,'2026-01-11','2026-01-11',2,N'Hoàn thành',N'Buổi 2',NULL),
(11,3,'2026-01-13',NULL,2,N'Chờ xử lý',N'Buổi 3',NULL),

-- LieuTrinh 12 (2 buổi)
(12,1,'2026-01-10','2026-01-10',3,N'Hoàn thành',N'Buổi 1',NULL),
(12,2,'2026-01-12',NULL,3,N'Chờ xử lý',N'Buổi 2',NULL),

-- LieuTrinh 13 (5 buổi)
(13,1,'2026-01-11','2026-01-11',1,N'Hoàn thành',N'Buổi 1',NULL),
(13,2,'2026-01-13','2026-01-13',1,N'Hoàn thành',N'Buổi 2',NULL),
(13,3,'2026-01-15',NULL,1,N'Chờ xử lý',N'Buổi 3',NULL),
(13,4,'2026-01-17',NULL,1,N'Chờ xử lý',N'Buổi 4',NULL),
(13,5,'2026-01-19',NULL,1,N'Chờ xử lý',N'Buổi 5',NULL);


INSERT INTO BuoiDieuTri_CanLamSang
(BuoiDieuTriID, CanLamSangID, KetQua, TrangThai)
>>>>>>> d4d8c8708a5009f37d5e1475d791376a70469453
VALUES
-- 1-20 (đã có)
(1,1,N'Bình thường',N'Hoàn thành'),
(2,2,N'Tăng nhẹ',N'Hoàn thành'),
(3,3,N'Bình thường',N'Hoàn thành'),
(4,4,N'Tăng',N'Hoàn thành'),
(5,5,N'Âm tính',N'Hoàn thành'),
(6,6,N'Bình thường',N'Hoàn thành'),
(7,7,N'Bình thường',N'Hoàn thành'),
(8,8,N'Tăng nhẹ',N'Hoàn thành'),
(9,9,N'Bình thường',N'Hoàn thành'),
(10,10,N'Bình thường',N'Hoàn thành'),
(11,11,N'Tăng',N'Hoàn thành'),
(12,12,N'Âm tính',N'Hoàn thành'),
(13,13,N'Bình thường',N'Hoàn thành');
