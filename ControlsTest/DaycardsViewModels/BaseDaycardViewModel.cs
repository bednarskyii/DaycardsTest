using System;
using System.ComponentModel;

namespace ControlsTest.DaycardsViewModels
{
    public class BaseDaycardViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
