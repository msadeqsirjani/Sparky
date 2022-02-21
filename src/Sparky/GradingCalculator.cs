namespace Sparky;

public class GradingCalculator
{
    public int Score { get; set; }
    public int AttendancePercentage { get; set; }

    public string GetGrade()
    {
        return Score <= 90 || AttendancePercentage <= 70
            ? Score <= 80 || AttendancePercentage <= 60
                ? Score > 60 && AttendancePercentage > 60
                    ? "C"
                    : "F"
                : "B"
            : "A";
    }
}