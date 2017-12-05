namespace AdventOfCode2016.Problems
{
    using System.Collections.Generic;
    using System.Linq;

    public class Problem6 : Problem
    {
        public Problem6() : base(6){}

        public override string Answer()
        {
            var counter = new Dictionary<int, Dictionary<char, int>>();

            foreach (var line in Input)
            {
                for (var i = 0; i < line.Length; i++)
                {
                    if (!counter.ContainsKey(i))
                    {
                        counter.Add(i, new Dictionary<char, int>());
                    }
                    if (!counter[i].ContainsKey(line[i]))
                    {
                        counter[i].Add(line[i], 0);
                    }

                    counter[i][line[i]]++;
                }
            }

            var firstAnswer = new char[counter.Count];
            var secondAnswer = new char[counter.Count];

            for (var i = 0; i < counter.Count; i++)
            {
                var orderedList = counter[i].OrderBy(x => x.Value).ToList();
                firstAnswer[i] = orderedList.Last().Key;
                secondAnswer[i] = orderedList.First().Key;
            }

            return $"Repaired message is \"{new string(firstAnswer)}\".\nModified message is \"{new string(secondAnswer)}\".";
        }
    }
}
