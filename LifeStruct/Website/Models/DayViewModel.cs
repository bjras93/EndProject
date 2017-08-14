using LifeStruct.Models.Diet;

namespace LifeStruct.Models
{
    public class DayViewModel
    {
        public static DaysModel GetDay(int Id)
        {
            DefaultConnection db = new DefaultConnection();

            return db.Days.Find(Id);

        }
    }
}