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
        private ObservableCollection<BaseDaycardViewModel> daycardsList;

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

            FillDaycardsList();
        }

        public ObservableCollection<BaseDaycardViewModel> DaycardsList
        {
            get => daycardsList;
            set
            {
                daycardsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DaycardsList)));
            }
        }

        public DayViewModel SelectedDate
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

        public ObservableCollection<DayViewModel> Dates
        {
            get => dates;
            set
            {
                dates = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Dates)));
            }
        }

        private async Task FillMonthDates()
        {
            var monthDates = new List<DayViewModel>();
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            for (int i = 0; i < 20; i++)
            {
                DayViewModel currentModel = new DayViewModel { Date = firstDay };

                if (firstDay.Date == DateTime.Now.Date)
                {
                    currentModel.BackgroundColor = Color.FromHex("#9cc254");
                    SelectedDate = currentModel;
                }

                monthDates.Add(await SetCountOfValidAndNot(currentModel));
                firstDay = firstDay.AddDays(1);
            }
            Dates = new ObservableCollection<DayViewModel>(monthDates);
        }

        private async Task FillDaycardsList()
        {
            List<BaseDaycardViewModel> baseDaycardViewModels = new List<BaseDaycardViewModel>();
            var daycardsModels = await database.GetDaycardsByDateAndTypeAsync(typeOfDaycard, SelectedDate.Date);

            foreach (var item in daycardsModels)
            {
                switch (typeOfDaycard)
                {
                    case DaycardType.Accomplishment:
                        baseDaycardViewModels.Add(new AccomplishmentDaycardViewModel((AccomplishmentDaycardModel)item, SelectedDate));
                        break;
                    case DaycardType.Cost:
                        baseDaycardViewModels.Add(new CostDaycardViewModel((CostDaycardModel)item, SelectedDate));
                        break;
                    case DaycardType.Equipment:
                        baseDaycardViewModels.Add(new EquipmentDaycardViewModel((EquipmentDaycardModel)item, SelectedDate));
                        break;
                    case DaycardType.Labor:
                        baseDaycardViewModels.Add(new LaborDaycardViewModel((LaborDaycardModel)item, SelectedDate));
                        break;
                    case DaycardType.Material:
                        baseDaycardViewModels.Add(new MaterialDaycardViewModel((MaterialDaycardModel)item, SelectedDate));
                        break;
                }
            }

            DaycardsList = new ObservableCollection<BaseDaycardViewModel>(baseDaycardViewModels);
        }

        private async Task OnGoBackClicked()
        {
            await Navigation.PopModalAsync();
        }

        private async Task OnAddDayCardClicked()
        {
            await database.SaveDaycardAsync(typeOfDaycard, SelectedDate.Date);
            FillDaycardsList();
        }

        private async Task<DayViewModel> SetCountOfValidAndNot(DayViewModel dayView)
        {
            if(dayView != null)
            {
                var daycardModels = await database.GetDaycardsByDateAndTypeAsync(typeOfDaycard ,dayView.Date);
                var baseViewModel = new BaseDaycardViewModel();

                foreach (var item in daycardModels)
                {
                    switch (typeOfDaycard)
                    {
                        case DaycardType.Accomplishment:
                            baseViewModel = new AccomplishmentDaycardViewModel((AccomplishmentDaycardModel)item, dayView);
                            break;
                        case DaycardType.Cost:
                            baseViewModel = new CostDaycardViewModel((CostDaycardModel)item, dayView);
                            break;
                        case DaycardType.Equipment:
                            baseViewModel = new EquipmentDaycardViewModel((EquipmentDaycardModel)item, dayView);
                            break;
                        case DaycardType.Labor:
                            baseViewModel = new LaborDaycardViewModel((LaborDaycardModel)item, dayView);
                            break;
                        case DaycardType.Material:
                            baseViewModel = new MaterialDaycardViewModel((MaterialDaycardModel)item, dayView);
                            break;
                    }

                    if (baseViewModel.IsValid)
                        dayView.CountOfValidated += 1;
                    else
                        dayView.CountOfNotValidated += 1;
                }
            }
            return dayView;
        }

        private async Task UpdateChangedDaycards()
        {
            List<BaseDaycardViewModel> updatedDaycards = new List<BaseDaycardViewModel>(DaycardsList);

            await database.DeleDaycardByDateAsync(typeOfDaycard ,SelectedDate.Date);

            foreach (var item in updatedDaycards)
            {
                await database.SaveUpdatedDaycardAsync(typeOfDaycard, item);
            }
        }
    }
}
