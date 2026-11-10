/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

namespace SmartHomeMini.Devices;

/// <summary>
/// Giao diện trừu tượng đại diện cho mọi thiết bị thông minh cơ bản trong ngôi nhà.
/// Cung cấp các thao tác cơ bản: bật nguồn (TurnOn) và tắt nguồn (TurnOff).
/// </summary>
public interface IDevice
{
    /// <summary>
    /// Bật thiết bị thông minh.
    /// </summary>
    void TurnOn();

    /// <summary>
    /// Tắt thiết bị thông minh.
    /// </summary>
    void TurnOff();
}
