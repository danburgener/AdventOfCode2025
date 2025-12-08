namespace AdventOfCode2025
{
    public class Day08 : IDay
    {
        private readonly string _fileDayName = "Eight";
        public string GetName() => "Day 08";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<JunctionBox> junctionBoxes = new();
            foreach (var line in data)
            {
                var parts = line.Split(',');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int x) &&
                    int.TryParse(parts[1], out int y) &&
                    int.TryParse(parts[2], out int z))
                {
                    junctionBoxes.Add(new JunctionBox(x, y, z));
                }
            }

            List<JunctionDistance> distances = new();
            for (int i = 0; i < junctionBoxes.Count; i++)
            {
                for (int j = i + 1; j < junctionBoxes.Count; j++)
                {
                    distances.Add(new JunctionDistance(junctionBoxes[i], junctionBoxes[j]));
                }
            }
            var orderedDistances = distances.OrderBy(d => d.Distance).Take(1000);
            List<List<JunctionBox>> closestGroups = new();
            foreach (var junctionBox in junctionBoxes)
            {
                closestGroups.Add([junctionBox]);
            }
            foreach (var ordered in orderedDistances)
            {
                var groupOne = closestGroups.First(g => g.Contains(ordered.BoxOne));
                var groupTwo = closestGroups.First(g => g.Contains(ordered.BoxTwo));
                if (groupOne != groupTwo)
                {
                    groupOne.AddRange(groupTwo);
                    closestGroups.Remove(groupTwo);
                }
            }
            var top3 = closestGroups.OrderByDescending(g => g.Count).Take(3);
            int total = 1;
            foreach (var group in top3)
            {
                total *= group.Count;
            }
            return total;
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<JunctionBox> junctionBoxes = new();
            foreach (var line in data)
            {
                var parts = line.Split(',');
                if (parts.Length == 3 &&
                    int.TryParse(parts[0], out int x) &&
                    int.TryParse(parts[1], out int y) &&
                    int.TryParse(parts[2], out int z))
                {
                    junctionBoxes.Add(new JunctionBox(x, y, z));
                }
            }

            List<JunctionDistance> distances = new();
            for (int i = 0; i < junctionBoxes.Count; i++)
            {
                for (int j = i + 1; j < junctionBoxes.Count; j++)
                {
                    distances.Add(new JunctionDistance(junctionBoxes[i], junctionBoxes[j]));
                }
            }
            var orderedDistances = distances.OrderBy(d => d.Distance);
            List<List<JunctionBox>> closestGroups = new();
            foreach (var junctionBox in junctionBoxes)
            {
                closestGroups.Add([junctionBox]);
            }
            while (closestGroups.Count > 1)
            {
                foreach (var ordered in orderedDistances)
                {
                    var groupOne = closestGroups.First(g => g.Contains(ordered.BoxOne));
                    var groupTwo = closestGroups.First(g => g.Contains(ordered.BoxTwo));
                    if (groupOne != groupTwo)
                    {
                        groupOne.AddRange(groupTwo);
                        closestGroups.Remove(groupTwo);
                    }
                    if (closestGroups.Count == 1)
                    {
                        return ordered.BoxOne.X * ordered.BoxTwo.X;
                    }
                }
            }
            return 0;
        }
    }

    public class JunctionDistance
    {
        public JunctionBox BoxOne { get; set; }
        public JunctionBox BoxTwo { get; set; }
        public double Distance { get; set; }
        public JunctionDistance(JunctionBox boxOne, JunctionBox boxTwo)
        {
            BoxOne = boxOne;
            BoxTwo = boxTwo;
            Distance = CalculateEuclieanDistance(boxOne, boxTwo);
        }

        private double CalculateEuclieanDistance(JunctionBox one, JunctionBox two)
        {
            long deltaX = two.X - one.X;
            long deltaY = two.Y - one.Y;
            long deltaZ = two.Z - one.Z;

            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY + deltaZ * deltaZ);
        }
    }

    public class JunctionBox
    {
        public long X { get; set; }
        public long Y { get; set; }
        public long Z { get; set; }
        public JunctionBox(long x, long y, long z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
