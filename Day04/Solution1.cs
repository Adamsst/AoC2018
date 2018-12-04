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
            Dictionary<DateTime, string> log = new Dictionary<DateTime, string>();
            Dictionary<string, double> totalSleep = new Dictionary<string, double>();
            Dictionary<int, double> sleepMinutes = new Dictionary<int, double>();
            List<GuardLog> logSort = new List<GuardLog>();
            DateTime dt;
            string tempGuard = "None";
            string sleepiestGuard;
            int sleepiestMinute;
            
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");

            while ((line = file.ReadLine()) != null)
            {
                int year = Convert.ToInt32(line.Substring(1, 4));
                int month = Convert.ToInt32(line.Substring(6, 2));
                int day = Convert.ToInt32(line.Substring(9, 2));
                int hour = Convert.ToInt32(line.Substring(12, 2));
                int minute = Convert.ToInt32(line.Substring(15, 2));
                dt = new DateTime(year, month, day, hour, minute, 0);
                log.Add(dt, line.Substring(19));
            }
            foreach(var item in log.OrderByDescending(key => key.Key).Reverse())
            {
                logSort.Add(new GuardLog(item.Key,item.Value));
            }
            for(int i = 0; i < logSort.Count; i++)
            {
                if (logSort[i].What.Substring(0, 5) == "Guard")
                {
                    tempGuard = logSort[i].What;
                    if (!totalSleep.ContainsKey(tempGuard))
                    {
                        totalSleep.Add(tempGuard, 0);
                    }
                }
                else if (logSort[i].What.Substring(0, 5) == "falls")
                {
                    totalSleep[tempGuard] += (logSort[i + 1].DT - logSort[i].DT).TotalMinutes;
                }
            }

            sleepiestGuard = totalSleep.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            for(int i = 0; i < 60; i++)
            {
                sleepMinutes.Add(i, 0);
            }

            for (int i = 0; i < logSort.Count; i++)
            {
                if (logSort[i].What.Substring(0, 5) == "Guard")
                {
                    tempGuard = logSort[i].What;
                }
                else if (logSort[i].What.Substring(0, 5) == "falls")
                {
                    if (tempGuard == sleepiestGuard)
                    {
                        for (int j = logSort[i].DT.Minute; j < logSort[i + 1].DT.Minute; j++)
                        {
                            sleepMinutes[j]++;
                        }
                    }
                }
            }

            sleepiestMinute = sleepMinutes.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            Console.WriteLine(sleepiestGuard);
            Console.WriteLine(sleepiestMinute);
            Console.Read();
        }
    }
}

public struct GuardLog
{
    public DateTime DT;
    public string What;

    public GuardLog(DateTime dt, string what){
        DT = dt;
        What = what;
    }
}