using System.Text.RegularExpressions;

var puzzle = File.ReadAllLines("input-1.txt");

const string match1 = "XMAS";
const string match2 = "SAMX";

int patternLength = match1.Length - 1;


var xmasCount = 0;
var samxCount = 0;

// Helper function to count matches in a sequence of lines
int CountMatches(IEnumerable<string> lines, string pattern) =>
    lines.Sum(line => Regex.Matches(line, pattern).Count);

// Horizontal check
xmasCount += CountMatches(puzzle, match1);
samxCount += CountMatches(puzzle, match2);

// Vertical check
var verticalLines = Enumerable.Range(0, puzzle[0].Length)
    .Select(i => new string(puzzle.Select(line => line[i]).ToArray()));
xmasCount += CountMatches(verticalLines, match1);
samxCount += CountMatches(verticalLines, match2);

// Diagonal checks
var diagonals = new List<string>();

// Top left to bottom right
for (var i = 0; i < puzzle.Length - patternLength; i++)
    diagonals.Add(new string(puzzle.Select((x, j) => j + i < puzzle.Length ? x[j + i] : ' ').ToArray()));

// Bottom left to top right
for (var i = 1; i < puzzle.Length - patternLength; i++)
    diagonals.Add(new string(puzzle.Select((x, j) => j - i >= 0 ? x[j - i] : ' ').ToArray()));

// Top right to bottom left
for (var i = 0; i < puzzle.Length - patternLength; i++)
    diagonals.Add(new string(puzzle.Select((x, j) => j - i >= 0 ? x[puzzle.Length - 1 - j + i] : ' ').ToArray()));

// Bottom right to top left
for (var i = 1; i < puzzle.Length - patternLength; i++)
    diagonals.Add(new string(puzzle.Select((x, j) => j + i < puzzle.Length ? x[puzzle.Length - 1 - j - i] : ' ').ToArray()));

xmasCount += CountMatches(diagonals, match1);
samxCount += CountMatches(diagonals, match2);

Console.WriteLine($"XMAS: {xmasCount + samxCount}");