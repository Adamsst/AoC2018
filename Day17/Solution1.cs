using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day17
{
    class Program
    {
        static void Main(string[] args)
        {
            var ground = new Dictionary<Point, char>();
            Stack<(int, int)> activeWater = new Stack<(int, int)>();
            int startX = 500;
            int startY = 0;
            bool keepGoing = true;
            int minFileY;

            string line;
            using (StreamReader file = new System.IO.StreamReader("input.txt"))
            {
                while ((line = file.ReadLine()) != null)
                {
                    //The below is a nifty Regex/Linq line
                    var result = Regex.Matches(line, @"-?\d+").OfType<Match>().Select(m => m.Value).ToList();
                    char axis = line[0];
                    int index = Convert.ToInt32(result[0]);
                    int min = Convert.ToInt32(result[1]);
                    int max = Convert.ToInt32(result[2]);

                    switch (axis)
                    {
                        case 'x':
                            for (int i = min; i <= max; i++)
                            {
                                ground[new Point(index, i)] = '#';
                            }
                            break;
                        case 'y':
                            for (int i = min; i <= max; i++)
                            {
                                //ground.Add(new Point(i, index), '#');
                                ground[new Point(i, index)] = '#';
                            }
                            break;
                    }
                }
            }

            var minX = ground.Min(x => x.Key.X) - 1;
            var maxX = ground.Max(x => x.Key.X) + 1;
            var minY = 0;
            minFileY = ground.Min(y => y.Key.Y);
            var maxY = ground.Max(y => y.Key.Y);

            ground.Add(new Point(startX, startY), '+');

            activeWater.Push((startX,startY));

            Console.WriteLine();

            while (activeWater.Count >= 1)
            {
                bool? markedRows = null;
                var temp = activeWater.Peek();
                if (ground.ContainsKey(new Point(temp.Item1, temp.Item2 + 1)))
                {
                    if (ground[new Point(temp.Item1, temp.Item2 + 1)] == '.')
                    {
                        ground[new Point(temp.Item1, temp.Item2 + 1)] = '|';
                        activeWater.Push((temp.Item1, temp.Item2 + 1));
                    }
                    else if (ground[new Point(temp.Item1, temp.Item2 + 1)] == '#' || ground[new Point(temp.Item1, temp.Item2 + 1)] == '~')
                    {
                        markedRows = MarkRowIfItsTrapped(temp.Item1, temp.Item2, minX, maxX, ground);
                        if ((bool) markedRows)
                        {
                            activeWater.Pop();
                        }
                    }
                    else
                    {
                        activeWater.Pop();
                    }
                }
                else
                {
                    activeWater.Pop();
                }

                if (markedRows == false)
                {
                    bool didSomething = false;
                    if (temp.Item1 != maxX && ground[new Point(temp.Item1 + 1, temp.Item2)] == '.')
                    {
                        ground[new Point(temp.Item1 + 1, temp.Item2)] = '|';
                        activeWater.Push((temp.Item1 + 1 , temp.Item2));
                        didSomething = true;
                    }
                    else if (temp.Item1 != minX &&ground[new Point(temp.Item1 - 1, temp.Item2)] == '.')
                    {
                        ground[new Point(temp.Item1 - 1, temp.Item2)] = '|';
                        activeWater.Push((temp.Item1 - 1, temp.Item2));
                        didSomething = true;
                    }
                    else
                    {
                        activeWater.Pop();
                    }
                    
                }
            }

            Console.Write(ground.Count(item => (item.Value == '~' || item.Value == '|') && item.Key.Y >= minFileY));
            Console.Read();
        }

        private static bool MarkRowIfItsTrapped(int x, int y, int minX, int maxX, Dictionary<Point, char> ground)
        {
            bool trappedleft = false;
            bool trappedRight = false;

            var tempX = x-1;
            while (tempX > minX)
            {
                if (ground[new Point(tempX, y + 1)] == '|' || ground[new Point(tempX, y + 1)] == '.')
                {
                    return false;//waterfall below or empty space
                }
                else if ((ground[new Point(tempX, y + 1)] == '#' || ground[new Point(tempX, y + 1)] == '~') && ground[new Point(tempX, y)] == '#')
                {
                    trappedleft = true;
                    break;
                }

                tempX--;
            }

            if (!trappedleft)
                return false;

            tempX = x + 1;
            while (tempX < maxX)
            {
                if (ground[new Point(tempX, y + 1)] == '|' || ground[new Point(tempX, y + 1)] == '.')
                {
                    return false;//waterfall below or empty space
                }
                else if ((ground[new Point(tempX, y + 1)] == '#' || ground[new Point(tempX, y + 1)] == '~') && ground[new Point(tempX, y)] == '#')
                {
                    trappedRight = true;
                    break;
                }

                tempX++;
            }

            if (trappedleft && trappedRight)
            {
                ground[new Point(x,y)] = '~';
                tempX = x-1;
                while (ground[new Point(tempX, y)] != '#')
                {
                    ground[new Point(tempX, y)] = '~';
                    tempX--;
                }

                tempX = x + 1;
                while (ground[new Point(tempX, y)] != '#')
                {
                    ground[new Point(tempX, y)] = '~';
                    tempX++;
                }

                return true;
            }

            return false;
        }
    }
}
