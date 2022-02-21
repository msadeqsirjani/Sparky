using FluentAssertions;
using Xunit;

namespace Sparky.XUnitTest;

public class GradingCalculatorTest
{
    private readonly GradingCalculator _calculator;

    public GradingCalculatorTest()
    {
        _calculator = new GradingCalculator();
    }

    [Fact]
    public void GradeCalculator_WithScore95AndWith90Attendance_ReturnAGrade()
    {
        _calculator.Score = 95;
        _calculator.AttendancePercentage = 90;

        _calculator.GetGrade().Should().Be("A");
    }

    [Theory]
    [InlineData(95, 90, "A")]
    [InlineData(85, 90, "B")]
    [InlineData(65, 90, "C")]
    [InlineData(95, 65, "B")]
    [InlineData(95, 55, "F")]
    [InlineData(65, 55, "F")]
    [InlineData(55, 90, "F")]
    public void GradeCalculator_WithScoreWithAttendance_ReturnGrade(int score, int attendancePercentage, string expected)
    {
        _calculator.Score = score;
        _calculator.AttendancePercentage = attendancePercentage;

        _calculator.GetGrade().Should().Be(expected);
    }
}