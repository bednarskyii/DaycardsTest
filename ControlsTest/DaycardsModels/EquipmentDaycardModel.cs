using System;
using ControlsTest.Enums;
using SQLite;

namespace ControlsTest.DaycardsModels
{
    public class EquipmentDaycardModel
    {
        [AutoIncrement] [PrimaryKey]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public Operators Operator { get; set; }

        public int Hours { get; set; }

        public int Miles { get; set; }
    }
}
