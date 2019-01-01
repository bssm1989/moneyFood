using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneyFood.Models
{
    public class AddPaidStudent
    {
        public int StudentId { get; set; }
        public int[] MonthIdAndPaid { get; set; }
        public int numPaid { get; set; }

    }
}
