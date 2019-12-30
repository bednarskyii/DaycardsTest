using System;
using SQLite;

namespace ControlsTest.DaycardsModels
{
    public class CostDaycardModel : IDaycard
    {
        [AutoIncrement] [PrimaryKey]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public decimal TotalCost { get; set; } //not sure for SQLite
    }
}
