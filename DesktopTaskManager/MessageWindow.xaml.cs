using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopTaskManager
{
    /// <summary>
    /// Логика взаимодействия для MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : Window
    {
        public MessageWindow(string title, string description, MessageBoxButton messageBoxButton, MessageBoxImage messageBoxImage)
        {
            InitializeComponent();
            YesOrOkButton.Visibility = Visibility.Hidden;
            NotButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            TitleMessage.Text = title;
            Description.Text = description;
            SetButtons(messageBoxButton);
            SetStatusMessage(messageBoxImage);
        }

        private void WindowHeader_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = null;
            this.Close();
        }

        //Отображение соответствующих кнопок у сообщения
        private void SetButtons(MessageBoxButton buttons)
        {
            switch (buttons)
            {
                case MessageBoxButton.OK:
                    YesOrOkButton.Visibility = Visibility.Visible;
                    YesOrOkButton.Content = "OK";
                    Grid.SetColumn(YesOrOkButton, 2);
                    break;

                case MessageBoxButton.OKCancel:
                    YesOrOkButton.Visibility = Visibility.Visible;
                    CancelButton.Visibility = Visibility.Visible;
                    Grid.SetColumn(YesOrOkButton, 1);
                    Grid.SetColumn(CancelButton, 2);
                    break;

                case MessageBoxButton.YesNo:
                    YesOrOkButton.Visibility = Visibility.Visible;
                    NotButton.Visibility = Visibility.Visible;
                    Grid.SetColumn(YesOrOkButton, 1);
                    Grid.SetColumn(NotButton, 2);
                    break;

                case MessageBoxButton.YesNoCancel:
                    YesOrOkButton.Visibility = Visibility.Visible;
                    NotButton.Visibility = Visibility.Visible;
                    CancelButton.Visibility = Visibility.Visible;
                    Grid.SetColumn(YesOrOkButton, 0);
                    Grid.SetColumn(NotButton, 1);
                    Grid.SetColumn(CancelButton, 2);
                    break;
            }
        }

        private void SetStatusMessage(MessageBoxImage image)
        {
            switch (image)
            {
                case MessageBoxImage.Information:
                    ColorWindow.Background = Brushes.MediumSlateBlue; break;
                    case MessageBoxImage.Warning:
                    ColorWindow.Background = Brushes.Gold; break;
                case MessageBoxImage.Error:
                    ColorWindow.Background = Brushes.OrangeRed; break;
                    case MessageBoxImage.Question:
                    ColorWindow.Background = Brushes.DodgerBlue; break;
            }
        }

        private void YesOrOkButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void NotButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
