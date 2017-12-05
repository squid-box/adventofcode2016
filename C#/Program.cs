namespace AdventOfCode2016
{
    using System;
    using System.Collections.Generic;
    using Problems;
    
    public class Program
    {
        public static void Main()
        {
            var problems = new List<Problem>
            {
                new Problem1(),
                new Problem2(),
                new Problem3(),
                new Problem4(),
                //new Problem5(),   // TODO: optimize to reasonable execution time. 1.5 minutes is too much...
                new Problem6(),
                new Problem7(),
                new Problem8(),
                new Problem9()
            };

            foreach (var problem in problems)
            {
                Console.Out.WriteLine(problem);
                Console.Out.WriteLine();
            }

            Console.In.ReadLine();
        }
    }
}
