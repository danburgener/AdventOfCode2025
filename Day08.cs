namespace AdventOfCode2025
{
    public class Day08 : IDay
    {
        private readonly string _fileDayName = "Eight";
        public string GetName() => "Day 08";

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
