namespace AdventOfCode2016.Problems
{
    using System;

    public class Problem3 : Problem
    {
        public Problem3() : base(3) { }
        
        public override string Answer()
        {
            var count = 0;

            foreach (var tri in Input)
            {
                var tmp = tri.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                var one = tmp[0];
                var two = tmp[1];
                var three = tmp[2];

                if (IsThereATriangleHere(one, two, three))
                {
                    count++;
                }
            }

            return $"{SolveFirst()}\n{SolveSecond()}";
        }

        private string SolveFirst()
        {
            var count = 0;
            var checkedLines = 0;

            foreach (var tri in Input)
            {
                var tmp = tri.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var one = tmp[0];
                var two = tmp[1];
                var three = tmp[2];

                if (IsThereATriangleHere(one, two, three))
                {
                    count++;
                }

                checkedLines++;
            }

            return $"Regular rows has {count}/{checkedLines} valid triangles.";
        }

        private string SolveSecond()
        {
            var input = Input;
            var count = 0;
            var checkedLines = 0;

            // Taking the results three rows at a time.
            for (var i = 0; i < input.Length-2; i += 3)
            {
                var rowOne = input[i].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var rowTwo = input[i+1].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                var rowThree = input[i+2].Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                for (var j = 0; j < 3; j++)
                {
                    if (IsThereATriangleHere(rowOne[j], rowTwo[j], rowThree[j]))
                    {
                        count++;
                    }
                }

                checkedLines += 3;
            }

            return $"Transposed rows has {count}/{checkedLines} valid triangles.";
        }

        private bool IsThereATriangleHere(string a, string b, string c)
        {
            return IsThisATriangle(a, b, c) && IsThisATriangle(b, c, a) && IsThisATriangle(a, c, b);
        }

        private bool IsThisATriangle(string a, string b, string last)
        {
            return (int.Parse(a) + int.Parse(b)) > int.Parse(last);
        }
    }
}
