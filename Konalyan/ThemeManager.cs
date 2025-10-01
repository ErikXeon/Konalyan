using System.Windows.Media;

namespace Konalyan
{
    public static class ThemeManager
    {
        public static SolidColorBrush MainColor { get; set; } = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#6A0DAD"));
        public static SolidColorBrush BackgroundColor { get; set; } = Brushes.White;
    }
}
