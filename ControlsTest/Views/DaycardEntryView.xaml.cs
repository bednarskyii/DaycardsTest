using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ControlsTest.Views
{
    public partial class DaycardEntryView : ContentView
    {

        public static readonly BindableProperty EntryPlaceholderProperty = BindableProperty.Create(
                                                 propertyName: "EntryPlaceholder",
                                                 defaultValue: "default",
                                                 returnType: typeof(string),
                                                 declaringType: typeof(DaycardEntryView),
                                                 defaultBindingMode: BindingMode.OneWayToSource);

        public string EntryPlaceholder
        {
            get { return (string)GetValue(EntryPlaceholderProperty); }
            set
            {
                SetValue(EntryPlaceholderProperty, value);
                MainEntry.Placeholder = value;
            }
        }


        public DaycardEntryView()
        {
            InitializeComponent();
            MainEntry.Placeholder = EntryPlaceholder;
        }
    }
}
