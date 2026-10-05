-- ===================================================================
-- BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN (LTTQ) - C# WINFORMS
-- ĐỀ TÀI: HỆ THỐNG QUẢN LÝ BÁN TIVI (TV STORE)
-- FILE 2: TRIGGERS & STORED PROCEDURES (XỬ LÝ NGHIỆP VỤ & BÁO CÁO THEO SƠ ĐỒ RM)
-- ===================================================================

USE QLBanTivi;
GO

-- -------------------------------------------------------------
-- 1. TRIGGER KHI THÊM/SỬA/XÓA CHI TIẾT HÓA ĐƠN NHẬP
-- Nghiệp vụ 1: Cập nhật tăng số lượng tồn kho trong bảng tblTV
-- Nghiệp vụ 2: Cập nhật Đơn giá nhập mới nhất vào bảng tblTV
-- Nghiệp vụ 3: Cập nhật Đơn giá bán = 110% Đơn giá nhập vào tblTV
-- Cập nhật tổng tiền hóa đơn nhập
-- -------------------------------------------------------------
CREATE OR ALTER TRIGGER trg_ChiTietHDN_CapNhat
ON tblChiTietHDN
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Xử lý khi XÓA hoặc CẬP NHẬT (trừ số lượng cũ đi)
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong - d.SoLuong
        FROM tblTV
        INNER JOIN deleted d ON tblTV.MaTV = d.MaTV;

        -- Cập nhật lại tổng tiền hóa đơn nhập
        UPDATE tblHoaDonNhap
        SET TongTien = ISNULL((SELECT SUM(ThanhTien) FROM tblChiTietHDN WHERE tblChiTietHDN.SoHDN = d.SoHDN), 0)
        FROM tblHoaDonNhap
        INNER JOIN deleted d ON tblHoaDonNhap.SoHDN = d.SoHDN;
    END

    -- Xử lý khi THÊM hoặc CẬP NHẬT (cộng số lượng mới, cập nhật giá nhập và giá bán = 1.1 * giá nhập)
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong + i.SoLuong,
            DonGiaNhap = i.DonGia,
            DonGiaBan = ROUND(i.DonGia * 1.1, 0) -- Yêu cầu 3: Đơn giá bán = 110% giá nhập
        FROM tblTV
        INNER JOIN inserted i ON tblTV.MaTV = i.MaTV;

        -- Cập nhật lại tổng tiền hóa đơn nhập
        UPDATE tblHoaDonNhap
        SET TongTien = ISNULL((SELECT SUM(ThanhTien) FROM tblChiTietHDN WHERE tblChiTietHDN.SoHDN = i.SoHDN), 0)
        FROM tblHoaDonNhap
        INNER JOIN inserted i ON tblHoaDonNhap.SoHDN = i.SoHDN;
    END
END;
GO

-- -------------------------------------------------------------
-- 2. TRIGGER KHI THÊM/SỬA/XÓA CHI TIẾT HÓA ĐƠN BÁN
-- Nghiệp vụ 1: Trừ số lượng tồn kho trong bảng tblTV
-- Cập nhật tổng tiền hóa đơn bán (có tính kèm Thuế VAT nếu có)
-- -------------------------------------------------------------
CREATE OR ALTER TRIGGER trg_ChiTietHDB_CapNhat
ON tblChiTietHDB
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Xử lý khi XÓA hoặc CẬP NHẬT (hoàn lại số lượng đã bán trước đó)
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong + d.SoLuong
        FROM tblTV
        INNER JOIN deleted d ON tblTV.MaTV = d.MaTV;

        UPDATE tblHoaDonBan
        SET TongTien = ISNULL((
            SELECT SUM(ThanhTien) * (1.0 + ISNULL(tblHoaDonBan.Thue, 0) / 100.0)
            FROM tblChiTietHDB 
            WHERE tblChiTietHDB.SoHDB = d.SoHDB
        ), 0)
        FROM tblHoaDonBan
        INNER JOIN deleted d ON tblHoaDonBan.SoHDB = d.SoHDB;
    END

    -- Xử lý khi THÊM hoặc CẬP NHẬT (trừ số lượng bán)
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong - i.SoLuong
        FROM tblTV
        INNER JOIN inserted i ON tblTV.MaTV = i.MaTV;

        UPDATE tblHoaDonBan
        SET TongTien = ISNULL((
            SELECT SUM(ThanhTien) * (1.0 + ISNULL(tblHoaDonBan.Thue, 0) / 100.0)
            FROM tblChiTietHDB 
            WHERE tblChiTietHDB.SoHDB = i.SoHDB
        ), 0)
        FROM tblHoaDonBan
        INNER JOIN inserted i ON tblHoaDonBan.SoHDB = i.SoHDB;
    END
END;
GO

