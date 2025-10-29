using System;
using System.IO;
using System.Windows;

namespace Konalyan
{
    public partial class MainWindow : Window
    {
        private string filePath = "users.txt";

        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists(filePath))
                File.Create(filePath).Close();

            LoginTab.Checked += ModeChanged;
            RegisterTab.Checked += ModeChanged;
            ModeChanged(null, null);

            ActionButton.Click += ActionButton_Click;
        }
        // метод чтобы при входе показывало одно,а при регистрации другое типа
        private void ModeChanged(object sender, RoutedEventArgs e)
        {
            if (RegisterTab.IsChecked == true)
            {
                ConfirmPasswordText.Visibility = Visibility.Visible;
                ConfirmPasswordBox.Visibility = Visibility.Visible;
                ActionButton.Content = "Зарегистрироваться";
            }
            else
            {
                ConfirmPasswordText.Visibility = Visibility.Collapsed;
                ConfirmPasswordBox.Visibility = Visibility.Collapsed;
                ActionButton.Content = "Войти";
            }
        }

        private void ActionButton_Click(object sender, RoutedEventArgs e)
        {
            // присваивание к логину логин с убиранием пробелов в конце, а паролю пароль 
            string login = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;
            // если пустое
            if (login == "" || password == "")
            {
                MessageBox.Show("Введите логин и пароль!");
                return;
            }

            if (RegisterTab.IsChecked == true) // если выбрана регистрация
            {
                //проверка на совпвдение паролей
                string confirm = ConfirmPasswordBox.Password;
                if (password != confirm)
                {
                    MessageBox.Show("Пароли не совпадают!");
                    return;
                }
                //цикл с гпт, но понимаю как работает(ну не люблю циклы)
                // а так метод проверяющий есть ли уже такой пользователь
                bool exists = false;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(';');
                    if (parts[0] == login)
                    {
                        exists = true;
                        break;
                    }
                }
                //что будет если есть такой пользователь
                if (exists)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует!");
                    return;
                }
                //добавление пользователя в файл
                File.AppendAllText(filePath, login + ";" + password + "\n");
                MessageBox.Show("Регистрация успешна!");
                UsernameBox.Clear();
                PasswordBox.Clear();
                ConfirmPasswordBox.Clear();
            }
            else // если выбран вход
            {
                bool found = false;
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    //проверка есть ли пользователь
                    string[] parts = line.Split(';');
                    if (parts[0] == login)
                    {
                        found = true;
                        //проверка пароля
                        if (parts[1] == password)
                        {
                            MessageBox.Show("Вход успешен!");
                            string user = UsernameBox.Text.Trim();
                            mainprogram mp = new mainprogram(user);
                            mp.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль!");
                        }
                        break;
                    }
                }
                //если пользователь не найден
                if (!found)
                {
                    MessageBox.Show("Пользователь не найден!");
                }

                UsernameBox.Clear();
                PasswordBox.Clear();
            }
        }
    }
}
