using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneyFood.Models
{
    public class studentNow
    {
        public int StudentId { get; set; }
        public string StudentTitle { get; set; }
        public string StudentName { get; set; }
        public string StudentLname { get; set; }
        public DateTime? StudentBirthday { get; set; }
        public string StudentIdcard { get; set; }
        public int? StudentPsisId { get; set; }
        public string StudentStatus { get; set; }
        public int ClassId { get; set; }
        public string Status { get; set; }
        public string ClassNameId { get; set; }
        public int? TeacherId { get; set; }
        public int? ClassRoom { get; set; }
        public int? ClassYearIndex { get; set; }
    }
}
