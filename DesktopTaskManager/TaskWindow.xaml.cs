using WebTaskManager.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using DesktopTaskManager.Model;
using System.Windows.Controls;

namespace DesktopTaskManager
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private User _userApi;
        private ApiClient _apiClient = new ApiClient("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTIzMTIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZlZXIiOiIxMjNlNDU2Ny1lODliLTEyZDMtYTQ1Ni00MjY2NTU0NDAwMDAiLCJleHAiOjE3MzA5MDY3NDN9.ero6vopzxEbCz0XWeRIQ0eNOKL6q_CeKRMDApZeTAyc", "LANVER2024@");
        List<WebTaskManager.Model.Task> tasks = new List<WebTaskManager.Model.Task>();
        List<Status> statuses;
        public TaskWindow(User user)
        {
            InitializeComponent();
            _userApi = user;
            GetDataApi();
            LoginUser.Text = "Логин пользователя: " + user.Login;
            ScrollTaskColumnEllipse.Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0x53, 0x54, 0xC8));
            ScrollTaskRowEllipse.Fill = Brushes.Gray;
        }

        private void WindowHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void RoolUp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        public int countClickUnWrap = 0;
        private void UnWrap_Click(object sender, RoutedEventArgs e)
        {
            if (countClickUnWrap == 0)
            {
                UnWrap.Content = "◱";
                this.WindowState = WindowState.Maximized;
                countClickUnWrap++;
            }
            else
            {
                UnWrap.Content = "▢";
                this.WindowState = WindowState.Normal;
                countClickUnWrap = 0;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = new MessageWindow("Подтверждение закрытия",
                "Вы уверены, что хотите закрыть программу?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            messageBoxResult.ShowDialog();

            if (messageBoxResult.DialogResult.Equals(true))
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }

        //void ClearStatusButton_Click(object sender, RoutedEventArgs e)
        //{
        //    var messageBoxResult = new MessageWindow("Подтверждение закрытия",
        //        "Вы уверены, что хотите закрыть программу?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        //    messageBoxResult.ShowDialog();

        //    if (messageBoxResult.DialogResult.Equals(true))
        //    {
        //        var mainWindow = new MainWindow();
        //        mainWindow.Show();
        //        this.Close();
        //    }
        //}

        private void ScrollTaskRowVisible_Click(object sender, RoutedEventArgs e)
        {
            ScrollTaskColumn.Visibility = Visibility.Hidden;
            ScrollTaskRow.Visibility = Visibility.Visible;
            ScrollTaskColumnEllipse.Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0x53, 0x54, 0xC8));
            ScrollTaskRowEllipse.Fill = Brushes.Gray;
        }

        private void ScrollTaskColumnVisible_Click(object sender, RoutedEventArgs e)
        {
            ScrollTaskRow.Visibility = Visibility.Hidden;
            ScrollTaskColumn.Visibility = Visibility.Visible;
            ScrollTaskColumnEllipse.Fill = Brushes.Gray;
            ScrollTaskRowEllipse.Fill = new SolidColorBrush(Color.FromArgb(0xFF, 0x53, 0x54, 0xC8));
        }

        private async void GetDataApi()
        {
            var statuses = await _apiClient.GetStatusAsync();
            var priorities = await _apiClient.GetPrioritiesAsync();
            var tasks = await _apiClient.GetTaskAsync(_userApi);

            Status.ItemsSource = statuses.ToList();
        }
    }
}
