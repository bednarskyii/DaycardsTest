using System.ComponentModel;
using ControlsTest.DaycardsModels;
using ControlsTest.Enums;

namespace ControlsTest.DaycardsViewModels
{
    public class LaborDaycardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LaborDaycardViewModel(LaborDaycardModel daycard)
        {
            Title = daycard.Title;
            Hours = daycard.Hours;
            TimeReportingCode = daycard.TimeReportingCode;
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

        private TimeReportingCode timeReportingCode;
        public TimeReportingCode TimeReportingCode
        {
            get => timeReportingCode;
            set
            {
                timeReportingCode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TimeReportingCode)));
            }
        }


    }
}
