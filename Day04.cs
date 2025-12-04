
namespace AdventOfCode2025
{
    public class Day04 : IDay
    {
        private readonly string _fileDayName = "Four";
        public string GetName() => "Day 04";

        const char Paper = '@';
        const char Empty = '.';

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<List<char>> grid = new();
            foreach (var line in data)
            {
                grid.Add(line.ToList());
            }
            int accessiblePaper = 0;
            for (var row = 0; row < grid.Count(); row++)
            {
                for(var column = 0; column < grid[0].Count(); column++)
                {
                    var currentItem = grid[row][column];
                    if (currentItem == Paper)
                    {
                        if (CanBeAccessed(row, column, grid))
                        {
                            accessiblePaper++;
                        }
                    }
                }
            }
            return accessiblePaper;
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<List<char>> grid = new();
            foreach (var line in data)
            {
                grid.Add(line.ToList());
            }
            int accessiblePaper = 0;
            while (true) {
                List<(int row, int column)> papersToRemove = new();
                for (var row = 0; row < grid.Count(); row++)
                {
                    for (var column = 0; column < grid[0].Count(); column++)
                    {
                        var currentItem = grid[row][column];
                        if (currentItem == Paper)
                        {
                            if (CanBeAccessed(row, column, grid))
                            {
                                papersToRemove.Add((row, column));
                                accessiblePaper++;
                            }
                        }
                    }
                }
                if (papersToRemove.Count == 0)
                {
                    break;
                }
                else
                {
                    foreach (var paper in papersToRemove)
                    {
                        grid[paper.row][paper.column] = Empty;
                    }
                }
            }
            return accessiblePaper;
        }

        private bool CanBeAccessed(int row, int column, List<List<char>> grid)
        {
            int paperAround = 0;
            //LEFT
            if (column > 0)
            {
                if (grid[row][column - 1] == Paper)
                {
                    paperAround++;
                }
            }
            //RIGHT
            if (column < grid[row].Count()-1)
            {
                if (grid[row][column + 1] == Paper)
                {
                    paperAround++;
                }
            }
            //UP
            if(row > 0)
            {
                if (grid[row - 1][column] == Paper)
                {
                    paperAround++;
                }
            }
            //DOWN
            if (row < grid.Count() - 1)
            {
                if (grid[row + 1][column] == Paper)
                {
                    paperAround++;
                }
            }
            //TOPLEFT
            if (column > 0 && row > 0)
            {
                if (grid[row - 1][column - 1] == Paper)
                {
                    paperAround++;
                }
            }
            //TOPRIGHT
            if (column < grid[row].Count() - 1 && row > 0)
            {
                if (grid[row - 1][column + 1] == Paper)
                {
                    paperAround++;
                }
            }
            //BOTTOMLEFT
            if (column > 0 && row < grid.Count()-1)
            {
                if (grid[row + 1][column - 1] == Paper)
                {
                    paperAround++;
                }
            }
            //BOTTOMRIGHT
            if (column < grid[row].Count() - 1 && row < grid.Count() - 1)
            {
                if (grid[row + 1][column + 1] == Paper)
                {
                    paperAround++;
                }
            }
            return paperAround < 4;
        }
    }
}
