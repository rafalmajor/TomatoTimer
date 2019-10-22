using System.ComponentModel;
using System.Windows;

namespace TomatoTimer
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
        }

        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {
            if (this.TimerView.DataContext is TimerViewModel timerViewModel)
            {
                timerViewModel.SaveWorkDone();
            }
        }
    }
}