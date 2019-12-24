using ControlsTest.Enums;
using ControlsTest.ViewModels;
using Xamarin.Forms;

namespace ControlsTest.Pages
{
    public partial class MainDaycardsPage : ContentPage
    {
        public MainDaycardsPage(DaycardType type)
        {
            InitializeComponent();
            BindingContext = new MainDaycardPageViewModel(type, Navigation);
        }
    }
}
