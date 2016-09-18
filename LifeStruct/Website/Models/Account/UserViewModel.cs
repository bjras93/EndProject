using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Web;

namespace LifeStruct.Models
{
    public class UserViewModel
    {
        public static ApplicationUser GetCurrentUser()
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(HttpContext.Current.User.Identity.GetUserId());

            return user;
        }
        public static ApplicationUser GetUser(string id)
        {
            ApplicationUser user = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>().FindById(id);

            return user;
        }
        public List<string> BmiCalc(string Height, string Weight)
        {

            List<string> bmiResult = new List<string>();
            var bmi = Convert.ToDouble(Weight.Replace(",", ".")) / Math.Pow(Convert.ToDouble(Height) / 100, 2);


            if (bmi < 18.5)
            {
                bmiResult.Add("Underweight");
            }
            if (bmi >= 18.5 && bmi <= 24.9)
            {
                bmiResult.Add("Normal");
            }
            if (bmi >= 24.9 && bmi <= 29.9)
            {
                bmiResult.Add("Overweight");
            }
            if (bmi >= 29.9 && bmi <= 39.9)
            {
                bmiResult.Add("Obesity class 1");
            }
            if (bmi > 40)
            {
                bmiResult.Add("Obesity class 2");
            }
            bmiResult.Add(bmi.ToString("##.##"));
            return bmiResult;
        }
    }
}