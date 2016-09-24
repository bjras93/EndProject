namespace LifeStruct
{
    using LifeStruct.Models;
    using System.Linq;
    public class BmiClassViewModel
    {

        public static BmiClassModel GetBmi(decimal Weight)
        {
            DefaultConnection db = new DefaultConnection();


            return db.BmiClass.ToList().Where(x => x.Start < Weight && x.End > Weight).First();
        }
    }
}