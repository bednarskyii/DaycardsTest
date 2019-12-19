using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ControlsTest.Database;
using ControlsTest.Models;
using Xamarin.Forms;

namespace ControlsTest.ViewModels
{
    public class DaycardsViewModel : INotifyPropertyChanged
    {
        private IDatabaseRepository database;
        private INavigation Navigation;
        private List<DayModel> monthDates = new List<DayModel>();
        private ObservableCollection<DayModel> dates;
        private ObservableCollection<DayCardsModel> cardsList;
        private DayModel selectesDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SelectOperator { get; set; }
        public Command GoBack { get; set; }
        public Command CreateNewDaycard { get; set; }
        public Command AddDayCard { get; set; }

        public DayModel SelectedDate
        {
            get => selectesDate;
            set
            {
                UpdateChangedDaycards();
                selectesDate = value;
                FillDaycardsList();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedDate)));
            }
        }

        public ObservableCollection<DayCardsModel> CardsList
        {
            get => cardsList;
            set
            {
                cardsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CardsList)));
            }
        }

        public ObservableCollection<DayModel> Dates
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
            database = new DatabaseRepository();
            Navigation = navigation;

            FillDaycardsList();
            FillMonthDates();
            Dates = new ObservableCollection<DayModel>(monthDates);

            AddDayCard = new Command(() => OnAddDayCardClicked());
            GoBack = new Command(() => OnGoBackClicked());
            CreateNewDaycard = new Command(() => OnCreateNewDaycardClicked());
            //SelectOperator = new Command(() => OnSelectOperatorClicked());
        }

        private async Task FillDaycardsList()
        {
            List<DayCardsModel> cards = await database.GetDayCardsAsync(SelectedDate.Date);
            CardsList = new ObservableCollection<DayCardsModel>(cards);
        }

        private async Task OnGoBackClicked()
        {
            await Navigation.PopModalAsync();
        }

        private void OnCreateNewDaycardClicked()
        {
            var t = CardsList;
        }

        private void FillMonthDates()
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Month, 1); i++)
            {
                if(firstDay.Date == DateTime.Now.Date)
                    monthDates.Add(new DayModel { Date = firstDay, BackgroundColor = Color.FromHex("#9cc254") });
                else
                    monthDates.Add(new DayModel { Date = firstDay});
                firstDay = firstDay.AddDays(1);
            }
        }

        private void OnSelectOperatorClicked(object sender, EventArgs args)
        {
            
        }

        private async Task OnAddDayCardClicked()
        {
            int random = new Random().Next(1000, 9999);
            await database.SaveDayCardAsync(new DayCardsModel { Date = SelectedDate.Date, DayCardNumber = random.ToString() });
            await FillDaycardsList();
        }

        private async Task UpdateChangedDaycards()
        {
            List<DayCardsModel> updatedDaycards = new List<DayCardsModel>(CardsList);
            await database.DeleteDayCardByDateAsync(SelectedDate.Date);

            foreach (var item in updatedDaycards)
            {
                await database.SaveDayCardAsync(item);
            }
        }
    }
}
