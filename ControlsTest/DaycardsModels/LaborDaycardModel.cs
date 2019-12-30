using System;
using ControlsTest.Enums;
using SQLite;

namespace ControlsTest.DaycardsModels
{
    public class LaborDaycardModel : IDaycard
    {
        [AutoIncrement] [PrimaryKey]
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public string Title { get; set; }

        public TimeReportingCode TimeReportingCode { get; set; }

        public string Hours { get; set; }
    }
}
