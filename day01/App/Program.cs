class Program
{
    static void Main()
    {
        // --- Part One ---
        string filePath = "input.txt";

        var lines = File.ReadLines(filePath)
            .Select(line => line.Split(["   "], StringSplitOptions.None))
            .Where(numbers => numbers.Length == 2)
            .Select(numbers => (n1: int.Parse(numbers[0]), n2: int.Parse(numbers[1])))
            .ToList();


        var numbers1 = lines.Select(t => t.n1).OrderBy(n => n).ToList();
        var numbers2 = lines.Select(t => t.n2).OrderBy(n => n).ToList();

        int sum = numbers1.Zip(numbers2, (n1, n2) => Math.Abs(n1 - n2)).Sum();
        Console.WriteLine(sum);

        // --- Part Two ---

        var groupedByOccurrence = numbers2
            .GroupBy(n => n)
            .ToDictionary(g => g.Key, g => g.Count());

        var similarityScore = lines.Sum(pair => pair.n1 * groupedByOccurrence.GetValueOrDefault(pair.n1, 0));
        Console.WriteLine(similarityScore);
    }
}