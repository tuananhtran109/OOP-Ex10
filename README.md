# BÀI THỰC HÀNH OOP - TUẦN 10: DESIGN PATTERNS (SMART HOME MINI)

- **Học phần:** Lập trình hướng đối tượng (OOP)
- **Họ và tên:** Trần Tuấn Anh
- **MSSV:** 202418834
- **Chủ đề:** Xây dựng ứng dụng điều khiển nhà thông minh (Smart Home Mini) áp dụng 3 mẫu thiết kế: Factory, Singleton, Facade
- **Kho lưu trữ Github:** [https://github.com/tuananhtran109/OOP-Ex10](https://github.com/tuananhtran109/OOP-Ex10)

---

## 1. Cấu trúc thư mục dự án

```text
Week10/
├── Source/
│   ├── Devices/
│   │   ├── IDevice.cs               # Giao diện cơ bản cho thiết bị (TurnOn, TurnOff)
│   │   ├── ILockable.cs             # Giao diện thiết bị có khóa an ninh (Lock, Unlock)
│   │   ├── SmartLight.cs            # Lớp Đèn thông minh (triển khai IDevice)
│   │   ├── SmartFan.cs              # Lớp Quạt thông minh (triển khai IDevice)
│   │   └── SmartDoor.cs             # Lớp Cửa thông minh (triển khai IDevice, ILockable)
│   ├── Factory/
│   │   └── DeviceFactory.cs         # Mẫu Factory khởi tạo thiết bị trả về IDevice
│   ├── Configuration/
│   │   └── HomeConfiguration.cs     # Mẫu Singleton quản lý cấu hình duy nhất bằng Lazy<T>
│   ├── Facade/
│   │   └── SmartHomeFacade.cs       # Mẫu Facade cung cấp kịch bản GoToWork(), GoToSleep()
│   ├── Program.cs                   # Chương trình chính (Demo thực tế & Bộ test tự động T1-T5)
│   └── SmartHomeMini.csproj         # File cấu hình dự án .NET 10.0
├── BaoCao.pdf                       # File báo cáo PDF hoàn chỉnh
├── README.md                        # Tài liệu hướng dẫn & trả lời câu hỏi lý thuyết
├── .gitignore                       # Cấu hình bỏ qua các file build tạm
└── lab w10-pattern.pdf              # File đề bài thực hành tuần 10
```

---

## 2. Hướng dẫn biên dịch và chạy chương trình

### Yêu cầu môi trường:
- .NET SDK 8.0, 9.0 hoặc 10.0
- Trình biên dịch LaTeX (XeLaTeX) để dịch file báo cáo PDF

### Lệnh chạy ứng dụng C#:
```bash
cd Source
dotnet build
dotnet run
```


---

## 3. Tổng hợp 3 Design Pattern được áp dụng

| Pattern | Vị trí áp dụng | Lợi ích đạt được | Hạn chế của thiết kế |
| :--- | :--- | :--- | :--- |
| **Factory** *(Simple Factory)* | `DeviceFactory.Create(type, name)` | Che giấu chi tiết khởi tạo đối tượng cụ thể (`new SmartLight`, `new SmartFan`, `new SmartDoor`); Client chỉ phụ thuộc vào abstraction `IDevice`; Giảm phụ thuộc trực tiếp (loose coupling); Dễ chuẩn hóa chuỗi đầu vào (case-insensitive) và kiểm soát lỗi tập trung. | Khi thêm loại thiết bị mới (ví dụ `SmartCamera`), ta phải sửa đổi câu lệnh `switch` trong `DeviceFactory`, vi phạm một phần nguyên lý Open/Closed Principle (OCP). *(Có thể khắc phục bằng Factory Method hoặc Reflection / DI Registration)*. |
| **Singleton** | `HomeConfiguration.Instance` | Đảm bảo duy nhất một thể hiện cấu hình trong suốt vòng đời ứng dụng; Dữ liệu cấu hình (`HomeName`, `NightLightLevel`) được chia sẻ thống nhất; Khởi tạo lười (lazy loading) và an toàn đa luồng (thread-safe) với `Lazy<T>`. | Tạo ra trạng thái dùng chung toàn cục (global state), làm tăng mức độ phụ thuộc ngầm (hidden dependency); Gây khó khăn khi viết Unit Test độc lập (khó cô lập, khó mock/stub); Có thể dẫn đến tranh chấp nếu cấu hình bị ghi đè đồng thời. |
| **Facade** | `SmartHomeFacade` | Cung cấp giao diện cấp cao đơn giản (`GoToWork()`, `GoToSleep()`), phối hợp đồng bộ nhiều thiết bị; Client không cần biết chi tiết quy trình tắt bật hay thứ tự gọi từng thiết bị; Vẫn cho phép client điều khiển riêng lẻ từng thiết bị khi có nhu cầu. | Lớp Facade có thể trở thành "God Object" nắm giữ quá nhiều phụ thuộc nếu hệ thống mở rộng thêm hàng chục thiết bị khác nhau; Khi có thêm thao tác mới cần phải cập nhật Facade. |

---

## 4. Trả lời chi tiết các câu hỏi phân tích (Phần G & các phần liên quan)

### Câu 1: Factory đang che giấu quyết định thiết kế nào đối với Client?
- **Trả lời:** Factory che giấu **chi tiết tạo lập đối tượng cụ thể** (concrete class instantiation). Client không cần biết lớp cụ thể nào được gọi (`SmartLight`, `SmartFan`, hay `SmartDoor`), không cần biết cách thức cấp phát bộ nhớ (`new`) hay thứ tự truyền tham số khởi tạo. Client chỉ cần gửi một chuỗi định danh ("light", "fan", "door") và làm việc hoàn toàn thông qua hợp đồng trừu tượng `IDevice`.

### Câu 2: Singleton bảo đảm điều gì, nhưng tạo ra rủi ro nào?
- **Trả lời:** 
  - **Điều bảo đảm:** Đảm bảo lớp chỉ có **duy nhất một thể hiện (single instance)** trong suốt vòng đời ứng dụng, cung cấp một điểm truy cập toàn cục thống nhất (`Instance`), tiết kiệm tài nguyên và bảo đảm tính nhất quán dữ liệu cấu hình.
  - **Rủi ro tạo ra:**
    1. **Toàn cục hóa trạng thái (Global State):** Mọi module đều có thể đọc/ghi, gây khó kiểm soát nguồn gốc thay đổi dữ liệu.
    2. **Khó kiểm thử đơn vị (Unit Testing):** Trạng thái bị chia sẻ giữa các test case, không thể cô lập hoặc reset môi trường kiểm tra mà không tác động đến ca kiểm thử khác.
    3. **Phá vỡ nguyên lý thiết kế:** Vi phạm Single Responsibility Principle (lớp vừa quản lý dữ liệu vừa quản lý vòng đời của chính nó).

### Câu 3: Facade khác Factory ở mục đích chính như thế nào?
- **Trả lời:**
  - **Mục đích của Factory (Creational Pattern):** Tập trung vào việc **tạo lập đối tượng**. Factory giải quyết bài toán: *"Khởi tạo đối tượng nào, bằng cách nào mà không để client phụ thuộc vào lớp cụ thể"*.
  - **Mục đích của Facade (Structural Pattern):** Tập trung vào việc **cung cấp giao diện sử dụng cấp cao**. Facade giải quyết bài toán: *"Làm thế nào để đơn giản hóa việc tương tác với một hệ thống con (subsystem) phức tạp gồm nhiều đối tượng đã tồn tại"*.

### Câu 4: Nếu bỏ SmartHomeFacade, client sẽ phải biết thêm những chi tiết nào?
- **Trả lời:** Client sẽ bị buộc phải:
  1. Trực tiếp nắm giữ tham chiếu đến từng thiết bị (`light`, `fan`, `door`) và đối tượng cấu hình `HomeConfiguration`.
  2. Tự mình ghi nhớ và thực hiện đúng quy trình / thứ tự thao tác cho từng chế độ (ví dụ chế độ ngủ: tắt quạt, khóa cửa, bật đèn, đọc cấu hình sáng từ `HomeConfiguration` và cấu hình mức sáng).
  3. Ép kiểu tường minh `(ILockable)` hoặc nắm giữ các interface an ninh riêng của cửa.
  4. Mã nguồn client sẽ bị phụ thuộc chặt chẽ (tight coupling) vào cấu trúc nội bộ của hệ thống thiết bị.

### Câu hỏi phụ (Phần C & D):
1. **Khi thêm `SmartCamera`, những thành phần nào cần sửa?**
   - Cần tạo lớp mới `SmartCamera : IDevice` (và có thể thêm interface riêng như `IRecordable` nếu có).
   - Thêm một nhánh rẽ trong `DeviceFactory.Create()`: `"camera" => new SmartCamera(name)`.
   - Nếu kịch bản Facade cần bật/tắt camera khi đi làm, cập nhật thêm dependency vào `SmartHomeFacade`.
2. **Constructor của Singleton phải có mức truy cập nào?**
   - Phải là `private` để ngăn chặn hoàn toàn việc gọi `new HomeConfiguration()` từ bên ngoài.
3. **Vì sao lớp Singleton được đánh dấu `sealed`?**
   - Đánh dấu `sealed` để ngăn chặn kế thừa. Nếu cho phép kế thừa, các lớp con có thể tạo constructor công khai, sinh ra nhiều thể hiện khác nhau, phá vỡ tính duy nhất của Singleton.
4. **Nếu dùng .NET Dependency Injection với lifetime `AddSingleton`, có còn nhất thiết phải tự cài Singleton pattern không?**
   - **Không nhất thiết.** Khi dùng DI Container (`services.AddSingleton<IHomeConfiguration, HomeConfiguration>()`), IoC container sẽ tự động quản lý vòng đời của đối tượng là duy nhất trong ứng dụng. Khi đó, lớp cấu hình có thể sử dụng constructor công khai và triển khai Interface, giúp mã nguồn tuân thủ nguyên lý Dependency Inversion, cực kỳ thuận tiện cho việc Mocking và Unit Testing.

---

## 5. Kết quả kiểm thử tự động (T1 -> T5)

Chương trình đã cài đặt sẵn bộ kiểm thử tự động đạt chuẩn 100%:
- **[T1] Tạo light, fan, door:** Factory trả đúng các kiểu đối tượng cụ thể tương ứng (`SmartLight`, `SmartFan`, `SmartDoor`). -> **PASS**
- **[T2] Tạo loại camera khi chưa hỗ trợ:** Phát sinh chính xác `ArgumentException` với thông điệp rõ ràng. -> **PASS**
- **[T3] Lấy HomeConfiguration.Instance hai lần:** Cả hai biến cùng tham chiếu đến một đối tượng (`ReferenceEquals` trả về `True`). -> **PASS**
- **[T4] Gọi GoToWork():** Đèn tắt, quạt tắt, cửa khóa an ninh. -> **PASS**
- **[T5] Gọi GoToSleep():** Quạt tắt, cửa khóa, đèn bật, hiển thị chính xác mức sáng cấu hình ban đêm (25%). -> **PASS**
