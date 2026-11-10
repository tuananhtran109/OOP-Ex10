/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

using SmartHomeMini.Devices;

namespace SmartHomeMini.Factory;

/// <summary>
/// Lớp Factory (Simple Factory) đóng gói quy trình tạo lập các đối tượng thiết bị.
/// Giúp tách rời (decouple) mã nguồn phía client khỏi việc gọi trực tiếp constructor của các lớp cụ thể.
/// </summary>
public static class DeviceFactory
{
    /// <summary>
    /// Tạo lập một đối tượng thiết bị thông minh trả về dưới dạng abstraction IDevice.
    /// Không phân biệt chữ hoa, chữ thường và tự động loại bỏ khoảng trắng thừa đầu cuối.
    /// </summary>
    /// <param name="type">Loại thiết bị yêu cầu tạo: "light", "fan", "door"</param>
    /// <param name="name">Tên định danh đặt cho thiết bị</param>
    /// <returns>Đối tượng cụ thể triển khai IDevice</returns>
    /// <exception cref="ArgumentException">Ném ra khi chuỗi loại thiết bị không hợp lệ hoặc không được hỗ trợ</exception>
    public static IDevice Create(string type, string name)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            throw new ArgumentException("Device type cannot be null or whitespace.", nameof(type));
        }

        return type.Trim().ToLowerInvariant() switch
        {
            "light" => new SmartLight(name),
            "fan" => new SmartFan(name),
            "door" => new SmartDoor(name),
            _ => throw new ArgumentException($"Unsupported device type: {type}")
        };
    }
}
