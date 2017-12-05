namespace AdventOfCode2016.Problems
{
    using System.IO;

    /// <summary>
    /// Base for all problem solutions.
    /// </summary>
    public abstract class Problem
    {
        /// <summary>
        /// The number (id) of this problem.
        /// </summary>
        public int Number { get; private set; }

        /// <summary>
        /// Gets the input of this problem.
        /// </summary>
        public string[] Input
        {
            get
            {
                try
                {
                    return File.ReadAllLines($"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}input{Path.DirectorySeparatorChar}{Number}.input");
                }
                catch (FileNotFoundException)
                {
                    throw;
                }      
            }
        }

        protected Problem(int number)
        {
            Number = number;
        }

        public abstract string Answer();

        /// <summary>
        /// Returns both solutions of this problem in a human-readable format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Problem #{Number}:\n{Answer()}";
        }
    }
}
