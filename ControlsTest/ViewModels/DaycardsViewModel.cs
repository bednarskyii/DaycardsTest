using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlsTest.ViewModels
{
    public class DaycardsViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation;
        private ObservableCollection<string> dates;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command GoBack { get; set; }

        public ObservableCollection<string> Dates
        {
            get => dates;
            set
            {
                dates = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dates)));
            }
        }

        public DaycardsViewModel(INavigation navigation)
        {
            List<string> dats = new List<string>();
            dats.Add("12.14");
            dats.Add("12.15");
            dats.Add("12.16");
            dats.Add("12.17");
            dats.Add("12.18");
            dats.Add("12.19");
            dats.Add("12.20");
            dats.Add("12.21");
            Navigation = navigation;

            Dates = new ObservableCollection<string>(dats);

            GoBack = new Command(() => OnGoBackClicked());
        }

        private async Task OnGoBackClicked()
        {
            await Navigation.PopModalAsync();
        }
    }
}
