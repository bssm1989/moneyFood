using System;
using System.Collections.Generic;

namespace moneyFood.Models
{
    public partial class EnrollPaid
    {
        
        public int? PaidId { get; set; }
        public int? PayId { get; set; }
        public int? StudentId { get; set; }
        public int? MonthlyId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? StaffPaidId { get; set; }
        public int? StudentTypeId { get; set; }
        public int? Paid { get; set; }
        public int ClassId { get; set; }
    }
}
