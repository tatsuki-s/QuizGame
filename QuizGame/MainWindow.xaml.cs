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
        private int randomNum;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ResetGame()
        {

        }
        private void StartGame()
        {

        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            Random random = new Random();

            if (StartButton.Content.ToString() == "スタート")
            {
                StartButton.Content = "送信";
                MethodButton.Content = "終了";
                InputBox.Visibility = Visibility.Visible;
                ModeStatus.Visibility = Visibility.Collapsed;
                count = 0;
                randomNum = random.Next(1, 100);

                //デバッグ用
                MessageBox.Show(randomNum.ToString());

            }
            else if(StartButton.Content.ToString() == "送信")
            {
                string input = InputBox.Text;
                if(int.TryParse(input, out int inputNum))
                {
                    MessageBox.Show(inputNum.ToString());

                    if (inputNum == randomNum)
                    {
                        //MessageBox.Show("正解！");
                        Check.Text = "正解";
                        //StartButton.Content = "リスタート";
                    }
                    else if (inputNum >= randomNum)
                    {
                        Check.Text = "まだ大きい";
                    }
                    else
                    {
                        Check.Text = "まだ小さい";
                    }
                    History.Text += " " + inputNum;
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
                if (Check.Text == "正解")
                {
                    StartButton.Content = "スタート";
                }
                StartButton.Content = "スタート";
                MethodButton.Content = "操作方法";
                History.Text = "";
                InputBox.Visibility = Visibility.Collapsed;
                ModeStatus.Visibility = Visibility.Visible;
                Check.Text = "";
            }
        }
    }
}