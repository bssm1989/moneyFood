using System;
using System.Collections.Generic;

namespace moneyFood.Models
{
    public partial class EnrollPay
    {
        public int PayId { get; set; }
        public int? PaidId { get; set; }
        public int? StudentId { get; set; }
        public int? Pay { get; set; }
        public int? StaffId { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
