namespace AdventOfCode2025
{
    public class Day09 : IDay
    {
        private readonly string _fileDayName = "Nine";
        public string GetName() => "Day 09";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            List<Point> points = new();
            long maxColumn = 0;
            long maxRow = 0;
            foreach (var line in data)
            {
                var pointData = line.Split(',', StringSplitOptions.TrimEntries);
                var point = new Point(long.Parse(pointData.ElementAt(1)), long.Parse(pointData.ElementAt(0)), '#');
                if (point.Column > maxColumn)
                {
                    maxColumn = point.Column;
                }
                if (point.Row > maxRow)
                {
                    maxRow = point.Row;
                }
                points.Add(point);
            }
            //PrintMap(maxRow, maxColumn, points);
            long largestArea = 0;
            foreach (var point in points)
            {
                var largestAreaFromPointData = point.GetAreaOfLargest(points.Where(p => p != point).ToList());
                if (largestAreaFromPointData.Item1 > largestArea)
                {
                    largestArea = largestAreaFromPointData.Item1;
                }
            }
            return largestArea;
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            List<Point> redTiles = new();
            List<Point> greenTiles = new();
            long maxColumn = 0;
            long maxRow = 0;
            long minRow = long.MaxValue;
            long minColumn = long.MaxValue;
            foreach (var line in data)
            {
                var pointData = line.Split(',', StringSplitOptions.TrimEntries);
                var point = new Point(long.Parse(pointData.ElementAt(1)), long.Parse(pointData.ElementAt(0)), '#');
                if (point.Column > maxColumn)
                {
                    maxColumn = point.Column;
                }
                if (point.Row > maxRow)
                {
                    maxRow = point.Row;
                }
                if (point.Column < minColumn)
                {
                    minColumn = point.Column;
                }
                if (point.Row < minRow)
                {
                    minRow = point.Row;
                }
                redTiles.Add(point);
            }
            //PrintMap(maxRow, maxColumn, points);
            for (var i = 0; i < redTiles.Count; i++)
            {
                if (i == 0)
                {
                    greenTiles.AddRange(GetListOfPointsBetweenTwoPoints(redTiles.Last(), redTiles.First()));
                    greenTiles.AddRange(GetListOfPointsBetweenTwoPoints(redTiles.First(), redTiles[1]));
                }
                else if (i == redTiles.Count - 1)
                {
                    continue;
                }
                else
                {
                    greenTiles.AddRange(GetListOfPointsBetweenTwoPoints(redTiles[i], redTiles[i + 1]));
                }
            }
            var polygon = redTiles.Concat(greenTiles).ToList();
            List<Point> pointsInPolygon = new();
            for (var row = minRow; row < maxRow; row++)
            {
                for (var column = minColumn; column < maxColumn; column++)
                {
                    var point = new Point(row, column, ' ');
                    if (Point.PointInPolygon(point, polygon))
                    {
                        pointsInPolygon.Add(point);
                    }
                }
            }
            //PrintMap(maxRow, maxColumn, redTiles.Concat(greenTiles).ToList());
            long largestArea = 0;
            foreach (var point in redTiles)
            {
                var largestAreaFromPointData = point.GetAreaOfLargestV2(redTiles.Where(p => p != point).ToList(), polygon);
                if (largestAreaFromPointData.Item1 > largestArea)
                {
                    largestArea = largestAreaFromPointData.Item1;
                }
            }
            return largestArea;
        }

        private List<Point> GetListOfPointsBetweenTwoPoints(Point left, Point right)
        {
            List<Point> points = new();
            if (left.Row == right.Row)
            {
                if (left.Column < right.Column)
                {
                    for (var column = left.Column + 1; column < right.Column; column++)
                    {
                        points.Add(new(left.Row, column, 'X'));
                    }
                }
                else
                {
                    for (var column = right.Column + 1; column < left.Column; column++)
                    {
                        points.Add(new(left.Row, column, 'X'));
                    }
                }
            }
            else
            {
                if (left.Row < right.Row)
                {
                    for (var row = left.Row + 1; row < right.Row; row++)
                    {
                        points.Add(new(row, left.Column, 'X'));
                    }
                }
                else
                {
                    for (var row = right.Row + 1; row < left.Row; row++)
                    {
                        points.Add(new(row, left.Column, 'X'));
                    }
                }
            }
            return points;
        }

