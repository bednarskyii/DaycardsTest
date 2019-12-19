using System;
using System.ComponentModel;
using SQLite;

namespace ControlsTest.Models
{
    public class DayCardsModel : INotifyPropertyChanged
    {
        [PrimaryKey] [AutoIncrement]
        public int DayCardId { get; set; }

        public DateTime Date { get; set; }
        public string DayCardNumber { get; set; }

        private bool isOperatorValid;
        public bool IsOperatorValid
        {
            get => isOperatorValid;
            set
            {
                isOperatorValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsOperatorValid)));
            }
        }


        private string _operator;
        public string Operator
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

        private bool isValid;
        public bool IsValid
        {
            get => isValid;
            set
            {
                isValid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsValid)));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void ValidationCheck()
        {
            try
            {
                int hrs;

                if (Hours != "")
                    hrs = Convert.ToInt32(Hours);
                else
                    hrs = 0;

                if (hrs > 0 && hrs < 24)
                    IsHoursValid = true;
                else
                    IsHoursValid = false;

                if (!string.IsNullOrEmpty(Operator))
                    IsOperatorValid = true;
                else
                    IsOperatorValid = false;

                if (IsOperatorValid && IsHoursValid)
                    IsValid = true;
                else
                    IsValid = false;
            }
            catch (Exception e)
            {
                IsValid = false;
            }


        }
    }
}
