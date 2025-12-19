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
(20, N'Mụn ẩn', N'Không', N'Không', N'Không', N'Không', N'');


INSERT INTO PhienKham (CaKhamID, BenhNhanID, NhanVienID, PhongChucNangID, TrieuChung, GhiChu, ChuanDoanCuoi, TrangThai)
VALUES
(1,1,1,1,N'Mụn trứng cá nặng',NULL,N'Mụn trứng cá', 'completed'),
(2,2,2,1,N'Ngứa và nổi mẩn',NULL,N'Dị ứng da', 'completed'),
(3,3,3,2,N'Vảy nến lan rộng',NULL,N'Vảy nến', 'completed'),
(4,4,1,1,N'Mụn sưng đỏ',NULL,N'Mụn trứng cá', 'completed'),
(5,5,2,1,N'Da khô, bong tróc',NULL,N'Da khô', 'completed'),
(6,6,3,2,N'Rụng tóc nhiều',NULL,N'Rụng tóc', 'completed'),
(7,7,1,1,N'Mụn bọc',NULL,N'Mụn trứng cá', 'completed'),
(8,8,2,2,N'Viêm da cơ địa',NULL,N'Viêm da cơ địa', 'completed'),
(9,9,3,2,N'Dị ứng mỹ phẩm',NULL,N'Dị ứng', 'completed'),
(10,10,1,1,N'Nám da mặt',NULL,N'Nám da', 'completed'),
(11,11,2,1,N'Mụn trứng cá',NULL,N'Mụn trứng cá', 'completed'),
(12,12,3,2,N'Da dầu nhiều',NULL,N'Da dầu', 'completed'),
(13,13,1,1,N'Vảy nến',NULL,N'Vảy nến', 'completed'),
(14,14,2,1,N'Viêm nang lông',NULL,N'Viêm nang lông', 'completed'),
(15,15,3,2,N'Rụng tóc',NULL,N'Rụng tóc', 'completed'),
(16,16,1,1,N'Mụn ẩn',NULL,N'Mụn trứng cá', 'completed'),
(17,17,2,1,N'Da nhạy cảm',NULL,N'Da nhạy cảm', 'completed'),
(18,18,3,2,N'Bỏng nắng',NULL,N'Bỏng nắng', 'completed'),
(19,19,1,1,N'Viêm da tiếp xúc',NULL,N'Viêm da tiếp xúc', 'completed'),
(20,20,2,1,N'Mụn ẩn',NULL,N'Mụn ẩn', 'completed');


INSERT INTO PhienKham_ThietBi (PhienKhamID, ThietBiID, SoLuong, GhiChu)
VALUES
(1,1,1,NULL),(2,2,1,NULL),(3,3,1,NULL),(4,1,1,NULL),(5,2,1,NULL),
(6,3,1,NULL),(7,1,1,NULL),(8,2,1,NULL),(9,3,1,NULL),(10,1,1,NULL),
(11,2,1,NULL),(12,3,1,NULL),(13,1,1,NULL),(14,2,1,NULL),(15,3,1,NULL),
(16,1,1,NULL),(17,2,1,NULL),(18,3,1,NULL),(19,1,1,NULL),(20,2,1,NULL);


INSERT INTO CanLamSang (TenCLS, MoTa, Gia, LoaiXetNghiem)
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


INSERT INTO PhienKham_CanLamSang (PhienKhamID, CanLamSangID, TrangThai, KetQua, NhanVienChiDinhID, NhanVienThucHienID)
VALUES
(1,1,'completed','Bình thường',1,1),
(2,2,'completed','Tăng nhẹ',2,2),
(3,3,'completed','Bình thường',3,3),
(4,4,'completed','Tăng',1,1),
(5,5,'completed','Âm tính',2,2),
(6,6,'completed','Bình thường',3,3),
(7,7,'completed','Bình thường',1,1),
(8,8,'completed','Tăng nhẹ',2,2),
(9,9,'completed','Bình thường',3,3),
(10,10,'completed','Bình thường',1,1),
(11,11,'completed','Tăng',2,2),
(12,12,'completed','Âm tính',3,3),
(13,13,'completed','Bình thường',1,1),
(14,14,'completed','Bình thường',2,2),
(15,15,'completed','Tăng',3,3),
(16,16,'completed','Âm tính',1,1),
(17,17,'completed','Bình thường',2,2),
(18,18,'completed','Bình thường',3,3),
(19,19,'completed','Tăng nhẹ',1,1),
(20,20,'completed','Bình thường',2,2);


