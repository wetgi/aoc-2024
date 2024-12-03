string filePath = "input.txt";

var safe = File.ReadLines(filePath)
    .Select(l => l.Split().Select(int.Parse))
    .Where(x => IsSafe2(x.ToArray())).Count();

Console.WriteLine(safe);


static bool IsValid(int a, int b) => a - b is >= 1 and <= 3;

static bool IsSafe(int[] levels)
{
    return levels.Window(2).All(w => IsValid(w[0], w[1])) || levels.Window(2).All(w => IsValid(w[1], w[0]));
}

static bool IsSafe2(int[] levels)
{
    for (int i = 0; i < levels.Length; i++)
    {
        var modifiedLevels = levels.Take(i).Concat(levels.Skip(i + 1)).ToArray();
        if (IsSafe(modifiedLevels))
        {
            return true;
        }
    }
    return false;
}
