/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

namespace SmartHomeMini.Devices;

/// <summary>
/// Lớp đại diện cho Quạt thông minh (SmartFan).
/// Triển khai giao diện IDevice.
/// </summary>
public class SmartFan : IDevice
{
    /// <summary>
    /// Tên riêng của thiết bị quạt (ví dụ: "Living room fan", "Ceiling fan").
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Khởi tạo quạt thông minh với tên được cung cấp.
    /// </summary>
    /// <param name="name">Tên riêng của quạt</param>
    public SmartFan(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Bật quạt và hiển thị trạng thái ra console.
    /// </summary>
    public void TurnOn()
    {
        Console.WriteLine($"{Name}: ON");
    }

    /// <summary>
    /// Tắt quạt và hiển thị trạng thái ra console.
    /// </summary>
    public void TurnOff()
    {
        Console.WriteLine($"{Name}: OFF");
    }
}
