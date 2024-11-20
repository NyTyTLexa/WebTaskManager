using DesktopTaskManager.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WebTaskManager.Model;

namespace DesktopTaskManager
{
    /// <summary>
    /// Логика взаимодействия для TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        private User _userApi;
        private ApiClient _apiClient = new ApiClient("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
            "eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW" +
            "1lIjoiMTIzMTIzIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0" +
            "eS9jbGFpbXMvbmFtZWlkZW50aWZlZXIiOiIxMjNlNDU2Ny1lODliLTEyZDMtYTQ1Ni00MjY2NTU0ND" +
            "AwMDAiLCJleHAiOjE3MzA5MDY3NDN9.ero6vopzxEbCz0XWeRIQ0eNOKL6q_CeKRMDApZeTAyc", "LANVER2024@");
        List<WebTaskManager.Model.Task> Tasks = new List<WebTaskManager.Model.Task>();
        List<Priority> Priority = new List<Priority>();
        List<Status> Statuses = new List<Status>();
        private List<StatusTask> _statusTasks = new List<StatusTask>();
        private ObservableCollection<Status> _selectedStatuses = new ObservableCollection<Status>();
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
            Statuses = await _apiClient.GetStatusAsync();
            Priority = await _apiClient.GetPrioritiesAsync();
            Tasks = await _apiClient.GetTaskAsync(_userApi, Statuses, Priority);

            StatusFilter.ItemsSource = Statuses.Select(name=>name.Name).ToList();


            _statusTasks = Statuses.Select(s => new StatusTask
            {
                Status = s,
                Tasks = Tasks.Where(t => t.StatusId == s.Id).ToList()
            }).ToList();

            UpdateLists();

            TaskBord.ItemsSource = Tasks.ToList();
            StatusesControl.ItemsSource = _statusTasks;
        }

        private void StatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Код для второго стиля ComboBox
            var comboBox = sender as ComboBox;
            var listBox = comboBox!.Template.FindName("ListCheckBox", comboBox) as ListBox;

            foreach (var item in comboBox.Items)
            {
                var checkBoxItem = item as CheckBox;

                if (checkBoxItem != null)
                {
                    var checkBox = checkBoxItem.Name;
                    if (checkBox != null)
                    {
                        // Выбранные значения
                        MessageBox.Show(checkBox.ToString());
                        // Делайте с value что угодно
                    }
                }
            }

            UpdateLists();
        }


        private void UpdateLists()
        {
            int selectedStatusId;
            var filteredTasks = Tasks;
            var filteredStatusTasks = _statusTasks;
            switch (StatusFilter.SelectedIndex)
            {
                case 0:
                    {
                        selectedStatusId = Statuses.Find(x => x.Name == StatusFilter.SelectedItem.ToString())!.Id;
                        filteredTasks = Tasks.Where(x => x.StatusId == selectedStatusId).ToList();
                        filteredStatusTasks = _statusTasks.Where(st => st.Tasks.Any(t => t.StatusId == selectedStatusId)).ToList();
                        break;
                    }
                case 1:
                    {
                        selectedStatusId = Statuses.Find(x => x.Name == StatusFilter.SelectedItem.ToString())!.Id;
                        filteredTasks = Tasks.Where(x => x.StatusId == selectedStatusId).ToList();
                        filteredStatusTasks = _statusTasks.Where(st => st.Tasks.Any(t => t.StatusId == selectedStatusId)).ToList();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            if (StartData.SelectedDate.HasValue && EndData.SelectedDate.HasValue)
            {
                filteredTasks = filteredTasks.Where(x => Convert.ToDateTime(x.DateStart) >= 
                StartData.SelectedDate && Convert.ToDateTime(x.DateEnd) <= EndData.SelectedDate).ToList();
                filteredStatusTasks = filteredStatusTasks.Where(st => st.Tasks.Any(t => Convert.ToDateTime(t.DateStart) >= 
                StartData.SelectedDate && Convert.ToDateTime(t.DateEnd) <= EndData.SelectedDate)).ToList();
            }
            else if (StartData.SelectedDate.HasValue)
            {
                filteredTasks = filteredTasks.Where(x => Convert.ToDateTime(x.DateStart) >= StartData.SelectedDate).ToList();
                filteredStatusTasks = filteredStatusTasks.Where(st => st.Tasks.Any(t => Convert.ToDateTime(t.DateStart) >= StartData.SelectedDate)).ToList();
            }
            else if (EndData.SelectedDate.HasValue)
            {
                filteredTasks = filteredTasks.Where(x => Convert.ToDateTime(x.DateEnd) <= EndData.SelectedDate).ToList();
                filteredStatusTasks = filteredStatusTasks.Where(st => st.Tasks.Any(t => Convert.ToDateTime(t.DateEnd) <= EndData.SelectedDate)).ToList();
            }

            TaskBord.ItemsSource = filteredTasks.ToList();
            StatusesControl.ItemsSource = filteredStatusTasks.ToList();

            if (TaskBord.Items.Count == 0|| StatusesControl.Items.Count == 0)
            {
                var messageWindow = new MessageWindow("Выборка данных"
                    ,"По результату выборки - данные отсутствуют", MessageBoxButton.OK, MessageBoxImage.Information);
                messageWindow.ShowDialog();
            }
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child!))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private T GetVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T)
                {
                    return (T)child;
                }
                else
                {
                    T result = GetVisualChild<T>(child);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }
            return null!;
        }

        private void StartData_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLists();
        }

        private void EndData_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLists();
        }
    }
}
