/****************/
// MSSV: 202418834
// Họ tên: Trần Tuấn Anh
/****************/

namespace SmartHomeMini.Configuration;

/// <summary>
/// Lớp cấu hình chung của ngôi nhà thông minh theo mẫu Singleton.
/// Đảm bảo chỉ có một thể hiện duy nhất trong suốt vòng đời ứng dụng.
/// Đạt chuẩn thread-safety (an toàn đa luồng) và lazy-loading nhờ sử dụng Lazy&lt;T&gt;.
/// Đánh dấu 'sealed' nhằm ngăn chặn việc kế thừa phá vỡ quy tắc singleton.
/// </summary>
public sealed class HomeConfiguration
{
    /// <summary>
    /// Đối tượng Lazy giữ thể hiện duy nhất, chỉ khởi tạo khi được truy cập lần đầu tiên.
    /// Mặc định LazyThreadSafetyMode.ExecutionAndPublication đảm bảo an toàn đa luồng.
    /// </summary>
    private static readonly Lazy<HomeConfiguration> lazy =
        new(() => new HomeConfiguration());

    /// <summary>
    /// Điểm truy cập toàn cục duy nhất để lấy thể hiện cấu hình.
    /// </summary>
    public static HomeConfiguration Instance => lazy.Value;

    /// <summary>
    /// Tên của ngôi nhà thông minh.
    /// </summary>
    public string HomeName { get; set; } = "My Home";

    /// <summary>
    /// Mức độ chiếu sáng ban đêm được áp dụng cho đèn ngủ (phần trăm: 0 - 100).
    /// </summary>
    public int NightLightLevel { get; set; } = 30;

    /// <summary>
    /// Constructor có phạm vi truy cập private, ngăn chặn các lớp bên ngoài gọi toán tử 'new'.
    /// </summary>
    private HomeConfiguration()
    {
    }
}
