using AdventOfCode2025;

IDay dayToRun = new Day01();

Console.WriteLine(dayToRun.GetName());
Console.WriteLine($"One: {await dayToRun.One()}");
Console.WriteLine($"Two: {await dayToRun.Two()}");