namespace AdventOfCode2016.Problems
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public class Problem4 : Problem
    {
        public Problem4() : base(4){}

        public override string Answer()
        {
            var sectorSum = 0;
            var secretRoomId = -1;

            foreach (var line in Input)
            {
                var encrypted = line.Substring(0, line.LastIndexOf('-'));
                var sectorId = int.Parse(line.Split('[')[0].Split('-').Last());
                var checksum = Regex.Match(line, @"\[([^]]*)\]").Groups[1].Value;

                if (ValidEncryption(encrypted, sectorId, checksum))
                {
                    sectorSum += sectorId;
                    if (Decrypt(encrypted, sectorId).Equals("northpole object storage"))
                    {
                        secretRoomId = sectorId;
                    }
                }
            }

            return $"Valid rooms sector sum: {sectorSum}. Presents in room {secretRoomId}.";
        }

        private string Decrypt(string encrypted, int sectorId)
        {
            var result = new StringBuilder();
            foreach(var c in encrypted)
            {
                if (c.Equals('-'))
                {
                    result.Append(' ');
                }
                else
                {
                    result.Append(RotateChar(c, sectorId));
                }
            }

            return result.ToString();
        }

        private char RotateChar(char c, int steps)
        {
            var character = c;

            for(var i = 0; i < steps; i++)
            {
                if (c.Equals('z'))
                {
                    c = 'a';
                }
                else
                {
                    c = (char)(c + 1);
                }                
            }

            return c;
        }

        private bool ValidEncryption(string encrypted, int sectorId, string checksum)
        {
            var trimmedEncryption = encrypted.Replace("-","");
            var charCount = new Dictionary<char, int>();

            foreach(var c in trimmedEncryption)
            {
                if (!charCount.ContainsKey(c))
                {
                    charCount.Add(c, 0);
                }
                charCount[c]++;
            }

            return FindHighestCount(5, charCount).Equals(checksum);
        }

        private string FindHighestCount(int numberOfChars, Dictionary<char,int> count)
        {
            // Order counts by how many occurences there were.
            var orderedList = count.OrderBy(x => x.Value).ToList();

            // Reverse order to have highest count first.
            orderedList.Reverse();
            
            var groupedList = new List<HashSet<char>>();

            for(var i = 0; i < orderedList[0].Value+1; i++)
            {
                groupedList.Add(new HashSet<char>());
            }

            foreach (var pair in orderedList)
            {
                groupedList[pair.Value].Add(pair.Key);
            }

            // Reversing list to have highest occurence index first.
            groupedList.Reverse();


            // Make a complete list of occurences...
            var result = new List<char>();

            for (var i = 0; i < groupedList.Count; i++)
            {
                var thisGroup = groupedList[i].ToList();
                thisGroup.Sort();

                foreach (var c in thisGroup)
                {
                    result.Add(c);
                }
            }
            
            // But only return the first five chars.
            return new string(result.ToArray()).Substring(0,5);
        }
    }
}
