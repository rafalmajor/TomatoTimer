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
        private BottomMenuViewModel bottomMenuViewModel;

        private ClockViewModel clockViewModel;

        
        public MainWindow()
        {
            this.InitializeComponent();
            this.bottomMenuViewModel = this.BottomMenuView.DataContext as BottomMenuViewModel;
            this.clockViewModel = this.ClockView.DataContext as ClockViewModel;


            //if (this.TimerView.DataContext is TimerViewModel timerViewModel)
            //{
            //    timerViewModel.TaskbarItemInfo = this.TaskbarItemInfo;
            //}
        }

    }
}