        private void PrintMap(long rowCount, long columnCount, List<Point> points)
        {
            for (var row = 0; row <= rowCount; row++)
            {
                for (var column = 0; column <= columnCount; column++)
                {
                    var point = points.FirstOrDefault(p => p.Row == row && p.Column == column);
                    if (point is not null)
                    {
                        Console.Write(point.Character);
                    }
                    else
                    {
                        Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
        }
    }

    public class Point
    {
        public long Row { get; set; }
        public long Column { get; set; }
        public char Character { get; set; }
        public Point(long row, long column, char character)
        {
            Row = row;
            Column = column;
            Character = character;
        }

        public (long, Point?) GetAreaOfLargest(List<Point> points)
        {
            long largestArea = 0;
            Point? largestAreaPoint = null;
            foreach (var point in points)
            {
                long height = Math.Abs(Row - point.Row) + 1;
                long width = Math.Abs(Column - point.Column) + 1;
                long area = height * width;
                if (area > largestArea)
                {
                    largestArea = area;
                    largestAreaPoint = point;
                }
            }
            return (largestArea, largestAreaPoint);
        }

        public (long, Point?) GetAreaOfLargestV2(List<Point> points, List<Point> polygon)
        {
            long largestArea = 0;
            Point? largestAreaPoint = null;
            foreach (var point in points)
            {
                long height = Math.Abs(Row - point.Row) + 1;
                long width = Math.Abs(Column - point.Column) + 1;
                long area = height * width;
                if (area > largestArea)
                {
                    long minRow = Math.Min(Row, point.Row);
                    long minColumn = Math.Min(Column, point.Column);
                    long maxRow = Math.Max(Row, point.Row);
                    long maxColumn = Math.Max(Column, point.Column);
                    bool allInPolygon = true;
                    for (var row = minRow; row < maxRow; row++)
                    {
                        for (var column = minColumn; column < maxColumn; column++)
                        {
                            //if (!IsInPolygon(row, column, polygon))
                            //{
                            //    allInPolygon = false;
                            //    break;
                            //}
                            if (!PointInPolygon(new Point(row, column, '.'), polygon))
                            {
                                allInPolygon = false;
                                break;
                            }
                        }
                        if (!allInPolygon)
                        {
                            break;
                        }
                    }
                    if (allInPolygon)
                    {
                        largestArea = area;
                        largestAreaPoint = point;
                    }
                }
            }
            return (largestArea, largestAreaPoint);
        }

        private bool IsInPolygon(long row, long column, IEnumerable<Point> polygon)
        {
            bool result = false;
            var a = polygon.Last();
            foreach (var b in polygon)
            {
                if ((b.Column == column) && (b.Row == row))
                    return true;

                if ((b.Row == a.Row) && (row == a.Row))
                {
                    if ((a.Column <= column) && (column <= b.Column))
                        return true;

                    if ((b.Column <= column) && (column <= a.Column))
                        return true;
                }

                if ((b.Row < row) && (a.Row >= row) || (a.Row < row) && (b.Row >= row))
                {
                    if (b.Column + (row - b.Row) / (a.Row - b.Row) * (a.Column - b.Column) <= column)
                        result = !result;
                }
                a = b;
            }
            return result;
        }

        public static bool PointInPolygon(Point p, List<Point> poly)
        {
            int n = poly.Count;
            int wn = 0; // the winding number counter

            for (int i = 0; i < n; i++)
            {
                Point p1 = poly[i];
                Point p2 = poly[(i + 1) % n];

                if (p1.Row <= p.Row)
                {
                    if (p2.Row > p.Row && IsLeft(p1, p2, p) > 0)
                        wn++;
                }
                else
                {
                    if (p2.Row <= p.Row && IsLeft(p1, p2, p) < 0)
                        wn--;
                }
            }
            return wn != 0;
        }

        private static float IsLeft(Point P0, Point P1, Point P2)
        {
            return ((P1.Column - P0.Column) * (P2.Row - P0.Row) - (P2.Column - P0.Column) * (P1.Row - P0.Row));
        }
    }
}
