namespace AdventOfCode2025
{
    public class Day10 : IDay
    {
        private readonly string _fileDayName = "Ten";
        public string GetName() => "Day 10";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<Machine> machines = new List<Machine>();
            foreach (var line in data)
            {
                var machine = new Machine(line);
                machines.Add(machine);
            }
            long fewestForAll = 0;
            foreach (var machine in machines)
            {
                fewestForAll += machine.GetMinPressesForLightDiagram();
            }
            return fewestForAll;
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<Machine> machines = new List<Machine>();
            foreach (var line in data)
            {
                var machine = new Machine(line);
                machines.Add(machine);
            }
            long fewestForAll = 0;
            foreach (var machine in machines)
            {
                fewestForAll += machine.GetMinPressesForJoltage();
            }
            return fewestForAll;
        }
    }

    public class Machine
    {
        public List<bool> LightDiagram { get; set; }
        public Dictionary<int, List<int>> Buttons { get; set; }
        public List<long> JoltageRequirements { get; set; }

        public Machine(string input)
        {
            var firstSplit = input.Split(']');
            LightDiagram = firstSplit[0].TrimStart('[').Select(c => c == '#').ToList();
            var secondSplit = firstSplit[1].Split('{');
            JoltageRequirements = secondSplit[1].Trim().TrimEnd('}').Split(',').Select(long.Parse).ToList();
            var thirdSplit = secondSplit[0].Trim().Split(' ');
            Buttons = new Dictionary<int, List<int>>();
            for (var i = 0; i < thirdSplit.Length; i++)
            {
                var trimmedData = thirdSplit[i].TrimStart('(').TrimEnd(')');
                Buttons[i] = trimmedData.Split(',').Select(int.Parse).ToList();
            }
        }

        public long GetMinPressesForLightDiagram()
        {
            List<bool> currentLights = Enumerable.Repeat(false, LightDiagram.Count).ToList();
            for (var i = 1; i < 10; i++)
            {
                Combinatorics.Collections.Variations<int> variations = new Combinatorics.Collections.Variations<int>(Buttons.Keys.ToList(), i, Combinatorics.Collections.GenerateOption.WithoutRepetition);
                foreach (var variation in variations)
                {
                    List<bool> testLights = new List<bool>(currentLights);
                    foreach (var buttonIndex in variation)
                    {
                        foreach (var lightIndex in Buttons[buttonIndex])
                        {
                            testLights[lightIndex] = !testLights[lightIndex];
                        }
                    }
                    if (testLights.SequenceEqual(LightDiagram))
                    {
                        return i;
                    }
                }
            }

            throw new Exception("No solution found within reasonable button presses.");
        }

        public long GetMinPressesForJoltage()
        {
            List<long> currentCounters = Enumerable.Repeat(0L, JoltageRequirements.Count).ToList();
            for (var i = 1; i < 20; i++)
            {
                Combinatorics.Collections.Variations<int> variations = new Combinatorics.Collections.Variations<int>(Buttons.Keys.ToList(), i, Combinatorics.Collections.GenerateOption.WithRepetition);
                foreach (var variation in variations)
                {
                    List<long> testCounter = new List<long>(currentCounters);
                    foreach (var buttonIndex in variation)
                    {
                        foreach (var lightIndex in Buttons[buttonIndex])
                        {
                            testCounter[lightIndex]++;
                        }
                    }
                    if (testCounter.SequenceEqual(JoltageRequirements))
                    {
                        return i;
                    }
                    Console.WriteLine($"Tried variation {string.Join(",", variation)} and got {string.Join(",", testCounter)}");
                }
            }

            throw new Exception("No solution found within reasonable button presses.");
        }
    }
}
