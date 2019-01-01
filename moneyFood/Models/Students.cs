using System;
using System.Collections.Generic;

namespace moneyFood.Models
{
    public partial class Students
    {
        public int StudentId { get; set; }
        public string StudentTitle { get; set; }
        public string StudentName { get; set; }
        public string StudentLname { get; set; }
        public DateTime? StudentBirthday { get; set; }
        public string StudentIdcard { get; set; }
        public int? StudentPsisId { get; set; }
        public string StudentStatus { get; set; }
        public DateTime? StudentTimestamp { get; set; }
    }
}
