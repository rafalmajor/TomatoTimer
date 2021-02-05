using System.Windows;
using TomatoTimerUI.ViewModels;

namespace TomatoTimerUI.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            //if (this.DataContext is MainWindowViewModel mainWindowViewModel)
            //{
            //    mainWindowViewModel.TaskbarItemInfo = this.TaskbarItemInfo;
            //}
        }
    }
}