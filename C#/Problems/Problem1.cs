namespace AdventOfCode2016.Problems
{
    using System.Collections.Generic;
    using Utils;

    public class Problem1 : Problem
    {
        public Problem1() : base(1) { }

        public override string Answer()
        {
            var currentDirection = new Direction(Directions.Up);
            var currentPosition = new Position(0, 0);
            var visits = new Dictionary<string, int>();
            Position firstDoubleVisit = null;

            foreach (var instruction in Input[0].Split(','))
            {
                var direction = instruction.Trim().Substring(0,1);
                var steps = int.Parse(instruction.Trim().Substring(1));

                if (direction.Equals("R"))
                {
                    currentDirection.TurnRight();
                }
                else
                {
                    currentDirection.TurnLeft();
                }

                for (var i = 0; i < steps; i++)
                {
                    currentPosition.Move(currentDirection.CurrentDirection);

                    if (!visits.ContainsKey(currentPosition.ToString()))
                    {
                        visits.Add(currentPosition.ToString(), 0);
                    }

                    visits[currentPosition.ToString()]++;
                    if (firstDoubleVisit == null && visits[currentPosition.ToString()] > 1)
                    {
                        firstDoubleVisit = new Position(currentPosition.X, currentPosition.Y);
                    }
                }
            }

            return $"Moved from (0,0) to {currentPosition}, {Position.DistanceBetweenTwoPositions(Position.Origo, currentPosition)} blocks from the start.\n" +
                $"First position visited twice was {firstDoubleVisit}, {Position.DistanceBetweenTwoPositions(Position.Origo, firstDoubleVisit)} blocks from the start.";
        }
    }

    class Direction
    {
        public Directions CurrentDirection { get; private set; }

        public Direction(Directions initialDirection = Directions.Up)
        {
            CurrentDirection = initialDirection;
        }

        public void TurnLeft()
        {
            var tmp = (int)CurrentDirection - 1;
            if (tmp < 0)
            {
                tmp = 3;
            }

            CurrentDirection = (Directions)tmp;
        }

        public void TurnRight()
        {
            var tmp = (int)CurrentDirection + 1;
            if (tmp > 3)
            {
                tmp = 0;
            }

            CurrentDirection = (Directions)tmp;
        }
    }
}
