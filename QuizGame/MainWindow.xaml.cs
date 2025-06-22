using System;
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

namespace QuizGame
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

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;

            if (StartButton.Content.ToString() == "スタート")
            {
                StartButton.Content = "送信";
                MethodButton.Content = "終了";
                InputBox.Visibility = Visibility.Visible;
                ModeStatus.Visibility = Visibility.Collapsed;
                count = 0;
            }
            else if(StartButton.Content.ToString() == "送信")
            {
                string input = InputBox.Text;
                if (int.TryParse(input, out int number))
                {
                    MessageBox.Show(number.ToString());
                    History.Text += " " + number;
                    InputBox.Clear();
                    count++;
                }
                else
                {
                    MessageBox.Show("数字を入力してください");
                    InputBox.Clear();
                }
            }
        }

        private void MethodButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartButton.Content.ToString() == "送信")
            {
                StartButton.Content = "スタート";
                MethodButton.Content = "操作方法";
                History.Text = "";
                InputBox.Visibility = Visibility.Collapsed;
                ModeStatus.Visibility = Visibility.Visible;
            }
        }
    }
}