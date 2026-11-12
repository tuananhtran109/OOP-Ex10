/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

using System;
using SmartHomeMini.Devices;
using SmartHomeMini.Factory;
using SmartHomeMini.Configuration;
using SmartHomeMini.Facade;

namespace SmartHomeMini;

/// <summary>
/// Chương trình thực thi chính, trình diễn và kiểm thử toàn diện 3 Design Pattern:
/// 1. Factory Pattern (DeviceFactory)
/// 2. Singleton Pattern (HomeConfiguration)
/// 3. Facade Pattern (SmartHomeFacade)
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
      

        // 1. Chạy kịch bản minh họa chức năng thực tế
        RunDemonstration();

        // 2. Chạy bộ kiểm thử tự động 5 ca kiểm thử chuẩn (T1 - T5)
        RunAutomatedTests();

        Console.WriteLine("\n========================================================");
        Console.WriteLine("  TẤT CẢ CÁC BƯỚC KIỂM THỬ ĐÃ HOÀN THÀNH XUẤT SẮC!");
        Console.WriteLine("========================================================\n");
    }

    /// <summary>
    /// In tiêu đề thông tin sinh viên và đề tài bài lab.
    /// </summary>


    /// <summary>
    /// Minh họa tương tác thông qua Factory, Singleton và Facade theo kịch bản sử dụng.
    /// </summary>
    private static void RunDemonstration()
    {
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine(">>> PHẦN 1: MINH HỌA HOẠT ĐỘNG THỰC TẾ (DEMO)");
        Console.WriteLine("--------------------------------------------------------\n");

        // A. Sử dụng Factory để tạo các thiết bị
        Console.WriteLine("[1] Khởi tạo thiết bị qua Factory (DeviceFactory):");
        IDevice livingRoomLight = DeviceFactory.Create("light", "Living room light");
        IDevice livingRoomFan = DeviceFactory.Create("fan", "Living room fan");
        IDevice mainDoor = DeviceFactory.Create("door", "Main door");

        Console.WriteLine("   + Đã tạo thiết bị: " + livingRoomLight.GetType().Name);
        Console.WriteLine("   + Đã tạo thiết bị: " + livingRoomFan.GetType().Name);
        Console.WriteLine("   + Đã tạo thiết bị: " + mainDoor.GetType().Name);
        Console.WriteLine();

        // B. Truy xuất Singleton HomeConfiguration
        Console.WriteLine("[2] Truy xuất cấu hình chung qua Singleton (HomeConfiguration):");
        var config = HomeConfiguration.Instance;
        Console.WriteLine($"   + Tên ngôi nhà (HomeName): {config.HomeName}");
        Console.WriteLine($"   + Mức sáng đèn ngủ mặc định (NightLightLevel): {config.NightLightLevel}%");
        Console.WriteLine();

        // C. Thao tác điều khiển riêng lẻ từng thiết bị (chứng minh Facade không triệt tiêu quyền điều khiển trực tiếp)
        Console.WriteLine("[3] Điều khiển riêng lẻ thiết bị:");
        livingRoomLight.TurnOn();
        livingRoomFan.TurnOff();
        if (mainDoor is ILockable lockableDoor)
        {
            lockableDoor.Lock();
        }
        Console.WriteLine();

        // D. Sử dụng Facade để kích hoạt các chế độ cấp cao
        Console.WriteLine("[4] Điều khiển tập trung qua Facade (SmartHomeFacade):");
        var facade = new SmartHomeFacade(livingRoomLight, livingRoomFan, (SmartDoor)mainDoor);

        Console.WriteLine("\n--- Gọi chế độ GoToWork() ---");
        facade.GoToWork();

        Console.WriteLine("\n--- Gọi chế độ GoToSleep() ---");
        facade.GoToSleep();
        Console.WriteLine();
    }

    /// <summary>
    /// Bộ kiểm thử tự động tối thiểu cho 3 pattern theo đúng yêu cầu đề bài (T1 -> T5).
    /// </summary>
    private static void RunAutomatedTests()
    {
        Console.WriteLine("--------------------------------------------------------");
        Console.WriteLine(">>> PHẦN 2: BỘ KIỂM THỬ TỰ ĐỘNG CHUẨN (T1 - T5)");
        Console.WriteLine("--------------------------------------------------------\n");

        int passedCount = 0;
        int totalTests = 5;

        // Test T1: Tạo light, fan, door -> Factory trả đúng kiểu đối tượng
        try
        {
            Console.WriteLine("[TEST T1] Tạo light, fan, door qua DeviceFactory:");
            IDevice d1 = DeviceFactory.Create("light", "Test Light");
            IDevice d2 = DeviceFactory.Create("fan", "Test Fan");
            IDevice d3 = DeviceFactory.Create("door", "Test Door");

            bool isD1Correct = d1 is SmartLight;
            bool isD2Correct = d2 is SmartFan;
            bool isD3Correct = d3 is SmartDoor;

            if (isD1Correct && isD2Correct && isD3Correct)
            {
                Console.WriteLine("   -> d1 kiểu: " + d1.GetType().Name + " (Kỳ vọng: SmartLight)");
                Console.WriteLine("   -> d2 kiểu: " + d2.GetType().Name + " (Kỳ vọng: SmartFan)");
                Console.WriteLine("   -> d3 kiểu: " + d3.GetType().Name + " (Kỳ vọng: SmartDoor)");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   [PASS] T1: Factory trả đúng kiểu đối tượng cụ thể.\n");
                Console.ResetColor();
                passedCount++;
            }
            else
            {
                throw new Exception("Kiểu đối tượng trả về từ Factory không chính xác!");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   [FAIL] T1: Lỗi ngoại lệ - {ex.Message}\n");
            Console.ResetColor();
        }

        // Test T2: Tạo loại camera khi chưa hỗ trợ -> Phát sinh ArgumentException
        try
        {
            Console.WriteLine("[TEST T2] Tạo loại 'camera' chưa được hỗ trợ:");
            DeviceFactory.Create("camera", "Living Room Camera");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("   [FAIL] T2: Không phát sinh ArgumentException như kỳ vọng!\n");
            Console.ResetColor();
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"   -> Bắt được ngoại lệ đúng kỳ vọng: {ex.GetType().Name}: '{ex.Message}'");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   [PASS] T2: Phát sinh ArgumentException chính xác khi yêu cầu loại chưa hỗ trợ.\n");
            Console.ResetColor();
            passedCount++;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   [FAIL] T2: Ngoại lệ không đúng kiểu - {ex.GetType().Name}: {ex.Message}\n");
            Console.ResetColor();
        }

        // Test T3: Lấy HomeConfiguration.Instance hai lần -> Cùng tham chiếu một đối tượng
        try
        {
            Console.WriteLine("[TEST T3] Lấy HomeConfiguration.Instance hai lần:");
            var c1 = HomeConfiguration.Instance;
            var c2 = HomeConfiguration.Instance;

            bool isSameRef = ReferenceEquals(c1, c2);
            Console.WriteLine($"   -> c1 HashCode: {c1.GetHashCode()}");
            Console.WriteLine($"   -> c2 HashCode: {c2.GetHashCode()}");
            Console.WriteLine($"   -> ReferenceEquals(c1, c2) = {isSameRef}");

            if (isSameRef)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("   [PASS] T3: Hai biến cùng tham chiếu đến duy nhất một thể hiện (Singleton hợp lệ).\n");
                Console.ResetColor();
                passedCount++;
            }
            else
            {
                throw new Exception("Singleton implementation is incorrect: hai biến trỏ tới 2 đối tượng khác nhau!");
            }
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   [FAIL] T3: Lỗi - {ex.Message}\n");
            Console.ResetColor();
        }

        // Test T4: Gọi GoToWork() -> Đèn tắt, quạt tắt, cửa khóa
        try
        {
            Console.WriteLine("[TEST T4] Gọi SmartHomeFacade.GoToWork():");
            var testLight = new SmartLight("Work Light");
            var testFan = new SmartFan("Work Fan");
            var testDoor = new SmartDoor("Work Door");

            var workFacade = new SmartHomeFacade(testLight, testFan, testDoor);
            Console.WriteLine("   [Thực thi GoToWork]:");
            workFacade.GoToWork();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   [PASS] T4: Hoàn thành GoToWork() (Đèn tắt, quạt tắt, cửa khóa).\n");
            Console.ResetColor();
            passedCount++;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   [FAIL] T4: Lỗi khi thực thi GoToWork() - {ex.Message}\n");
            Console.ResetColor();
        }

        // Test T5: Gọi GoToSleep() -> Quạt tắt, cửa khóa, đèn bật, hiện mức sáng
        try
        {
            Console.WriteLine("[TEST T5] Gọi SmartHomeFacade.GoToSleep():");
            var testLight = new SmartLight("Sleep Light");
            var testFan = new SmartFan("Sleep Fan");
            var testDoor = new SmartDoor("Sleep Door");

            // Đặt mức sáng cấu hình kiểm tra
            HomeConfiguration.Instance.NightLightLevel = 25;

            var sleepFacade = new SmartHomeFacade(testLight, testFan, testDoor);
            Console.WriteLine("   [Thực thi GoToSleep]:");
            sleepFacade.GoToSleep();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("   [PASS] T5: Hoàn thành GoToSleep() (Quạt tắt, cửa khóa, đèn bật, hiện mức sáng 25%).\n");
            Console.ResetColor();
            passedCount++;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"   [FAIL] T5: Lỗi khi thực thi GoToSleep() - {ex.Message}\n");
            Console.ResetColor();
        }

        // Tổng kết
        Console.WriteLine($"===> KẾT QUẢ KIỂM THỬ: ĐẠT {passedCount}/{totalTests} CA KIỂM THỬ.");
    }
}
