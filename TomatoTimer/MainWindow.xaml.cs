using System;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Shell;

namespace TomatoTimer
{
    /// <summary>Interaction logic for MainWindow.xaml</summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            if (this.TimerView.DataContext is TimerViewModel timerViewModel)
            {
                timerViewModel.TaskbarItemInfo = this.TaskbarItemInfo;
            }
        }

    }
}