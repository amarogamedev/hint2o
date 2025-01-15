using System;
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Forms;

namespace hint2o
{
    public partial class MainWindow : Window
    {
        private System.Timers.Timer timer = new System.Timers.Timer();
        private NotifyIcon notifyIcon;

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += Window_Closing;

            notifyIcon = new NotifyIcon
            {
                Icon = System.Drawing.SystemIcons.Information,
                Visible = true
            };

            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Abrir", null, (s, e) => ShowWindow());
            contextMenu.Items.Add("Sair", null, (s, e) => ExitApplication());
            notifyIcon.ContextMenuStrip = contextMenu;

            notifyIcon.DoubleClick += (s, e) => ShowWindow();
        }

        private void startTimerClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IntervalTextBox.Text, out int minuteInterval) || minuteInterval <= 0)
            {
                System.Windows.MessageBox.Show("please input a value greater than zero :(", "error");
                return;
            }

            timer.Stop();
            timer = new System.Timers.Timer(minuteInterval * 60 * 100); //alterar pra 1000 de volta dps
            timer.Elapsed += TimerElapsed;
            timer.Start();

            System.Windows.MessageBox.Show($"reminding you in " + minuteInterval + (minuteInterval > 1 ? " minutes!" : " minute!"), "success");
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                notifyIcon.BalloonTipTitle = "hey there";
                notifyIcon.BalloonTipText = "time to drink water!";
                notifyIcon.ShowBalloonTip(3000);
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            notifyIcon.ShowBalloonTip(3000, "hint2o", "the program will still be running on the tray", ToolTipIcon.Info);
        }

        private void ShowWindow()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
        }

        private void ExitApplication()
        {
            notifyIcon.Visible = false;
            System.Windows.Application.Current.Shutdown();
        }
    }
}
