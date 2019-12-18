using System;
using System.Collections.Generic;
using System.ComponentModel;
using ControlsTest.ViewModels;
using Xamarin.Forms;

namespace ControlsTest.Views
{
    public partial class EditorView : ContentView, INotifyPropertyChanged
    {
        private string editorText { get; set; }

        public string EditorText
        {
            get => editorText;
            set
            {
                editorText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditorText)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnChangeEditorSizeClicked(object sender, EventArgs args)
        {
            if (expandedEditor.IsVisible)
            {
                expandedEditor.IsVisible = false;
                mainEditor.IsVisible = true;
            }
            else
            {
                expandedEditor.IsVisible = true;
                mainEditor.IsVisible = false;
            }
        }


        public EditorView()
        {            
            InitializeComponent();
        }
    }
}
