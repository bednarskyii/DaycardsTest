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
                    SelectedDate = currentModel;
                }

                monthDates.Add(currentModel);
                firstDay = firstDay.AddDays(1);
            }
            Dates = new ObservableCollection<DayViewModel>(monthDates);
        }

        private async Task FillDaycardsList()
        {
            if (typeOfDaycard == DaycardType.Labor)
            {
                List<LaborDaycardModel> equipmentDaycardModels = await database.GetLaborDaycardsByDateAsync(SelectedDate.Date);
                List<LaborDaycardViewModel> laborDaycardViewModels = new List<LaborDaycardViewModel>();

                foreach (var item in equipmentDaycardModels)
                {
                    laborDaycardViewModels.Add(new LaborDaycardViewModel(item));
                }

                DaycardsList = new ObservableCollection<BaseDaycardViewModel>(laborDaycardViewModels);
            }
            if (typeOfDaycard == DaycardType.Equipment)
            {
                List<EquipmentDaycardModel> equipmentDaycardModels = await database.GetEquipmentDaycardsByDateAsync(SelectedDate.Date);
                List<EquipmentDaycardViewModel> equipmentDaycardViewModels = new List<EquipmentDaycardViewModel>();

                foreach (var item in equipmentDaycardModels)
                {
                    var currentViewModel = new EquipmentDaycardViewModel(item, SelectedDate);

                    if (currentViewModel.IsValid)
                        SelectedDate.CountOfValidated += 1;
                    else
                        SelectedDate.CountOfNotValidated += 1;

                    equipmentDaycardViewModels.Add(currentViewModel);
                }

                DaycardsList = new ObservableCollection<BaseDaycardViewModel>(equipmentDaycardViewModels);
            }
            if (typeOfDaycard == DaycardType.Cost)
            {
                List<CostDaycardModel> equipmentDaycardModels = await database.GetCostsDaycardsByDateAsync(SelectedDate.Date);
                List<CostDaycardViewModel> laborDaycardViewModels = new List<CostDaycardViewModel>();

                foreach (var item in equipmentDaycardModels)
                {
                    laborDaycardViewModels.Add(new CostDaycardViewModel(item));
                }

                DaycardsList = new ObservableCollection<BaseDaycardViewModel>(laborDaycardViewModels);
            }
            if (typeOfDaycard == DaycardType.Accomplishment)
            {
                List<AccomplishmentDaycardModel> equipmentDaycardModels = await database.GetAccomplishmentDaycardsByDateAsync(SelectedDate.Date);
                List<AccomplishmentDaycardViewModel> equipmentDaycardViewModels = new List<AccomplishmentDaycardViewModel>();

                foreach (var item in equipmentDaycardModels)
                {
                    equipmentDaycardViewModels.Add(new AccomplishmentDaycardViewModel(item));
                }

                DaycardsList = new ObservableCollection<BaseDaycardViewModel>(equipmentDaycardViewModels);
            }
            if (typeOfDaycard == DaycardType.Material)
            {
                List<MaterialDaycardModel> equipmentDaycardModels = await database.GetMaterialDaycardsByDateAsync(SelectedDate.Date);
                List<MaterialDaycardViewModel> laborDaycardViewModels = new List<MaterialDaycardViewModel>();

                foreach (var item in equipmentDaycardModels)
                {
                    laborDaycardViewModels.Add(new MaterialDaycardViewModel(item));
                }

                DaycardsList = new ObservableCollection<BaseDaycardViewModel>(laborDaycardViewModels);
            }
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

            return dayView;
        }
    }
}
