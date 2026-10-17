-- Trigger cập nhật tồn kho và các thủ tục báo cáo.

USE QLBanTivi;
GO

-- Trigger xử lý cả thêm, sửa và xóa chi tiết; dùng chênh lệch để không cập nhật kho hai lần.
CREATE OR ALTER TRIGGER trg_ChiTietHDN_CapNhat
ON tblChiTietHDN AFTER INSERT,UPDATE,DELETE AS
BEGIN
    SET NOCOUNT ON;
    -- Cập nhật tồn kho theo chênh lệch số lượng trong một lần,
    -- tránh trừ tạm toàn bộ lượng nhập cũ và vi phạm ràng buộc không âm.
    ;WITH Movement AS (
        SELECT MaTV,SoLuong AS Delta FROM inserted
        UNION ALL SELECT MaTV,-SoLuong FROM deleted
    ), Delta AS (SELECT MaTV,SUM(Delta) AS Amount FROM Movement GROUP BY MaTV)
    UPDATE t WITH (UPDLOCK)
    SET t.SoLuong=t.SoLuong+d.Amount
    FROM tblTV t JOIN Delta d ON t.MaTV=d.MaTV;
    -- Ràng buộc CK_TV_SoLuong chặn xóa lượng nhập nếu làm tồn kho âm.
    -- Chọn lần nhập mới nhất theo ngày lập hóa đơn, không theo thứ tự thao tác.
    -- Vì vậy xóa hoặc sửa một lần nhập cũ không ghi đè giá của lần nhập mới hơn.
    UPDATE t SET DonGiaNhap=ISNULL(lastReceipt.DonGia,0),
        DonGiaBan=ROUND(ISNULL(lastReceipt.DonGia,0)*1.1,0)
    FROM tblTV t
    JOIN (SELECT MaTV FROM inserted UNION SELECT MaTV FROM deleted) changed ON t.MaTV=changed.MaTV
    OUTER APPLY (
        SELECT TOP 1 c.DonGia
        FROM tblChiTietHDN c JOIN tblHoaDonNhap h ON c.SoHDN=h.SoHDN
        WHERE c.MaTV=t.MaTV
        ORDER BY h.NgayNhap DESC,h.SoHDN DESC
    ) lastReceipt;
    UPDATE h SET TongTien=ISNULL((SELECT SUM(c.ThanhTien) FROM tblChiTietHDN c WHERE c.SoHDN=h.SoHDN),0)
    FROM tblHoaDonNhap h JOIN (SELECT SoHDN FROM inserted UNION SELECT SoHDN FROM deleted) x ON h.SoHDN=x.SoHDN;
END;
GO

CREATE OR ALTER TRIGGER trg_ChiTietHDB_CapNhat
ON tblChiTietHDB AFTER INSERT,UPDATE,DELETE AS
BEGIN
    SET NOCOUNT ON;
    ;WITH Movement AS (
        SELECT MaTV,-SoLuong AS Delta FROM inserted
        UNION ALL SELECT MaTV,SoLuong FROM deleted
    ), Delta AS (SELECT MaTV,SUM(Delta) AS Amount FROM Movement GROUP BY MaTV)
    UPDATE t WITH (UPDLOCK)
    SET t.SoLuong=t.SoLuong+d.Amount
    FROM tblTV t JOIN Delta d ON t.MaTV=d.MaTV;
    -- Nếu tồn kho âm, CK_TV_SoLuong từ chối và hoàn tác thay đổi hóa đơn.
    UPDATE h SET TongTien=ISNULL((
        SELECT ROUND(SUM(c.ThanhTien)*(1+ISNULL(h.Thue,0)/100.0),0)
        FROM tblChiTietHDB c WHERE c.SoHDB=h.SoHDB),0)
    FROM tblHoaDonBan h JOIN (SELECT SoHDB FROM inserted UNION SELECT SoHDB FROM deleted) x ON h.SoHDB=x.SoHDB;
END;
GO

-- Danh sách Top 3 sản phẩm Tivi được mua nhiều nhất từ một Khách hàng chọn trước
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

-- Danh sách các hóa đơn và tổng tiền nhập hàng từ một Nhà cung cấp chọn trước
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

-- Danh sách hóa đơn và tổng tiền mua (nhập)/bán hàng theo Quý chọn trước
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

-- Danh sách 5 Nhà cung cấp giao nhiều hàng nhất theo Tháng chọn trước
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
