namespace AdventOfCode2025
{
    public class Day11 : IDay
    {
        private readonly string _fileDayName = "Eleven";
        public string GetName() => "Day 11";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            Dictionary<string, List<string>> connections = new();
            foreach (var line in data)
            {
                var splitLine = line.Split(": ");
                connections.Add(splitLine[0], splitLine[1].Split(' ').ToList());
            }
            return GetNumberOfPaths("you", "out", connections);
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            Dictionary<string, List<string>> connections = new();
            foreach (var line in data)
            {
                var splitLine = line.Split(": ");
                connections.Add(splitLine[0], splitLine[1].Split(' ').ToList());
            }
            return GetNumberOfPathsV2("svr", "out", connections, false, false);
        }

        private long GetNumberOfPaths(string current, string ending, Dictionary<string, List<string>> connections)
        {
            if (current == ending)
            {
                return 1;
            }
            var pathsFromCurrent = connections[current];
            long pathCounts = 0;
            foreach (var connection in pathsFromCurrent)
            {
                pathCounts += GetNumberOfPaths(connection, ending, connections);
            }
            return pathCounts;
        }

        private long GetNumberOfPathsV2(string current, string ending, Dictionary<string, List<string>> connections, bool visitedDac, bool visitedFft)
        {
            if (current == ending)
            {
                return visitedDac && visitedFft ? 1 : 0;
            }
            if (current == "dac")
            {
                visitedDac = true;
            }
            else if (current == "fft")
            {
                visitedFft = true;
            }
            var pathsFromCurrent = connections[current];
            long pathCounts = 0;
            foreach (var connection in pathsFromCurrent)
            {
                pathCounts += GetNumberOfPathsV2(connection, ending, connections, visitedDac, visitedFft);
            }
            return pathCounts;
        }
    }
}
