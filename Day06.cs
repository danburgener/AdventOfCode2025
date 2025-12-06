namespace AdventOfCode2025
{
    public class Day06 : IDay
    {
        private readonly string _fileDayName = "Six";
        public string GetName() => "Day 06";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<MathData> mathData = new();
            var mathsToAdd = data[0].Split(' ').Count();
            for (int i = 0; i < mathsToAdd; i++)
            {
                mathData.Add(new MathData());
            }

            foreach (var line in data)
            {
                var segments = line.Split(' ', StringSplitOptions.TrimEntries).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();
                if (long.TryParse(segments[0], out _))
                {
                    for (int i = 0; i < segments.Count; i++)
                    {
                        mathData[i].Values.Add(long.Parse(segments[i]));
                    }
                }
                else
                {
                    for (int i = 0; i < segments.Count; i++)
                    {
                        mathData[i].Operation = segments[i];
                    }
                }

            }
            return mathData.Sum(m => m.CalculateValue());
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<MathDataTwo> mathData = new();

            List<int> emptyColumns = new();
            foreach (var line in data)
            {
                var emptyIndexes = line.Select((c, index) => new { Character = c, Index = index })
                    .Where(item => item.Character == ' ')
                    .Select(item => item.Index);
                if (!emptyColumns.Any())
                {
                    emptyColumns.AddRange(emptyIndexes);
                }
                else
                {
                    emptyColumns = emptyColumns.Where(c => emptyIndexes.Contains(c)).ToList();
                }

            }
            for (var i = 0; i <= emptyColumns.Count; i++)
            {
                mathData.Add(new MathDataTwo());
            }
            foreach (var line in data)
            {
                if (line[0] == '*' || line[0] == '+')
                {
                    var splitOperations = line.Split(' ', StringSplitOptions.TrimEntries).Where(l => !string.IsNullOrWhiteSpace(l));
                    for (var i = 0; i < splitOperations.Count(); i++)
                    {
                        mathData[i].Operation = splitOperations.ElementAt(i);
                    }
                }
                else
                {
                    for (var i = 0; i <= emptyColumns.Count; i++)
                    {
                        if (i == 0)
                        {
                            mathData[0].Values.Add(line[..emptyColumns[0]]);
                        }
                        else if (i == emptyColumns.Count)
                        {
                            mathData[i].Values.Add(line[(emptyColumns[i - 1] + 1)..]);
                        }
                        else
                        {
                            mathData[i].Values.Add(line[(emptyColumns[i - 1] + 1)..emptyColumns[i]]);
                        }
                    }
                }
            }
            return mathData.Sum(m => m.CalculateCephalopodValue());
        }
    }

    public class MathData
    {
        public List<long> Values { get; set; } = new();
        public string Operation { get; set; }

        public long CalculateValue()
        {
            switch (Operation)
            {
                case "*":
                    return Values.Aggregate(1L, (currentProduct, number) => currentProduct * number);
                case "+":
                    return Values.Sum();
                default:
                    return 0;
            }
        }


    }

    public class MathDataTwo
    {
        public List<string> Values { get; set; } = new();

        public string Operation { get; set; }

        private int MostDigits()
        {
            int mostDigits = 0;
            foreach (var value in Values)
            {
                int valueLength = value.ToString().Length;
                if (valueLength > mostDigits)
                {
                    mostDigits = valueLength;
                }
            }
            return mostDigits;
        }

        public long CalculateCephalopodValue()
        {
            int mostDigits = MostDigits();
            List<List<char>> newValues = new();
            for (var i = 0; i < mostDigits; i++)
            {
                newValues.Add(new());
            }
            for (var j = 0; j < Values.Count; j++)
            {
                IEnumerable<char> valueString = Values[j].ToString().Reverse();
                for (var i = 0; i < valueString.Count(); i++)
                {
                    newValues[i].Add(valueString.ElementAt(i));
                }
            }

            List<long> newNewValues = new();
            foreach (var value in newValues)
            {
                newNewValues.Add(long.Parse(string.Join("", value.Where(v => v != ' '))));
            }
            switch (Operation)
            {
                case "*":
                    return newNewValues.Aggregate(1L, (currentProduct, number) => currentProduct * number);
                case "+":
                    return newNewValues.Sum();
                default:
                    return 0;
            }
        }
    }
}
