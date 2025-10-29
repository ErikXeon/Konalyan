using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Konalyan
{
    public partial class MyPublications : Window
    {
        private string publicationsFile = "publications.txt";
        private string separator = "---PUB---";
        private string currentUser;

        public MyPublications(string username)
        {
            InitializeComponent();
            currentUser = username;
            LoadPublications();
        }

        private void LoadPublications(string filter = "")
        {
            MyPublicationsPanel.Children.Clear();

            if (!File.Exists(publicationsFile))
                File.Create(publicationsFile).Close();

            string content = File.ReadAllText(publicationsFile);
            string[] posts = content.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < posts.Length; i++)
            {
                string post = posts[i].Trim();
                var lines = post.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                string author = lines.Length > 0 ? lines[0].Replace("Автор: ", "") : "";
                string title = lines.Length > 1 ? lines[1].Replace("Заголовок: ", "") : "";
                string text = lines.Length > 2 ? lines[2].Replace("Текст: ", "") : "";
                string hashtags = lines.Length > 3 ? lines[3].Replace("Хэштэги: ", "") : "";

                if (author != currentUser)
                    continue;

                if (!string.IsNullOrEmpty(filter) && hashtags.IndexOf(filter, StringComparison.OrdinalIgnoreCase) < 0)
                    continue;

                StackPanel postStack = new StackPanel();

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

                // Кнопка удаления
                Button deleteBtn = new Button
                {
                    Content = "Удалить",
                    Width = 70,
                    Height = 25,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 5, 0, 0)
                };

                deleteBtn.Click += (s, e) =>
                {
                    var result = MessageBox.Show("Вы уверены, что хотите удалить эту публикацию?",
                                                 "Подтверждение удаления",
                                                 MessageBoxButton.YesNo,
                                                 MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        var allPosts = File.ReadAllText(publicationsFile)
                                           .Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                                           .ToList();

                        // Удаляем именно текущий пост, а не по индексу
                        allPosts.RemoveAll(p => p.Trim() == post);

                        File.WriteAllText(publicationsFile,
                            string.Join(separator + Environment.NewLine, allPosts) +
                            Environment.NewLine + separator);

                        LoadPublications(filter);
                    }
                };





                postStack.Children.Add(titleBlock);
                postStack.Children.Add(textBlock);
                postStack.Children.Add(hashtagsBlock);
                postStack.Children.Add(deleteBtn);

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

                MyPublicationsPanel.Children.Add(postBorder);
            }
        }


        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoadPublications(SearchBox.Text.Trim());
        }

        private void BtnHome_Click(object sender, RoutedEventArgs e)
        {
            mainprogram mp = new mainprogram(currentUser);
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
