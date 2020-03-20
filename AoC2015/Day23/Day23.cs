using System;

/*

--- Day 23: Opening the Turing Lock ---

Little Jane Marie just got her very first computer for Christmas from some unknown benefactor. 
It comes with instructions and an example program, but the computer itself seems to be malfunctioning. 
She's curious what the program does, and would like you to help her run it.

The manual explains that the computer supports two registers and six instructions (truly, it goes on to remind the reader, a state-of-the-art technology). 
The registers are named a and b, can hold any non-negative integer, and begin with a value of 0. 
The instructions are as follows:

hlf r sets register r to half its current value, then continues with the next instruction.
tpl r sets register r to triple its current value, then continues with the next instruction.
inc r increments register r, adding 1 to it, then continues with the next instruction.
jmp offset is a jump; it continues with the instruction offset away relative to itself.
jie r, offset is like jmp, but only jumps if register r is even ("jump if even").
jio r, offset is like jmp, but only jumps if register r is 1 ("jump if one", not odd).

All three jump instructions work with an offset relative to that instruction. 
The offset is always written with a prefix + or - to indicate the direction of the jump (forward or backward, respectively). 
For example, jmp +1 would simply continue with the next instruction, while jmp +0 would continuously jump back to itself forever.

The program exits when it tries to run an instruction beyond the ones defined.

For example, this program sets a to 2, because the jio instruction causes it to skip the tpl instruction:

inc a
jio a, +2
tpl a
inc a

What is the value in register b when the program in your puzzle input is finished executing?

--- Part Two ---

The unknown benefactor is very thankful for releasi-- er, helping little Jane Marie with her computer. 
Definitely not to distract you, what is the value in register b after the program is finished executing if register a starts as 1 instead?

*/

namespace Day23
{
    class Program
    {
        static long sA;
        static long sB;
        static long sPC;

        enum Instruction { hlf, tpl, inc, jmp, jie, jio };
        enum Register { A, B };

        struct Line
        {
            public Instruction instruction;
            public Register register;
            public int offset;
        }

        static Line[] sLines;

        private Program(string inputFile, bool part1)
        {
            var lines = AoC2015.Program.ReadLines(inputFile);
            ParseInput(lines);

            if (part1)
            {
                RunProgram(0, 0);
                var result1 = GetB();
                Console.WriteLine($"Day23: Result1 {result1}");
                var expected = 170;
                if (result1 != expected)
                {
                    throw new InvalidProgramException($"Part1 result is broken {result1} != {expected}");
                }
            }
            else
            {
                RunProgram(1, 0);
                var result2 = GetB();
                Console.WriteLine($"Day23: Result2 {result2}");
                var expected = 247;
                if (result2 != expected)
                {
                    throw new InvalidProgramException($"Part2 result is broken {result2} != {expected}");
                }
            }
        }

        static Register ParseRegister(string token)
        {
            token = token.Trim();
            if (token == "a")
            {
                return Register.A;
            }
            else if (token == "b")
            {
                return Register.B;
            }
            throw new InvalidProgramException($"Unknown register token '{token}'");
        }

        static int ParseOffset(string token)
        {
            token = token.Trim();
            if (int.TryParse(token, out int offset))
            {
                return offset;
            }
            throw new InvalidProgramException($"Unknown offset token '{token}'");
        }

        public static void ParseInput(string[] program)
        {
            sA = 0;
            sB = 0;
            sPC = 0;
            sLines = new Line[program.Length];
            for (var l = 0; l < program.Length; ++l)
            {
                var programLine = program[l];
                var tokens = programLine.Split(' ');
                var instruction = tokens[0];
                ref var line = ref sLines[l];
                if (instruction == "hlf")
                {
                    //hlf r
                    line.instruction = Instruction.hlf;
                    line.register = ParseRegister(tokens[1]);
                }
                else if (instruction == "tpl")
                {
                    //tpl r
                    line.instruction = Instruction.tpl;
                    line.register = ParseRegister(tokens[1]);
                }
                else if (instruction == "inc")
                {
                    //inc r
                    line.instruction = Instruction.inc;
                    line.register = ParseRegister(tokens[1]);
                }
                else if (instruction == "jmp")
                {
                    //jmp offset
                    line.instruction = Instruction.jmp;
                    line.offset = ParseOffset(tokens[1]);
                }
                else if (instruction == "jie")
                {
                    //jie r, offset
                    line.instruction = Instruction.jie;
                    line.register = ParseRegister(tokens[1].TrimEnd(','));
                    line.offset = ParseOffset(tokens[2]);
                }
                else if (instruction == "jio")
                {
                    //jio r, offset
                    line.instruction = Instruction.jio;
                    line.register = ParseRegister(tokens[1].TrimEnd(','));
                    line.offset = ParseOffset(tokens[2]);
                }
                else
                {
                    throw new InvalidProgramException($"Unknown instruction token {instruction}");
                }
            }
        }

        public static void RunProgram(int a, int b)
        {
            sA = a;
            sB = b;
            sPC = 0;
            while ((sPC >= 0) && (sPC < sLines.Length))
            {
                var current = sLines[sPC];
                var register = current.register;
                var offset = current.offset;
                switch (current.instruction)
                {
                    case Instruction.hlf:
                        if (register == Register.A)
                        {
                            sA /= 2;
                        }
                        else if (register == Register.B)
                        {
                            sB /= 2;
                        }
                        else
                        {
                            throw new InvalidProgramException($"Unknown register '{register}'");
                        }
                        sPC += 1;
                        break;
                    case Instruction.tpl:
                        if (register == Register.A)
                        {
                            sA *= 3;
                        }
                        else if (register == Register.B)
                        {
                            sB *= 3;
                        }
                        else
                        {
                            throw new InvalidProgramException($"Unknown register '{register}'");
                        }
                        sPC += 1;
                        break;
                    case Instruction.inc:
                        if (register == Register.A)
                        {
                            sA += 1;
                        }
                        else if (register == Register.B)
                        {
                            sB += 1;
                        }
                        else
                        {
                            throw new InvalidProgramException($"Unknown register '{register}'");
                        }
                        sPC += 1;
                        break;
                    case Instruction.jmp:
                        sPC += offset;
                        break;
                    case Instruction.jie:
                        if (register == Register.A)
                        {
                            offset = (sA % 2 == 0) ? offset : 1;
                        }
                        else if (register == Register.B)
                        {
                            offset = (sB % 2 == 0) ? offset : 1;
                        }
                        else
                        {
                            throw new InvalidProgramException($"Unknown register '{register}'");
                        }
                        sPC += offset;
                        break;
                    case Instruction.jio:
                        if (register == Register.A)
                        {
                            offset = (sA == 1) ? offset : 1;
                        }
                        else if (register == Register.B)
                        {
                            offset = (sB == 1) ? offset : 1;
                        }
                        else
                        {
                            throw new InvalidProgramException($"Unknown register '{register}'");
                        }
                        sPC += offset;
                        break;
                }
            };
        }

        public static long GetA()
        {
            return sA;
        }

        public static long GetB()
        {
            return sB;
        }

        public static void Run()
        {
            Console.WriteLine("Day23 : Start");
            _ = new Program("Day23/input.txt", true);
            _ = new Program("Day23/input.txt", false);
            Console.WriteLine("Day23 : End");
        }
    }
}
