using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shell;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Mvvm;

namespace TomatoTimerUI.ViewModels
{
    /// <summary>The Main Window ViewModel.</summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class MainWindowViewModel : BindableBase
    {
        private const int Tomato = 2;
        private const int Break = 1;
        private readonly SoundPlayer soundPlayerAlarm = new SoundPlayer(@"Resources/Various-04.wav");

        private readonly SoundPlayer soundPlayerBravo = new SoundPlayer(@"Resources/Various-01.wav");

        private SoundPlayer currentSoundPlayer;

        private ICommand setBreakCommand;

        private ICommand setTomatoCommand;

        private ICommand startCommand;

        private ICommand stopCommand;

        private TaskbarItemProgressState taskbarItemProgressState;

        public MainWindowViewModel()
        {
            this.TaskbarItemProgressState = TaskbarItemProgressState.Normal;

            this.Timer.End += (o, a) =>
            {
                this.currentSoundPlayer.Play();
                Task.Run(() =>
                {
                    foreach (int current in Enumerable.Range(0, 10))
                    {
                        this.TaskbarItemProgressState = TaskbarItemProgressState.Error;
                        Thread.Sleep(200);
                        this.TaskbarItemProgressState = TaskbarItemProgressState.Normal;
                        Thread.Sleep(200);
                    }
                });
            };
        }

        public TimerViewModel Timer { get; } = new TimerViewModel();

        public TaskbarItemProgressState TaskbarItemProgressState
        {
            get => this.taskbarItemProgressState;
            set => this.SetProperty(ref this.taskbarItemProgressState, value);
        }

        public ICommand StartCommand => this.startCommand ??= new ActionCommand(this.Timer.Start);

        public ICommand StopCommand => this.stopCommand ??= new ActionCommand(this.Timer.Stop);

        public ICommand SetTomatoCommand => this.setTomatoCommand ??= new ActionCommand(() =>
        {
            this.currentSoundPlayer = this.soundPlayerAlarm;
            this.Timer.SetTime(Tomato);
            this.Timer.Start();
        });

        public ICommand SetBreakCommand => this.setBreakCommand ??= new ActionCommand(() =>
        {
            this.currentSoundPlayer = this.soundPlayerBravo;
            this.Timer.SetTime(Break);
            this.Timer.Start();
        });
    }
}