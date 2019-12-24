using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ControlsTest.DaycardsViewModels
{
    public class DayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DateTime Date { get; set; }
        public Color BackgroundColor { get; set; }

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
    }
}
