namespace AdventOfCode2025
{
    public class Day01 : IDay
    {
        private readonly string _fileDayName = "One";
        public string GetName() => "Day 01";

        const char Left = 'L';
        const char Right = 'R';

        const int MinPosition = 0;
        const int MaxPosition = 99;
        const int MaxNumbersInACycle = 100;

        public async Task<long> One()
        {
            var data = await Common.ReadFile(_fileDayName, "One");
            long currentPosition = 50;
            long zeroCycles = 0;
            foreach (var item in data)
            {
                char direction = item[0];
                long amountToMove = long.Parse(item[1..]);
                if (direction == Left)
                {
                    currentPosition = RotateLeft(currentPosition, amountToMove).Item1;
                }
                else if (direction == Right)
                {
                    currentPosition = RotateRight(currentPosition, amountToMove).Item1;
                }
                if (currentPosition == MinPosition)
                {
                    zeroCycles++;
                }
            }
            return zeroCycles;
        }

        public async Task<long> Two()
        {
            var data = await Common.ReadFile(_fileDayName, "Two");
            long currentPosition = 50;
            long zeroCycles = 0;
            foreach (var item in data)
            {
                char direction = item[0];
                long amountToMove = long.Parse(item[1..]);
                (long,int) rotationData;
                if (direction == Left)
                {
                    rotationData = RotateLeft(currentPosition, amountToMove);
                }
                else
                {
                    rotationData = RotateRight(currentPosition, amountToMove);
                }
                currentPosition = rotationData.Item1;
                zeroCycles += rotationData.Item2;
            }
            return zeroCycles;
        }

        private (long, int) RotateLeft(long currentPosition, long amountToMove)
        {
            int cycles = 0;
            if (amountToMove == 0)
            {
                return (currentPosition, cycles);
            }
            if (amountToMove < 0)
            {
                throw new ArgumentException("amountToMove must be non-negative");
            }
            if (amountToMove >= MaxNumbersInACycle)
            {
                int extraCycles = (int)(amountToMove / MaxNumbersInACycle);
                cycles += extraCycles;
                amountToMove = amountToMove % MaxNumbersInACycle;
            }
            if (currentPosition != MinPosition && currentPosition - amountToMove <= MinPosition)
            {
                cycles++;
            }
            currentPosition -= amountToMove;
            if (currentPosition < MinPosition)
            {
                currentPosition = MaxNumbersInACycle - Math.Abs(currentPosition);
            }
            return (currentPosition,cycles);
        }

        private (long, int) RotateRight(long currentPosition, long amountToMove)
        {
            int cycles = 0;
            if (amountToMove == 0)
            {
                return (currentPosition, cycles);
            }

            if (amountToMove < 0)
            {
                throw new ArgumentException("amountToMove must be non-negative");
            }
            if (amountToMove >= MaxNumbersInACycle)
            {
                int extraCycles = (int)(amountToMove / MaxNumbersInACycle);
                cycles += extraCycles;
                amountToMove = amountToMove % MaxNumbersInACycle;
            }
            if (currentPosition != MinPosition && currentPosition + amountToMove >= MaxNumbersInACycle)
            {
                cycles++;
            }
            currentPosition += amountToMove;
            if (currentPosition > MaxPosition)
            {
                currentPosition = currentPosition - MaxNumbersInACycle;
            }
            return (currentPosition, cycles);
        }
    }
}
