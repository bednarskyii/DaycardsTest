using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace ControlsTest.Views
{
    public partial class EntryView : ContentView
    {
        public static readonly BindableProperty EntryTextColorProperty = BindableProperty.Create(
                                                 propertyName: "EntryTextColor",
                                                 defaultValue: Color.FromHex("#7185f7"),
                                                 returnType: typeof(Color),
                                                 declaringType: typeof(EntryView),
                                                 defaultBindingMode: BindingMode.OneWay);

        public Color EntryTextColor
        {
            get { return (Color)GetValue(EntryTextColorProperty); }
            set
            {
                SetValue(EntryTextColorProperty, value);
                MainEntry.TextColor = value;
            }
        }

        public EntryView()
        {
            InitializeComponent();
            MainEntry.TextColor = EntryTextColor;
        }
    }
}
