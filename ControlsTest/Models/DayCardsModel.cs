using System;
using System.ComponentModel;
using SQLite;
using Xamarin.Forms;

namespace ControlsTest.Models
{
    public class DayCardsModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey] [AutoIncrement]
        public int DayCardId { get; set; }
        public DateTime Date { get; set; }
        public string DayCardNumber { get; set; }
        public DayModel DayUrl = new DayModel();

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
                {
                    if (!IsValid)
                    {
                        IsValid = true;
                        DayUrl.CountOfValidated += 1;
                        if (DayUrl.CountOfNotValidated > 0)
                            DayUrl.CountOfNotValidated -= 1;
                    }
                }
                else
                {
                    if (IsValid)
                    {
                        IsValid = false;
                        DayUrl.CountOfNotValidated += 1;
                        if (DayUrl.CountOfNotValidated > 0)
                            DayUrl.CountOfValidated -= 1;
                    }
                }
            }
            catch (Exception e)
            {
                IsValid = false;
            }


        }
    }
}
