using System.ComponentModel;
using ControlsTest.DaycardsModels;
using ControlsTest.Enums;

namespace ControlsTest.DaycardsViewModels
{
    public class EquipmentDaycardViewModel : BaseDaycardViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public EquipmentDaycardViewModel(EquipmentDaycardModel daycard)
        {
            Title = daycard.Title;
            Operator = daycard.Operator;
            Hours = daycard.Hours;
            Miles = daycard.Miles;
        }

        private string title { get; set; }
        public string Title
        {
            get => title;
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Title)));
            }
        }

        private Operators _operator;
        public Operators Operator
        {
            get => _operator;
            set
            {
                _operator = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Operator)));
            }
        }

        private int hours;
        public int Hours
        {
            get => hours;
            set
            {
                hours = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hours)));
            }
        }

        private int miles;
        public int Miles
        {
            get => miles;
            set
            {
                miles = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Miles)));
            }
        }


    }
}
