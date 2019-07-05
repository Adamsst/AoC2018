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
            int inp = 0;
            int runs = 0;
            int lumber = 0;
            int tree = 0;

            while ((line = file.ReadLine()) != null)
            {
                grid.Add(new List<char>());
                grid[inp].Add('0');
                foreach (char c in line)
                {
                    grid[inp].Add(c);
                }
                grid[inp].Add('0');
                inp++;
            }

            List<char> temp = new List<char>();
            foreach (char c in grid[0])
            {
                temp.Add('0');
            }
            grid.Insert(0, temp);
            grid.Add(temp);

            while (runs < 10)
            {
                List<List<char>> nextGrid = new List<List<char>>();
                nextGrid.Add(grid[0]);
                for (int i = 1; i < grid.Count - 1; i++)
                {
                    List<char> nextTemp = new List<char>();
                    nextTemp.Add('0');
                    for (int j = 1; j < grid[i].Count - 1; j++)
                    {
                        int cnt = 0;
                        int cnt2 = 0;
                        switch (grid[i][j])
                        {
                            case '.':
                                cnt = 0;
                                foreach (char c in new List<char>{ grid[i-1][j-1], grid[i-1][j], grid[i-1][j+1], grid[i][j-1], grid[i][j+1], grid[i+1][j-1], grid[i+1][j], grid[i+1][j+1] } ){
                                    if(c == '|')
                                    {
                                        cnt++;
                                    }
                                }
                                if(cnt >= 3)
                                {
                                    nextTemp.Add('|');
                                }
                                else
                                {
                                    nextTemp.Add('.');
                                }
                                break;
                            case '|':
                                cnt = 0;
                                foreach (char c in new List<char> { grid[i-1][j-1], grid[i-1][j], grid[i-1][j+1], grid[i][j-1], grid[i][j+1], grid[i+1][j-1], grid[i+1][j], grid[i+1][j+1] })
                                {
                                    if (c == '#')
                                    {
                                        cnt++;
                                    }
                                }
                                if (cnt >= 3)
                                {
                                    nextTemp.Add('#');
                                }
                                else
                                {
                                    nextTemp.Add('|');
                                }
                                break;
                            case '#':
                                cnt = 0;
                                cnt2 = 0;
                                foreach (char c in new List<char> { grid[i-1][j-1], grid[i-1][j], grid[i-1][j+1], grid[i][j-1], grid[i][j+1], grid[i+1][j-1], grid[i+1][j], grid[i+1][j+1] })
                                {
                                    if (c == '|')
                                    {
                                        cnt++;
                                    }
                                    else if (c == '#')
                                    {
                                        cnt2++;
                                    }
                                }
                                if (cnt >= 1 && cnt2 >= 1)
                                {
                                    nextTemp.Add('#');
                                }
                                else
                                {
                                    nextTemp.Add('.');
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    nextTemp.Add('0');
                    nextGrid.Add(nextTemp);
                }

                nextGrid.Add(grid.Last());
                grid = nextGrid;
                runs++;
            }

            for (int x = 0; x < grid.Count; x++)
            {
                for (int y = 0; y < grid[x].Count; y++)
                {
                    if(grid[x][y] == '#')
                    {
                        lumber++;
                    }
                    else if(grid[x][y] == '|')
                    {
                        tree++;
                    }
                }
            }

            Console.WriteLine(lumber * tree);
            Console.Read();
        }               
    }
}