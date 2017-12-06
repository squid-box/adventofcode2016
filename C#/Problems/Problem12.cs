namespace AdventOfCode2016.Problems
{
    using System;
    using System.Collections.Generic;

    public class Problem12 : Problem
    {
        public Problem12() : base(12){}

        public override string Answer()
        {
            var instructions = new List<Instruction>();

            var computerPt1 = new Computer(0, 0, 0, 0);
            var computerPt2 = new Computer(0, 0, 1, 0);

            foreach (var line in Input)
            {
                instructions.Add(new Instruction(line));
            }

            RunInstructions(computerPt1, instructions);
            RunInstructions(computerPt2, instructions);
                      
            return $"Register \'a\' contains \n * Part 1: {computerPt1.Registers['a']}\n * Part 2: {computerPt2.Registers['a']}";
        }

        private void RunInstructions(Computer computer, List<Instruction> instructions)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                var instruction = instructions[i];

                if (instruction.Function.Equals("jnz"))
                {
                    int val;

                    if (instruction.JnzRegistry == 'x')
                    {
                        val = instruction.JnzValue;
                    }
                    else
                    {
                        val = computer.Registers[instruction.JnzRegistry];
                    }

                    if (val != 0)
                    {
                        i += instruction.JnzDistance - 1;
                    }
                }
                else
                {
                    instruction.Execute(computer);
                }
            }
        }
    }

    internal class Instruction
    {
        public string Function { get; private set; }
        public char Registry { get; private set; }
        public int CopyValue { get; private set; }
        public char CopyRegistry { get; private set; }
        public char JnzRegistry { get; private set; }
        public int JnzValue { get; private set; }
        public int JnzDistance { get; private set; }

        public Instruction(string line)
        {
            var split = line.Split(' ');
            Function = split[0];

            switch(Function)
            {
                case "cpy":
                    FigureOutCopy(split[1]);
                    Registry = split[2][0];
                    break;
                case "jnz":
                    FigureOutJnz(split[1]);
                    JnzDistance = Convert.ToInt32(split[2]);
                    break;
                case "inc":
                case "dec":
                    Registry = split[1][0];
                    break;
            }
        }
        
        private void FigureOutCopy(string input)
        {
            // Try to cast input to an int:
            var isNumber = Int32.TryParse(input, out int tmp);

            if (isNumber)
            {
                CopyValue = tmp;
                CopyRegistry = 'x';
            }
            else
            {
                CopyRegistry = input[0];
            }
        }

        private void FigureOutJnz(string input)
        {
            // Try to cast input to an int:
            var isNumber = Int32.TryParse(input, out int tmp);

            if (isNumber)
            {
                JnzValue = tmp;
                JnzRegistry = 'x';
            }
            else
            {
                JnzRegistry = input[0];
            }
        }

        public void Execute(Computer computer)
        {
            switch(Function)
            {
                case "cpy":
                    if (CopyRegistry == 'x')
                    {
                        // We're copying a value
                        computer.cpy(CopyValue, Registry);
                    }
                    else
                    {
                        // We're copying value from a register.
                        computer.cpy(CopyRegistry, Registry);
                    }
                    break;
                case "inc":
                    computer.inc(Registry);
                    break;
                case "dec":
                    computer.dec(Registry);
                    break;
            }
        }
    }

    internal class Computer
    {
        public Dictionary<char, int> Registers { get; }

        public Computer(int a, int b, int c, int d)
        {
            Registers = new Dictionary<char, int>
            {
                { 'a', a },
                { 'b', b },
                { 'c', c },
                { 'd', d }
            };
        }

        public void inc(char targetRegister)
        {
            Registers[targetRegister]++;
        }

        public void dec(char targetRegister)
        {
            Registers[targetRegister]--;
        }

        public void cpy(char valueRegister, char targetRegister)
        {
            Registers[targetRegister] = Registers[valueRegister];
        }

        public void cpy(int value, char targetRegister)
        {
            Registers[targetRegister] = value;
        }
    }
}
