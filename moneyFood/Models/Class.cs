using System;
using System.Collections.Generic;

namespace moneyFood.Models
{
    public partial class Class
    {
        public int ClassId { get; set; }
        public string Status { get; set; }
        public string ClassNameId { get; set; }
        public int? TeacherId { get; set; }
        public int? ClassRoom { get; set; }
        public int? ClassYearIndex { get; set; }
    }
}
