-- ===================================================================
-- BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN (LTTQ) - C# WINFORMS
-- ĐỀ TÀI: HỆ THỐNG QUẢN LÝ BÁN TIVI (TV STORE)
-- FILE 3: DỮ LIỆU MẪU ĐỂ CHẠY THỬ NGHIỆM HỆ THỐNG (CHUẨN THEO SƠ ĐỒ RM)
-- ===================================================================

USE QLBanTivi;
GO

-- Tạm thời vô hiệu hóa trigger trong lúc nạp seed data để giữ đúng các số liệu tồn kho ban đầu
IF OBJECT_ID('trg_ChiTietHDN_CapNhat', 'TR') IS NOT NULL ALTER TABLE tblChiTietHDN DISABLE TRIGGER trg_ChiTietHDN_CapNhat;
IF OBJECT_ID('trg_ChiTietHDB_CapNhat', 'TR') IS NOT NULL ALTER TABLE tblChiTietHDB DISABLE TRIGGER trg_ChiTietHDB_CapNhat;
GO

-- 1. HÃNG SẢN XUẤT
INSERT INTO tblHangSX (MaHangSX, TenHangSX) VALUES
('HSX01', N'Sony'),
('HSX02', N'Samsung'),
('HSX03', N'LG'),
('HSX04', N'TCL'),
('HSX05', N'Xiaomi');

-- 2. KIỂU DÁNG
INSERT INTO tblKieuDang (MaKieu, TenKieu) VALUES
('KD01', N'Màn hình phẳng tràn viền'),
('KD02', N'Màn hình cong nghệ thuật'),
('KD03', N'Khung tranh treo tường'),
('KD04', N'Chân đế chữ V kim loại');

-- 3. MÀU SẮC
INSERT INTO tblMauSac (MaMau, TenMau) VALUES
('M01', N'Đen bóng Titan'),
('M02', N'Xám bạc không gỉ'),
('M03', N'Trắng tinh tế'),
('M04', N'Vàng kim ánh kim');

-- 4. LOẠI MÀN HÌNH
INSERT INTO tblManHinh (MaManHinh, TenManHinh) VALUES
('MH01', N'OLED 4K Ultra HD'),
('MH02', N'QLED Smart TV 4K'),
('MH03', N'Neo QLED 8K'),
('MH04', N'Mini-LED Full Array'),
('MH05', N'LED 4K HDR');

-- 5. CỠ MÀN HÌNH
INSERT INTO tblCoManHinh (MaCo, TenCo) VALUES
('CO01', N'43 inch'),
('CO02', N'50 inch'),
('CO03', N'55 inch'),
('CO04', N'65 inch'),
('CO05', N'75 inch'),
('CO06', N'85 inch');

-- 6. NƯỚC SẢN XUẤT
INSERT INTO tblNuocSX (MaNuocSX, TenNuocSX) VALUES
('NSX01', N'Việt Nam'),
('NSX02', N'Nhật Bản'),
('NSX03', N'Hàn Quốc'),
('NSX04', N'Malaysia'),
('NSX05', N'Thái Lan');

-- 7. CA LÀM & CÔNG VIỆC
INSERT INTO tblCaLam (MaCa, TenCa) VALUES
('CA01', N'Ca Sáng (08:00 - 12:00)'),
('CA02', N'Ca Chiều (13:00 - 17:00)'),
('CA03', N'Ca Tối (17:00 - 21:30)');

INSERT INTO tblCongViec (MaCV, TenCV) VALUES
('CV01', N'Quản lý cửa hàng'),
('CV02', N'Nhân viên bán hàng'),
('CV03', N'Nhân viên kho vận'),
('CV04', N'Kế toán thu ngân');

-- 8. NHÂN VIÊN
INSERT INTO tblNhanVien (MaNV, TenNV, GioiTinh, NgaySinh, DienThoai, DiaChi, MaCa, MaCV) VALUES
('NV01', N'Nguyễn Hoàng Long', N'Nam', '1995-04-12', '0912345678', N'Cầu Giấy, Hà Nội', 'CA01', 'CV01'),
('NV02', N'Trần Thị Mai Phương', N'Nữ', '1998-08-20', '0987654321', N'Đống Đa, Hà Nội', 'CA02', 'CV02'),
('NV03', N'Lê Minh Tuấn', N'Nam', '2000-01-15', '0934567890', N'Thanh Xuân, Hà Nội', 'CA03', 'CV03'),
('NV04', N'Phạm Thu Hà', N'Nữ', '1999-11-05', '0978123456', N'Hà Đông, Hà Nội', 'CA01', 'CV04');

-- TÀI KHOẢN
INSERT INTO tblTaiKhoan (TenDangNhap, MatKhau, MaNV, Quyen) VALUES
('admin', '123456', 'NV01', N'Admin'),
('nv_banhang', '123456', 'NV02', N'Nhân viên'),
('nv_kho', '123456', 'NV03', N'Nhân viên');

-- 9. KHÁCH HÀNG (MaKhach, TenKhach)
INSERT INTO tblKhachHang (MaKhach, TenKhach, DiaChi, DienThoai) VALUES
('KH01', N'Công ty Cổ phần Alpha Media', N'Hai Bà Trưng, Hà Nội', '0901234567'),
('KH02', N'Nguyễn Văn Hùng', N'Ba Đình, Hà Nội', '0913579246'),
('KH03', N'Vũ Thị Bích Ngọc', N'Tây Hồ, Hà Nội', '0988776655'),
('KH04', N'Trịnh Quốc Dũng', N'Hoàng Mai, Hà Nội', '0945678123');

