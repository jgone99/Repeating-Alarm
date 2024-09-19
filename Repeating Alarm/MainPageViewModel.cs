using CommunityToolkit.Mvvm.ComponentModel;

namespace Repeating_Alarm
{
    public partial class MainPageViewModel : ObservableObject
    {
        [ObservableProperty]
        public int _hour = 0;
        [ObservableProperty]
        public int _minute = 0;
        [ObservableProperty]
        public int _second = 0;

        public MainPageViewModel(int hour, int minute, int second)
        {
            Hour = hour;
            Minute = minute;
            Second = second;
        }
    }
}
