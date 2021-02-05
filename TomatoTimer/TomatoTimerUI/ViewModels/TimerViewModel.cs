﻿using System;
using System.Timers;
using NodaTime;
using Prism.Mvvm;

namespace TomatoTimerUI.ViewModels
{
    public class TimerViewModel : BindableBase
    {
        private const int UpdateMilliseconds = 200;
        private readonly Timer timer = new Timer(UpdateMilliseconds);
        private bool isOn;
        private Period period = Period.Zero;
        private Period startPeriod = Period.Zero;

        public TimerViewModel()
        {
            this.timer.Elapsed += this.TimerOnElapsed;
        }

        public string Current => $"{this.Period.Minutes:D2}:{this.Period.Seconds:D2}";

        public double Progress
        {
            get
            {
                if (this.Period == Period.Zero)
                {
                    return this.startPeriod == Period.Zero ? 0 : 1;
                }
                
                double startAllSeconds = this.startPeriod.Minutes * 60 + this.startPeriod.Seconds;
                double periodAllSeconds = this.period.Minutes * 60 + this.period.Seconds;
                return Convert.ToDouble(startAllSeconds - periodAllSeconds) / startAllSeconds;
            }
        }

        private Period Period
        {
            get => this.period;
            set
            {
                this.SetProperty(ref this.period, value);
                this.RaisePropertyChanged(nameof(this.Current));
                this.RaisePropertyChanged(nameof(this.Progress));
            }
        }

        public bool IsOn
        {
            get => this.isOn;
            private set => this.SetProperty(ref this.isOn, value);
        }

        public event EventHandler End;

        public void Start()
        {
            this.IsOn = true;
            this.timer.Start();
        }

        public void Stop()
        {
            this.IsOn = false;
            this.timer.Stop();
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            if (this.Period.Minutes > 0 || this.Period.Seconds > 0)
            {
                this.Period = (this.Period - Period.FromMilliseconds(UpdateMilliseconds)).Normalize();
            }
            else
            {
                this.timer.Stop();
                this.Period = Period.Zero;
                this.End?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SetTime(int minute)
        {
            this.SetTimeInternal(minute);
        }

        private void SetTimeInternal(int minute)
        {
            this.startPeriod = Period.FromMinutes(minute);
            this.Period = this.startPeriod;
        }
    }
}