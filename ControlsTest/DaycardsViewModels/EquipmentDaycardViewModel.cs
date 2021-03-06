﻿using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using ControlsTest.Database;
using ControlsTest.DaycardsModels;
using ControlsTest.Enums;

namespace ControlsTest.DaycardsViewModels
{
    public class EquipmentDaycardViewModel : BaseDaycardViewModel, INotifyPropertyChanged
    {
        private IDatabaseRepository database;
        public event PropertyChangedEventHandler PropertyChanged;

        public EquipmentDaycardViewModel(EquipmentDaycardModel daycard, DayViewModel dayViewModel)
        {
            database = new DatabaseRepository();

            IdDaycard = daycard.Id;
            Title = daycard.Title;
            Operator = daycard.Operator;
            miles = daycard.Miles;
            hours = daycard.Hours;
            DayUrl = dayViewModel;

            if (daycard != null)
                ValidationCheck();
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

        private bool isHoursValid;
        public bool IsHoursValid
        {
            get => isHoursValid;
            set
            {
                isHoursValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsHoursValid)));
            }
        }

        private string hours;
        public string Hours
        {
            get => hours;
            set
            {
                hours = value;
                ValidationCheck();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hours)));
            }
        }

        private bool isMilesValid;
        public bool IsMilesValid
        {
            get => isMilesValid;
            set
            {
                isMilesValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsMilesValid)));
            }
        }


        private string miles;
        public string Miles
        {
            get => miles;
            set
            {
                miles = value;
                ValidationCheck();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Miles)));
            }
        }

        private bool isValid { get; set; }
        public override bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }

        private void ValidationCheck()
        {
            Regex regexHours = new Regex("^[1-2]?[0-9]$"); // regex between 0 and 29 
            Regex regexMiles = new Regex("^[0-9]*$"); // regex for only numbers

            if (!string.IsNullOrEmpty(Hours) && regexHours.IsMatch(Hours))
            {
                int hrs = Convert.ToInt32(Hours);
                if (hrs > 0 && hrs <= 24)
                    IsHoursValid = true;
                else
                    IsHoursValid = false;
            }
            else
            {
                IsHoursValid = false;
            }

            IsMilesValid = !string.IsNullOrEmpty(Miles) && regexMiles.IsMatch(Miles);

            if (IsHoursValid && IsMilesValid && !IsValid)
            {
                IsValid = true;
                DayUrl.CountOfValidated += 1;
                if(DayUrl.CountOfNotValidated > 0)
                    DayUrl.CountOfNotValidated -= 1;
            }
            if(!IsHoursValid || !IsMilesValid && IsValid) 
            {
                IsValid = false;
                DayUrl.CountOfNotValidated += 1;
                if (DayUrl.CountOfValidated > 0)
                    DayUrl.CountOfValidated -= 1;
            }

        }
    }
}
