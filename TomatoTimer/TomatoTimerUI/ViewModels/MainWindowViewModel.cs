using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Mvvm;

namespace TomatoTimerUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string currentTime = "00:00";
        private bool isStarted;
        private ICommand setBreakCommand;
        private ICommand setTomatoCommand;
        private ICommand startCommand;
        private ICommand stopCommand;

        public string CurrentTime
        {
            get => this.currentTime;
            set => this.SetProperty(ref this.currentTime, value);
        }

        public bool IsStarted
        {
            get => this.isStarted;
            set => this.SetProperty(ref this.isStarted, value);
        }

        public ICommand StartCommand => this.startCommand ??= new ActionCommand(() => { this.IsStarted = true; });

        public ICommand StopCommand => this.stopCommand ??= new ActionCommand(() => { this.IsStarted = false; });

        public ICommand SetTomatoCommand => this.setTomatoCommand ??= new ActionCommand(() => { });

        public ICommand SetBreakCommand => this.setBreakCommand ??= new ActionCommand(() => { });
    }
}