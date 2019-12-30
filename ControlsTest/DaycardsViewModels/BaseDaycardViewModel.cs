using System;
using System.ComponentModel;

namespace ControlsTest.DaycardsViewModels
{
    public class BaseDaycardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Title { get; set; }
        public int IdDaycard;
        public DayViewModel DayUrl = new DayViewModel();

        private bool isValid { get; set; }
        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }

    }
}
