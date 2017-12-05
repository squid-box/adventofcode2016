namespace AdventOfCode2016.Problems
{
    using System.Text;

    public class Problem9 : Problem
    {
        public Problem9() : base(9){}

        public override string Answer()
        {
            var input = Input[0];

            return $"Decompressed V1 document is {PartOne(input)} characters long.\nDecompressed V2 document is {PartTwo(input)} characters long.";
        }

        private static int PartOne(string input)
        {
            var answer = new StringBuilder();

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i].Equals('('))
                {
                    var endOfMarker = input.IndexOf(')', i);
                    var marker = input.Substring(i + 1, endOfMarker - i - 1);

                    var markerData = marker.Split('x');
                    var numOfChars = int.Parse(markerData[0]);
                    var numOfRepeats = int.Parse(markerData[1]);
                    var textToRepeat = input.Substring(endOfMarker + 1, numOfChars);

                    for (var n = 0; n < numOfRepeats; n++)
                    {
                        answer.Append(textToRepeat);
                    }

                    // Continue after 
                    i = endOfMarker + numOfChars;
                }
                else
                {
                    answer.Append(input[i]);
                }
            }

            return answer.ToString().Length;
        }

        private static int PartTwo(string input)
        {
            var answer = new StringBuilder();

            for (var i = 0; i < input.Length; i++)
            {
                if (input[i].Equals('('))
                {
                    var endOfMarker = input.IndexOf(')', i);
                    var marker = input.Substring(i + 1, endOfMarker - i - 1);

                    var markerData = marker.Split('x');
                    var numOfChars = int.Parse(markerData[0]);
                    var numOfRepeats = int.Parse(markerData[1]);
                    var textToRepeat = input.Substring(endOfMarker + 1, numOfChars);

                    for (var n = 0; n < numOfRepeats; n++)
                    {
                        answer.Append(textToRepeat);
                    }

                    // Continue after 
                    i = endOfMarker + numOfChars;
                }
                else
                {
                    answer.Append(input[i]);
                }
            }

            return answer.Length;
        }

        private static string DecompessPart(string part)
        {
            var sb = new StringBuilder();

            for (var i = 0; i < part.Length; i++)
            {
                var endOfMarker = part.IndexOf(')', i);
                var marker = part.Substring(i + 1, endOfMarker - i - 1);

                var markerData = marker.Split('x');
                var numOfChars = int.Parse(markerData[0]);
                var numOfRepeats = int.Parse(markerData[1]);
                var textToRepeat = part.Substring(endOfMarker + 1, numOfChars);
            }

            return sb.ToString();
        }
    }
}
