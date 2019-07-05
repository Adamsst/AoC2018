using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            string input = null;
            int x = 0;
            int y = 0;
            double fd = 0;//furthest distance
            double cd = 0;//current distance
            List<int[]> lastLocs = new List<int[]>();
            List<double> lastDist = new List<double>();
            List<char> dir = new List<char>();
            List<Point> rooms = new List<Point>();
            bool loop = false;

            while ((line = file.ReadLine()) != null)
            {
                input = line.Substring(1, line.Length - 2);
            }

            foreach (char c in input)
            {
                dir.Add(c);
            }

            for (int i = 0; i < dir.Count; i++)
            {
                switch (dir[i])
                {
                    case 'N':
                        y++;
                        if (loop)
                        {
                            cd += .5;
                        }
                        else
                        {
                            cd++;
                        }
                        if(cd >= 999.5)
                        {
                            if (!rooms.Contains(new Point(x, y)))
                            {
                                rooms.Add(new Point(x, y));
                            }
                        }
                        break;
                    case 'E':
                        x++;
                        if (loop)
                        {
                            cd += .5;
                        }
                        else
                        {
                            cd++;
                        }
                        if (cd >= 999.5)
                        {
                            if (!rooms.Contains(new Point(x, y)))
                            {
                                rooms.Add(new Point(x, y));
                            }
                        }
                        break;
                    case 'S':
                        y--;
                        if (loop)
                        {
                            cd += .5;
                        }
                        else
                        {
                            cd++;
                        }
                        if (cd >= 999.5)
                        {
                            if (!rooms.Contains(new Point(x, y)))
                            {
                                rooms.Add(new Point(x, y));
                            }
                        }
                        break;
                    case 'W':
                        x--;
                        if (loop)
                        {
                            cd += .5;
                        }
                        else
                        {
                            cd++;
                        }
                        if(cd >= 999.5)
                        {
                            if (!rooms.Contains(new Point(x, y)))
                            {
                                rooms.Add(new Point(x, y));
                            }
                        }
                        break;
                    case '(':
                        lastLocs.Add(new int[] { x, y });
                        lastDist.Add(cd);
                        int ignore = 0;

                        for (int j = i+1; j < dir.Count; j++)
                        {
                            if (dir[j] == '|' && ignore == 0)
                            {
                                if (dir[j + 1] == ')')
                                {
                                    loop = true;
                                    break;
                                }
                                else
                                {
                                    loop = false;
                                    break;
                                }
                            }
                            else if (dir[j] == '(')
                            {
                                ignore++;
                            }
                            else if (dir[j] == ')')
                            {
                                ignore--;
                            }
                        }
                        break;
                    case ')':
                        x = lastLocs.Last()[0];
                        y = lastLocs.Last()[1];
                        cd = lastDist.Last();
                        lastLocs.RemoveAt(lastLocs.Count - 1);
                        lastDist.RemoveAt(lastDist.Count - 1);
                        break;
                    case '|':
                        if (!loop)
                        {
                            x = lastLocs.Last()[0];
                            y = lastLocs.Last()[1];
                            cd = lastDist.Last();
                        }
                        else
                        {
                            loop = false;
                        }
                        break;
                    default:
                        break;
                }

                if (cd > fd)
                {
                    fd = cd;
                }
            }

            Console.WriteLine(rooms.Count());
            Console.Read();
        }
    }
}