using System;
using System.Collections.Generic;

/*

--- Day 7: Some Assembly Required ---

This year, Santa brought little Bobby Tables a set of wires and bitwise logic gates! Unfortunately, little Bobby is a little under the recommended age range, and he needs help assembling the circuit.

Each wire has an identifier (some lowercase letters) and can carry a 16-bit signal (a number from 0 to 65535). A signal is provided to each wire by a gate, another wire, or some specific value. Each wire can only get a signal from one source, but can provide its signal to multiple destinations. A gate provides no signal until all of its inputs have a signal.

The included instructions booklet describes how to connect the parts together: x AND y -> z means to connect wires x and y to an AND gate, and then connect its output to wire z.

For example:

123 -> x means that the signal 123 is provided to wire x.
x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
p LSHIFT 2 -> q means that the value from wire p is left-shifted by 2 and then provided to wire q.
NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
Other possible gates include OR (bitwise OR) and RSHIFT (right-shift). If, for some reason, you'd like to emulate the circuit instead, almost all programming languages (for example, C, JavaScript, or Python) provide operators for these gates.

For example, here is a simple circuit:

123 -> x
456 -> y
x AND y -> d
x OR y -> e
x LSHIFT 2 -> f
y RSHIFT 2 -> g
NOT x -> h
NOT y -> i
After it is run, these are the signals on the wires:

d: 72
e: 507
f: 492
g: 114
h: 65412
i: 65079
x: 123
y: 456

In little Bobby's kit's instructions booklet (provided as your puzzle input), what signal is ultimately provided to wire a?

Your puzzle answer was 3176.

--- Part Two ---

Now, take the signal you got on wire a, override wire b to that signal, and reset the other wires (including wire a). What new signal is ultimately provided to wire a?

*/

namespace Day07
{
    class Program
    {
        struct Operation
        {
            public enum LogicGate
            {
                XOR,
                AND,
                OR,
                LSHIFT,
                RSHIFT
            };
            public LogicGate gate;
            public string argA;
            public string argB;
            public string output;
        };

        static readonly int MAX_NUM_WIRES = 65536;
        static readonly Dictionary<string, Operation> sConnections = new Dictionary<string, Operation>(MAX_NUM_WIRES);
        static readonly bool[] sInit = new bool[MAX_NUM_WIRES];
        static readonly ushort[] sValues = new ushort[MAX_NUM_WIRES];
        static string sOverrideWire = "IGNORE";
        static ushort sOverrideValue = 0;

        private Program(string inputFile, bool part1)
        {
            var instructions = AoC2015.Program.ReadLines(inputFile);
            if (part1)
            {
                Program.CreateCircuit(instructions);
                var result1 = GetWire("a");
                Console.WriteLine($"Day07 : Result1 {result1}");
                int expected = 3176;
                if (result1 != expected)
                {
                    throw new InvalidOperationException($"Part1 is broken {result1} != {expected}");
                }
            }
            else
            {
                sOverrideWire = "b";
                sOverrideValue = 3176;
                Program.CreateCircuit(instructions);
                var result2 = GetWire("a");
                Console.WriteLine($"Day07 : Result2 {result2}");
                int expected = 14710;
                if (result2 != expected)
                {
                    throw new InvalidOperationException($"Part2 is broken {result2} != {expected}");
                }
            }
        }

