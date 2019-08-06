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

            #region Part 2 "Search Cubes"
            //See: https://raw.githack.com/ypsu/experiments/master/aoc2018day23/vis.html
//My original idea was to divide the area into points each separated by minradius/2
//Id think take the points that were in range of the most nano bots and recalculate new areas
//Id reduce the radius again and restart. This technique would miss the most populous point by 
//missing the busiest intersections completely. So I turned to the above article for help
//which uses a very straightforward approach and supported my notion that Id have to start measuring
//the points in range of cubes and not just points.

            double startXLength = Math.Abs(nanobots.Min(x => x.x) - nanobots.Max(x => x.x));
            double startYLength = Math.Abs(nanobots.Min(x => x.y) - nanobots.Max(x => x.y));
            double startZLength = Math.Abs(nanobots.Min(x => x.z) - nanobots.Max(x => x.z));

            startXLength = Math.Pow(2, Math.Ceiling(Math.Log(startXLength) / Math.Log(2)));
            startYLength = Math.Pow(2, Math.Ceiling(Math.Log(startYLength) / Math.Log(2)));
            startZLength = Math.Pow(2, Math.Ceiling(Math.Log(startZLength) / Math.Log(2)));

            double startSize = Math.Max(startXLength, startYLength);
            startSize = Math.Max(startXLength, startZLength);

            List<SearchSquare> squares = new List<SearchSquare>();

            squares.Add(new SearchSquare(nanobots.Min(x => x.x),
                nanobots.Min(x => x.y),
                nanobots.Min(x => x.z),
                startSize
                ));

            squares[0].CalculateScore(nanobots);

            bool run = true;

            while (run)
            {
                squares = squares.OrderByDescending(x => x.Score).ThenBy(x => x.Manhatten).ThenBy(x => x.Size).ToList();
                SearchSquare ss = squares[0];

                if (ss.Size == 1)
                {
                    Console.WriteLine($"ss.Manhatten: {ss.Manhatten}, ss.Score: {ss.Score}");
                    run = false;
                }
                else
                {
                    SearchSquare s1 = new SearchSquare(ss.X, ss.Y, ss.Z,ss.Size / 2);
                    s1.CalculateScore(nanobots);
                    SearchSquare s2 = new SearchSquare(ss.X, ss.Y, ss.Z + ss.Size / 2, ss.Size / 2);
                    s2.CalculateScore(nanobots);
                    SearchSquare s3 = new SearchSquare(ss.X, ss.Y + ss.Size / 2, ss.Z, ss.Size / 2);
                    s3.CalculateScore(nanobots);
                    SearchSquare s4 = new SearchSquare(ss.X, ss.Y + ss.Size / 2, ss.Z + ss.Size / 2, ss.Size / 2);
                    s4.CalculateScore(nanobots);
                    SearchSquare s5 = new SearchSquare(ss.X + ss.Size / 2, ss.Y, ss.Z, ss.Size / 2);
                    s5.CalculateScore(nanobots);
                    SearchSquare s6 = new SearchSquare(ss.X + ss.Size / 2, ss.Y, ss.Z + ss.Size / 2, ss.Size / 2);
                    s6.CalculateScore(nanobots);
                    SearchSquare s7 = new SearchSquare(ss.X + ss.Size / 2, ss.Y + ss.Size / 2, ss.Z, ss.Size / 2);
                    s7.CalculateScore(nanobots);
                    SearchSquare s8 = new SearchSquare(ss.X + ss.Size / 2, ss.Y + ss.Size / 2, ss.Z + ss.Size / 2, ss.Size / 2);
                    s8.CalculateScore(nanobots);

                    squares.RemoveAt(0);
                    squares.Add(s1);
                    squares.Add(s2);
                    squares.Add(s3);
                    squares.Add(s4);
                    squares.Add(s5);
                    squares.Add(s6);
                    squares.Add(s7);
                    squares.Add(s8);
                }
            }

            Console.Read();

            #endregion
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

    public class SearchSquare
    {
        public double X; 
        public double Y; 
        public double Z;
        public double Size;
        public int Score;
        public double Manhatten;
        public SearchSquare(double x, double y, double z, double size)
        {
            X = x;
            Y = y;
            Z = z;
            Size = size;
            Score = 0;
            Manhatten = Math.Abs(X) + Math.Abs(Y) + Math.Abs(Z);
        }

        public void CalculateScore(List<nano> nbots)
        {
            foreach (nano n in nbots)
            {
                double range = n.range;
                if (n.x < X)
                {
                    range -= Math.Abs(X - n.x);
                }
                else if (n.x > (X + Size) - 1)
                {
                    range -= Math.Abs((X + Size) - 1 - n.x);
                }

                if (n.y < Y)
                {
                    range -= Math.Abs(Y - n.y);
                }
                else if (n.y > (Y + Size) - 1)
                {
                    range -= Math.Abs((Y + Size) - 1 - n.y);
                }

                if (n.z < Z)
                {
                    range -= Math.Abs(Z - n.z);
                }
                else if (n.z > (Z + Size) - 1)
                {
                    range -= Math.Abs((Z + Size) - 1 - n.z);
                }

                if (range >= 0)
                {
                    Score++;
                }
            }
        }
    }
}