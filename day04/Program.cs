using System.Text.RegularExpressions;

string[] puzzle = File.ReadAllLines("input.txt");

const string pattern1 = "XMAS";
const string pattern2 = "SAMX";

string[] ExtractSubmatrix(int x, int y)
{
    string[] matrix = new string[4];
    for (int i = 0; i < 4; i++)
    {
        matrix[i] = "0000";
    }

    for (int i = 0; i < 4; i++)
    {
        if (y + i >= 0 && y + i < puzzle.Length)
        {
            for (int j = 0; j < 4; j++)
            {
                if (x + j >= 0 && x + j < puzzle[y + i].Length)
                {
                    matrix[i] = matrix[i].Substring(0, j) + puzzle[y + i][x + j] + matrix[i].Substring(j + 1);
                }
            }
        }
    }

    return matrix;
}

int CountPatternsInMatrix(string[] matrix, int row, int col)
{
    int count = 0;

    // horizontal
    if (row % 4 == 0)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            count += Regex.Matches(matrix[i], pattern1).Count;
            count += Regex.Matches(matrix[i], pattern2).Count;
        }
    }

    // vertical
    if (col % 4 == 0)
    {
        for (int i = 0; i < matrix[0].Length; i++)
        {
            string vertical = "";
            for (int j = 0; j < matrix.Length; j++)
            {
                vertical += matrix[j][i];
            }

            count += Regex.Matches(vertical, pattern1).Count;
            count += Regex.Matches(vertical, pattern2).Count;
        }
    }

    // diagonal
    string diagonal1 = matrix[0][0].ToString() + matrix[1][1] + matrix[2][2] + matrix[3][3];
    string diagonal2 = matrix[0][3].ToString() + matrix[1][2] + matrix[2][1] + matrix[3][0];
    count += Regex.Matches(diagonal1, pattern1).Count;
    count += Regex.Matches(diagonal1, pattern2).Count;
    count += Regex.Matches(diagonal2, pattern1).Count;
    count += Regex.Matches(diagonal2, pattern2).Count;

    return count;
}


// for every position in the puzzle, get the matrix and count the patterns
int total = puzzle.SelectMany((line, y) => line.Select((_, x) => new { x, y }))
                  .Sum(pos => CountPatternsInMatrix(ExtractSubmatrix(pos.x, pos.y), pos.y, pos.x));

Console.WriteLine(total);