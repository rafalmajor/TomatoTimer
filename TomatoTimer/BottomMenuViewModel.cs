using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

namespace TomatoTimer
{
    public class BottomMenuViewModel : BindableBase
    {
        private ICommand startCommand;

        private ICommand stopCommand;

        private ICommand setBreakCommand;

        private ICommand setTomatoCommand;

        private bool isStarted;

        public ICommand StartCommand => this.startCommand ??= new DelegateCommand(this.Start);

        public ICommand StopCommand => this.stopCommand ??= new DelegateCommand(this.Stop);

        public ICommand SetBreakCommand => this.setBreakCommand ??= new DelegateCommand(this.SetBreak);

        public ICommand SetTomatoCommand => this.setTomatoCommand ??= new DelegateCommand(this.SetTomato);

        public bool IsStarted
        {
            get => this.isStarted;
            set => this.SetProperty(ref this.isStarted, value);
        }

        private void Start()
        {
            this.IsStarted = true;
        }

        private void Stop()
        {
            this.IsStarted = false;
        }

        private void SetTomato()
        {

        }

        private void SetBreak()
        {

        }
    }
}