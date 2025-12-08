namespace AdventOfCode2025
{
    public class Day07 : IDay
    {
        private readonly string _fileDayName = "Seven";
        public string GetName() => "Day 07";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<int> indexOfSplits = new();
            var startingPosition = data[0].IndexOf('S');
            indexOfSplits.Add(startingPosition);
            int splits = 0;
            for (var i = 1; i < data.Length; i++)
            {
                List<int> newIndexOfSplits = new();
                foreach (var indexOfSplit in indexOfSplits)
                {
                    if (data[i][indexOfSplit] == '.')
                    {
                        newIndexOfSplits.Add(indexOfSplit);
                    }
                    else
                    {
                        splits++;
                        if (data[i][indexOfSplit - 1] == '.')
                        {
                            newIndexOfSplits.Add(indexOfSplit - 1);
                        }
                        if (data[i][indexOfSplit + 1] == '.')
                        {
                            newIndexOfSplits.Add(indexOfSplit + 1);
                        }
                        newIndexOfSplits.Remove(indexOfSplit);
                    }
                }
                indexOfSplits = newIndexOfSplits.Distinct().ToList();
            }
            return splits;
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<int> indexOfSplits = new();
            var startingPosition = data[0].IndexOf('S');
            indexOfSplits.Add(startingPosition);
            List<List<long>> timelineIndexes = new();
            for (var i = 0; i <= data.Length; i++)
            {
                timelineIndexes.Add(Enumerable.Repeat(0L, data[0].Length).ToList());
            }
            timelineIndexes[1][startingPosition] = 1;
            for (var i = 1; i < data.Length; i++)
            {
                List<int> newIndexOfSplits = new();
                foreach (var indexOfSplit in indexOfSplits)
                {
                    if (data[i][indexOfSplit] == '.')
                    {
                        newIndexOfSplits.Add(indexOfSplit);
                        timelineIndexes[i + 1][indexOfSplit] += timelineIndexes[i][indexOfSplit];
                    }
                    else
                    {
                        if (data[i][indexOfSplit - 1] == '.')
                        {
                            newIndexOfSplits.Add(indexOfSplit - 1);
                            timelineIndexes[i + 1][indexOfSplit - 1] += timelineIndexes[i][indexOfSplit];
                        }
                        if (data[i][indexOfSplit + 1] == '.')
                        {
                            newIndexOfSplits.Add(indexOfSplit + 1);
                            timelineIndexes[i + 1][indexOfSplit + 1] += timelineIndexes[i][indexOfSplit];
                        }
                        newIndexOfSplits.Remove(indexOfSplit);
                    }
                }
                indexOfSplits = newIndexOfSplits.Distinct().ToList();
            }
            return timelineIndexes.Last().Sum();
        }
    }
}
