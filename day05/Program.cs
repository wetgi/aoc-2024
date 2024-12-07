string[] input = File.ReadAllLines("input.txt");

int[][] rules = input.TakeWhile(x => !string.IsNullOrEmpty(x))
                     .Select(r => r.Split('|').Select(int.Parse).ToArray())
                     .ToArray();

int[][] pages = input.Skip(rules.Length + 1)
                     .Select(s => s.Split(',').Select(int.Parse).ToArray())
                     .ToArray();

var middlePageNumbersOfCorrectPages = 0;
var middlePageNumbersOfInvalidPages = 0;

int[][] getPairs(int[] page)
{

    return page.Skip(1)
               .Select((x, index) => new[] { page[index], x })
               .ToArray();
}

int[] getInvalidPairIndices(int[] page, int[][] pairs)
{
    var invalidPairIndices = new List<int>();
    for (int i = 0; i < pairs.Length; i++)
    {
        var pair = pairs[i];
        if (!rules.Any(rule => rule.SequenceEqual(pair)))
        {
            invalidPairIndices.Add(i);
        }
    }
    return [.. invalidPairIndices];
}

int[] flipPairs(int[][] pairs, int[] invalidPairIndices)
{
    int[] newPage = pairs.Select(x => x[0]).Concat([pairs[^1][1]]).ToArray();

    foreach (var index in invalidPairIndices)
    {
        (newPage[index], newPage[index + 1]) = (newPage[index + 1], newPage[index]);
    }

    return newPage;
}

foreach (var page in pages)
{
    var pairs = getPairs(page);
    var invalidPairIndices = getInvalidPairIndices(page, pairs);

    if (invalidPairIndices.Length == 0)
    {
        var middle = pairs[pairs.Length / 2];
        middlePageNumbersOfCorrectPages += middle[0];
    }
    else
    {
        while (invalidPairIndices.Length > 0)
        {
            var updatedPage = flipPairs(pairs, invalidPairIndices);
            pairs = getPairs(updatedPage);
            invalidPairIndices = getInvalidPairIndices(updatedPage, pairs);
        }
        var middle = pairs[pairs.Length / 2];
        middlePageNumbersOfInvalidPages += middle[0];
    }
}

Console.WriteLine(middlePageNumbersOfCorrectPages);
Console.WriteLine(middlePageNumbersOfInvalidPages);