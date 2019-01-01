using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneyFood.Models
{
    public class viewPaid

    {
        public int StudentId { get; set; }
        public string StudentTitle { get; set; }
        public string StudentName { get; set; }
        public string StudentLname { get; set; }
        public string MonthName { get; set; }
        public int? MonthYear { get; set; }
        public int? MonthCourse { get; set; }
        public int ClassId { get; set; }
        public string Status { get; set; }
        public string ClassNameId { get; set; }
        public int? TeacherId { get; set; }
        public int? ClassRoom { get; set; }
        public int? ClassYearIndex { get; set; }
        public int? PaidId { get; set; }
        public int PaidSum { get; set; }
        public int mustPay { get; set; }
        public int? MonthlyId { get; set; }
        public DateTime? Timestamp { get; set; }
        public int? StaffPaidId { get; set; }
        public int? totalPaid { get; set; }
    }
}
