using System;
using System.Collections.Generic;

namespace moneyFood.Models
{
    public partial class Monthly
    {
        public int MonthlyId { get; set; }
        public string MonthName { get; set; }
        public int? MonthYear { get; set; }
        public int? MonthCourse { get; set; }
        public bool? MonthStatus { get; set; }
        public int? MonthYearindex { get; set; }
    }
}
