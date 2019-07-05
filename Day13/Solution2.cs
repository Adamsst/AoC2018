using System;
using System.Collections.Generic;
using System.Linq;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            List<List<char>> grid = new List<List<char>>();
            List<cart> carts = new List<cart>();
            bool collide = false;

            int inp = 0;
            while ((line = file.ReadLine()) != null)
            {
                grid.Add(new List<char>());
                foreach (char c in line)
                {
                    grid[inp].Add(c);
                }
                inp++;
            }

            List<char> cartObjs = new List<char>() {'>','<','^','v'};
            for(int i = 0; i < grid.Count; i++)
            {
                for (int j = 0; j < grid[i].Count; j++)
                {
                    if (cartObjs.Contains(grid[i][j]))
                    {
                        carts.Add(new cart(j, i, grid[i][j]));
                        if(grid[i][j] == '>' || grid[i][j] == '<')
                        {
                            grid[i][j] = '-';
                        }
                        else if (grid[i][j] == '^' || grid[i][j] == 'v')
                        {
                            grid[i][j] = '|';
                        }
                    }
                }
            }

            while (!collide)
            {
                carts = carts.OrderBy(x => x.y).ThenBy(x => x.x).ToList();
                for (int i=0; i < carts.Count; i++)
                {
                    if(carts[i].d == 'N')
                    {
                        if (grid[carts[i].y - 1][carts[i].x] == '|')
                        {
                            carts[i].y--;
                        }
                        else if (grid[carts[i].y - 1][carts[i].x] == '\\')
                        {
                            carts[i].y--;
                            carts[i].d = 'W';
                        }
                        else if (grid[carts[i].y - 1][carts[i].x] == '/')
                        {
                            carts[i].y--;
                            carts[i].d = 'E';
                        }
                        else if (grid[carts[i].y - 1][carts[i].x] == '+')
                        {
                            carts[i].y--;
                            if (carts[i].inter == "left")
                            {
                                carts[i].d = 'W';
                                carts[i].inter = "straight";
                            }
                            else if (carts[i].inter == "straight")
                            {
                                carts[i].inter = "right";
                            }
                            else if (carts[i].inter == "right")
                            {
                                carts[i].d = 'E';
                                carts[i].inter = "left";
                            }
                        }
                    }
                    else if (carts[i].d == 'E')
                    {
                        if(grid[carts[i].y][carts[i].x+1] == '-')
                        {
                            carts[i].x++;
                        }
                        else if(grid[carts[i].y][carts[i].x + 1] == '\\')
                        {
                            carts[i].x++;
                            carts[i].d = 'S';
                        }
                        else if (grid[carts[i].y][carts[i].x + 1] == '/')
                        {
                            carts[i].x++;
                            carts[i].d = 'N';
                        }
                        else if (grid[carts[i].y][carts[i].x + 1] == '+')
                        {
                            carts[i].x++;
                            if(carts[i].inter == "left")
                            {
                                carts[i].d = 'N';
                                carts[i].inter = "straight";
                            }
                            else if (carts[i].inter == "straight")
                            {
                                carts[i].inter = "right";
                            }
                            else if (carts[i].inter == "right")
                            {
                                carts[i].d = 'S';
                                carts[i].inter = "left";
                            }
                        }
                    }
                    else if (carts[i].d == 'S')
                    {
                        if (grid[carts[i].y + 1][carts[i].x] == '|')
                        {
                            carts[i].y++;
                        }
                        else if (grid[carts[i].y + 1][carts[i].x] == '\\')
                        {
                            carts[i].y++;
                            carts[i].d = 'E';
                        }
                        else if (grid[carts[i].y + 1][carts[i].x] == '/')
                        {
                            carts[i].y++;
                            carts[i].d = 'W';
                        }
                        else if (grid[carts[i].y + 1][carts[i].x] == '+')
                        {
                            carts[i].y++;
                            if (carts[i].inter == "left")
                            {
                                carts[i].d = 'E';
                                carts[i].inter = "straight";
                            }
                            else if (carts[i].inter == "straight")
                            {
                                carts[i].inter = "right";
                            }
                            else if (carts[i].inter == "right")
                            {
                                carts[i].d = 'W';
                                carts[i].inter = "left";
                            }
                        }
                    }
                    else if (carts[i].d == 'W')
                    {
                        if (grid[carts[i].y][carts[i].x - 1] == '-')
                        {
                            carts[i].x--;
                        }
                        else if (grid[carts[i].y][carts[i].x - 1] == '\\')
                        {
                            carts[i].x--;
                            carts[i].d = 'N';
                        }
                        else if (grid[carts[i].y][carts[i].x - 1] == '/')
                        {
                            carts[i].x--;
                            carts[i].d = 'S';
                        }
                        else if (grid[carts[i].y][carts[i].x - 1] == '+')
                        {
                            carts[i].x--;
                            if (carts[i].inter == "left")
                            {
                                carts[i].d = 'S';
                                carts[i].inter = "straight";
                            }
                            else if (carts[i].inter == "straight")
                            {
                                carts[i].inter = "right";
                            }
                            else if (carts[i].inter == "right")
                            {
                                carts[i].d = 'N';
                                carts[i].inter = "left";
                            }
                        }
                    }

                    for(int j = 0; j < carts.Count; j++)
                    {
                        if(j != i)
                        {
                            if(carts[j].x == carts[i].x && carts[j].y == carts[i].y && carts[j].collide==false && carts[i].collide == false)
                            {
                                carts[j].collide = true;
                                carts[i].collide = true;
                            }
                        }
                    }

                    var collisions = carts.Where(x => x.collide == false).Count();
                    if(collisions == 1)
                    {
                        foreach(cart c in carts)
                        {
                            if(c.collide == false)
                            {
                                Console.WriteLine(String.Format("{0}, {1} dir {2}", c.x, c.y , c.d));
                            }
                        }
                        collide = true;
                        break;
                    }
                }
            }
            Console.Read();
        }               
    }

    public class cart
    {
        public int x, y;
        public char d;
        public string inter;
        public bool collide;
        public cart(int X, int Y, char D)
        {
            x = X;
            y = Y;
            inter = "left";
            collide = false;
            switch (D)
            {
                case 'v':
                    d = 'S';
                    break;
                case '>':
                    d = 'E';
                    break;
                case '^':
                    d = 'N';
                    break;
                case '<':
                    d = 'W';
                    break;
                default:
                    break;
            }
        }
    }
}