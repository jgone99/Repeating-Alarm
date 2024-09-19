using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Repeating_Alarm
{
    public partial class MainPage : ContentPage
    {
        public bool EditMode { get; set; } = false;
        public Microsoft.Maui.Dispatching.IDispatcherTimer Timer;
        public TimeSpan TimeSpan { get; set; } = TimeSpan.Zero;
        public int Hour { get; set; } = 1;
        public int Minute { get; set; } = 0;
        public int Second { get; set; } = 0;

        private readonly MainPageViewModel _viewModel;
        public MainPage(MainPageViewModel viewModel)
        {
            _viewModel = viewModel;
            InitializeComponent();
            BindingContext = _viewModel;
            HourEntry.Unfocus();
            MinuteEntry.Unfocus();
            SecondEntry.Unfocus();
            Timer = HourEntry.Dispatcher.CreateTimer();
            Timer.Interval = TimeSpan.FromSeconds(1);
            Timer.Tick += UpdateTimerDisplay;
        }

        private void UpdateTimerDisplay(object? sender, EventArgs e)
        {
            TimeSpan = TimeSpan.Subtract(TimeSpan.FromSeconds(1));
            Hour = TimeSpan.Hours;
            Minute = TimeSpan.Minutes;
            Second = TimeSpan.Seconds;
        }

        private async void OpenSettings(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
        
        private void EnableEdit(object sender, EventArgs e)
        {
            EditMode = !EditMode;

            HourEntry.Unfocus();
            MinuteEntry.Unfocus();
            SecondEntry.Unfocus();

            HourEntry.InputTransparent = !EditMode;
            MinuteEntry.InputTransparent = !EditMode;
            SecondEntry.InputTransparent = !EditMode;

            if (EditMode)
            {
                StartButton.Opacity = 0;
                StartButton.IsEnabled = false;
            }
            else
            {
                StartButton.IsEnabled = true;
                StartButton.Opacity = 100;
            }
        }

        private void StartTimer(object sender, EventArgs e)
        {
            StartButton.Opacity = 0;
            StartButton.IsEnabled = false;
            TimeSpan = new TimeSpan(Hour, Minute, Second);
            Timer.Start();
        }

        private void ValidateLength(object sender, EventArgs e)
        {
            Entry entry = ((Entry)sender);
            Debug.WriteLine(entry.Text);
            Debug.WriteLine(entry.Text.Length);
            entry.UpdateText(entry.Text.PadLeft(2, '0'));
        }

        private void ValidateIntegerOnly(object sender, TextChangedEventArgs e)
        {
            if (!TimeRegex().Match(e.NewTextValue).Success)
            {
                ((Entry)sender).UpdateText(e.OldTextValue);
            }
            //Debug.WriteLine("runs");
        }

        [GeneratedRegex("^[0-9]*$")]
        public static partial Regex TimeRegex();
    }

}
