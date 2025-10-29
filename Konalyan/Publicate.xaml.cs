using System;
using System.IO;
using System.Windows;

namespace Konalyan
{
    public partial class Publicate : Window
    {
        private string filePath = "publications.txt";
        private string separator = "---PUB---";
        private string currentUser;

        public Publicate(string username)
        {
            InitializeComponent();
            currentUser = username;

            if (!File.Exists(filePath))
                File.Create(filePath).Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnPublish_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();
            string content = PublicationTextBox.Text.Trim();
            string hashtags = HashtagsTextBox.Text.Trim();

            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(content))
            {
                MessageBox.Show("Введите заголовок и текст публикации!");
                return;
            }

            string publication = $"Автор: {currentUser}{Environment.NewLine}" +
                                 $"Заголовок: {title}{Environment.NewLine}" +
                                 $"Текст: {content}{Environment.NewLine}" +
                                 $"Хэштэги: {hashtags}{separator}";

            File.AppendAllText(filePath, publication);
            MessageBox.Show("Публикация добавлена!");
            this.Close();
            TitleTextBox.Clear();
            PublicationTextBox.Clear();
            HashtagsTextBox.Clear();
        }
    }
}
