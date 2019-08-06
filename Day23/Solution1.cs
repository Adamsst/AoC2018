using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day232018
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader file = new System.IO.StreamReader("input.txt");
            string line;
             List<nano> nanobots = new List<nano>();
            while ((line = file.ReadLine()) != null)
            {
                //The below is a nifty Regex/Linq line
                var result = Regex.Matches(line, @"-?\d+").OfType<Match>().Select(m => m.Value).ToList();
                nanobots.Add(new nano(
                    Convert.ToDouble(result[0]),
                    Convert.ToDouble(result[1]),
                    Convert.ToDouble(result[2]),
                    Convert.ToDouble(result[3])));
            }

            nano strongest = nanobots.OrderByDescending(x => x.range).First();
            int count = 0;

            foreach (nano n in nanobots)
            {
                double xDist = Math.Abs(strongest.x - n.x);
                double yDist = Math.Abs(strongest.y - n.y);
                double zDist = Math.Abs(strongest.z - n.z);
                if ((xDist + yDist + zDist) <= strongest.range)
                {
                    count++;
                }
            }

            Console.WriteLine($"Total in Range: {count}");
            Console.WriteLine("Finished!");
        }
    }

    public class nano
    {
        public double x;
        public double y;
        public double z;
        public double range;

        public nano(double X, double Y, double Z, double Range)
        {
            this.x = X;
            this.y = Y;
            this.z = Z;
            this.range = Range;
        }
    }

}