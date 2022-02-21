namespace Sparky;

public class Fibonacci
{
    public int Range { get; set; }

    public List<int> GetRange()
    {
        List<int> collection = new();
        int first = 0, second = 1;

        if (Range == 1)
        {
            collection.Add(0);

            return collection;
        }
        else
        {
            collection.Add(0);
            collection.Add(1);

            for (var i = 2; i < Range; i++)
            {
                var current = first + second;

                collection.Add(current);

                first = second;
                second = current;
            }

            return collection;
        }
    }
}