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
            registers.Add(0, 1);
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

            //Instructions 3,4,5,6,8,9,10,11 constantly repeat
            //10551330 is stored in Register3
            //This program will find the sum of all factors of 10551330
            //I used wolfram alpha, this problem required a lot of pencil deciphering

            Console.WriteLine("Find the sum of all factors of 10551330 (28137600, see comments in code)");
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