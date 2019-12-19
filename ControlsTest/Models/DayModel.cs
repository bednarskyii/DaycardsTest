using System;
using Xamarin.Forms;

namespace ControlsTest.Models
{
    public class DayModel
    {
        public DateTime Date { get; set; }
        public bool HasValidated { get; set; }
        public bool HasNotValidated { get; set; }
        public Color BackgroundColor { get; set; }
    }
}
