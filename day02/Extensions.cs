public static class Extensions
{
    public static IEnumerable<int[]> Window(this IEnumerable<int> source, int size)
    {
        var arr = source.ToArray();
        for (int i = 0; i < arr.Length - size + 1; i++)
        {
            yield return arr.Skip(i).Take(size).ToArray();
        }
    }
}
