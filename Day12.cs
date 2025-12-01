namespace AdventOfCode2025
{
    public class Day12 : IDay
    {
        private readonly string _fileDayName = "Twelve";
        public string GetName() => "Day 12";

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
