namespace AdventOfCode2025
{
    public class Day05 : IDay
    {
        private readonly string _fileDayName = "Five";
        public string GetName() => "Day 05";

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
