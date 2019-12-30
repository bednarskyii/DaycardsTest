using System;
using SQLite;

namespace ControlsTest.DaycardsModels
{
    public class MaterialDaycardModel : IDaycard
    {
        [AutoIncrement] [PrimaryKey]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public int Quantity { get; set; }
    }
}
