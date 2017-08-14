using System.Linq;

namespace LifeStruct.Models.Account
{
    public class BmiClassViewModel
    {

        public static BmiClassModel GetBmi(decimal Weight)
        {
            DefaultConnection db = new DefaultConnection();


            return db.BmiClass.ToList().Where(x => x.Start < Weight && x.End > Weight).First();
        }
    }
}