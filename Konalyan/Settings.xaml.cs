using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Konalyan
{
    public partial class Settings : Window
    {
        private string currentUser;
        public Settings(string username)
        {
            InitializeComponent();
            currentUser = username;
        }
        //главное меню

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            mainprogram mp = new mainprogram(currentUser);
            mp.Show();
            this.Close();
        }
        //в мои публикации
        private void BtnMyArticles_Click(object sender, RoutedEventArgs e)
        {
            MyPublications mp = new MyPublications(currentUser);
            mp.Show();
            this.Close();
        }
        //выйти в окно входа
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }
        //метод смены основного цвета
        private void ChangeMainColor_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Background is SolidColorBrush brush)
            {
                Application.Current.Resources["MainColor"] = brush;
            }
        }

        private void ChangeBackgroundColor_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Background is SolidColorBrush brush)
            {
                Application.Current.Resources["BackgroundColor"] = brush;
            }
        }

    }
}
