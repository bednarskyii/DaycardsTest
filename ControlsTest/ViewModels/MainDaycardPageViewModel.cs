using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ControlsTest.Database;
using ControlsTest.DaycardsModels;
using ControlsTest.DaycardsViewModels;
using ControlsTest.Enums;
using Xamarin.Forms;

namespace ControlsTest.ViewModels
{
    public class MainDaycardPageViewModel : INotifyPropertyChanged
    {
        private IDatabaseRepository database;
        private readonly DaycardType typeOfDaycard;
        private INavigation Navigation;
        private ObservableCollection<DayViewModel> dates;
        private DayViewModel selectedDate;

        public event PropertyChangedEventHandler PropertyChanged;
        public string DaycardName { get; set; }

        public Command GoBack { get; set; }
        public Command AddDayCard { get; set; }

        public MainDaycardPageViewModel(DaycardType type, INavigation navigation)
        {
            database = new DatabaseRepository();
            DaycardName = type.ToString() + " Day Cards";
            Navigation = navigation;
            typeOfDaycard = type;

            AddDayCard = new Command(() => OnAddDayCardClicked());
            GoBack = new Command(() => OnGoBackClicked());

            FillMonthDates();
        }

        public DayViewModel SelectedDate
        {
            get => selectedDate;
            set
            {
                selectedDate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDate)));
            }
        }

        public ObservableCollection<DayViewModel> Dates
        {
            get => dates;
            set
            {
                dates = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dates)));
            }
        }

        private void FillMonthDates()
        {
            var monthDates = new List<DayViewModel>();
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Month, 1); i++)
            {
                DayViewModel currentModel = new DayViewModel { Date = firstDay };

                if (firstDay.Date == DateTime.Now.Date)
                {
                    currentModel.BackgroundColor = Color.FromHex("#9cc254");
                }

                monthDates.Add(currentModel);
                firstDay = firstDay.AddDays(1);
            }
            Dates = new ObservableCollection<DayViewModel>(monthDates);
        }

        private async Task OnGoBackClicked()
        {
            await Navigation.PopModalAsync();
        }

        private async Task OnAddDayCardClicked()
        {
            await database.SaveDaycardAsync(typeOfDaycard, SelectedDate.Date);
        }
    }
}
