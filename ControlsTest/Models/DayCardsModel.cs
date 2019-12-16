using System;
using System.ComponentModel;

namespace ControlsTest.Models
{
    public class DayCardsModel : INotifyPropertyChanged
    {
        public string DayCardId { get; set; }
        public string Operator { get; set; }

        private int? hours;
        public int? Hours
        {
            get => hours;
            set
            {
                try
                {
                    hours = value;
                    if (value != 0 && Miles != 0 && value != null && Miles != null)
                        IsValid = true;
                    else
                        IsValid = false;
                }
                catch (Exception e)
                {
                    IsValid = false;
                    var t = e.Message;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Hours)));
            }
        }

        private int? miles;
        public int? Miles
        {
            get => miles;
            set
            {
                try
                {
                    miles = value;
                    if (value != 0 && Hours != 0 && value != null && Hours != null)
                        IsValid = true;
                    else
                        IsValid = false;
                }
                catch(Exception e)
                {
                    IsValid = false;
                    var t = e.Message;
                }
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
    }
}
