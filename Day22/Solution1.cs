using System;
using System.Collections.Generic;

namespace AoCDay22
{
    //depth: 10647
    //target: 7,770
    //sample depth:510
    //sample target: 10,10  
    class Program
    {
        static public List<List<cr>> cave = new List<List<cr>>();
        private static int depth = 10647;
        private static int xt = 7;//+500 buffer on each
        private static int yt = 770;//+500 buffer on each
        private static double risk = 0;
        private static int tarx = 7;
        private static int tary = 770;

        static void Main(string[] args)
        {

            #region initCave

            //Cave is initialized so cave[0][0] is top left and cave[0][1] is 1 x-coord east, cave[1][0] is 1 y-coord south
            for (int i = 0; i <= yt; i++)
            {
                List<cr> temp = new List<cr>();
                for (int j = 0; j <= xt; j++)
                {
                    temp.Add(new cr("Unknown", 0, 0, j, i));
                }

                cave.Add(temp);
            }

            #endregion

            #region initDetails

            for (int i = 0; i <= yt; i++)
            {
                for (int j = 0; j <= xt; j++)
                {
                    if (j == 0 && i == 0)
                    {
                        cave[i][j].geoindex = 0;
                        cave[i][j].erolevel = (cave[i][j].geoindex + depth) % 20183;
                        cave[i][j].type = GetCaveType(cave[i][j].erolevel);
                    }
                    else if (j == tarx && i == tary)
                    {
                        cave[i][j].geoindex = 0;
                        cave[i][j].erolevel = (cave[i][j].geoindex + depth) % 20183;
                        cave[i][j].type = GetCaveType(cave[i][j].erolevel);
                    }
                    else if (i == 0 && j != 0)
                    {
                        cave[i][j].geoindex = j * 16807;
                        cave[i][j].erolevel = (cave[i][j].geoindex + depth) % 20183;
                        cave[i][j].type = GetCaveType(cave[i][j].erolevel);
                    }
                    else if (j == 0 && i != 0)
                    {
                        cave[i][j].geoindex = i * 48271;
                        cave[i][j].erolevel = (cave[i][j].geoindex + depth) % 20183;
                        cave[i][j].type = GetCaveType(cave[i][j].erolevel);
                    }
                    else
                    {
                        cave[i][j].geoindex = cave[i - 1][j].erolevel * cave[i][j - 1].erolevel;
                        cave[i][j].erolevel = (cave[i][j].geoindex + depth) % 20183;
                        cave[i][j].type = GetCaveType(cave[i][j].erolevel);
                    }
                }
            }

            #endregion

            #region calculateRisk

            for (int i = 0; i <= yt; i++)
            {
                for (int j = 0; j <= xt; j++)
                {
                    risk += GetRisk(cave[i][j].type);
                }
            }

            #endregion

            Console.WriteLine(risk);//Part 2 after

            Console.WriteLine("Finished.");
            Console.Read();
        }

        static public string GetCaveType(double erolevel)
        {
            string cavetype = "UnknownBad";
            if (erolevel % 3 == 0)
            {
                cavetype = "rocky";
            }
            else if (erolevel % 3 == 1)
            {
                cavetype = "wet";
            }
            else if (erolevel % 3 == 2)
            {
                cavetype = "narrow";
            }
            else
            {
                Console.WriteLine("Error!!!!!");
            }

            return cavetype;
        }

        static public double GetRisk(string type)
        {
            switch (type)
            {
                case "rocky":
                    return 0;
                case "wet":
                    return 1;
                case "narrow":
                    return 2;
            }
            Console.WriteLine("Error!!!!!");

            return 0;
        }
    }

    public class cr //Cave Region
    {
        public cr(string s, double i1, double i2 ,int x, int y)
        {
            type = s;
            erolevel = i1;
            geoindex = i2;
            X = x;
            Y = y;
            equip = new List<string>();
            costs = new Dictionary<string, int?>();
            costs.Add("torch",null);
            costs.Add("none", null);
            costs.Add("gear", null);
        }
        public string type;
        public double erolevel;
        public double geoindex;
        public int X;
        public int Y;
        public Dictionary<string, int?> costs;
        public List<string> equip;
    }
}
