using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ControlsTest.Models;
using Xamarin.Forms;

namespace ControlsTest.ViewModels
{
    public class DaycardsViewModel : INotifyPropertyChanged
    {
        private INavigation Navigation;
        private List<DateTime> monthDates = new List<DateTime>();
        private ObservableCollection<DateTime> dates;
        private ObservableCollection<DayCardsModel> cardsList;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SelectOperator { get; set; }
        public Command GoBack { get; set; }
        public Command CreateNewDaycard { get; set; }

        public ObservableCollection<DayCardsModel> CardsList
        {
            get => cardsList;
            set
            {
                cardsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CardsList)));
            }
        }

        public ObservableCollection<DateTime> Dates
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
            Navigation = navigation;

            List<DayCardsModel> cards = new List<DayCardsModel>();
            cards.Add(new DayCardsModel { DayCardId = "0301", Hours = "12", Miles = "132", Operator = "Serg" });
            cards.Add(new DayCardsModel { DayCardId = "0131", Hours = "16", Miles = "12", Operator = "Vasyl" });
            cards.Add(new DayCardsModel { DayCardId = "0000", Miles = "12", Operator = "Vasyl" });

            CardsList = new ObservableCollection<DayCardsModel>(cards);

            FillMonthDates();
            Dates = new ObservableCollection<DateTime>(monthDates);

            GoBack = new Command(() => OnGoBackClicked());
            CreateNewDaycard = new Command(() => OnCreateNewDaycardClicked());
            //SelectOperator = new Command(() => OnSelectOperatorClicked());
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
                monthDates.Add(firstDay);
                firstDay = firstDay.AddDays(1);
            }
        }

        private void OnSelectOperatorClicked(object sender, EventArgs args)
        {
            
        }
    }
}
