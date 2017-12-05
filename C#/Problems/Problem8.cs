namespace AdventOfCode2016.Problems
{
    using System;
    using System.Text;

    public class Problem8 : Problem
    {
        public Problem8() : base(8){}

        public override string Answer()
        {
            var display = new Display(50, 6);

            foreach (var line in Input)
            {
                var split = line.Split(' ');

                switch (split[0])
                {
                    case "rect":
                        var rectValues = split[1].Split('x');
                        display.Rect(int.Parse(rectValues[0]), int.Parse(rectValues[1]));
                        break;
                    case "rotate":
                        var target = int.Parse(split[2].Split('=')[1]);
                        var amount = int.Parse(split[4]);

                        if (split[1].Equals("row"))
                        {
                            display.RotateRow(target, amount);
                        }
                        else if (split[1].Equals("column"))
                        {
                            display.RotateColumn(target, amount);
                        }
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown instruction: {split[0]}.");
                }
            }

            return $"{display.PixelsOn()}/{display.PixelsTotal()} pixels enabled.\nDisplay shows:\n{display.ToString()}";
        }
    }

    public class Display
    {
        public int Width { get; }

        public int Height { get; }

        private readonly bool[,] _pixels;

        public Display(int x, int y)
        {
            Width = x;
            Height = y;

            _pixels = new bool[x,y];
        }

        public void Rect(int a, int b)
        {
            for (var x = 0; x < a; x++)
            {
                for (var y = 0; y < b; y++)
                {
                    _pixels[x, y] = true;
                }
            }
        }

        public void RotateRow(int a, int b)
        {
            var oldRow = new bool[Width];

            for (var n = 0; n < Width; n++)
            {
                oldRow[n] = _pixels[n, a];
            }

            for (var i = 0; i < Width; i++)
            {
                var newIndex = i + b;
                _pixels[newIndex%Width, a] = oldRow[i];
            }
        }

        public void RotateColumn(int a, int b)
        {
            var oldColumn = new bool[Height];

            for (var n = 0; n < Height; n++)
            {
                oldColumn[n] = _pixels[a, n];
            }

            for (var i = 0; i < Height; i++)
            {
                var newIndex = i + b;
                _pixels[a, newIndex % Height] = oldColumn[i];
            }
        }

        public int PixelsOn()
        {
            var count = 0; 

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    if (_pixels[x, y])
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public int PixelsTotal()
        {
            return Width*Height;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    sb.Append(_pixels[x, y] ? '#' : ' ');
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
