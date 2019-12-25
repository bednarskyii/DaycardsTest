﻿using System;
using System.ComponentModel;
using ControlsTest.DaycardsModels;

namespace ControlsTest.DaycardsViewModels
{
    public class AccomplishmentDaycardViewModel : BaseDaycardViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public AccomplishmentDaycardViewModel(AccomplishmentDaycardModel daycard)
        {
            Title = daycard.Title;
            Quantity = daycard.Quantity;
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

    }
}