-- ToaThuoc
INSERT INTO ToaThuoc (PhienKhamID, NhanVienKeDonID, TongTien)
VALUES
(1,1,500000),(2,2,400000),(3,3,450000),(4,1,350000),(5,2,300000),
(6,3,400000),(7,1,500000),(8,2,450000),(9,3,500000),(10,1,300000),
(11,2,400000),(12,3,450000),(13,1,350000),(14,2,300000),(15,3,400000),
(16,1,500000),(17,2,450000),(18,3,400000),(19,1,350000),(20,2,300000);

-- ChiTietToaThuoc
INSERT INTO ChiTietToaThuoc (ToaThuocID, ThuocID, LieuDung, SoLuong, DonGia, ThanhTien)
VALUES
(1,1,N'Sáng 1 viên',10,50000,500000),
(2,2,N'Sáng-Tối',8,50000,400000),
(3,3,N'Tối',9,50000,450000),
(4,4,N'Sáng 1 viên',7,50000,350000),
(5,5,N'Sáng-Tối',6,50000,300000),
(6,6,N'Tối',8,50000,400000),
(7,7,N'Sáng 1 viên',10,50000,500000),
(8,8,N'Sáng-Tối',9,50000,450000),
(9,9,N'Sáng 1 viên',10,50000,500000),
(10,10,N'Tối',6,50000,300000),
(11,11,N'Sáng 1 viên',8,50000,400000),
(12,12,N'Tối',9,50000,450000),
(13,13,N'Sáng-Tối',7,50000,350000),
(14,14,N'Tối',6,50000,300000),
(15,15,N'Sáng-Tối',8,50000,400000),
(16,16,N'Sáng 1 viên',10,50000,500000),
(17,17,N'Sáng-Tối',9,50000,450000),
(18,18,N'Tối',8,50000,400000),
(19,19,N'Sáng-Tối',7,50000,350000),
(20,20,N'Tối',6,50000,300000);


INSERT INTO PhienKham_Benh (PhienKhamID, LoaiBenhID, LoaiChuanDoan, GhiChu)
VALUES
(1,1,'primary',N'Mụn trứng cá nặng'),
(2,2,'primary',N'Dị ứng da'),
(3,3,'primary',N'Vảy nến'),
(4,1,'secondary',N'Mụn trứng cá dạng nhẹ'),
(5,4,'primary',N'Da khô'),
(6,5,'primary',N'Rụng tóc'),
(7,1,'primary',N'Mụn bọc'),
(8,2,'secondary',N'Viêm da cơ địa nhẹ'),
(9,3,'primary',N'Dị ứng mỹ phẩm'),
(10,4,'primary',N'Nám da'),
(11,1,'primary',N'Mụn trứng cá'),
(12,2,'primary',N'Da dầu'),
(13,3,'secondary',N'Vảy nến mức trung bình'),
(14,4,'primary',N'Viêm nang lông'),
(15,5,'primary',N'Rụng tóc'),
(16,1,'primary',N'Mụn ẩn'),
(17,2,'primary',N'Da nhạy cảm'),
(18,3,'primary',N'Bỏng nắng'),
(19,4,'secondary',N'Viêm da tiếp xúc'),
(20,5,'primary',N'Mụn ẩn nhẹ');


INSERT INTO TaiKham (PhienKhamID, BenhNhanID, NgayDuKien, LyDo, TrangThai)
VALUES
(1,1,'2025-12-19',N'Tái khám mụn', 'pending'),
(2,2,'2025-12-20',N'Tái khám dị ứng', 'pending'),
(3,3,'2025-12-21',N'Tái khám vảy nến', 'pending'),
(4,4,'2025-12-22',N'Tái khám mụn', 'pending'),
(5,5,'2025-12-23',N'Tái khám da khô', 'pending'),
(6,6,'2025-12-24',N'Tái khám rụng tóc', 'pending'),
(7,7,'2025-12-25',N'Tái khám mụn', 'pending'),
(8,8,'2025-12-26',N'Tái khám viêm da cơ địa', 'pending'),
(9,9,'2025-12-27',N'Tái khám dị ứng mỹ phẩm', 'pending'),
(10,10,'2025-12-28',N'Tái khám nám da', 'pending'),
(11,11,'2025-12-29',N'Tái khám mụn', 'pending'),
(12,12,'2025-12-30',N'Tái khám da dầu', 'pending'),
(13,13,'2025-12-31',N'Tái khám vảy nến', 'pending'),
(14,14,'2026-01-01',N'Tái khám viêm nang lông', 'pending'),
(15,15,'2026-01-02',N'Tái khám rụng tóc', 'pending'),
(16,16,'2026-01-03',N'Tái khám mụn ẩn', 'pending'),
(17,17,'2026-01-04',N'Tái khám da nhạy cảm', 'pending'),
(18,18,'2026-01-05',N'Tái khám bỏng nắng', 'pending'),
(19,19,'2026-01-06',N'Tái khám viêm da tiếp xúc', 'pending'),
(20,20,'2026-01-07',N'Tái khám mụn ẩn', 'pending');


