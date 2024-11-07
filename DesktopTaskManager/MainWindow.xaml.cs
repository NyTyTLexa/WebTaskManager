using DesktopTaskManager.Model;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebTaskManager.Model;

namespace DesktopTaskManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();    
        }

        //Возможность перемещения окна при зажатие заголовка
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

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void LoginEnter_Click(object sender, RoutedEventArgs e)
        {
            var password = ((PasswordBox)Password.Template.FindName("PART_PasswordBox", Password)).Password;
            if (Login.Text != string.Empty && password != string.Empty)
            {
                var apiClient = new ApiClient("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiMTIzMTIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZWlkZW50aWZlZXIiOiIxMjNlNDU2Ny1lODliLTEyZDMtYTQ1Ni00MjY2NTU0NDAwMDAiLCJleHAiOjE3MzA5MDY3NDN9.ero6vopzxEbCz0XWeRIQ0eNOKL6q_CeKRMDApZeTAyc", "LANVER2024@");
                var result = await apiClient.AuthorizationUser(Login.Text, password);
                if (result != null && result.Error != string.Empty) {
                    var messageWindow = new MessageWindow("Ошибка авторизации",
                        $"{result!.Error}", MessageBoxButton.OK, MessageBoxImage.Error);
                    messageWindow.ShowDialog(); 
                }
                else
                {
                    var taskWindow = new TaskWindow(result);
                    taskWindow.Show();
                    this.Close();
                }
            }
            else
            {
                var messageWindow = new MessageWindow("Ошибка авторизации",
                        "Необходимо ввести учетные данные пользователя для входа", MessageBoxButton.OK, MessageBoxImage.Error);
                messageWindow.ShowDialog();
            }
        }

        private void RegistrationText_Click(object sender, RoutedEventArgs e)
        {
            //var taskWindow = new TaskWindow();
            //taskWindow.Show();
            //this.Close();
        }
    }
}