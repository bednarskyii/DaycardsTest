using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ControlsTest.ViewModels
{
    public class EditorViewModel : INotifyPropertyChanged
    {
        private int editorHeight { get; set; } = 50;
        private bool isEditorExpanded;

        public int EditorHeight
        {
            get => editorHeight;
            set
            {
                editorHeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(EditorHeight)));
            }
        }

        public Command ChangeEditorSize { get; set; }
            
        public EditorViewModel()
        {
            ChangeEditorSize = new Command(() => OnChangeEditorSizeClicked());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChangeEditorSizeClicked()
        {
            if (isEditorExpanded)
            {
                EditorHeight = 50;
                isEditorExpanded = false;
            }
            else
            {
                EditorHeight = 200;
                isEditorExpanded = true;
            }
        }
    }
}
