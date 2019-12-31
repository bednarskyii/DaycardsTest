using System;
using System.ComponentModel;

namespace ControlsTest.DaycardsViewModels
{
    public class BaseDaycardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual string Title { get; set; }
        public int IdDaycard;
        public DayViewModel DayUrl = new DayViewModel();


        private bool isValid { get; set; }
        public virtual bool IsValid { get; set; }

    }
}
