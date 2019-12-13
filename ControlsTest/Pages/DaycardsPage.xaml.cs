using System;
using System.Collections.Generic;
using ControlsTest.ViewModels;
using Xamarin.Forms;

namespace ControlsTest.Pages
{
    public partial class DaycardsPage : ContentPage
    {
        public DaycardsPage()
        {
            InitializeComponent();
            BindingContext = new DaycardsViewModel(Navigation);
        }
    }
}