INSERT INTO LieuTrinhDieuTri (BenhNhanID, PhienKhamID, TenLieuTrinh, TongSoBuoi, TrangThai)
VALUES
(1,1,N'Liệu trình mụn',4,'in-progress'),
(2,2,N'Liệu trình dị ứng',3,'in-progress'),
(3,3,N'Liệu trình vảy nến',5,'in-progress'),
(4,4,N'Liệu trình mụn',4,'in-progress'),
(5,5,N'Liệu trình da khô',3,'in-progress'),
(6,6,N'Liệu trình rụng tóc',4,'in-progress'),
(7,7,N'Liệu trình mụn',4,'in-progress'),
(8,8,N'Liệu trình viêm da cơ địa',3,'in-progress'),
(9,9,N'Liệu trình dị ứng mỹ phẩm',3,'in-progress'),
(10,10,N'Liệu trình nám da',5,'in-progress'),
(11,11,N'Liệu trình mụn',4,'in-progress'),
(12,12,N'Liệu trình da dầu',3,'in-progress'),
(13,13,N'Liệu trình vảy nến',5,'in-progress'),
(14,14,N'Liệu trình viêm nang lông',3,'in-progress'),
(15,15,N'Liệu trình rụng tóc',4,'in-progress'),
(16,16,N'Liệu trình mụn ẩn',4,'in-progress'),
(17,17,N'Liệu trình da nhạy cảm',3,'in-progress'),
(18,18,N'Liệu trình bỏng nắng',2,'in-progress'),
(19,19,N'Liệu trình viêm da tiếp xúc',3,'in-progress'),
(20,20,N'Liệu trình mụn ẩn',4,'in-progress');


INSERT INTO LieuTrinh_BuoiDieuTri (LieuTrinhID, SoBuoi, NgayDuKien, NhanVienID)
VALUES
(1,1,'2025-12-20',1),(1,2,'2025-12-22',1),(1,3,'2025-12-24',1),(1,4,'2025-12-26',1),
(2,1,'2025-12-21',2),(2,2,'2025-12-23',2),(2,3,'2025-12-25',2),
(3,1,'2025-12-22',3),(3,2,'2025-12-24',3),(3,3,'2025-12-26',3),(3,4,'2025-12-28',3),(3,5,'2025-12-30',3),
(4,1,'2025-12-23',1),(4,2,'2025-12-25',1),(4,3,'2025-12-27',1),(4,4,'2025-12-29',1),
(5,1,'2025-12-24',2),(5,2,'2025-12-26',2),(5,3,'2025-12-28',2),
(6,1,'2025-12-25',3);


INSERT INTO BuoiDieuTri_CanLamSang (BuoiDieuTriID, CanLamSangID, KetQua, TrangThai)
VALUES
(1,1,'Bình thường','done'),
(2,2,'Tăng nhẹ','done'),
(3,3,'Bình thường','done'),
(4,4,'Tăng','done'),
(5,5,'Âm tính','done'),
(6,6,'Bình thường','done'),
(7,7,'Bình thường','done'),
(8,8,'Tăng nhẹ','done'),
(9,9,'Bình thường','done'),
(10,10,'Bình thường','done'),
(11,11,'Tăng','done'),
(12,12,'Âm tính','done'),
(13,13,'Bình thường','done'),
(14,14,'Bình thường','done'),
(15,15,'Tăng','done'),
(16,16,'Âm tính','done'),
(17,17,'Bình thường','done'),
(18,18,'Bình thường','done'),
(19,19,'Tăng nhẹ','done'),
(20,20,'Bình thường','done');
