using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using ControlsTest.DaycardsModels;
using ControlsTest.Enums;

namespace ControlsTest.DaycardsViewModels
{
    public class LaborDaycardViewModel : BaseDaycardViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public LaborDaycardViewModel(LaborDaycardModel daycard, DayViewModel dayView)
        {
            Title = daycard.Title;
            Hours = daycard.Hours;
            TimeReportingCode = daycard.TimeReportingCode;
            DayUrl = dayView;
            IdDaycard = daycard.Id;

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


            if (IsHoursValid && !IsValid)
            {
                IsValid = true;
                DayUrl.CountOfValidated += 1;
                if (DayUrl.CountOfNotValidated > 0)
                    DayUrl.CountOfNotValidated -= 1;
            }
            if (!IsHoursValid && IsValid)
            {
                IsValid = false;
                DayUrl.CountOfNotValidated += 1;
                if (DayUrl.CountOfValidated > 0)
                    DayUrl.CountOfValidated -= 1;
            }

        }
    }
}
