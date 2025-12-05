namespace AdventOfCode2025
{
    public class Day05 : IDay
    {
        private readonly string _fileDayName = "Five";
        public string GetName() => "Day 05";

        const char RangeSeparator = '-';

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<KeyValuePair<long, long>> freshIngredientIdRanges = [];
            bool emptyLineFound = false;
            List<long> availableIngredientIds = [];
            foreach (var line in data)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    emptyLineFound = true;
                    continue;
                }
                if (emptyLineFound)
                {
                    availableIngredientIds.Add(long.Parse(line));
                }
                else
                {
                    var splitData = line.Split(RangeSeparator, StringSplitOptions.RemoveEmptyEntries);
                    freshIngredientIdRanges.Add(new KeyValuePair<long, long>(long.Parse(splitData[0]), long.Parse(splitData[1])));
                }
            }

            return GetFreshIngredientsCount(freshIngredientIdRanges, availableIngredientIds);
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<KeyValuePair<long, long>> freshIngredientIdRanges = new();
            foreach (var line in data)
            {
                var splitData = line.Split(RangeSeparator, StringSplitOptions.RemoveEmptyEntries);
                freshIngredientIdRanges.Add(new KeyValuePair<long, long>(long.Parse(splitData[0]), long.Parse(splitData[1])));
            }
            return GetAllNumbersInRanges(freshIngredientIdRanges);
        }

        private long GetFreshIngredientsCount(List<KeyValuePair<long, long>> freshIngredientIdRanges, List<long> availableIngredientIds)
        {
            long freshIngredientsCount = 0;
            foreach (var availableIngredientId in availableIngredientIds)
            {
                foreach (var range in freshIngredientIdRanges)
                {
                    if (availableIngredientId >= range.Key && availableIngredientId <= range.Value)
                    {
                        freshIngredientsCount++;
                        break;
                    }
                }
            }
            return freshIngredientsCount;
        }

        private long GetAllNumbersInRanges(List<KeyValuePair<long, long>> ranges)
        {
            long total = 0;
            bool mergedHappened;
            do
            {
                List<KeyValuePair<long, long>> mergedRanges = new();
                mergedHappened = false;
                foreach (var range in ranges)
                {
                    bool isMerged = false;
                    for (int i = 0; i < mergedRanges.Count; i++)
                    {
                        var mergedRange = mergedRanges[i];
                        if (!(range.Value < mergedRange.Key || range.Key > mergedRange.Value))
                        {
                            long newStart = Math.Min(range.Key, mergedRange.Key);
                            long newEnd = Math.Max(range.Value, mergedRange.Value);
                            mergedRanges[i] = new KeyValuePair<long, long>(newStart, newEnd);
                            isMerged = true;
                            mergedHappened = true;
                            break;
                        }
                    }
                    if (!isMerged)
                    {
                        mergedRanges.Add(range);
                    }
                }
                ranges = mergedRanges.ToList();
                mergedRanges.Clear();
            } while (mergedHappened);
            foreach (var mergedRange in ranges)
            {
                total += (mergedRange.Value - mergedRange.Key + 1);
            }
            return total;
        }
    }
}
