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
using System.Windows.Threading;
using System.Collections.Generic;

namespace QuizGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int randomNum;
        private DispatcherTimer timer;
        private int timeLeft = 100;
        private int count;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            InputBox.Focus();
        }
        private void StartTimer(int limit)
        {
            timeLeft = limit;
            Title.Text = timeLeft.ToString();
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            timeLeft--;
            Title.Text = timeLeft.ToString();
            if (timeLeft <= 0)
            {
                GameOver();
            }
        }
        private void StartGame()
        {
            count = 0;
            Random random = new Random();

            if (StartButton.Content.ToString() == "スタート")
            {
                StartButton.Content = "送信";
                MethodButton.Content = "終了";
                InputBox.Visibility = Visibility.Visible;
                InputBox.IsEnabled = true;
                ModeStatus.Visibility = Visibility.Collapsed;
                count = 0;
                InputBox.Text = "";
                randomNum = random.Next(1, 100);
                InputBox.Focus();
                ComboBoxItem selectedItem = ModeStatus.SelectedItem as ComboBoxItem;
                string selected = selectedItem?.Content.ToString();

                if (selected == "Easy")
                {
                    StartTimer(60);
                }
                else if (selected == "Nomal")
                {
                    StartTimer(30);
                }
                else if (selected == "Hard")
                {
                    StartTimer(10);
                }
                //デバッグ用
                //MessageBox.Show(randomNum.ToString());

            }
            else if (StartButton.Content.ToString() == "送信")
            {
                CheckValue();
            }
        }
        private void CheckValue()
        {
            string input = InputBox.Text;
            if (int.TryParse(input, out int inputNum))
            {
                //デバッグ用
                //MessageBox.Show(inputNum.ToString());

                if (inputNum == randomNum)
                {
                    Check.Text = "正解";
                    StopGame();
                    StartButton.IsEnabled = false;
                    InputBox.IsEnabled = false;
                    Title.Text = "Clear!";
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
                if(count >= 10 && inputNum != randomNum)
                {
                    GameOver();
                }
                InputBox.Focus();
            }
            else
            {
                MessageBox.Show("1から100までの数字を入力してください");
                InputBox.Clear();
            }
        }
        private void StopGame()
        {
            timer.Stop();
            Check.Text = "";
        }
        private void GameOver()
        {

            timer.Stop();
            Title.Text = "GAME OVER ^^";
            Check.Text = "正解：" + randomNum.ToString();
            StartButton.IsEnabled = false;
            InputBox.IsEnabled = false;   
        }

        private void ShutDownGame()
        {
            Application.Current.Shutdown();
        }
        private void GoStart()
        {
            InputBox.Visibility = Visibility.Collapsed;
            ModeStatus.Visibility = Visibility.Visible;
            StartButton.Content = "スタート";
            MethodButton.Content = "操作方法";
            History.Text = "";
            Check.Text = "";
            Title.Text = "The GAME";
            StartButton.IsEnabled = true;
            timer.Stop();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void MethodButton_Click(object sender, RoutedEventArgs e)
        {
            if (StartButton.Content.ToString() == "送信")
            {
                GoStart();
            }
            else if (MethodButton.Content.ToString() == "操作方法")
            {

                Window1 window1 = new Window1();
                window1.Show();
            }
        }
        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            ShutDownGame();
        }
        //参照
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {   
            if (Check.Text != "正解")
            {
                Check.Text = "";
            }
            if (e.Key == Key.Escape)
            {
                ShutDownGame();
            }
            if (e.Key == Key.Enter && StartButton.Content.ToString() == "送信")
            {
                if (StartButton.IsEnabled)
                {
                    CheckValue();
                }
                else
                {
                    GoStart();
                }
            }
            if (e.Key == Key.F1)
            {
                Window1 window1 = new Window1();
                window1.Show();
            }
            if (e.Key == Key.Space)
            {
                if (StartButton.IsEnabled && StartButton.Content.ToString() == "スタート")
                {
                    StartGame();
                }
            }
            if (e.Key == Key.Z)
            {
                if (StartButton.IsEnabled && StartButton.Content.ToString() == "送信")
                {
                    GoStart();
                }
            }
        }

    }
}