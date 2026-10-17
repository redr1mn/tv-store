# Quản lý bán tivi

Ứng dụng C# WinForms, .NET Framework 4.7.2, SQL Server và Microsoft Excel desktop.

## Cơ sở dữ liệu

- **Máy đã có dữ liệu:** chỉ chạy `Database_Scripts/04_Upgrade_Requirements.sql` trong SQL Server Management Studio. Script bổ sung trường mật khẩu nhân viên, giá vốn lúc bán và cập nhật trigger, giữ nguyên hóa đơn và tồn kho. Có thể chạy lại.
- **Cài mới:** chạy lần lượt `01_Create_Tables.sql`, `02_Triggers_StoreProcedures.sql`, `03_Sample_Data.sql`.
- Script **01 xóa các bảng cũ**, script **03 xóa dữ liệu để nạp mẫu**. Không chạy chúng để nâng cấp dữ liệu đang dùng.
- Địa chỉ SQL Server được cấu hình tại `tv-store/App.config`, tên connection string `QLBanTivi`. Chương trình chỉ dùng cấu hình này, không tự dò hoặc chuyển sang SQL Server khác.

## Đăng nhập

Tên đăng nhập là **mã nhân viên**, mật khẩu nằm trong `tblNhanVien.Passw` theo yêu cầu bài tập. Khi nâng cấp, mật khẩu cũ trong `tblTaiKhoan` được chuyển sang nhân viên tương ứng. Quyền Admin vẫn lấy từ `tblTaiKhoan`.

Với dữ liệu mẫu: `NV01`, `NV02`, `NV03` có mật khẩu `123456`; `NV01` là Admin. Nhân viên mới chưa đặt mật khẩu không thể đăng nhập. Admin chọn nhân viên đã lưu ở form Nhân viên, bấm **Đặt mật khẩu đăng nhập** để cấp hoặc đổi mật khẩu. Không có tài khoản mặc định vượt qua kiểm tra CSDL.

## Hóa đơn

- Bấm **Thêm HĐ**, nhân viên lập tự lấy từ đăng nhập. Mã bán có dạng `HĐB_ddMMyyyy0001`; mã nhập tương tự với `HĐN_`. Mã được cấp chính thức khi lưu để tránh trùng giữa nhiều máy.
- Với hóa đơn bán, nhập số điện thoại rồi chuyển sang ô khác. Khách cũ tự hiện thông tin; khách mới nhập tên, địa chỉ. Khách mới và hóa đơn được lưu trong cùng giao dịch.
- Chọn tivi, nhập số lượng và giảm giá 0–100%, bấm thêm mặt hàng rồi lưu hóa đơn. Giá tiền dùng `decimal`.
- Chọn hoặc gõ mã ở danh sách mã hóa đơn, bấm **Tìm HĐ** để mở lại; cũng có thể click đúp kết quả ở màn hình tra cứu.
- Hóa đơn đã lưu được khóa phần nhập liệu. Click đúp dòng sản phẩm để trả/xóa **toàn bộ dòng hàng đó**, tự cập nhật tổng tiền và kho. Bấm **Hủy HĐ** để xóa toàn bộ hóa đơn đã lưu và cập nhật kho. Hủy hóa đơn nhập sẽ bị từ chối nếu gây âm kho.
- **In HĐ/Xuất Excel** lấy dữ liệu đã lưu. Máy cần cài Microsoft Excel desktop để dùng chức năng xuất Excel qua Interop.

## Ảnh và báo cáo

Ảnh mới được sao chép vào thư mục `Images` cạnh file chương trình; CSDL chỉ lưu tên file. Khi chuyển máy, sao chép cả thư mục `Images`. Ảnh cũ lưu đường dẫn tuyệt đối vẫn đọc được; mở sản phẩm và lưu lại sẽ chuyển ảnh sang cách lưu mới nếu file cũ còn tồn tại.

Trong **Báo cáo thống kê → Lãi lỗ và biểu đồ**, chọn tháng/quý/năm, khoảng năm và mặt hàng. Có bảng doanh thu, giá vốn, lãi/lỗ gộp, tăng trưởng doanh thu giữa hai kỳ liên tiếp, biểu đồ và xuất Excel. Doanh thu không gồm VAT; lợi nhuận gộp chưa trừ chi phí vận hành. Khi kỳ trước có doanh thu bằng 0, tỷ lệ tăng trưởng để trống.

Giá vốn của hóa đơn mới được chốt theo đơn giá nhập sản phẩm tại lúc bán. Dữ liệu bán cũ không có giá vốn lịch sử đáng tin cậy nên để trống giá vốn/lãi lỗ và đánh dấu số dòng thiếu giá vốn. Không tự lấy giá nhập hiện tại để gán ngược cho hóa đơn cũ.

## Kiểm tra

Project bật `UseWinFormsOutOfProcDesigner` cho WinForms Designer và chặn nạp nghiệp vụ/CSDL khi đang thiết kế. Sau khi thay đổi cấu hình project, đóng các tab Designer, đóng/mở lại solution, rồi Rebuild trước khi mở form bằng View Designer. Việc kiểm tra nạp form trên DesignSurface không thay thế việc mở Designer trực tiếp trong Visual Studio.

Build bằng Visual Studio có .NET Framework 4.7.2 targeting pack và thư viện COM Excel. Sau khi build Debug, chạy PowerShell:

```powershell
.\tests\Run-IntegrationTests.ps1
.\tests\Run-IntegrationTests.ps1 -LegacySchema
```

Bộ kiểm thử tạo CSDL tên ngẫu nhiên `TVStore_Requirements_Test_...`, kiểm tra giao dịch, tồn kho, nâng cấp dữ liệu cũ, báo cáo, giao diện và bán đồng thời; sau đó xóa CSDL thử. Không chạy các script xóa dữ liệu trên `QLBanTivi`. Tài khoản SQL chạy kiểm thử cần quyền tạo/xóa CSDL. Ảnh kết xuất form nằm trong `tv-store/bin/Debug`.

## Tổ chức mã nguồn

- `frmHoaDonBan` và `frmHoaDonNhap` xử lý trực tiếp control và sự kiện của từng form; không tìm control bằng tên chuỗi qua controller trung gian.
- `DatabaseHelper` dùng chung cho truy vấn có tham số và tự giải phóng kết nối sau mỗi lần gọi. Các hàm hỗ trợ form danh mục cũng dùng cơ chế này.
- `InvoiceRepository` giữ nghiệp vụ hóa đơn: tính tiền dòng hàng, cấp mã, lưu bán/nhập và hủy/trả hàng trong giao dịch. Trigger là nơi cập nhật tồn kho và tổng tiền đã lưu.
- Bốn báo cáo thống kê gọi stored procedure trong script 02, không chép lại câu SQL trong form. Báo cáo lãi/lỗ giữ truy vấn riêng theo các bộ lọc nâng cao.
- Hai loại hóa đơn dùng chung mẫu xuất Excel. Comment nghiệp vụ được viết bằng tiếng Việt.
