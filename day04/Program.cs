string[] puzzle = File.ReadAllLines("input.txt");

string[] ExtractSubmatrix(int x, int y, int size = 4)
{
    string[] matrix = new string[size];
    for (int i = 0; i < size; i++)
    {
        matrix[i] = new string('0', size);
    }

    for (int i = 0; i < size; i++)
    {
        if (y + i >= 0 && y + i < puzzle.Length)
        {
            for (int j = 0; j < size; j++)
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
    string[] patterns = ["XMAS", "SAMX"];

    // horizontal
    if (row % 4 == 0)
    {
        count += matrix.Count(line => patterns.Contains(line));
    }

    // vertical
    if (col % 4 == 0)
    {
        for (int i = 0; i < matrix[0].Length; i++)
        {
            string vertical = new(matrix.Select(line => line[i]).ToArray());
            if (patterns.Contains(vertical))
            {
                count += 1;
            }
        }
    }

    // diagonal
    string[] diagonals = [
        new([matrix[0][0], matrix[1][1], matrix[2][2], matrix[3][3]]),
        new([matrix[0][3], matrix[1][2], matrix[2][1], matrix[3][0]])
    ];
    count += diagonals.Count(diagonal => patterns.Contains(diagonal));

    return count;
}

int CountMas(string[] matrix)
{
    int count = 0;
    string[] patterns = ["MAS", "SAM"];

    // diagonal
    string[] diagonals = [
        new([matrix[0][0], matrix[1][1], matrix[2][2]]),
        new([matrix[0][2], matrix[1][1], matrix[2][0]])
    ];


    // incremet count the two patterns are found in the diagonals
    if (diagonals.Count(diagonal => patterns.Contains(diagonal)) == 2)
    {
        count += 1;
    }

    return count;
}



// for every position in the puzzle, get the matrix and count the patterns
int partOne = puzzle.SelectMany((line, y) => line.Select((_, x) => new { x, y }))
                  .Sum(pos => CountPatternsInMatrix(ExtractSubmatrix(pos.x, pos.y), pos.y, pos.x));


int partTwo = puzzle.SelectMany((line, y) => line.Select((_, x) => new { x, y }))
                  .Sum(pos => CountMas(ExtractSubmatrix(pos.x, pos.y, 3)));



Console.WriteLine(partOne);
Console.WriteLine(partTwo);