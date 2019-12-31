using System;
using System.ComponentModel;
using ControlsTest.DaycardsModels;

namespace ControlsTest.DaycardsViewModels
{
    public class MaterialDaycardViewModel : BaseDaycardViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MaterialDaycardViewModel(MaterialDaycardModel daycard, DayViewModel dayView)
        {
            Title = daycard.Title;
            Quantity = daycard.Quantity;
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

        private int quantity { get; set; }
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
            }
        }

        public override bool IsValid { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