-- 10. NHÀ CUNG CẤP (MaNCC, TenNCC)
INSERT INTO tblNhaCungCap (MaNCC, TenNCC, DiaChi, DienThoai) VALUES
('NCC01', N'Tổng Công ty Điện Máy Sony Việt Nam', N'Quận 1, TP. Hồ Chí Minh', '02838221144'),
('NCC02', N'Công ty TNHH Điện tử Samsung Vina', N'KCN Yên Phong, Bắc Ninh', '02223889900'),
('NCC03', N'Công ty Cổ phần LG Electronics Hải Phòng', N'KCN Tràng Duệ, Hải Phòng', '02253778899'),
('NCC04', N'Tập đoàn Điện tử TCL Technology', N'Bình Dương', '02743556677');

-- 11. SẢN PHẨM TIVI (MaTV, TenTV)
INSERT INTO tblTV (MaTV, TenTV, MaHangSX, MaKieu, MaMau, MaManHinh, MaCo, MaNuocSX, SoLuong, DonGiaNhap, DonGiaBan, ThoiGianBaoHanh, Anh, GhiChu) VALUES
('TV01', N'Google Tivi Sony 4K 55 inch KD-55X75K', 'HSX01', 'KD01', 'M01', 'MH05', 'CO03', 'NSX04', 15, 11500000, 12650000, 24, 'sony_55x75k.jpg', N'Bán chạy nhất phân khúc 55 inch'),
('TV02', N'Smart Tivi Samsung Neo QLED 4K 65 inch QA65QN85C', 'HSX02', 'KD01', 'M02', 'MH03', 'CO04', 'NSX01', 8, 23000000, 25300000, 24, 'samsung_65qn85c.jpg', N'Âm thanh vòm Dolby Atmos đỉnh cao'),
('TV03', N'Smart Tivi OLED LG 4K 55 inch 55C3PSA', 'HSX03', 'KD03', 'M01', 'MH01', 'CO03', 'NSX05', 12, 26000000, 28600000, 36, 'lg_55c3psa.jpg', N'Màn hình OLED đen tuyệt đối'),
('TV04', N'Google Tivi TCL QLED 4K 43 inch 43Q646', 'HSX04', 'KD04', 'M01', 'MH02', 'CO01', 'NSX01', 20, 6800000, 7480000, 24, 'tcl_43q646.jpg', N'Giá rẻ cho phòng ngủ'),
('TV05', N'Smart Tivi Xiaomi A Pro 4K 65 inch', 'HSX05', 'KD01', 'M02', 'MH05', 'CO04', 'NSX01', 10, 10200000, 11220000, 24, 'xiaomi_65apro.jpg', N'Màn hình viền kim loại siêu mỏng');

-- 12. DỮ LIỆU HÓA ĐƠN NHẬP & CHI TIẾT (SoHDN, MaTV)
INSERT INTO tblHoaDonNhap (SoHDN, MaNV, NgayNhap, MaNCC, TongTien) VALUES
('HDN001', 'NV03', '2026-02-10', 'NCC01', 115000000),
('HDN002', 'NV03', '2026-03-05', 'NCC02', 184000000),
('HDN003', 'NV03', '2026-05-18', 'NCC03', 130000000),
('HDN004', 'NV03', '2026-07-22', 'NCC01', 57500000);

INSERT INTO tblChiTietHDN (SoHDN, MaTV, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDN001', 'TV01', 10, 11500000, 0, 115000000),
('HDN002', 'TV02', 8, 23000000, 0, 184000000),
('HDN003', 'TV03', 5, 26000000, 0, 130000000),
('HDN004', 'TV01', 5, 11500000, 0, 57500000);

-- 13. DỮ LIỆU HÓA ĐƠN BÁN & CHI TIẾT (SoHDB, MaKhach, Thue, MaTV, DonGia)
INSERT INTO tblHoaDonBan (SoHDB, MaNV, NgayBan, MaKhach, Thue, TongTien) VALUES
('HDB001', 'NV02', '2026-02-15', 'KH01', 10, 41745000),
('HDB002', 'NV02', '2026-03-20', 'KH01', 10, 55660000),
('HDB003', 'NV02', '2026-04-10', 'KH02', 10, 31460000),
('HDB004', 'NV02', '2026-05-25', 'KH03', 10, 13915000),
('HDB005', 'NV02', '2026-06-12', 'KH01', 10, 27830000);

INSERT INTO tblChiTietHDB (SoHDB, MaTV, SoLuong, DonGia, GiamGia, ThanhTien) VALUES
('HDB001', 'TV01', 3, 12650000, 0, 37950000),
('HDB002', 'TV02', 2, 25300000, 0, 50600000),
('HDB003', 'TV03', 1, 28600000, 0, 28600000),
('HDB004', 'TV01', 1, 12650000, 0, 12650000),
('HDB005', 'TV02', 1, 25300000, 0, 25300000);
GO

-- Bật lại Trigger sau khi đã nạp xong toàn bộ dữ liệu mẫu
IF OBJECT_ID('trg_ChiTietHDN_CapNhat', 'TR') IS NOT NULL ALTER TABLE tblChiTietHDN ENABLE TRIGGER trg_ChiTietHDN_CapNhat;
IF OBJECT_ID('trg_ChiTietHDB_CapNhat', 'TR') IS NOT NULL ALTER TABLE tblChiTietHDB ENABLE TRIGGER trg_ChiTietHDB_CapNhat;
GO
