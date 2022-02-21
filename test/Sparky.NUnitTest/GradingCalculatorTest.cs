using NUnit.Framework;

namespace Sparky.NUnitTest;

[TestFixture]
public class GradingCalculatorTest
{
    private GradingCalculator _calculator = null!;

    [SetUp]
    public void Setup()
    {
        _calculator = new GradingCalculator();
    }

    [Test]
    public void GradeCalculator_WithScore95AndWith90Attendance_ReturnAGrade()
    {
        _calculator.Score = 95;
        _calculator.AttendancePercentage = 90;

        Assert.That(_calculator.GetGrade(), Is.EqualTo("A"));
    }

    [Test]
    [TestCase(95, 90, ExpectedResult = "A")]
    [TestCase(85, 90, ExpectedResult = "B")]
    [TestCase(65, 90, ExpectedResult = "C")]
    [TestCase(95, 65, ExpectedResult = "B")]
    [TestCase(95, 55, ExpectedResult = "F")]
    [TestCase(65, 55, ExpectedResult = "F")]
    [TestCase(55, 90, ExpectedResult = "F")]
    public string GradeCalculator_WithScoreWithAttendance_ReturnGrade(int score, int attendancePercentage)
    {
        _calculator.Score = score;
        _calculator.AttendancePercentage = attendancePercentage;

        return _calculator.GetGrade();
    }
}