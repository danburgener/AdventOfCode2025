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
            List<Point> redTiles = [];
            foreach (var line in data)
            {
                var pointData = line.Split(',', StringSplitOptions.TrimEntries);
                var point = new Point(long.Parse(pointData.ElementAt(1)), long.Parse(pointData.ElementAt(0)), '#');
                redTiles.Add(point);
            }

            var polyCoordinates = redTiles.Select(p => new NetTopologySuite.Geometries.Coordinate(p.Column, p.Row)).ToList();
            polyCoordinates.Add(new NetTopologySuite.Geometries.Coordinate(redTiles[0].Column, redTiles[0].Row)); //Close loop
            var geometry = new NetTopologySuite.Geometries.GeometryFactory().CreatePolygon(polyCoordinates.ToArray());
            long largestArea = 0;
            foreach (var point in redTiles)
            {
                var largestAreaFromPointData = point.GetAreaOfLargestV2(redTiles.Where(p => p != point).ToList(), geometry);
                if (largestAreaFromPointData.Item1 > largestArea)
                {
                    largestArea = largestAreaFromPointData.Item1;
                }
            }
            return largestArea;
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

        public (long, Point?) GetAreaOfLargestV2(List<Point> points, NetTopologySuite.Geometries.Geometry containingGeometry)
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
                    NetTopologySuite.Geometries.Geometry rectangle = new NetTopologySuite.Geometries.GeometryFactory().CreatePolygon(new NetTopologySuite.Geometries.Coordinate[]
                    {
                        new(Column, Row),
                        new(Column, point.Row),
                        new(point.Column, point.Row),
                        new(point.Column, Row),
                        new(Column, Row)
                    });

                    if (containingGeometry.Covers(rectangle))
                    {
                        largestArea = area;
                        largestAreaPoint = point;
                    }
                }
            }
            return (largestArea, largestAreaPoint);
        }
    }
}
