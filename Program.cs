using AdventOfCode2025;

IDay dayToRun = new Day02();

Console.WriteLine(dayToRun.GetName());
System.Diagnostics.Stopwatch stopwatch = new();
stopwatch.Start();
Console.WriteLine($"One: {await dayToRun.One()}");
stopwatch.Stop();
Console.WriteLine($"One Time: {stopwatch.ElapsedMilliseconds}");
stopwatch.Restart();
Console.WriteLine($"Two: {await dayToRun.Two()}");
stopwatch.Stop();
Console.WriteLine($"Two Time: {stopwatch.ElapsedMilliseconds}");