using Prism.Mvvm;

namespace TomatoTimer
{
    public class ClockViewModel : BindableBase
    {
        private string currentTime = "00:00";

        public string CurrentTime
        {
            get => this.currentTime;
            set => this.SetProperty(ref this.currentTime, value);
        }
    }
}