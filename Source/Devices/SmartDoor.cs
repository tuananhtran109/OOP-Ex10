/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

namespace SmartHomeMini.Devices;

/// <summary>
/// Lớp đại diện cho Cửa thông minh (SmartDoor).
/// Triển khai cả hai giao diện IDevice (điều khiển đóng/mở nguồn) và ILockable (chốt khóa an ninh).
/// </summary>
public class SmartDoor : IDevice, ILockable
{
    /// <summary>
    /// Tên riêng của thiết bị cửa (ví dụ: "Main door", "Back door").
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Khởi tạo cửa thông minh với tên được cung cấp.
    /// </summary>
    /// <param name="name">Tên riêng của cửa</param>
    public SmartDoor(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Bật/mở cửa thông minh và hiển thị trạng thái ra console.
    /// </summary>
    public void TurnOn()
    {
        Console.WriteLine($"{Name}: OPEN");
    }

    /// <summary>
    /// Tắt/đóng cửa thông minh và hiển thị trạng thái ra console.
    /// </summary>
    public void TurnOff()
    {
        Console.WriteLine($"{Name}: CLOSED");
    }

    /// <summary>
    /// Khóa chốt an ninh của cửa và hiển thị trạng thái ra console.
    /// </summary>
    public void Lock()
    {
        Console.WriteLine($"{Name}: LOCKED");
    }

    /// <summary>
    /// Mở chốt an ninh của cửa và hiển thị trạng thái ra console.
    /// </summary>
    public void Unlock()
    {
        Console.WriteLine($"{Name}: UNLOCKED");
    }
}
