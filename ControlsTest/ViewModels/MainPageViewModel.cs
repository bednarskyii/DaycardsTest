using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using ControlsTest.Pages;
using Xamarin.Forms;

namespace ControlsTest.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private INavigation Navigation;
        private bool isSyncPopUpVissible;
        private bool isChangesEnabled = true;
        private double progress;
        private Color mainColor = Color.Blue;

        public Command Sync { get; set; }
        public Command DayCardsPage { get; set; }

        public Color MainColor
        {
            get => mainColor;
            set
            {
                mainColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MainColor)));
            }
        }

        public double Progress
        {
            get => progress;
            set
            {
                progress = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Progress)));
            }
        }

        public bool IsSyncPopUpVissible
        {
            get => isSyncPopUpVissible;
            set
            {
                isSyncPopUpVissible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSyncPopUpVissible)));
            }
        }

        public bool IsChangesEnabled
        {
            get => isChangesEnabled;
            set
            {
                isChangesEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsChangesEnabled)));
            }
        }

        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Sync = new Command(() => OnSyncClicked());
            DayCardsPage = new Command(() => OnDayCardsPageClicked());
        }

        private void OnSyncClicked()
        {
            IsSyncPopUpVissible = true;

            for (double i = 0.1; i <= 1; i += 0.1)
            {
                Thread.Sleep(500);
                Progress += i;
            }

            IsSyncPopUpVissible = false;
            IsChangesEnabled = false;
            MainColor = Color.Black;
        }

        private async Task OnDayCardsPageClicked()
        {
            try
            {
                await Navigation.PushModalAsync(new DaycardsPage());
            }
            catch(Exception e)
            {
                var d = e.Message;
            }
        }

    }
}
