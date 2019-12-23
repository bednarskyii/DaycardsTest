using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ControlsTest.Models
{
    public class DayModel : INotifyPropertyChanged
    {
        public DateTime Date { get; set; }

        private int countOfValidated { get; set; }
        public int CountOfValidated
        {
            get => countOfValidated;
            set
            {
                countOfValidated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountOfValidated)));
            }
        }
        private int countOfNotValidated { get; set; }
        public int CountOfNotValidated
        {
            get => countOfNotValidated;
            set
            {
                countOfNotValidated = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CountOfNotValidated)));
            }
        }
        //private ObservableCollection<DayCardsModel> daycardsList { get; set; }
        //public ObservableCollection<DayCardsModel> DaycardsList
        //{
        //    get => daycardsList;
        //    set
        //    {
        //        daycardsList = value;
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DaycardsList))); 
        //    }
        //}
        private Color backgroundColor { get; set; }
        public Color BackgroundColor
        {
            get => backgroundColor;
            set
            {
                backgroundColor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(BackgroundColor)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
