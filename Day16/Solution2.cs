using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day16
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            bool prevLineWasEmpty = false;
            int[] one = new int[4];
            int[] two = new int[4];
            int[] tre = new int[4];
            int[] initreg = new int[4] { 0, 0, 0, 0 };
            int tracker = 1;
            int overallScore = 0;
            List<Operation> operations = new List<Operation>();
            Dictionary<int, List<string>> evaluations = new Dictionary<int, List<string>>();
            bool next = false;
            List<int[]> code = new List<int[]>();

            List<string> opCodes = new List<string>()
            {
                "addr", "addi", "mulr", "muli", "banr", "bani", "borr", "bori", "setr", "seti", "gtir", "gtri", "gtrr",
                "eqir", "eqri", "eqrr"
            };
            using (StreamReader file = new System.IO.StreamReader("input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    if (!next)
                    {
                        if (line == string.Empty)
                        {
                            if (prevLineWasEmpty)
                                next = true;
                            operations.Add(new Operation(one, two, tre));
                            prevLineWasEmpty = true;
                            tracker = 1;
                        }
                        else
                        {
                            prevLineWasEmpty = false;
                            var temp = Regex.Split(line, @"\D+").ToList();
                            if (tracker != 2)
                            {
                                temp.RemoveAt(0);
                                temp.RemoveAt(temp.Count - 1);
                            }

                            switch (tracker)
                            {
                                case 1:
                                    one = temp.Select(int.Parse).ToArray();
                                    break;
                                case 2:
                                    two = temp.Select(int.Parse).ToArray();
                                    break;
                                case 3:
                                    tre = temp.Select(int.Parse).ToArray();
                                    break;
                                default:
                                    break;
                            }

                            tracker++;
                        }
                    }
                    else
                    {
                        if (line != string.Empty)
                        {
                            var temp = Regex.Split(line, @"\D+").ToList();
                            code.Add(temp.Select(int.Parse).ToArray());
                        }
                    }
                }
            }

            for (int i = 0; i <= 15; i++)
            {
                evaluations.Add(i, new List<string>());
                foreach (string s in opCodes)
                {
                    evaluations[i].Add(s);
                }
            }

            foreach (var op in operations)
            {
                var opScore = 0;

                foreach (var s in opCodes)
                {
                    bool success = OpCodeMatches(s, op);
                    if (success)
                    {
                        opScore++;
                    }
                    else
                    {
                        evaluations[op.mods[0]].Remove(s);
                    }
                }
                if (opScore >= 3)
                    overallScore++;
            }

            //Determine each opcode
            bool done = false;
            while (!done)
            {
                done = true;
                foreach (var kvp in evaluations)
                {
                    if (evaluations[kvp.Key].Count > 1)
                    {
                        done = false;
                    }
                }
                foreach (var kvp in evaluations)
                {
                    if (evaluations[kvp.Key].Count == 1)
                    {
                        foreach (var kvp2 in evaluations)
                        {
                            if (kvp2.Key != kvp.Key)
                            {
                                evaluations[kvp2.Key].Remove(evaluations[kvp.Key][0]);
                            }
                        }
                    }
                }
            }

            foreach (var c in code)
            {
                PerformOperation(evaluations[c[0]][0], initreg, c[1], c[2], c[3]);
            }

            Console.WriteLine(overallScore);
            Console.WriteLine(initreg[0]);
            Console.Read();
        }

        public static bool OpCodeMatches(string opCode, Operation op)
        {
            var matches = false;
            int[] result = new int[4];
            result = (int[])op.before.Clone();

            switch (opCode)
            {
                case "addr":
                    result[op.mods[3]] = op.before[op.mods[1]] + op.before[op.mods[2]];
                    break;
                case "addi":
                    result[op.mods[3]] = op.before[op.mods[1]] + op.mods[2];
                    break;
                case "mulr":
                    result[op.mods[3]] = op.before[op.mods[1]] * op.before[op.mods[2]];
                    break;
                case "muli":
                    result[op.mods[3]] = op.before[op.mods[1]] * op.mods[2];
                    break;
                case "banr":
                    result[op.mods[3]] = op.before[op.mods[1]] & op.before[op.mods[2]];
                    break;
                case "bani":
                    result[op.mods[3]] = op.before[op.mods[1]] & op.mods[2];
                    break;
                case "borr":
                    result[op.mods[3]] = op.before[op.mods[1]] | op.before[op.mods[2]];
                    break;
                case "bori":
                    result[op.mods[3]] = op.before[op.mods[1]] | op.mods[2];
                    break;
                case "setr":
                    result[op.mods[3]] = op.before[op.mods[1]];
                    break;
                case "seti":
                    result[op.mods[3]] = op.mods[1];
                    break;
                case "gtir":
                    result[op.mods[3]] = op.mods[1] > op.before[op.mods[2]] ? 1 : 0;
                    break;
                case "gtri":
                    result[op.mods[3]] = op.before[op.mods[1]] > op.mods[2] ? 1 : 0;
                    break;
                case "gtrr":
                    result[op.mods[3]] = op.before[op.mods[1]] > op.before[op.mods[2]] ? 1 : 0;
                    break;
                case "eqir":
                    result[op.mods[3]] = op.mods[1] == op.before[op.mods[2]] ? 1 : 0;
                    break;
                case "eqri":
                    result[op.mods[3]] = op.before[op.mods[1]] == op.mods[2] ? 1 : 0;
                    break;
                case "eqrr":
                    result[op.mods[3]] = op.before[op.mods[1]] == op.before[op.mods[2]] ? 1 : 0;
                    break;
            }

            return (result[0] == op.after[0] && result[1] == op.after[1] && result[2] == op.after[2] && result[3] == op.after[3]);
        }

        public static void PerformOperation(string opCode, int[] initReg, int A, int B, int C)
        {
            switch (opCode)
            {
                case "addr":
                    initReg[C] = initReg[A] + initReg[B];
                    break;
                case "addi":
                    initReg[C] = initReg[A] + B;
                    break;
                case "mulr":
                    initReg[C] = initReg[A] * initReg[B];
                    break;
                case "muli":
                    initReg[C] = initReg[A] * B;
                    break;
                case "banr":
                    initReg[C] = initReg[A] & initReg[B];
                    break;
                case "bani":
                    initReg[C] = initReg[A] & B;
                    break;
                case "borr":
                    initReg[C] = initReg[A] | initReg[B];
                    break;
                case "bori":
                    initReg[C] = initReg[A] | B;
                    break;
                case "setr":
                    initReg[C] = initReg[A];
                    break;
                case "seti":
                    initReg[C] = A;
                    break;
                case "gtir":
                    initReg[C] = A > initReg[B] ? 1 : 0;
                    break;
                case "gtri":
                    initReg[C] = initReg[A] > B ? 1 : 0;
                    break;
                case "gtrr":
                    initReg[C] = initReg[A] > initReg[B] ? 1 : 0;
                    break;
                case "eqir":
                    initReg[C] = A == initReg[B] ? 1 : 0;
                    break;
                case "eqri":
                    initReg[C] = initReg[A] == B ? 1 : 0;
                    break;
                case "eqrr":
                    initReg[C] = initReg[A] == initReg[B] ? 1 : 0;
                    break;
            }
        }

    }

    public class Operation
    {
        public int[] before;
        public int[] mods;
        public int[] after;

        public Operation(int[] before, int[] mods, int[] after)
        {
            this.before = before;
            this.mods = mods;
            this.after = after;
        }
    }
}
