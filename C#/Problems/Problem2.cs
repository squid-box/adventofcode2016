namespace AdventOfCode2016.Problems
{
    using System;
    using Utils;

    public class Problem2 : Problem
    {
        public Problem2() : base(2) { }

        public override string Answer()
        {
            var keypadOneResult = "";
            var keypadTwoResult = "";
            var keyPadOne = new Keypad(new Position(1, 1), Keypad.GenerateFirstKeypad(), 3, 3);
            var keyPadTwo = new Keypad(new Position(0, 2), Keypad.GenerateSecondKeypad(), 5, 5);

            foreach (var line in Input)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                foreach (var instruction in line)
                {
                    var dir = Directions.Up;

                    switch (instruction)
                    {
                        case 'U':
                            dir = Directions.Up;
                            break;
                        case 'R':
                            dir = Directions.Right;
                            break;
                        case 'D':
                            dir = Directions.Down;
                            break;
                        case 'L':
                            dir = Directions.Left;
                            break;
                        default:
                            Console.Error.WriteLine($"Unknown instruction: {instruction}.");
                            break;
                    }
                    
                    keyPadOne.MoveKey(dir);
                    keyPadTwo.MoveKey(dir);
                }

                keypadOneResult += keyPadOne.GetCurrentDigit();
                keypadTwoResult += keyPadTwo.GetCurrentDigit();
            }

            return $"First part: {keypadOneResult}\nSecond part: {keypadTwoResult}";
        }
    }

    public class Keypad
    {
        private Position _currentPosition;

        private char[,] _keyPad;

        public int Width;
        public int Height;

        public static char[,] GenerateFirstKeypad()
        {
            const int width = 3;
            const int height = 3;

            var result = new char[width, height];

            var digit = 1;

            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    result[j, i] = char.Parse(digit.ToString());
                    digit++;
                }
            }

            return result;
        }

        public static char[,] GenerateSecondKeypad()
        {
            const int width = 5;
            const int height = 5;

            var result = new char[width, height];

            result[0, 0] = '\t';
            result[1, 0] = '\t';
            result[2, 0] = '1';
            result[3, 0] = '\t';
            result[4, 0] = '\t';

            result[0, 1] = '\t';
            result[1, 1] = '2';
            result[2, 1] = '3';
            result[3, 1] = '4';
            result[4, 1] = '\t';

            result[0, 2] = '5';
            result[1, 2] = '6';
            result[2, 2] = '7';
            result[3, 2] = '8';
            result[4, 2] = '9';

            result[0, 3] = '\t';
            result[1, 3] = 'A';
            result[2, 3] = 'B';
            result[3, 3] = 'C';
            result[4, 3] = '\t';

            result[0, 4] = '\t';
            result[1, 4] = '\t';
            result[2, 4] = 'D';
            result[3, 4] = '\t';
            result[4, 4] = '\t';

            return result;
        }

        public Keypad(Position startingPosition, char[,] keypad, int height, int width)
        {
            _keyPad = keypad;
            _currentPosition = startingPosition;

            Width = width;
            Height = height;
        }

        public void MoveKey(Directions dir)
        {
            var x = _currentPosition.X;
            var y = _currentPosition.Y;

            switch (dir)
            {
                case Directions.Up:
                    y = Math.Max(_currentPosition.Y - 1, 0);
                    break;
                case Directions.Right:
                    x = Math.Min(_currentPosition.X + 1, Width-1);
                    break;
                case Directions.Down:
                    y = Math.Min(_currentPosition.Y + 1, Height-1);
                    break;
                case Directions.Left:
                    x = Math.Max(_currentPosition.X - 1, 0);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }

            // I'm using tab chars for "outside" keys.
            if (_keyPad[x, y] == '\t')
            {
                return;
            }
            
            _currentPosition.SetPosition(x,y);
        }

        public char GetCurrentDigit()
        {
            return _keyPad[_currentPosition.X, _currentPosition.Y];
        }
    }
}
