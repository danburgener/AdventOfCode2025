namespace AdventOfCode2025
{
    public class Day02 : IDay
    {
        private readonly string _fileDayName = "Two";
        public string GetName() => "Day 02";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<long> invalidIds = new();
            foreach(var item in data[0].Split(","))
            {
                var splitItem = item.Split("-");
                var low = long.Parse(splitItem[0]);
                var high = long.Parse(splitItem[1]);
                invalidIds.AddRange(GetInvalidIds(low, high));
            }
            return invalidIds.Sum();
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<long> invalidIds = new();
            foreach (var item in data[0].Split(","))
            {
                var splitItem = item.Split("-");
                var low = long.Parse(splitItem[0]);
                var high = long.Parse(splitItem[1]);
                invalidIds.AddRange(GetInvalidIdsV2(low, high));
            }
            return invalidIds.Sum();
        }

        private List<long> GetInvalidIds(long low, long high)
        {
            List<long> invalidIds = [];
            for (long currentNumber = low; currentNumber <= high; currentNumber++)
            {
                string currentNumberString = currentNumber.ToString();
                int length = currentNumberString.Length;
                if (length % 2 == 0)
                {
                    int halfLength = length / 2;
                    if (currentNumberString[..halfLength] == currentNumberString[halfLength..])
                    {
                        invalidIds.Add(currentNumber);
                    }
                }
            }
            return invalidIds;
        }

        private List<long> GetInvalidIdsV2(long low, long high)
        {
            List<long> invalidIds = [];
            for (long currentNumber = low; currentNumber <= high; currentNumber++)
            {
                string currentNumberString = currentNumber.ToString();
                int length = currentNumberString.Length;
                for (var i = 1; i < length; i++)
                {
                    var chunks = currentNumberString.Chunk(i);
                    var stringChunks = chunks.Select(c => int.Parse(c));
                    if (stringChunks.Distinct().Count() == 1)
                    {
                        invalidIds.Add(currentNumber);
                        break;
                    }
                }
            }
            return invalidIds;
        }
    }
}
