namespace AdventOfCode2025
{
    public class Day03 : IDay
    {
        private readonly string _fileDayName = "Three";
        public string GetName() => "Day 03";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            return data.Count();
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            return data.Count();
        }
    }
}
