/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

using SmartHomeMini.Devices;
using SmartHomeMini.Configuration;

namespace SmartHomeMini.Facade;

/// <summary>
/// Lớp Facade đóng vai trò cung cấp giao diện cấp cao, đơn giản hóa các thao tác điều khiển
/// phức tạp cho hệ thống con (gồm nhiều thiết bị đèn, quạt, cửa và cấu hình).
/// Che giấu chi tiết tương tác từng thiết bị khỏi client.
/// </summary>
public sealed class SmartHomeFacade
{
    private readonly IDevice light;
    private readonly IDevice fan;
    private readonly ILockable door;

    /// <summary>
    /// Khởi tạo Facade với các thiết bị cần phối hợp.
    /// Sử dụng abstraction IDevice và ILockable để giảm thiểu phụ thuộc vào các lớp cụ thể.
    /// </summary>
    /// <param name="light">Thiết bị chiếu sáng</param>
    /// <param name="fan">Thiết bị quạt</param>
    /// <param name="door">Thiết bị cửa thông minh hỗ trợ giao diện khóa ILockable</param>
    public SmartHomeFacade(IDevice light, IDevice fan, ILockable door)
    {
        this.light = light ?? throw new ArgumentNullException(nameof(light));
        this.fan = fan ?? throw new ArgumentNullException(nameof(fan));
        this.door = door ?? throw new ArgumentNullException(nameof(door));
    }

    /// <summary>
    /// Thao tác cấp cao: Kích hoạt chế độ "Đi làm" (GoToWork).
    /// Chuỗi hành động:
    /// 1. Tắt đèn.
    /// 2. Tắt quạt.
    /// 3. Khóa cửa.
    /// </summary>
    public void GoToWork()
    {
        light.TurnOff();
        fan.TurnOff();
        door.Lock();
    }

    /// <summary>
    /// Thao tác cấp cao: Kích hoạt chế độ "Đi ngủ" (GoToSleep).
    /// Chuỗi hành động:
    /// 1. Tắt quạt.
    /// 2. Khóa cửa.
    /// 3. Bật đèn ngủ.
    /// 4. Đọc mức sáng ban đêm từ cấu hình chung HomeConfiguration và hiển thị ra màn hình.
    /// </summary>
    public void GoToSleep()
    {
        fan.TurnOff();
        door.Lock();
        light.TurnOn();

        int level = HomeConfiguration.Instance.NightLightLevel;
        Console.WriteLine($"Night light level: {level}%");
    }
}
