using System;
using ControlsTest.DaycardsModels;
using ControlsTest.DaycardsViewModels;
using Xamarin.Forms;

namespace ControlsTest.TemplateSelector
{
    public class DaycardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate EquipmentDaycardTemplate { get; set; }
        public DataTemplate LaborDaycardTemplate { get; set; }
        public DataTemplate AccomplishmentDaycardTemplate { get; set; }
        public DataTemplate MaterialDaycardTemplate { get; set; }
        public DataTemplate CostDaycardTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is EquipmentDaycardViewModel)
                return EquipmentDaycardTemplate;
            if (item is LaborDaycardViewModel)
                return LaborDaycardTemplate;
            if (item is AccomplishmentDaycardViewModel)
                return AccomplishmentDaycardTemplate;
            if (item is MaterialDaycardViewModel)
                return MaterialDaycardTemplate;
            if (item is CostDaycardViewModel)
                return CostDaycardTemplate;

            return null;
        }
    }
}
