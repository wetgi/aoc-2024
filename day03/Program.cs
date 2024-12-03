using System.Text.RegularExpressions;

string filePath = "input.txt";

string mulRegex = @"mul\((\d+),(\d+)\)";
string instructionRegex = @"mul\((\d+),(\d+)\)|don't\(\)|do\(\)";


var memory = File.ReadAllText(filePath);

int Solve1(string input)
{
    var total = 0;
    foreach (Match m in Regex.Matches(input, mulRegex, RegexOptions.Multiline))
    {
        total += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
    }
    return total;
}

int Solve2(string input)
{
    var total = 0;
    bool isEnabled = true;
    foreach (Match m in Regex.Matches(input, instructionRegex, RegexOptions.Multiline))
    {
        if (m.Value == "don't()")
        {
            isEnabled = false;
        }
        else if (m.Value == "do()")
        {
            isEnabled = true;
        }
        else if (isEnabled)
        {
            total += int.Parse(m.Groups[1].Value) * int.Parse(m.Groups[2].Value);
        }
    }
    return total;
}

Console.WriteLine(Solve1(memory));
Console.WriteLine(Solve2(memory));