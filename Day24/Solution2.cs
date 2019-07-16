using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string line;
            StreamReader file = new System.IO.StreamReader("input.txt");
            string lastteamline = null;
            Squad Immune = new Squad("Immune System");
            Squad Infection = new Squad("Infection");
            int unitid = 0;
            string squadid = null;
            bool battling = true;
            bool immuneWin = false;
            int boost = 0;
            int prevImmune;
            int prevInfect;

            #region Initialize the units on both teams
            while (!immuneWin)
            {
                Immune = new Squad("Immune System");
                Infection = new Squad("Infection");
                unitid = 0;
                squadid = null;
                battling = true;
                immuneWin = false;
                file = new System.IO.StreamReader("input.txt");
                boost++;
                while ((line = file.ReadLine()) != null)
                {
                    if (line == "Immune System:" || line == "Infection:")
                    {
                        lastteamline = line;
                        unitid = 0;
                        switch (line)
                        {
                            case "Immune System:":
                                squadid = "Immune Group";
                                break;
                            case "Infection:":
                                squadid = "Infection Group";
                                break;
                            default:
                                break;
                        }
                    }

                    if (line.Length > 15)//Kind hacky for just unit description lines
                    {
                        string[] info = line.Split(' ');
                        units newUnits = new units();

                        for (int i = 0; i < info.Length; i++)
                        {
                            int n;
                            bool isNum = int.TryParse(info[i], out n);
                            if (isNum)
                            {
                                if (i + 1 == info.Length)
                                {
                                    newUnits.initiative = Convert.ToInt32(info[i]);
                                }
                                else
                                {
                                    if (info[i + 1] == "units")
                                    {
                                        newUnits.count = Convert.ToInt32(info[i]);
                                    }
                                }
                            }
                            else
                            {
                                if (info[i] == "each")
                                {
                                    newUnits.hp = Convert.ToInt32(info[i + 2]);
                                }
                                else if (info[i] == "does")
                                {
                                    newUnits.damage = Convert.ToInt32(info[i + 1]);
                                    newUnits.damagetype = info[i + 2];
                                }
                                else if (info[i][0] == '(')
                                {
                                    string curType = info[i].Substring(1);
                                    bool cont = true;
                                    int newI = i + 2;

                                    while (cont)
                                    {
                                        if (curType == "weak")
                                        {
                                            if (info[newI].Contains(","))
                                            {
                                                newUnits.weakTo.Add(info[newI].Substring(0, info[newI].Length - 1));
                                                newI++;
                                            }
                                            else if (info[newI].Contains(")"))
                                            {
                                                newUnits.weakTo.Add(info[newI].Substring(0, info[newI].Length - 1));
                                                cont = false;
                                            }
                                            if (info[newI].Contains(";"))
                                            {
                                                newUnits.weakTo.Add(info[newI].Substring(0, info[newI].Length - 1));
                                                curType = info[newI + 1];
                                                newI += 3;
                                            }
                                        }
                                        else if (curType == "immune")
                                        {
                                            if (info[newI].Contains(","))
                                            {
                                                newUnits.immuneTo.Add(info[newI].Substring(0, info[newI].Length - 1));
                                                newI++;
                                            }
                                            else if (info[newI].Contains(")"))
                                            {
                                                newUnits.immuneTo.Add(info[newI].Substring(0, info[newI].Length - 1));
                                                cont = false;
                                            }
                                            if (info[newI].Contains(";"))
                                            {
                                                newUnits.immuneTo.Add(info[newI].Substring(0, info[newI].Length - 1));
                                                curType = info[newI + 1];
                                                newI += 3;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        newUnits.Initialize(unitid, squadid);
                        unitid++;
                        switch (lastteamline)
                        {
                            case "Immune System:":
                                newUnits.damage += boost;
                                Immune.team.Add(newUnits);
                                break;
                            case "Infection:":
                                Infection.team.Add(newUnits);
                                break;
                            default:
                                break;
                        }
                    }
                }
                prevImmune = Immune.GetUnitCount();
                prevInfect = Infection.GetUnitCount();
                #endregion
                Console.WriteLine($"boost {boost}");
                foreach (units u in Immune.team)
                {
                    u.CalculatePower();                 
                }
                foreach (units u in Infection.team)
                {
                    u.CalculatePower();
                }
                
                //Battle
                while (battling)//Going to try to implement the output shown on the AoC site here
                {
                    #region Unit Counts
                    Immune.team = Immune.team.OrderByDescending(x => x.power).ThenByDescending(x => x.initiative).ToList();
                    Infection.team = Infection.team.OrderByDescending(x => x.power).ThenByDescending(x => x.initiative).ToList();
                    #endregion

                    #region Target Selection 
                    for (int i = 0; i < Immune.team.Count; i++)
                    {
                        for (int j = 0; j < Infection.team.Count; j++)
                        {
                            if (!Infection.team[j].istargeted && !Infection.team[j].immuneTo.Contains(Immune.team[i].damagetype))
                            {
                                if (Infection.team[j].weakTo.Contains(Immune.team[i].damagetype))
                                {
                                    GetTarget(Immune.team[i], Infection.team[j], true);
                                }
                                else
                                {
                                    GetTarget(Immune.team[i], Infection.team[j], false);
                                }

                            }
                        }
                        units nowtargeted = Infection.team.Where(x => x.id == Immune.team[i].targetid).FirstOrDefault();
                        if (nowtargeted != null)
                        {
                            nowtargeted.istargeted = true;
                        }
                    }
                    for (int i = 0; i < Infection.team.Count; i++)
                    {
                        for (int j = 0; j < Immune.team.Count; j++)
                        {
                            if (!Immune.team[j].istargeted && !Immune.team[j].immuneTo.Contains(Infection.team[i].damagetype))
                            {
                                if (Immune.team[j].weakTo.Contains(Infection.team[i].damagetype))
                                {
                                    GetTarget(Infection.team[i], Immune.team[j], true);
                                }
                                else
                                {
                                    GetTarget(Infection.team[i], Immune.team[j], false);
                                }
                            }
                        }
                        units nowtargeted = Immune.team.Where(x => x.id == Infection.team[i].targetid).FirstOrDefault();
                        if (nowtargeted != null)
                        {
                            nowtargeted.istargeted = true;
                        }
                    }
                    #endregion

                    #region Order Battle
                    List<units> troops = new List<units>();
                    foreach (units u in Infection.team)
                    {
                        troops.Add(u);
                    }
                    foreach (units u in Immune.team)
                    {
                        troops.Add(u);
                    }
                    troops = troops.OrderByDescending(x => x.initiative).ToList(); 
                    #endregion

                    #region Units Attack
                    foreach (units u in troops)
                    {
                        units attacker = null;
                        switch (u.squad)
                        {
                            case "Infection Group":
                                attacker = Infection.team.FirstOrDefault(x => x.id == u.id);
                                if (attacker != null && attacker.targetid != null && attacker.count > 0)
                                {
                                    units defender = Immune.team.FirstOrDefault(x => x.id == attacker.targetid);
                                    if (defender != null)
                                    {
                                        int mult = 1;
                                        int kills = 0;
                                        if (defender.weakTo.Contains(attacker.damagetype))
                                        {
                                            mult++;
                                        }
                                        kills = (int)(attacker.count * attacker.damage * mult / defender.hp);
                                        defender.count -= kills;
                                        if (defender.count > 0)
                                        {
                                            defender.CalculatePower();
                                        }
                                    }
                                }
                                break;
                            case "Immune Group":
                                attacker = Immune.team.FirstOrDefault(x => x.id == u.id);
                                if (attacker != null && attacker.targetid != null && attacker.count > 0)
                                {
                                    units defender = Infection.team.FirstOrDefault(x => x.id == attacker.targetid);
                                    if (defender != null)
                                    {
                                        int mult = 1;
                                        int kills = 0;
                                        if (defender.weakTo.Contains(attacker.damagetype))
                                        {
                                            mult++;
                                        }
                                        kills = (int)(attacker.count * attacker.damage * mult / defender.hp);
                                        defender.count -= kills;
                                        if (defender.count > 0)
                                        {
											defender.CalculatePower();
                                        }
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    #endregion

                    #region Clear Targets & Calculate Power
                    foreach (units u in Infection.team)
                    {
                        u.ClearTargets();
                        u.CalculatePower();
                    }
                    foreach (units u in Immune.team)
                    {
                        u.ClearTargets();
                        u.CalculatePower();
                    }
                    #endregion

                    #region Check if over
                    for (int i = 0; i < Infection.team.Count; i++)
                    {
                        if (Infection.team[i].count <= 0)
                        {
                            Infection.team.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < Immune.team.Count; i++)
                    {
                        if (Immune.team[i].count <= 0)
                        {
                            Immune.team.RemoveAt(i);
                            i--;
                        }
                    }

                    if (Immune.GetUnitCount() == prevImmune && Infection.GetUnitCount() == prevInfect)
                    {
                        Console.WriteLine("Tie!");
                        battling = false;
                        immuneWin = false;
                    }
                    else
                    {
                        prevImmune = Immune.GetUnitCount();
                        prevInfect = Infection.GetUnitCount();
                        if (Infection.team.Count == 0)
                        {
                            int total = 0;
                            Console.WriteLine("Infection:" + Environment.NewLine + "No groups remain.");
                            Console.WriteLine("Immune System:");
                            for (int z = 0; z < Immune.team.Count; z++)
                            {
                                Console.WriteLine($"Group {Immune.team[z].id} contains {Immune.team[z].count} units");
                                total += Immune.team[z].count;
                            }
                            Console.WriteLine($"Total: {total}");
                            battling = false;
                            immuneWin = true;
                            Console.WriteLine($"Boost: {boost}");
                        }
                        else if (Immune.team.Count == 0)
                        {
                            Console.WriteLine("Immune:" + Environment.NewLine + "No groups remain.");
                            Console.WriteLine("Infection:");
                            int total = 0;
                            for (int z = 0; z < Infection.team.Count; z++)
                            {
                                Console.WriteLine($"Group {Infection.team[z].id} contains {Infection.team[z].count} units");
                                total += Infection.team[z].count;
                            }
                            Console.WriteLine($"Total: {total}");
                            immuneWin = false;
                            battling = false;
                        }
                    }
                    #endregion
                }
            }

            Console.WriteLine(Environment.NewLine + "Finished!");
            Console.Read();
        }

        public static void GetTarget(units attacker, units potentialtarget, bool critical)
        {
            if (attacker.targetid == null) //if the attacker currently doesnt have a target
            {
                attacker.targetid = potentialtarget.id;
                attacker.targetdam = critical ? (attacker.count * attacker.damage * 2) : (attacker.count * attacker.damage);
                attacker.targetpower = potentialtarget.count * potentialtarget.damage;
                attacker.targetinit = potentialtarget.initiative;
                attacker.targetcrit = critical;
            }
            else
            {
                if (critical)
                {
                    if ((potentialtarget.count * potentialtarget.damage) > attacker.targetpower || attacker.targetcrit == false)
                    {
                        attacker.targetcrit = true;
                        attacker.targetid = potentialtarget.id;
                        attacker.targetdam = attacker.count * attacker.damage * 2;
                        attacker.targetpower = potentialtarget.count * potentialtarget.damage;
                        attacker.targetinit = potentialtarget.initiative;
                    }
                    else if ((potentialtarget.count * potentialtarget.damage) == attacker.targetpower)
                    {
                        if (potentialtarget.initiative > attacker.targetinit)
                        {
                            attacker.targetid = potentialtarget.id;
                            attacker.targetdam = attacker.count * attacker.damage * 2;
                            attacker.targetinit = potentialtarget.initiative;
                        }
                    }
                }
                else
                {
                    if (!attacker.targetcrit)//having already targeted a critical will always do more damage, skip clause
                    {
                        if ((potentialtarget.count * potentialtarget.damage) > attacker.targetpower)
                        {
                            attacker.targetid = potentialtarget.id;
                            attacker.targetdam = attacker.count * attacker.damage;
                            attacker.targetpower = potentialtarget.count * potentialtarget.damage;
                            attacker.targetinit = potentialtarget.initiative;
                        }
                        else if ((potentialtarget.count * potentialtarget.damage) == attacker.targetpower)
                        {
                            if (potentialtarget.initiative > attacker.targetinit)
                            {
                                attacker.targetid = potentialtarget.id;
                                attacker.targetdam = attacker.count * attacker.damage;
                                attacker.targetinit = potentialtarget.initiative;
                            }
                        }
                    }
                }
            }

        }

        public class Squad
        {
            public List<units> team = new List<units>();
            public string Name;
            public Squad(string s)
            {
                this.Name = s;
            }

            public int GetUnitCount()
            {
                int cnt = 0;
                foreach(units u in team)
                {
                    cnt += u.count;
                }
                return cnt;
            }
        }

        public class units
        {
            public List<string> weakTo = new List<string>();
            public List<string> immuneTo = new List<string>();
            public int damage;
            public string damagetype;
            public int initiative;
            public int count;
            public int hp;
            public int power;
            public int id;
            public int? targetdam;
            public int? targetpower;
            public int? targetid;
            public int? targetinit;
            public bool istargeted;
            public bool targetcrit;
            public string squad;
            public units()
            {

            }

            public void Initialize(int uid, string sid)
            {
                id = uid;
                squad = sid;
                targetid = null;
                targetdam = null;
                targetpower = null;
                targetinit = null;
                targetcrit = false;
                istargeted = false;
                CalculatePower();
            }

            public void ClearTargets()
            {
                targetid = null;
                targetdam = null;
                targetpower = null;
                targetinit = null;
                targetcrit = false;
                istargeted = false;
            }

            public void CalculatePower()
            {
                power = count * damage;
                if (power < 0)
                {
                    power = 0;
                }
            }
        }
    }
}
