-- Nâng cấp CSDL hiện có mà không xóa dữ liệu; không chạy script 01/03 để nâng cấp.
-- Giữ nguyên hóa đơn, tồn kho và nhân viên; có thể chạy lại.
USE QLBanTivi;
GO
SET XACT_ABORT ON;
BEGIN TRANSACTION;
GO
IF COL_LENGTH('dbo.tblNhanVien','Passw') IS NULL
    ALTER TABLE dbo.tblNhanVien ADD Passw NVARCHAR(256) NULL;
IF COL_LENGTH('dbo.tblChiTietHDB','GiaVonChot') IS NULL
    ALTER TABLE dbo.tblChiTietHDB ADD GiaVonChot DECIMAL(18,2) NULL
        CONSTRAINT CK_CTHDB_GiaVon CHECK (GiaVonChot>=0);
GO
-- Chuyển mật khẩu từ tài khoản cũ, ưu tiên Admin rồi theo tên đăng nhập.
-- Không ghi đè mật khẩu nhân viên đã được thiết lập.
UPDATE nv SET Passw=account.MatKhau
FROM dbo.tblNhanVien nv
CROSS APPLY (
    SELECT TOP 1 t.MatKhau FROM dbo.tblTaiKhoan t
    WHERE t.MaNV=nv.MaNV
    ORDER BY CASE WHEN t.Quyen=N'Admin' THEN 0 ELSE 1 END,t.TenDangNhap
) account
WHERE nv.Passw IS NULL;
GO
-- Hóa đơn bán cũ không có giá vốn lịch sử đáng tin cậy nên giữ NULL.
-- Báo cáo đánh dấu thiếu giá vốn thay vì tự ước tính lợi nhuận cũ.
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
    -- Giữ cùng quy tắc giá nhập mới nhất với trigger trong script tạo mới.
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


COMMIT TRANSACTION;
GO
