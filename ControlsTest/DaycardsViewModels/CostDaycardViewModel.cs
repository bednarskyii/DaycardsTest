using System;
using System.ComponentModel;
using ControlsTest.DaycardsModels;

namespace ControlsTest.DaycardsViewModels
{
    public class CostDaycardViewModel : BaseDaycardViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CostDaycardViewModel(CostDaycardModel daycard)
        {
            Title = daycard.Title;
            TotalCost = daycard.TotalCost;
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

        private decimal totalCost { get; set; }
        public decimal TotalCost
        {
            get => totalCost;
            set
            {
                totalCost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TotalCost)));
            }
        }
    }
}
