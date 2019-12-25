using System;
using ControlsTest.DaycardsModels;
using Xamarin.Forms;

namespace ControlsTest.TemplateSelector
{
    public class DaycardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EquipmentDaycardTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is EquipmentDaycardModel)
                return EquipmentDaycardTemplate;

            return null;
        }
    }
}
