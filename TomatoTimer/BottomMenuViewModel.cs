using System.Diagnostics;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

namespace TomatoTimer
{
    public class BottomMenuViewModel : BindableBase
    {
        private ICommand startCommand;

        private ICommand stopCommand;

        private ICommand breakCommand;


        public ICommand StartCommand => this.startCommand ??= new DelegateCommand(() => {});

        public ICommand StopCommand => this.stopCommand ??= new DelegateCommand(() => {});

        public ICommand BreakCommand => this.breakCommand ??= new DelegateCommand(() => {});
    }
}