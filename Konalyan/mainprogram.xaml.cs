using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Konalyan
{
    public partial class mainprogram : Window
    {
        private string publicationsFile = "publications.txt";
        private string separator = "---PUB---";
        private string currentUser;

        public mainprogram(string username)
        {
            InitializeComponent();
            currentUser = username;

            if (Application.Current.MainWindow != null)
            {
                this.Left = Application.Current.MainWindow.Left;
                this.Top = Application.Current.MainWindow.Top;
            }
            LoadPublications();
        }

        private void LoadPublications()
        {
            PublicationsPanel.Children.Clear();

            if (!File.Exists(publicationsFile))
                File.Create(publicationsFile).Close();

            string content = File.ReadAllText(publicationsFile);
            string[] posts = content
                .Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                .Where(p => !string.IsNullOrWhiteSpace(p.Trim())).ToArray();

            foreach (string post in posts)
            {
                var lines = post.Trim().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                string author = lines.Length > 0 ? lines[0].Replace("Автор: ", "") : "";
                string title = lines.Length > 1 ? lines[1].Replace("Заголовок: ", "") : "";
                string text = lines.Length > 2 ? lines[2].Replace("Текст: ", "") : "";
                string hashtags = lines.Length > 3 ? lines[3].Replace("Хэштэги: ", "") : "";

                StackPanel postStack = new StackPanel();

                TextBlock authorBlock = new TextBlock
                {
                    Text = $"Автор: {author}",
                    FontSize = 14,
                    Foreground = Brushes.Gray
                };

                TextBlock titleBlock = new TextBlock
                {
                    Text = title,
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    Foreground = (Brush)Application.Current.Resources["MainColor"],
                    TextWrapping = TextWrapping.Wrap
                };

                TextBlock textBlock = new TextBlock
                {
                    Text = text,
                    FontSize = 16,
                    Margin = new Thickness(0, 5, 0, 5),
                    TextWrapping = TextWrapping.Wrap
                };

                TextBlock hashtagsBlock = new TextBlock
                {
                    Text = hashtags,
                    FontSize = 14,
                    Foreground = Brushes.Gray,
                    TextWrapping = TextWrapping.Wrap
                };

                postStack.Children.Add(authorBlock);
                postStack.Children.Add(titleBlock);
                postStack.Children.Add(textBlock);
                postStack.Children.Add(hashtagsBlock);

                Border postBorder = new Border
                {
                    Background = Brushes.White,
                    BorderBrush = (Brush)Application.Current.Resources["MainColor"],
                    BorderThickness = new Thickness(2),
                    CornerRadius = new CornerRadius(10),
                    Margin = new Thickness(0, 0, 0, 10),
                    Padding = new Thickness(10),
                    Child = postStack
                };

                PublicationsPanel.Children.Add(postBorder);
            }
        }

        private void BtnMyArticles_Click(object sender, RoutedEventArgs e)
        {
            MyPublications mp = new MyPublications(currentUser);
            mp.Left = this.Left;
            mp.Top = this.Top;
            mp.Show();
            this.Close();
        }

        private void BtnSettings_Click(object sender, RoutedEventArgs e)
        {
            Settings st = new Settings(currentUser);
            st.Left = this.Left;
            st.Top = this.Top;
            st.Show();
            this.Close();
        }

        private void BtnPublicate_Click(object sender, RoutedEventArgs e)
        {
            Publicate pub = new Publicate(currentUser);
            pub.Left = this.Left;
            pub.Top = this.Top;
            pub.ShowDialog();
            LoadPublications();
           
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Left = this.Left;
            mw.Top = this.Top;
            mw.Show();
            this.Close();
        }
    }
}
