using System;
using System.Windows.Input;

using Prism.Commands;
using Prism.Mvvm;

namespace TomatoTimer
{
    public class TimerViewModel : BindableBase
    {
        private const int TomatoTime = 25;

        private const int BreakTime = 5;

        private readonly TimerModel timer = new TimerModel();

        private ICommand breakCommand;

        private bool breakPeriod;

        private int interruption;

        private ICommand interruptionCommand;

        private int lostFocus;

        private ICommand lostFocusCommand;

        private ICommand startCommand;

        private bool startStop;

        private ICommand stopCommand;

        private int tomato;

        private ICommand resetCommand;

        private Storage storage;

        public TimerViewModel()
        {
            this.timer.Update += this.TimerOnUpdate;
            this.timer.End += this.TimerOnEnd;

            this.storage = new Storage();

            var workDone = this.storage.GetWorkDone(DateTime.Today);
            this.Tomato = workDone.Tomato;
            this.Interruption = workDone.Interruption;
            this.LostFocus = workDone.LostFocus;
        }

        public string CurrentTime => this.timer.CurrentTime;

        public string StartStopLabel => this.startStop ? "Continue" : "Stop";

        public int Tomato
        {
            get => this.tomato;
            set => this.SetProperty(ref this.tomato, value);
        }

        public int Interruption
        {
            get => this.interruption;
            set => this.SetProperty(ref this.interruption, value);
        }

        public int LostFocus
        {
            get => this.lostFocus;
            set => this.SetProperty(ref this.lostFocus, value);
        }

        public ICommand StartCommand => this.startCommand ??= new DelegateCommand(
            () =>
            {
                this.breakPeriod = false;
                this.timer.Start(TomatoTime);
                this.startStop = false;
                this.RaisePropertyChanged(nameof(this.StartStopLabel));
            });

        public ICommand StopCommand => this.stopCommand ??= new DelegateCommand(
            () =>
            {
                if (this.startStop)
                {
                    this.startStop = false;
                    this.timer.Continue();
                }
                else
                {
                    this.startStop = true;
                    this.timer.Stop();
                }

                this.RaisePropertyChanged(nameof(this.StartStopLabel));
            });

        public ICommand BreakCommand => this.breakCommand ??= new DelegateCommand(
            () =>
            {
                this.breakPeriod = true;
                this.timer.Start(BreakTime);
                this.startStop = false;
                this.RaisePropertyChanged(nameof(this.StartStopLabel));
            });

        public ICommand InterruptionCommand => this.interruptionCommand ??= new DelegateCommand(() =>
        {
            this.Interruption++;
            this.SaveWorkDone();
        });

        public ICommand LostFocusCommand => this.lostFocusCommand ??= new DelegateCommand(() =>
        {
            this.LostFocus++;
            this.SaveWorkDone();
        });

        public ICommand ResetCommand => this.resetCommand ??= new DelegateCommand(() => {});

        private void TimerOnEnd(object sender, EventArgs e)
        {
            if (!this.breakPeriod)
            {
                this.Tomato++;
                this.SaveWorkDone();
            }
        }

        public void SaveWorkDone()
        {
            var workDone = new WorkDone()
                           {
                               Day = DateTime.Today,
                               Tomato = this.Tomato,
                               Interruption = this.Interruption,
                               LostFocus = this.LostFocus
                           };
            this.storage.Store(workDone);
        }

        private void TimerOnUpdate(object sender, EventArgs e)
        {
            this.RaisePropertyChanged(nameof(this.CurrentTime));
        }
    }
}