namespace AdventOfCode2025
{
    public static class Common
    {
        public static async Task<string[]> ReadFile(string day, string questionNumber)
        {
            return await File.ReadAllLinesAsync($"Files/Day{day}{questionNumber}Data.txt");
        }
    }
}
