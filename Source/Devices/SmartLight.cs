/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

namespace SmartHomeMini.Devices;

/// <summary>
/// Lớp đại diện cho Đèn thông minh (SmartLight).
/// Triển khai giao diện IDevice.
/// </summary>
public class SmartLight : IDevice
{
    /// <summary>
    /// Tên riêng của thiết bị đèn (ví dụ: "Living room light", "Bed room light").
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Khởi tạo đèn thông minh với tên được cung cấp.
    /// </summary>
    /// <param name="name">Tên riêng của đèn</param>
    public SmartLight(string name)
    {
        Name = name;
    }

    /// <summary>
    /// Bật đèn và hiển thị trạng thái ra console.
    /// </summary>
    public void TurnOn()
    {
        Console.WriteLine($"{Name}: ON");
    }

    /// <summary>
    /// Tắt đèn và hiển thị trạng thái ra console.
    /// </summary>
    public void TurnOff()
    {
        Console.WriteLine($"{Name}: OFF");
    }
}
