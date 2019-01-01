using System;
using System.Collections.Generic;

namespace moneyFood.Models
{
    public partial class Food
    {
        public int FoodId { get; set; }
        public string FoodIdStudent { get; set; }
        public string FoodName { get; set; }
        public string FoodLastname { get; set; }
        public string FoodMonthTh { get; set; }
        public string FoodMonthIndex { get; set; }
        public int? FoodPaid { get; set; }
        public int? FoodFullPaid { get; set; }
        public string FoodClass { get; set; }
        public string FoodYear { get; set; }
        public DateTime? FoodTimesatamp { get; set; }
        public string FoodUpdate { get; set; }
        public int? FoodPaidId { get; set; }
        public string FoodTitle { get; set; }
    }
}
