namespace AdventOfCode2016.Problems
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class Problem5 : Problem
    {
        private readonly MD5 _md5;

        public Problem5() : base(5)
        {
            _md5 = MD5.Create();
        }

        public override string Answer()
        {
            var doorCode = Input[0];

            var doorOne = new char[8];
            var doorTwo = new char[8];

            var firstIndex = 0;

            for (var i = 0; i < int.MaxValue; i++)
            {
                var iteration = $"{doorCode}{i}";
                var md5 = GenerateMd5(iteration).Replace("-","");

                // A hash indicates the next character in the password if its 
                // hexadecimal representation starts with five zeroes.
                if (StartsWithFiveZeroes(md5))
                {
                    // Add chars until password is full.
                    if (firstIndex < 8)
                    {
                        doorOne[firstIndex] = md5[5];
                        firstIndex++;
                    }

                    try
                    {
                        var doorTwoIndex = int.Parse(md5[5].ToString());

						// Password is only 8 chars long, ignore higher indices.
                        if (doorTwoIndex > 7)
                        {
                            continue;
                        }

						// Only add char if given index isn't already assigned.
                        if (doorTwo[doorTwoIndex].Equals('\0'))
                        {
                            doorTwo[doorTwoIndex] = md5[6];
                        }
                    }
                    catch (FormatException)
                    {
						// Can't use non-integer indices.
                        continue;
                    }
                }

				// Evaluate end criteria.
                if (BothPasswordsFound(firstIndex, doorTwo))
                {
                    break;
                }
            }

			_md5.Dispose();
            return $"First door passcode is \"{new string(doorOne).ToLower()}\".\nSecond door passcode is \"{new string(doorTwo).ToLower()}\".";
        }

        private bool BothPasswordsFound(int oneIndex, char[] twoArray)
        {
            if (oneIndex < 7)
            {
                return false;
            }

            foreach (var c in twoArray)
            {
                if (c.Equals('\0'))
                {
                    return false;
                }
            }

            return true;
        }

        private string GenerateMd5(string input)
        {
            var hashBytes = _md5.ComputeHash(Encoding.Default.GetBytes(input));
            return BitConverter.ToString(hashBytes);
        }

        private bool StartsWithFiveZeroes(string input)
        {
            for (var i = 0; i < 5; i++)
            {
                if (!input[i].Equals('0'))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