-- -------------------------------------------------------------
-- 3. STORED PROCEDURE: BÁO CÁO 1 (YÊU CẦU SỐ 6)
-- Danh sách Top 3 sản phẩm Tivi được mua nhiều nhất từ một Khách hàng chọn trước
-- -------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_Top3TiviTheoKhachHang
    @MaKhach NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 3 
        tv.MaTV,
        tv.TenTV,
        hsx.TenHangSX,
        mh.TenManHinh,
        co.TenCo,
        SUM(ct.SoLuong) AS TongSoLuongMua,
        SUM(ct.ThanhTien) AS TongTienChi
    FROM tblChiTietHDB ct
    INNER JOIN tblHoaDonBan hdb ON ct.SoHDB = hdb.SoHDB
    INNER JOIN tblTV tv ON ct.MaTV = tv.MaTV
    LEFT JOIN tblHangSX hsx ON tv.MaHangSX = hsx.MaHangSX
    LEFT JOIN tblManHinh mh ON tv.MaManHinh = mh.MaManHinh
    LEFT JOIN tblCoManHinh co ON tv.MaCo = co.MaCo
    WHERE hdb.MaKhach = @MaKhach
    GROUP BY tv.MaTV, tv.TenTV, hsx.TenHangSX, mh.TenManHinh, co.TenCo
    ORDER BY TongSoLuongMua DESC;
END;
GO

-- -------------------------------------------------------------
-- 4. STORED PROCEDURE: BÁO CÁO 2 (YÊU CẦU SỐ 7)
-- Danh sách các hóa đơn và tổng tiền nhập hàng từ một Nhà cung cấp chọn trước
-- -------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_BaoCaoNhapHangTheoNCC
    @MaNCC NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        hdn.SoHDN,
        hdn.NgayNhap,
        nv.TenNV AS NguoiLap,
        ncc.TenNCC,
        hdn.TongTien
    FROM tblHoaDonNhap hdn
    INNER JOIN tblNhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC
    INNER JOIN tblNhanVien nv ON hdn.MaNV = nv.MaNV
    WHERE hdn.MaNCC = @MaNCC
    ORDER BY hdn.NgayNhap DESC;
END;
GO

-- -------------------------------------------------------------
-- 5. STORED PROCEDURE: BÁO CÁO 3 (YÊU CẦU SỐ 8)
-- Danh sách hóa đơn và tổng tiền mua (nhập)/bán hàng theo Quý chọn trước
-- -------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_BaoCaoHoaDonTheoQuy
    @Quy INT,
    @Nam INT,
    @LoaiHoaDon NVARCHAR(10) -- 'BAN' hoặc 'NHAP'
AS
BEGIN
    SET NOCOUNT ON;
    IF @LoaiHoaDon = 'BAN'
    BEGIN
        SELECT 
            hdb.SoHDB AS MaHoaDon,
            hdb.NgayBan AS NgayLap,
            nv.TenNV,
            kh.TenKhach AS DoiTac,
            hdb.Thue,
            hdb.TongTien
        FROM tblHoaDonBan hdb
        INNER JOIN tblNhanVien nv ON hdb.MaNV = nv.MaNV
        INNER JOIN tblKhachHang kh ON hdb.MaKhach = kh.MaKhach
        WHERE DATEPART(QUARTER, hdb.NgayBan) = @Quy AND YEAR(hdb.NgayBan) = @Nam
        ORDER BY hdb.NgayBan DESC;
    END
    ELSE
    BEGIN
        SELECT 
            hdn.SoHDN AS MaHoaDon,
            hdn.NgayNhap AS NgayLap,
            nv.TenNV,
            ncc.TenNCC AS DoiTac,
            0 AS Thue,
            hdn.TongTien
        FROM tblHoaDonNhap hdn
        INNER JOIN tblNhanVien nv ON hdn.MaNV = nv.MaNV
        INNER JOIN tblNhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC
        WHERE DATEPART(QUARTER, hdn.NgayNhap) = @Quy AND YEAR(hdn.NgayNhap) = @Nam
        ORDER BY hdn.NgayNhap DESC;
    END
END;
GO

-- -------------------------------------------------------------
-- 6. STORED PROCEDURE: BÁO CÁO 4 (YÊU CẦU SỐ 9)
-- Danh sách 5 Nhà cung cấp giao nhiều hàng nhất theo Tháng chọn trước
-- -------------------------------------------------------------
CREATE OR ALTER PROCEDURE sp_Top5NCCGiaoHangTheoThang
    @Thang INT,
    @Nam INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 5 
        ncc.MaNCC,
        ncc.TenNCC,
        ncc.DiaChi,
        ncc.DienThoai,
        COUNT(DISTINCT hdn.SoHDN) AS SoDonNhap,
        SUM(ct.SoLuong) AS TongSoLuongGiao,
        SUM(ct.ThanhTien) AS TongGiaTriNhap
    FROM tblHoaDonNhap hdn
    INNER JOIN tblNhaCungCap ncc ON hdn.MaNCC = ncc.MaNCC
    INNER JOIN tblChiTietHDN ct ON hdn.SoHDN = ct.SoHDN
    WHERE MONTH(hdn.NgayNhap) = @Thang AND YEAR(hdn.NgayNhap) = @Nam
    GROUP BY ncc.MaNCC, ncc.TenNCC, ncc.DiaChi, ncc.DienThoai
    ORDER BY TongSoLuongGiao DESC;
END;
GO
