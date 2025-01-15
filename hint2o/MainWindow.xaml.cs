using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hint2o
{
    public partial class MainWindow : Window
    {
        private Timer timer = new Timer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void startTimerClick(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(IntervalTextBox.Text, out int minuteInterval) || minuteInterval <= 0)
            {
                MessageBox.Show("please input a value greater than zero :(", "error");
                return;
            }

            timer.Stop();
            timer = new Timer(minuteInterval * 60 * 100); //alterar pra 1000 de volta dps
            timer.Elapsed += TimerElapsed;
            timer.Start();

            MessageBox.Show($"reminding you in " + minuteInterval + (minuteInterval > 1 ? " minutes!" : " minute!"), "success");
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var notifyIcon = new System.Windows.Forms.NotifyIcon
                {
                    Icon = System.Drawing.SystemIcons.Information,
                    Visible = true,
                    BalloonTipTitle = "hey there",
                    BalloonTipText = "time to drink water!"
                };
                notifyIcon.ShowBalloonTip(3000);
            });
        }
    }
}
