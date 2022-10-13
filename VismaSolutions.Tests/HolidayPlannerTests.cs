using FluentAssertions;
using NSubstitute;
using VismaSolutions.Interfaces;
using Xunit;

namespace VismaSolutions.Tests
{
    public class HolidayPlannerTests
    {
        private IHolidayPlanner _holidayPlanner;

        [Fact]
        public void BindNationalHolidays_Should_Return_National_Holidays()
        {
            // Arrange
            _holidayPlanner = new HolidayPlanner(Arg.Any<DateTime>(), Arg.Any<DateTime>());
            var expectedNationalHolidays = GetNationalHolidays();

            // Act
            var receivedNationalHolidays = _holidayPlanner.BindNationalHolidays();

            // Assert
            receivedNationalHolidays.Should().BeEquivalentTo(expectedNationalHolidays);
        }

        [Fact]
        public void ValidateDates_ShouldThrow_Exception_When_EndDate_LessThan_StartDate()
        {
            // Arrange
            _holidayPlanner = new HolidayPlanner(new DateTime(2022, 03, 28), new DateTime(2022, 03, 25));

            // Act
            var result = Assert.Throws<ArgumentException>(() => _holidayPlanner.ValidateDates());

            // Assert
            result.Message.Should().Be("End date can not be less than start date.");
        }

        [Fact]
        public void ValidateDates_ShouldThrow_Exception_When_StartDate_Year_LessThan_2021()
        {
            // Arrange
            _holidayPlanner = new HolidayPlanner(new DateTime(2020, 03, 28), new DateTime(2020, 04, 01));

            // Act
            var result = Assert.Throws<ArgumentException>(() => _holidayPlanner.ValidateDates());

            // Assert
            result.Message.Should().Be("You can only choose date between 2021 - 2022.");
        }

        [Fact]
        public void ValidateDates_ShouldThrow_Exception_When_EndDate_Year_GreaterThan_2022()
        {
            // Arrange
            _holidayPlanner = new HolidayPlanner(Arg.Any<DateTime>(), new DateTime(2023, 03, 25));

            // Act
            var result = Assert.Throws<ArgumentException>(() => _holidayPlanner.ValidateDates());

            // Assert
            result.Message.Should().Be("You can only choose date between 2021 - 2022.");
        }

        [Fact]
        public void ValidateDates_ShouldThrow_Exception_When_StartDate_EndDate_AreNot_InSame_Period()
        {
            // Arrange
            _holidayPlanner = new HolidayPlanner(new DateTime(2022, 03, 28), new DateTime(2022, 04, 25));

            // Act
            var result = Assert.Throws<ArgumentException>(() => _holidayPlanner.ValidateDates());

            // Assert
            result.Message.Should().Be("Holidays can be applied for the same holiday period only.");
        }

        [Fact]
        public void GetAvailableHolidays_ShouldThrow_Exception_When_Total_Holidays_AreGreater_Than_50()
        {
            // Arrange
            _holidayPlanner = new HolidayPlanner(new DateTime(2021, 04, 01), new DateTime(2022, 12, 25));

            // Act
            var result = Assert.Throws<Exception>(() => _holidayPlanner.GetAvailableHolidays());

            // Assert
            result.Message.Should().Be("Holidays can not exceed 50 days.");
        }

        [Fact]
        public void GetAvailableHolidays_Should_Return_Allowed_Holidays()
        {
            // Arrange
            var expectedHolidays = GetExpectedOutput();
            _holidayPlanner = new HolidayPlanner(new DateTime(2022, 12, 01), new DateTime(2022, 12, 31));

            // Act
            var result = _holidayPlanner.GetAvailableHolidays();

            // Assert
            result.Should().BeEquivalentTo(expectedHolidays);
        }

        [Fact]
        public void GetAvailableHolidays_Should_Not_Return_National_Holidays()
        {
            // Arrange
            var nationalHoliday = new DateTime(2022, 12, 25);
            _holidayPlanner = new HolidayPlanner(new DateTime(2022, 12, 01), new DateTime(2022, 12, 31));

            // Act
            var result = _holidayPlanner.GetAvailableHolidays();

            // Assert
            result.Should().NotContain(nationalHoliday);
        }

        [Fact]
        public void GetAvailableHolidays_Should_Not_Return_Sundays()
        {
            // Arrange
            var sunday = new DateTime(2022, 12, 18);
            _holidayPlanner = new HolidayPlanner(new DateTime(2022, 12, 01), new DateTime(2022, 12, 1));

            // Act
            var result = _holidayPlanner.GetAvailableHolidays();

            // Assert
            result.Should().NotContain(sunday);
        }

        public List<DateTime> GetExpectedOutput()
            => new List<DateTime>()
                {
                    new DateTime(2022, 12, 01),
                    new DateTime(2022, 12, 02),
                    new DateTime(2022, 12, 03),
                    new DateTime(2022, 12, 05),
                    new DateTime(2022, 12, 07),
                    new DateTime(2022, 12, 08),
                    new DateTime(2022, 12, 09),
                    new DateTime(2022, 12, 10),
                    new DateTime(2022, 12, 12),
                    new DateTime(2022, 12, 13),
                    new DateTime(2022, 12, 14),
                    new DateTime(2022, 12, 15),
                    new DateTime(2022, 12, 16),
                    new DateTime(2022, 12, 17),
                    new DateTime(2022, 12, 19),
                    new DateTime(2022, 12, 20),
                    new DateTime(2022, 12, 21),
                    new DateTime(2022, 12, 22),
                    new DateTime(2022, 12, 23),
                    new DateTime(2022, 12, 27),
                    new DateTime(2022, 12, 28),
                    new DateTime(2022, 12, 29),
                    new DateTime(2022, 12, 30),
                    new DateTime(2022, 12, 31),
                };

        public List<DateTime> GetNationalHolidays()
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
    }
}
