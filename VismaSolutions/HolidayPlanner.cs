using VismaSolutions.Interfaces;

namespace VismaSolutions
{
    public class HolidayPlanner : IHolidayPlanner
    {
        private readonly List<DateTime> _nationalHolidays;
        private readonly DateTime _startDate;
        private readonly DateTime _endDate;

        public HolidayPlanner(DateTime startDate, DateTime endDate)
        {
            _nationalHolidays = BindNationalHolidays();
            _startDate = startDate;
            _endDate = endDate;
        }

        public List<DateTime> BindNationalHolidays()
            => new List<DateTime>()
                {
                    new DateTime(2021, 1, 1),
                    new DateTime(2021, 1, 6),
                    new DateTime(2021, 4, 2),
                    new DateTime(2021, 4, 5),
                    new DateTime(2021, 5, 13),
                    new DateTime(2021, 6, 25),
                    new DateTime(2021, 12, 6),
                    new DateTime(2021, 12, 24),
                    new DateTime(2022, 1, 1),
                    new DateTime(2022, 1, 6),
                    new DateTime(2022, 4, 15),
                    new DateTime(2022, 4, 18),
                    new DateTime(2022, 5, 1),
                    new DateTime(2022, 5, 26),
                    new DateTime(2022, 6, 5),
                    new DateTime(2022, 6, 24),
                    new DateTime(2022, 12, 6),
                    new DateTime(2022, 12, 24),
                    new DateTime(2022, 12, 25),
                    new DateTime(2022, 12, 26),
                };

        public void ValidateDates()
        {
            if (_endDate < _startDate)
                throw new ArgumentException("End date can not be less than start date.");

            if (_startDate.Year < 2021 || _endDate.Year > 2022)
                throw new ArgumentException("You can only choose date between 2021 - 2022.");

            if (_startDate.Month < 4 && _endDate.Month >= 4)
                throw new ArgumentException("Holidays can be applied for the same holiday period only.");
        }

        public List<DateTime> GetAvailableHolidays()
        {
            var availableHolidays = new List<DateTime>();

            var i = _startDate;
            while (i <= _endDate)
            {
                // Holidays without Sundays and National Holidays
                if (i.DayOfWeek != DayOfWeek.Sunday && !_nationalHolidays.Contains(i))
                    availableHolidays.Add(i);

                i = i.AddDays(1);
            }

            if (availableHolidays.Count > 50)
                throw new Exception("Holidays can not exceed 50 days.");

            return availableHolidays;
        }
    }
}
