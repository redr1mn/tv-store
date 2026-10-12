-- ===================================================================
-- BÀI TẬP LỚN LẬP TRÌNH TRỰC QUAN (LTTQ) - C# WINFORMS
-- ĐỀ TÀI: HỆ THỐNG QUẢN LÝ BÁN TIVI (TV STORE)
-- FILE 2: TRIGGERS & STORED PROCEDURES (XỬ LÝ NGHIỆP VỤ & BÁO CÁO THEO SƠ ĐỒ RM)
-- GIAI ĐOẠN 1: NÂNG CẤP TRIGGER CHỐNG ÂM KHO, PHỤC HỒI GIÁ VỐN & ĐỒNG BỘ DECIMAL
-- ===================================================================

USE QLBanTivi;
GO

-- -------------------------------------------------------------
-- 1. TRIGGER KHI THÊM/SỬA/XÓA CHI TIẾT HÓA ĐƠN NHẬP
-- Nghiệp vụ 1: Chống âm kho khi XÓA hoặc GIẢM số lượng nhập nếu hàng đã bán
-- Nghiệp vụ 2: Cập nhật tăng/giảm số lượng tồn kho trong bảng tblTV
-- Nghiệp vụ 3: Cập nhật Đơn giá nhập mới nhất vào bảng tblTV
-- Nghiệp vụ 4: Cập nhật Đơn giá bán = 110% Đơn giá nhập vào tblTV
-- Nghiệp vụ 5: Khôi phục Đơn giá nhập gần nhất nếu dòng nhập bị xóa
-- Nghiệp vụ 6: Cập nhật tổng tiền hóa đơn nhập
-- -------------------------------------------------------------
CREATE OR ALTER TRIGGER trg_ChiTietHDN_CapNhat
ON tblChiTietHDN
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Chống âm tồn kho khi XÓA hoặc CẬP NHẬT giảm số lượng hóa đơn nhập
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        IF EXISTS (
            SELECT 1 
            FROM tblTV tv
            INNER JOIN (
                SELECT 
                    d.MaTV, 
                    (d.TongSL - ISNULL(i.TongSL, 0)) AS NetGiam
                FROM (
                    SELECT MaTV, SUM(SoLuong) AS TongSL 
                    FROM deleted 
                    GROUP BY MaTV
                ) d
                LEFT JOIN (
                    SELECT MaTV, SUM(SoLuong) AS TongSL 
                    FROM inserted 
                    GROUP BY MaTV
                ) i ON d.MaTV = i.MaTV
            ) delta ON tv.MaTV = delta.MaTV
            WHERE delta.NetGiam > 0 AND tv.SoLuong < delta.NetGiam
        )
        BEGIN
            RAISERROR(N'Không thể xóa hoặc giảm số lượng hóa đơn nhập vì hàng trong kho đã được xuất bán, sẽ gây âm tồn kho!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
    END

    -- 2. Trừ số lượng cũ khi XÓA hoặc CẬP NHẬT
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong - d.TongSL
        FROM tblTV
        INNER JOIN (
            SELECT MaTV, SUM(SoLuong) AS TongSL
            FROM deleted
            GROUP BY MaTV
        ) d ON tblTV.MaTV = d.MaTV;
    END

    -- 3. Cộng số lượng mới & cập nhật đơn giá khi THÊM hoặc CẬP NHẬT
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong + i.TongSL,
            DonGiaNhap = i.DonGiaMoiNhat,
            DonGiaBan = ROUND(i.DonGiaMoiNhat * 1.1, 0) -- Yêu cầu 3: Đơn giá bán = 110% giá nhập
        FROM tblTV
        INNER JOIN (
            SELECT MaTV, SUM(SoLuong) AS TongSL, MAX(DonGia) AS DonGiaMoiNhat
            FROM inserted
            GROUP BY MaTV
        ) i ON tblTV.MaTV = i.MaTV;
    END

    -- 4. Nếu XÓA (không có inserted cho mã Tivi đó), khôi phục lại đơn giá nhập gần nhất còn lại trong hệ thống
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE tv
        SET tv.DonGiaNhap = ISNULL(lastHDN.DonGia, 0),
            tv.DonGiaBan = ROUND(ISNULL(lastHDN.DonGia, 0) * 1.1, 0)
        FROM tblTV tv
        INNER JOIN (
            SELECT DISTINCT d.MaTV 
            FROM deleted d
            WHERE NOT EXISTS (SELECT 1 FROM inserted i WHERE i.MaTV = d.MaTV)
        ) delOnly ON tv.MaTV = delOnly.MaTV
        OUTER APPLY (
            SELECT TOP 1 ct.DonGia
            FROM tblChiTietHDN ct
            INNER JOIN tblHoaDonNhap hdn ON ct.SoHDN = hdn.SoHDN
            WHERE ct.MaTV = tv.MaTV
            ORDER BY hdn.NgayNhap DESC, hdn.SoHDN DESC
        ) lastHDN;
    END

    -- 5. Cập nhật lại tổng tiền hóa đơn nhập
    UPDATE tblHoaDonNhap
    SET TongTien = ISNULL((
        SELECT SUM(ThanhTien) 
        FROM tblChiTietHDN 
        WHERE tblChiTietHDN.SoHDN = tblHoaDonNhap.SoHDN
    ), 0)
    WHERE tblHoaDonNhap.SoHDN IN (
        SELECT SoHDN FROM deleted 
        UNION 
        SELECT SoHDN FROM inserted
    );
END;
GO

-- -------------------------------------------------------------
-- 2. TRIGGER KHI THÊM/SỬA/XÓA CHI TIẾT HÓA ĐƠN BÁN
-- Nghiệp vụ 1: Chống âm kho khi bán (kiểm tra tồn kho khả dụng)
-- Nghiệp vụ 2: Trừ số lượng tồn kho trong bảng tblTV khi bán
-- Nghiệp vụ 3: Hoàn lại tồn kho khi sửa hoặc xóa hóa đơn bán
-- Nghiệp vụ 4: Cập nhật tổng tiền hóa đơn bán (có tính kèm Thuế VAT nếu có)
-- -------------------------------------------------------------
CREATE OR ALTER TRIGGER trg_ChiTietHDB_CapNhat
ON tblChiTietHDB
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Chống âm tồn kho khi THÊM hoặc CẬP NHẬT tăng số lượng bán
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        IF EXISTS (
            SELECT 1 
            FROM tblTV tv
            INNER JOIN (
                SELECT 
                    i.MaTV,
                    (i.TongSL - ISNULL(d.TongSL, 0)) AS NetBan
                FROM (
                    SELECT MaTV, SUM(SoLuong) AS TongSL
                    FROM inserted
                    GROUP BY MaTV
                ) i
                LEFT JOIN (
                    SELECT MaTV, SUM(SoLuong) AS TongSL
                    FROM deleted
                    GROUP BY MaTV
                ) d ON i.MaTV = d.MaTV
            ) delta ON tv.MaTV = delta.MaTV
            WHERE delta.NetBan > 0 AND tv.SoLuong < delta.NetBan
        )
        BEGIN
            RAISERROR(N'Số lượng bán vượt quá số lượng tồn kho khả dụng!', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
    END

    -- 2. Hoàn lại số lượng đã bán trước đó khi XÓA hoặc CẬP NHẬT
    IF EXISTS (SELECT * FROM deleted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong + d.TongSL
        FROM tblTV
        INNER JOIN (
            SELECT MaTV, SUM(SoLuong) AS TongSL
            FROM deleted
            GROUP BY MaTV
        ) d ON tblTV.MaTV = d.MaTV;
    END

    -- 3. Trừ số lượng bán khi THÊM hoặc CẬP NHẬT
    IF EXISTS (SELECT * FROM inserted)
    BEGIN
        UPDATE tblTV
        SET SoLuong = tblTV.SoLuong - i.TongSL
        FROM tblTV
        INNER JOIN (
            SELECT MaTV, SUM(SoLuong) AS TongSL
            FROM inserted
            GROUP BY MaTV
        ) i ON tblTV.MaTV = i.MaTV;
    END

    -- 4. Cập nhật lại tổng tiền hóa đơn bán (có tính kèm Thuế VAT)
    UPDATE tblHoaDonBan
    SET TongTien = ISNULL((
        SELECT ROUND(SUM(ThanhTien) * (1.0 + ISNULL(tblHoaDonBan.Thue, 0) / 100.0), 0)
        FROM tblChiTietHDB 
        WHERE tblChiTietHDB.SoHDB = tblHoaDonBan.SoHDB
    ), 0)
    WHERE tblHoaDonBan.SoHDB IN (
        SELECT SoHDB FROM deleted 
        UNION 
        SELECT SoHDB FROM inserted
    );
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
    ORDER BY TongSoLuongMua DESC, tv.MaTV ASC;
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
            CAST(0 AS DECIMAL(5, 2)) AS Thue,
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
    ORDER BY TongSoLuongGiao DESC, TongGiaTriNhap DESC;
END;
GO
