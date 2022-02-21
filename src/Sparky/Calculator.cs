namespace Sparky;

public class Calculator
{
    public static int Sum(params int[] args) => args.Sum();

    public static double Sum(params double[] args) => args.Sum();

    public static bool IsOdd(int args) => args % 2 != 0;

    public static IEnumerable<int> OddRange(int start, int stop) =>
        Enumerable.Range(start, stop - start + 1).Where(IsOdd);
}