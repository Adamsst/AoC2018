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
        static public Dictionary<(int x, int y),cr> newCave = new Dictionary<(int x, int y), cr>();
        private static int depth = 10647;
        private static int xt = 507;//+500 buffer on each
        private static int yt = 1270;//+500 buffer on each
        private static double risk = 0;
        private static int tarx = 7;
        private static int tary = 770;

        static public List<cr> crToCheck = new List<cr>();
        private static Dictionary<string,List<string>> equipDict = new Dictionary<string, List<string>>();
        private static Dictionary<(string,string),string> moves = new Dictionary<(string, string), string>();
        private static List<string> equips = new List<string>();

        static public List<cr> toProcess = new List<cr>();
        static void Main(string[] args)
        {
            #region initCaveRules

            equipDict.Add("rocky", new List<string>(){"gear","torch"});
            equipDict.Add("wet", new List<string>() { "gear", "none" });
            equipDict.Add("narrow", new List<string>() { "none", "torch" });
            equips.Add("gear");
            equips.Add("torch");
            equips.Add("none");
            moves.Add(("rocky", "wet"),"gear");
            moves.Add(("rocky", "narrow"), "torch");
            moves.Add(("wet", "rocky"), "gear");
            moves.Add(("wet", "narrow"), "none");
            moves.Add(("narrow", "wet"), "none");
            moves.Add(("narrow", "rocky"), "torch");

            #endregion

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

                    newCave.Add((i,j),cave[i][j]);
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

            newCave[(0, 0)].costs["torch"] = 0;
            toProcess.Add(newCave[(0, 0)]);

            while (toProcess.Count > 0)
            {
                CheckCr(toProcess[0]);
                toProcess.RemoveAt(0);
            }

            newCave[(tary,tarx)].OutputStats();

            Console.WriteLine("If gear+7 is less than the torch value then that is our answer.");
            Console.WriteLine("Finished.");
            Console.Read();
        }

        private static void CheckCr(cr cr)
        {
            if (cr.X - 1 > 0)
            {
                AnalyzeCr(newCave[(cr.Y, cr.X - 1)], cr);
            }
            if (cr.X + 1 <= xt)
            {
                AnalyzeCr(newCave[(cr.Y, cr.X + 1)], cr);
            }
            if (cr.Y - 1 > 0)
            {
                AnalyzeCr(newCave[(cr.Y - 1, cr.X)], cr);
            }
            if (cr.Y + 1 <= yt)
            {
                AnalyzeCr(newCave[(cr.Y + 1, cr.X)], cr);
            }
        }

        private static void AnalyzeCr(cr tocr, cr fromcr)
        {
            foreach (var item in fromcr.costs)//for every entry in our equip-cost dictionary at a region
            {
                if (item.Value != null)//if the cost of that region has been defined(aka we've been there)
                {
                    int? cost = item.Value;
                    string newEquip = null;
                    bool add = false;
                    if (equipDict[tocr.type].Contains(item.Key))//if the region we're traveling to allows the equipment
                    {
                        cost += 1;
                    }
                    else//region we're travelling to doesn't allow equipment
                    {
                        cost += 8;
                        newEquip = moves[(fromcr.type, tocr.type)];//this will be the new equipment for this region->region move
                    }

                    if (newEquip == null)//Aka we didnt have to change equipment
                    {
                        if (tocr.costs[item.Key] == null || tocr.costs[item.Key] > cost) //if we've never been to the loc with equipment or this way is cheaper
                        {
                            tocr.costs[item.Key] = cost;
                            add = true;
                        }
                    }
                    else//Aka we did have to change equipment
                    {
                        if (tocr.costs[newEquip] == null || tocr.costs[newEquip] > cost) //if we've never been to the loc with equipment or this way is cheaper
                        {
                            tocr.costs[newEquip] = cost;
                            add = true;
                        }
                    }

                    if (add)
                    {
                        toProcess.Add(tocr);
                    }
                }
            }
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

        public void OutputStats()
        {
            Console.WriteLine($"Type:{type}, Erolevel:{erolevel}, geoindex:{geoindex}, X:{X}, Y:{Y}");
            Console.Write("Equips:");
            foreach (string s in equip)
            {
                Console.Write($" {s}");
            }
            foreach (var item in costs)
            {
                Console.Write($" {item.Key} : {item.Value}");
            }
            Console.WriteLine();
        }
    }
}