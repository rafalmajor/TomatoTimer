using System.Windows;
using Prism.Ioc;
using TomatoTimerUI.Views;

namespace TomatoTimerUI
{
    /// <summary>Interaction logic for App.xaml</summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return this.Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}