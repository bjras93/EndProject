using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LifeStruct.Models
{
    public class DietProgressModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string DietId { get; set; }
        public string CalorieIntake { get; set; }
        public string Day { get; set; }
        public string FoodId { get; set; }
    }
}