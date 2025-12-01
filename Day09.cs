namespace AdventOfCode2025
{
    public class Day09 : IDay
    {
        private readonly string _fileDayName = "Nine";
        public string GetName() => "Day 09";

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
