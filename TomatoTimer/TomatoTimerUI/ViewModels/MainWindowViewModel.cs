using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Mvvm;

namespace TomatoTimerUI.ViewModels
{
    /// <summary>The Main Window ViewModel.</summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class MainWindowViewModel : BindableBase
    {
        private bool isStarted;
        private ICommand setBreakCommand;
        private ICommand setTomatoCommand;
        private ICommand startCommand;
        private ICommand stopCommand;

        public TimerViewModel Timer => new TimerViewModel();

        public bool IsStarted
        {
            get => this.isStarted;
            set => this.SetProperty(ref this.isStarted, value);
        }

        public ICommand StartCommand => this.startCommand ??= new ActionCommand(() =>
        {
            this.IsStarted = true;
            this.Timer.Start();
        });

        public ICommand StopCommand => this.stopCommand ??= new ActionCommand(() =>
        {
            this.IsStarted = false;
            this.Timer.Stop();
        });

        public ICommand SetTomatoCommand => this.setTomatoCommand ??= new ActionCommand(() => { });

        public ICommand SetBreakCommand => this.setBreakCommand ??= new ActionCommand(() => { });
    }
}