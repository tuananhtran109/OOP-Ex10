/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

namespace SmartHomeMini.Devices;

/// <summary>
/// Giao diện dành riêng cho các thiết bị có tính năng khóa an ninh (như cửa thông minh).
/// Tuân thủ nguyên lý Phân tách Giao diện (Interface Segregation Principle - ISP),
/// tách biệt thao tác khóa an ninh khỏi IDevice để đèn và quạt không bị ép buộc triển khai.
/// </summary>
public interface ILockable
{
    /// <summary>
    /// Khóa cơ chế an ninh của thiết bị.
    /// </summary>
    void Lock();

    /// <summary>
    /// Mở khóa cơ chế an ninh của thiết bị.
    /// </summary>
    void Unlock();
}
