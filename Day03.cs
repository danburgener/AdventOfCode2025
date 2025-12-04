namespace AdventOfCode2025
{
    public class Day03 : IDay
    {
        private readonly string _fileDayName = "Three";
        public string GetName() => "Day 03";

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            long currentSum = 0;
            foreach (var line in data)
            {
                currentSum += FindLargestNumber(line.Select(l => int.Parse(l.ToString())).ToArray());
            }
            return currentSum;
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            long currentSum = 0;
            foreach (var line in data)
            {
                currentSum += FindLargestNumberV2(line.Select(l => int.Parse(l.ToString())).ToArray());
            }
            return currentSum;
        }

        private int FindLargestNumber(int[] numbers)
        {
            int largestNumber = 0;
            for (var i = 0; i < numbers.Length-1; i++)
            {
                for(var j = i+1; j < numbers.Length; j++)
                {
                    int currentNumber = int.Parse(numbers[i].ToString() + numbers[j].ToString());
                    if (currentNumber > largestNumber)
                    {
                        largestNumber = currentNumber;
                    }
                }
            }
            return largestNumber;
        }

        private long FindLargestNumberV2(int[] numbers)
        {
            const int desiredLength = 12;
            Stack<int> numbersStack = new();
            numbersStack.Push(numbers[0]);
            int remainingToRemove = numbers.Length - desiredLength;
            for(var i = 1; i < numbers.Length; i++)
            {
                if (remainingToRemove > 0)
                {
                    while (numbersStack.Count > 0 && numbers[i] > numbersStack.First() && remainingToRemove > 0)
                    {
                        numbersStack.Pop();
                        remainingToRemove--;
                    }
                }
                numbersStack.Push(numbers[i]);
            }
            return long.Parse(string.Join("", numbersStack.Reverse().Take(desiredLength)));
        }
    }
}
