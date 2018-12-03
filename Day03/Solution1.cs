using System;
using System.Collections.Generic;
using System.Drawing;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            int x, y, xS, yS, total = 0;
            Dictionary<Point, int> board = new Dictionary<Point, int>();

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    board.Add(new Point(i, j), 0);
                }
            }

            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            while((line = file.ReadLine()) != null)
            {
                string temp = line.Split('@')[1].Trim();
                x = Convert.ToInt32(temp.Split(',')[0]);
                temp = temp.Split(',')[1];
                y = Convert.ToInt32(temp.Split(':')[0]);
                temp = temp.Split(':')[1].Trim();
                xS = Convert.ToInt32(temp.Split('x')[0]);
                yS = Convert.ToInt32(temp.Split('x')[1]);

                for (int i = y; i < y + yS; i++)
                {
                    for (int j = x; j < x + xS; j++)
                    {
                        board[new Point(i, j)]++;
                    }
                }
            }

            for (int i = 0; i < 1000; i++)
            {
                for (int j = 0; j < 1000; j++)
                {
                    if(board[new Point(i, j)] > 1)
                    {
                        total++;
                    }
                }
            }

            Console.WriteLine(total);
            Console.Read();
        }
    }
}