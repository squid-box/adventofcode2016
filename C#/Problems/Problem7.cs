namespace AdventOfCode2016.Problems
{
    using System;
    using System.Collections.Generic;
    using System.Text.RegularExpressions;

    class Problem7 : Problem
    {
        public Problem7() : base(7){}

        public override string Answer()
        {
            var totalAddresses = 0;
            var supportTls = 0;
            var supportSsl = 0;

            foreach (var line in Input)
            {
                var ip = new IpAddress(line);

                if (ip.SupportsTls)
                {
                    supportTls++;
                }

                if (ip.SupportsSsl)
                {
                    supportSsl++;
                }

                totalAddresses++;
            }

            return $"{supportTls}/{totalAddresses} support TLS.\n{supportSsl}/{totalAddresses} support SSL";
        }

        
    }

    public class IpAddress
    {
        private readonly List<string> _supernetSequences;
        private readonly List<string> _hypernetSequences;
        
        public IpAddress(string address)
        {
            _supernetSequences = new List<string>();
            _hypernetSequences = new List<string>();

            var outsideBrackets = Regex.Matches(address, @"(^.*?\[)|(\].*?\[)|(\].*?$)");
            var insideBrackets = Regex.Matches(address, @"\[(.*?)\]");
            
            foreach (var match in outsideBrackets)
            {
                _supernetSequences.Add(match.ToString().Replace("[", string.Empty).Replace("]", string.Empty));
            }

            foreach (var match in insideBrackets)
            {
                _hypernetSequences.Add(match.ToString().Replace("[", string.Empty).Replace("]", string.Empty));
            }
        }

        public bool SupportsTls
        {
            get
            {
                foreach (var hypernetBlock in _hypernetSequences)
                {
                    if (DoesStringContainAbba(hypernetBlock))
                    {
                        return false;
                    }
                }

                foreach (var supernetBlock in _supernetSequences)
                {
                    if (DoesStringContainAbba(supernetBlock))
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public bool SupportsSsl
        {
            get
            {
                // First find all ABA's in the Supernet sequences.
                var areaBroadcastAccessors = new List<string>();

                foreach (var supernetBlock in _supernetSequences)
                {
                    areaBroadcastAccessors.AddRange(FindAreaBroadcastAccessors(supernetBlock));
                }

                if (areaBroadcastAccessors.Count == 0)
                {
                    // No ABA's means no SSL support.
                    return false;
                }

                // Then check for corresponding BAB's in the Hypernet sequences.
                foreach (var hypernetBloc in _hypernetSequences)
                {
                    foreach (var areaBroadcastAccessor in areaBroadcastAccessors)
                    {
                        if (ContainsByteAllocationBlock(hypernetBloc, areaBroadcastAccessor[0], areaBroadcastAccessor[1]))
                        {
                            return true;
                        }
                    }
                }

                // If we can't find any matching BAB's, SSL is not supported.
                return false;
            }
        }

        private static bool DoesStringContainAbba(string input)
        {
            for (var i = 0; i < input.Length - 3; i++)
            {
                if (IsAbba(input.Substring(i, 4)))
                {
                    // As soon as we find one match, we return true.
                    return true;
                }
            }

            // If no match is found, return false.
            return false;
        }

        private static bool IsAbba(string input)
        {
            if (input.Length != 4)
            {
                throw new ArgumentException("Input for ABBA test has to be 4 characters.");
            }
            if (input[0].Equals(input[1]))
            {
                // The two characters in a pair must be different.
                return false;
            }

            return input[0].Equals(input[3]) && input[1].Equals(input[2]);
        }

        private static List<string> FindAreaBroadcastAccessors(string input)
        {
            var res = new List<string>();

            for (var i = 0; i < input.Length-2; i++)
            {
                // If the first two letters are the same, skip this iteration.
                if (input[i].Equals(input[i + 1]))
                {
                    continue;
                }

                // If first and third letters are not the same, skip this iteration.
                if (!input[i].Equals(input[i + 2]))
                {
                    continue;
                }

                res.Add(input.Substring(i, 3));
            }

            return res;
        }

        private static bool ContainsByteAllocationBlock(string input, char a, char b)
        {
            return (input.Contains($"{b}{a}{b}"));
        }
    }
}