        public static void CreateCircuit(string[] instructions)
        {
            for (var i = 0; i < MAX_NUM_WIRES; ++i)
            {
                sValues[i] = 0;
                sInit[i] = false;
            }
            // a NOT
            // a AND b
            // a OR b
            // a LSHFIT b
            // a RSHFIT b
            foreach (var line in instructions)
            {
                Operation operation;
                var tokens = line.Split(" ");
                // 123 -> x means that the signal 123 is provided to wire x.
                int outputToken = -1;
                if (tokens[1] == "->")
                {
                    // -> => a OR 0x0000
                    outputToken = 2;
                    operation.argA = tokens[0];
                    operation.gate = Operation.LogicGate.OR;
                    operation.argB = "0";
                }
                // NOT e -> f means that the bitwise complement of the value from wire e is provided to wire f.
                else if ((tokens[0] == "NOT") && (tokens[2] == "->"))
                {
                    // NOT => a XOR 0xFFFF
                    outputToken = 3;
                    operation.argA = tokens[1];
                    operation.gate = Operation.LogicGate.XOR;
                    operation.argB = "65535";
                }
                /*
                x AND y -> z means that the bitwise AND of wire x and wire y is provided to wire z.
                x OR y -> z means that the bitwise OR of wire x and wire y is provided to wire z.
                p LSHIFT 2 -> q means that the value from wire p is left-shifted by 2 and then provided to wire q.
                p RSHIFT 2 -> q means that the value from wire p is right-shifted by 2 and then provided to wire q.
                */
                else if (tokens[3] == "->")
                {
                    outputToken = 4;
                    operation.argA = tokens[0];
                    operation.argB = tokens[2];
                    var op = tokens[1];
                    if (op == "AND")
                    {
                        operation.gate = Operation.LogicGate.AND;
                    }
                    else if (op == "OR")
                    {
                        operation.gate = Operation.LogicGate.OR;
                    }
                    else if (op == "LSHIFT")
                    {
                        operation.gate = Operation.LogicGate.LSHIFT;
                    }
                    else if (op == "RSHIFT")
                    {
                        operation.gate = Operation.LogicGate.RSHIFT;
                    }
                    else
                    {
                        throw new InvalidOperationException($"Failed to parse line {line} invalid op {op}");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Failed to parse line {line}");
                }

                if ((outputToken < 0) || (outputToken >= tokens.Length))
                {
                    throw new InvalidOperationException($"Failed to parse line {line} invalid outputToken {outputToken}");
                }
                operation.output = tokens[outputToken];
                sConnections[operation.output] = operation;
            }
        }

        static ushort ConvertValue(int value)
        {
            uint temp = (uint)value;
            temp &= 0xFFFF;
            ushort output = (ushort)temp;
            return output;
        }

        static ushort GetValue(string arg)
        {
            if (arg == sOverrideWire)
            {
                return sOverrideValue;
            }
            if (IsValidWire(arg))
            {
                return GetWire(arg);
            }
            else
            {
                return ushort.Parse(arg);
            }
        }

        static bool IsValidWire(string wire)
        {
            if (wire.Length > 2)
            {
                return false;
            }
            if (wire.Length == 0)
            {
                return false;
            }

            var c0 = wire[0];
            if ((c0 < 'a') || (c0 > 'z'))
            {
                return false;
            }
            if (wire.Length > 1)
            {
                var c1 = wire[1];
                if ((c1 < 'a') || (c1 > 'z'))
                {
                    return false;
                }
            }
            return true;
        }

        static int GetWireIndex(string wire)
        {
            int wireIndex = (int)wire[0];
            if (wire.Length > 1)
            {
                wireIndex += (int)wire[1] << 8;
            }
            if ((wireIndex < 0) || (wireIndex >= MAX_NUM_WIRES))
            {
                throw new ArgumentOutOfRangeException($"wire out of range {wireIndex} 0 -> {MAX_NUM_WIRES}");
            }
            return wireIndex;
        }

        static void SetWire(string wire, ushort value)
        {
            if (!IsValidWire(wire))
            {
                throw new ArgumentOutOfRangeException($"Invalid wire {wire} must be two of less lower case characters");
            }

            var wireIndex = GetWireIndex(wire);
            sValues[wireIndex] = value;
        }

        public static ushort GetWire(string wire)
        {
            if (!IsValidWire(wire))
            {
                throw new ArgumentOutOfRangeException($"Invalid wire {wire} must be two or less lower case characters");
            }

            var wireIndex = GetWireIndex(wire);
            if (sInit[wireIndex])
            {
                return sValues[wireIndex];
            }
            Operation op = sConnections[wire];
            if (op.output != wire)
            {
                throw new ArgumentOutOfRangeException($"Invalid op.output {op.output} it should match wire {wire}");
            }
            int argA = GetValue(op.argA);
            int argB = GetValue(op.argB);
            int output = 0;
            switch (op.gate)
            {
                case Operation.LogicGate.AND:
                    output = argA & argB;
                    break;
                case Operation.LogicGate.OR:
                    output = argA | argB;
                    break;
                case Operation.LogicGate.XOR:
                    output = argA ^ argB;
                    break;
                case Operation.LogicGate.LSHIFT:
                    output = argA << argB;
                    break;
                case Operation.LogicGate.RSHIFT:
                    output = argA >> argB;
                    break;
            }
            ushort wireValue = ConvertValue(output);
            sValues[wireIndex] = wireValue;
            sInit[wireIndex] = true;
            return wireValue;
        }

        public static void Run()
        {
            Console.WriteLine("Day07 : Start");
            _ = new Program("Day07/input.txt", true);
            _ = new Program("Day07/input.txt", false);
            Console.WriteLine("Day07 : End");
        }
    }
}
