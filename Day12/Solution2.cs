using System;
using System.Collections.Generic;

namespace testConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string line = null;
            System.IO.StreamReader file = new System.IO.StreamReader("input.txt");
            List<char> plants = new List<char>();
            Dictionary<string,char> rules = new Dictionary<string, char>();
            int iterations = 0;
            double sum = 0;
            int idx = 0;

            while ((line = file.ReadLine()) != null)
            {
                if (line.Length >= 5)
                {
                    if (line.Substring(0, 3) == "ini")
                    {
                        string[] temp = line.Split(':');
                        temp[1] = temp[1].Trim();
                        foreach (char c in temp[1])
                        {
                            plants.Add(c);
                        }
                    }
                    else
                    {
                        rules.Add(line.Substring(0,5),line[9]);
                    }
                }
            }

            while(iterations <= 1000)//this prints out enough information for the pattern
            {
                List<char> newPlants = new List<char>();
                plants.Insert(0, '.');
                plants.Insert(0, '.');
                plants.Insert(0, '.');
                plants.Insert(0, '.');
                plants.Insert(0, '.');
                plants.Add('.');
                plants.Add('.');
                plants.Add('.');
                plants.Add('.');
                plants.Add('.');
                idx += 5;

                for (int i = 0; i < plants.Count; i++)
                {
                    if (i >= 2 && i < plants.Count - 2)
                    {
                        string temp = "";
                        for (int j = i - 2; j <= i + 2; j++)
                        {
                            temp = temp + plants[j];
                        }
                        if (rules.ContainsKey(temp))
                        {
                            newPlants.Add(rules[temp]);
                        }
                        else
                        {
                            newPlants.Add('.');
                        }
                    }
                    else
                    {
                        newPlants.Add(plants[i]);
                    }
                }

                plants = newPlants;    
                iterations++;
                if (iterations % 100 == 0)
                {
                    double lastsum = sum;
                    sum = 0;
                    for (int i = 0; i < plants.Count; i++)
                    {
                        if (plants[i] == '#')
                        {
                            sum += (i - idx);
                        }
                    }
                    Console.Write("iterations :" + iterations +    "    sum: " + sum + "    difference:" + (sum-lastsum));
                    Console.WriteLine();
                }
            }

            //this is implemented after seeing the pattern
            sum = 52195;
            for(double i = 1000; i < 50000000000; i+=100)
            {
                sum += (5100);
            }
            
            Console.WriteLine(sum);

            Console.Read();
        }        
    }
}
