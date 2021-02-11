using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shell;
using Microsoft.Xaml.Behaviors.Core;
using Prism.Events;
using Prism.Mvvm;
using TomatoTimerUI.Events;

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

        private bool isBreakOnGoing;

        private bool isTomatoOnGoing;

        private ICommand setBreakCommand;

        private ICommand setTomatoCommand;

        private ICommand startCommand;

        private ICommand stopCommand;

        private TaskbarItemProgressState taskbarItemProgressState;

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            this.TaskbarItemProgressState = TaskbarItemProgressState.Normal;
            this.Timer = new TimerViewModel(eventAggregator);

            this.eventAggregator.GetEvent<TimeUpEvent>().Subscribe(this.TimeUp);
        }

        private void TimeUp()
        {
            this.AddTomatoIfDone();

            this.IsTomatoOnGoing = false;
            this.IsBreakOnGoing = false;
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

                this.Timer.SetTime(0);
            });
        }

        private void AddTomatoIfDone()
        {
            if (!this.IsTomatoOnGoing)
            {
                return;
            }

            this.Tomatos.Add(new TomatoViewModel());
        }

        public TimerViewModel Timer { get; private set;}

        private readonly IEventAggregator eventAggregator;

        public TaskbarItemProgressState TaskbarItemProgressState
        {
            get => this.taskbarItemProgressState;
            set => this.SetProperty(ref this.taskbarItemProgressState, value);
        }

        public bool IsTomatoOnGoing
        {
            get => this.isTomatoOnGoing;
            set => this.SetProperty(ref this.isTomatoOnGoing, value);
        }

        public bool IsBreakOnGoing
        {
            get => this.isBreakOnGoing;
            set => this.SetProperty(ref this.isBreakOnGoing, value);
        }

        public ObservableCollection<TomatoViewModel> Tomatos = new ObservableCollection<TomatoViewModel>();

        public ICommand StartCommand => this.startCommand ??= new ActionCommand(this.Timer.Start);

        public ICommand StopCommand => this.stopCommand ??= new ActionCommand(this.Timer.Stop);

        public ICommand SetTomatoCommand => this.setTomatoCommand ??= new ActionCommand(() =>
        {
            this.currentSoundPlayer = this.soundPlayerAlarm;
            this.IsTomatoOnGoing = true;
            this.IsBreakOnGoing = false;
            this.Timer.SetTime(Tomato);
            this.Timer.Start();
        });

        public ICommand SetBreakCommand => this.setBreakCommand ??= new ActionCommand(() =>
        {
            this.currentSoundPlayer = this.soundPlayerBravo;
            this.IsBreakOnGoing = true;
            this.IsTomatoOnGoing = false;
            this.Timer.SetTime(Break);
            this.Timer.Start();
        });
    }
}