namespace AdventOfCode2016.Utils
{
    using System;

    public class Position
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Position(int x = 0, int y = 0)
        {
            SetPosition(x, y);
        }

        public void SetPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        public void Move(Directions dir, int steps = 1)
        {
            switch (dir)
            {
                case Directions.Left:
                    X -= steps;
                    break;
                case Directions.Right:
                    X += steps;
                    break;
                case Directions.Down:
                    Y += steps;
                    break;
                case Directions.Up:
                    Y -= steps;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }

        public static Position Origo = new Position(0, 0);

        public static int DistanceBetweenTwoPositions(Position first, Position second)
        {
            return Math.Abs(first.X - second.X) + Math.Abs(first.Y - second.Y);
        }
    }
}
