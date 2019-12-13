using System;
using System.Collections.Generic;
using ControlsTest.ViewModels;
using Xamarin.Forms;

namespace ControlsTest.Views
{
    public partial class EditorView : ContentView
    {

        public EditorView()
        {
            InitializeComponent();
            BindingContext = new EditorViewModel();
        }
    }
}
