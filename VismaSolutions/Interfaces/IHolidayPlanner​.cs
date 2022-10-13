namespace VismaSolutions.Interfaces
{
    public interface IHolidayPlanner​
    {
        List<DateTime> BindNationalHolidays();
        List<DateTime> GetAvailableHolidays();
        void ValidateDates();
    }
}
