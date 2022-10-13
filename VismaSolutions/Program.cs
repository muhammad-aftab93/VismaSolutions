using VismaSolutions.Interfaces;

namespace VismaSolutions
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter start date: (yyyy/mm/dd)");
                var startDateString = Console.ReadLine();

                if (!DateTime.TryParse(startDateString, out var startDate))
                    throw new ArgumentException("\nInvalid start date.");

                Console.WriteLine("\nEnter end date: (yyyy/mm/dd)");
                var endDateString = Console.ReadLine();

                if (!DateTime.TryParse(endDateString, out var endDate))
                    throw new ArgumentException("\nInvalid end date.");

                IHolidayPlanner holidayPlanner = new HolidayPlanner(startDate, endDate);
                holidayPlanner.ValidateDates();
                var holidays = holidayPlanner.GetAvailableHolidays();

                Console.WriteLine($"\nTotal number of holidays: {holidays.Count}");
                foreach (var holiday in holidays)
                    Console.WriteLine($"{holiday:dd/MM/yyyy} {holiday.DayOfWeek}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}