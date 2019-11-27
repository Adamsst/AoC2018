using System;
using System.Collections.Generic;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            int v = 0;
            List<instruct> instructions = new List<instruct>();
            Dictionary<int, int> registers = new Dictionary<int, int>();
            registers.Add(0, 0);
            registers.Add(1, 0);
            registers.Add(2, 0);
            registers.Add(3, 0);
            registers.Add(4, 0);
            registers.Add(5, 0);
            int ip = 0;

            while ((line = file.ReadLine()) != null)
            {
                string[] temp = line.Split(' ');
                if (v != 0)
                {
                    instructions.Add(new instruct(temp[0].Substring(0, 3), temp[0][3], Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]), Convert.ToInt32(temp[3])));
                }
                else
                {
                    ip = Convert.ToInt32(temp[1]);
                }
                v++;
            }

            for(int i = 0; i < instructions.Count; i++) {
                i = registers[ip];
                if (i < instructions.Count)
                {
                    switch (instructions[i].inst)
                    {
                        case "set":
                            if (instructions[i].jump == 'i')
                            {
                                registers[instructions[i].numC] = instructions[i].numA;
                            }
                            else
                            {
                                registers[instructions[i].numC] = registers[instructions[i].numA];
                            }
                            registers[ip]++;
                            break;
                        case "add":
                            if (instructions[i].jump == 'i')
                            {
                                registers[instructions[i].numC] = registers[instructions[i].numA] + instructions[i].numB;
                            }
                            else
                            {
                                registers[instructions[i].numC] = registers[instructions[i].numA] + registers[instructions[i].numB];
                            }
                            registers[ip]++;
                            break;
                        case "mul":
                            if (instructions[i].jump == 'i')
                            {
                                registers[instructions[i].numC] = registers[instructions[i].numA] * instructions[i].numB;
                            }
                            else
                            {
                                registers[instructions[i].numC] = registers[instructions[i].numA] * registers[instructions[i].numB];
                            }
                            registers[ip]++;
                            break;
                        case "gtr":
                            if (registers[instructions[i].numA] > registers[instructions[i].numB])
                            {
                                registers[instructions[i].numC] = 1;
                            }
                            else
                            {
                                registers[instructions[i].numC] = 0;
                            }
                            registers[ip]++;
                            break;
                        case "eqr":
                            if (registers[instructions[i].numA] == registers[instructions[i].numB])
                            {
                                registers[instructions[i].numC] = 1;
                            }
                            else
                            {
                                registers[instructions[i].numC] = 0;
                            }
                            registers[ip]++;
                            break;
                        default:
                            break;
                    }
                }
            }
            Console.WriteLine(registers[0]);
            Console.Read();
        }               
    }

    public struct instruct
    {
        public string inst;
        public char jump;
        public int numA;
        public int numB;
        public int numC;

        public instruct(string a, char b, int c, int d, int e)
        {
            inst = a;
            jump = b;
            numA = c;
            numB = d;
            numC = e;
        }
    }
}
