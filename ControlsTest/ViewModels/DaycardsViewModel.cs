using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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
        private DayModel selectedDate;

        public event PropertyChangedEventHandler PropertyChanged;

        public Command SelectOperator { get; set; }
        public Command GoBack { get; set; }
        public Command AddDayCard { get; set; }
        public Command ChangeOperator { get; set; }

        public DayModel SelectedDate
        {
            get => selectedDate;
            set
            {
                UpdateChangedDaycards();
                selectedDate = value;
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

            FillMonthDates();
            FillDaycardsList();

            AddDayCard = new Command(() => OnAddDayCardClicked());
            GoBack = new Command(() => OnGoBackClicked());
            ChangeOperator = new Command(() => OnChangeOperatorClicked());
            //SelectOperator = new Command(() => OnSelectOperatorClicked());
        }

        private async Task FillDaycardsList()
        {
            List<DayCardsModel> cards = await database.GetDayCardsAsync(SelectedDate.Date);

            foreach (var item in cards)
            {
                item.DayUrl = SelectedDate;
            }

            CardsList = new ObservableCollection<DayCardsModel>(cards);
        }

        private async Task OnGoBackClicked()
        {
            await Navigation.PopModalAsync();
        }

        private async Task FillMonthDates()
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            for (int i = 0; i < DateTime.DaysInMonth(DateTime.Now.Month, 1); i++)
            {
                DayModel currentModel = new DayModel{ Date = firstDay};
                //TODO - not a best practice  
                List<DayCardsModel> cards = await database.GetDayCardsAsync(firstDay.Date);

                currentModel.CountOfValidated = cards.Where(item => item.IsValid == true).Count();
                currentModel.CountOfNotValidated = cards.Where(item => item.IsValid == false).Count();

                if (firstDay.Date == DateTime.Now.Date)
                {
                    currentModel.BackgroundColor = Color.FromHex("#9cc254");
                    monthDates.Add(currentModel);
                }
                else
                    monthDates.Add(currentModel);

                firstDay = firstDay.AddDays(1);
            }
            Dates = new ObservableCollection<DayModel>(monthDates);
        }

        private void OnSelectOperatorClicked(object sender, EventArgs args)
        {
            
        }

        private async Task OnAddDayCardClicked()
        {
            int random = new Random().Next(1000, 9999);
            await database.SaveDayCardAsync(new DayCardsModel { Date = SelectedDate.Date, DayCardNumber = random.ToString(), Operator = "defaultOperator"});
            await FillDaycardsList();
            SelectedDate.CountOfNotValidated += 1;
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

        private void OnChangeOperatorClicked()
        {

        }
    }
}
