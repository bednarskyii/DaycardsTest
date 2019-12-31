using System;
using System.ComponentModel;
using ControlsTest.DaycardsModels;

namespace ControlsTest.DaycardsViewModels
{
    public class CostDaycardViewModel : BaseDaycardViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CostDaycardViewModel(CostDaycardModel daycard, DayViewModel dayView)
        {
            Title = daycard.Title;
            TotalCost = daycard.TotalCost;
            DayUrl = dayView;
            IdDaycard = daycard.Id;
        }

        private string title { get; set; }
        public override string Title
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

        public override bool IsValid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
